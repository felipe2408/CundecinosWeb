using Azure.Storage.Blobs;
using CundecinosWeb.Data;
using CundecinosWeb.Models;
using CundecinosWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace CundecinosWeb.Controllers
{
	public class PublicationController : Controller
	{
        private readonly DataContext _context;
        private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=cunpublication;AccountKey=8vdy7cuwlVkUdYw25qEnDcJqZy3DbktxPxxcUaw7ZB6Sh7fypyykIoHjK8irHbtN2hvdfxL4zO8l+AStvbK57A==;EndpointSuffix=core.windows.net";

        public PublicationController(DataContext context)
		{
			_context = context;

		}
		public IActionResult Index()
		{
			var vmodel = new vPersonPublication();
			var model = _context.People.Include( x => x.CollegeCareer).Where(x => x.UID == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).FirstOrDefault();
            vmodel.Person = model;
			vmodel.Publication = new Publication();
            vmodel.PublicationUsers = _context.Publication.Include(x => x.Person).Include(x => x.PublicationAttachment).Where(x => x.PublicationDate >= DateTime.Now || x.PublicationDate <= DateTime.Now).ToList();
            vmodel.Publication.PersonID = model.PersonID;

            //var publications =

            //var publicationUsers = new List<vPublicationCreated>();
            //foreach (var item in publications)
            //{

            //    var modelPublication = new vPublicationCreated();

            //    modelPublication.Publication = item;
            //    modelPublication.AvatarURL = item.Person.AvatarUrl;

            //    //publicationUsers.Add(modelPublication);
            //}


            return View(vmodel);
		}
		[HttpPost]
		public async Task<IActionResult> CreatePublication(Publication publication, List<IFormFile> files)
		{
			try
			{
				var urls = new List<string>();

                // Crea un cliente del Blob Storage
                BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

                // Crea una referencia al contenedor donde se guardará la imagen
                string containerName = "publication";
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                publication.PublicationDate = DateTime.Now;

                _context.Publication.Add(publication);

                _context.SaveChanges();
                
                    foreach (var file in Request.Form.Files)
                    {

                        // Genera un nombre único para la imagen
                        string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                        // Crea un blob con el nombre generado y sube el archivo
                        BlobClient blobClient = containerClient.GetBlobClient(nombreArchivo);
                        await blobClient.UploadAsync(file.OpenReadStream());

                        urls.Add(blobClient.Uri.ToString());

                        var attachment = new PublicationAttachment();
                    attachment.PublicationAttachmentID = Guid.NewGuid();
                        attachment.PublicationID = publication.PublicationID;
                        attachment.ImageScreen = blobClient.Uri.ToString();
                        attachment.ImageThumbNail = blobClient.Uri.ToString();
                        attachment.Created = DateTime.Now;
                        attachment.CreatedBy = "5BDADA86-69A6-4465-A82F-08DB40DB6B03";
                        _context.Add(attachment);

                    }




                _context.SaveChanges();



            }
			catch (Exception e)
			{
				return View("Ocurrio un error "+e);
			}

			return RedirectToAction("Index","Home");
		}


    }
}
