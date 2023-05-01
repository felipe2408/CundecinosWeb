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

            ViewBag.FullName = user.FirstName + " " + user.LastName;
            ViewBag.Email = user.Email;
            ViewBag.Avatar = user.AvatarUrl;

            var publication = _context.Publication.Include(x => x.Person).Include(x => x.PublicationAttachment).Where(x => x.PublicationID == Id).FirstOrDefault();
			var publicationComments = _context.PublicationComments.Where(x => x.PublicationID == Id).ToList();
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

          

            foreach (var file in Request.Form.Files)
            {

                // Genera un nombre único para la imagen
                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                // Crea un blob con el nombre generado y sube el archivo
                BlobClient blobClient = containerClient.GetBlobClient(nombreArchivo);
                await blobClient.UploadAsync(file.OpenReadStream());

                urls.Add(blobClient.Uri.ToString());

                

            }
            _context.PublicationComments.Add(comment);

            _context.SaveChanges();
            return View();
        }

    }
}
