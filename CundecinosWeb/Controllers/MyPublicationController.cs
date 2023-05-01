using Azure.Storage.Blobs;
using CundecinosWeb.Data;
using CundecinosWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Policy;

namespace CundecinosWeb.Controllers
{
	public class MyPublicationController : Controller
	{
        private DataContext _context;
        private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=cunpublication;AccountKey=8vdy7cuwlVkUdYw25qEnDcJqZy3DbktxPxxcUaw7ZB6Sh7fypyykIoHjK8irHbtN2hvdfxL4zO8l+AStvbK57A==;EndpointSuffix=core.windows.net";


        public MyPublicationController(DataContext context)
        {

            _context = context;
        }
        public async Task<IActionResult> Index()
		{
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.People.Where(x => x.UID == Guid.Parse(claim)).FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Register","User");
            }
            var publications = await _context.Publication.Where(x => x.PersonID == user.PersonID && x.IsActive == true).OrderByDescending(x => x.PublicationDate).Include(x => x.PublicationAttachment).ToListAsync();
            return View(publications.Count == 0 ? null : publications);
		}
        public async Task<IActionResult> EditPublication(Guid id)
        {
            var publication = await _context.Publication.Where(x => x.PublicationID == id).Include(x => x.Person).Include(x => x.PublicationAttachment).FirstOrDefaultAsync();
            return View(publication);
        }
        [HttpPost]
        public async Task<IActionResult> EditPublication(Publication publication)
        {
            try
            {
                var publicacion = _context.Publication.Where(p => p.PublicationID == publication.PublicationID).AsNoTracking().First();
                publication.PublicationDate = publicacion.PublicationDate;
                publication.IsActive = true;
                _context.Entry(publication).State = EntityState.Modified;
                if (Request.Form.Files.Count > 0 )
                {
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

                        var attachment = new PublicationAttachment();
                        attachment.PublicationAttachmentID = Guid.NewGuid();
                        attachment.PublicationID = publication.PublicationID;
                        attachment.ImageScreen = blobClient.Uri.ToString();
                        attachment.ImageThumbNail = blobClient.Uri.ToString();
                        attachment.Created = DateTime.Now;
                        attachment.CreatedBy = "5BDADA86-69A6-4465-A82F-08DB40DB6B03";
                        _context.Add(attachment);

                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("EditPublication", new { id = publication.PublicationID });
            }
            catch (Exception e)
            {
                return View("Ocurrio un error " + e);
            }
        }
        public async Task<IActionResult> HidePublication(Guid id)
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.People.Where(x => x.UID == Guid.Parse(claim) && x.IsActive == true).FirstOrDefault();
            var publication = _context.Publication.Where(x => x.PublicationID == id).AsNoTracking().FirstOrDefault();
            publication.IsActive = false;
            _context.Entry(publication).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteImage(Guid id, Guid publication)
        {
            _context.PublicationAttachments.Remove(new PublicationAttachment { PublicationAttachmentID = id });
            await _context.SaveChangesAsync();
            return RedirectToAction("EditPublication", new { id = publication });
        }
    }
}
