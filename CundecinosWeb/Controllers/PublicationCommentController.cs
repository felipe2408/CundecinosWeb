using Azure.Storage.Blobs;
using CundecinosWeb.Data;
using CundecinosWeb.Models;
using CundecinosWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CundecinosWeb.Controllers
{
	public class PublicationCommentController : Controller
	{

		private readonly DataContext _context;
        private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=cunpublication;AccountKey=8vdy7cuwlVkUdYw25qEnDcJqZy3DbktxPxxcUaw7ZB6Sh7fypyykIoHjK8irHbtN2hvdfxL4zO8l+AStvbK57A==;EndpointSuffix=core.windows.net";

        public PublicationCommentController(DataContext context)
		{
			_context = context;
		}
		public IActionResult PublicationDescription(Guid Id)
		{
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.People.Where(x => x.UID == Guid.Parse(claim)).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Register", "User");
            }
            ViewBag.UserID = user.PersonID;
            ViewBag.FullName = user.FirstName + " " + user.LastName;
            ViewBag.Email = user.Email;
            ViewBag.Avatar = user.AvatarUrl;

            var publication = _context.Publication.Include(x => x.Person).Include(x => x.PublicationAttachment).Where(x => x.PublicationID == Id).FirstOrDefault();
			var publicationComments = _context.PublicationComments.Where(x => x.PublicationID == Id).Include(x=>x.Person).Include(x => x.CommentAttachment).ToList();
			var model = new vPublicationComment();
			model.Person = publication.Person;
			model.Publication = publication;
			model.PublicationComments = publicationComments;
			model.PublicationComment = new PublicationComments();
            model.PublicationComment.Person = publication.Person;
			return View(model);
		}

		[HttpPost]
        public async Task<IActionResult> PublicationDescription(PublicationComments comment)
        {

            var urls = new List<string>();

            // Crea un cliente del Blob Storage
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            // Crea una referencia al contenedor donde se guardará la imagen
            string containerName = "publication";
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);


            comment.StatusInnofer = Enum.StatusInnofer.InNegotiation;
            comment.CommentDate = DateTime.Now;
            //comment.IsCalificationPerson = false;

            _context.PublicationComments.Add(comment);

            _context.SaveChanges();
            foreach (var file in Request.Form.Files)
            {

                // Genera un nombre único para la imagen
                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                // Crea un blob con el nombre generado y sube el archivo
                BlobClient blobClient = containerClient.GetBlobClient(nombreArchivo);
                await blobClient.UploadAsync(file.OpenReadStream());

                urls.Add(blobClient.Uri.ToString());

                var attachment = new CommentAttachment();
                attachment.CommentAttachmentID = Guid.NewGuid();
                attachment.PublicationCommentsID = comment.PublicationCommentsID;
                attachment.ImageScreen = blobClient.Uri.ToString();
                attachment.ImageThumbNail = blobClient.Uri.ToString();
                attachment.Created = DateTime.Now;
                attachment.CreatedBy = "5BDADA86-69A6-4465-A82F-08DB40DB6B03";
                _context.Add(attachment);


            }

            _context.SaveChanges();
            return RedirectToAction("PublicationDescription", new { id = comment.PublicationID});
        }

    }
}
