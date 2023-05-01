using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CundecinosWeb.Data;
using CundecinosWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace CundecinosWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=cunpublication;AccountKey=8vdy7cuwlVkUdYw25qEnDcJqZy3DbktxPxxcUaw7ZB6Sh7fypyykIoHjK8irHbtN2hvdfxL4zO8l+AStvbK57A==;EndpointSuffix=core.windows.net";
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }
        public IActionResult PersonalInformation()
        {
            var model = _context.People.Include(x => x.CollegeCareer).Include(x => x.Extension).Where(x => x.UID == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).FirstOrDefault();

            if (model == null)
            {
                return RedirectToAction("Register","User");
            }
            
            return View(model);
        
        
        
        }
        [HttpPost]
        public async Task<IActionResult> PersonalInformation(Person person)
        {

            var data = await _context.People.Where(x => x.UID == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).AsNoTracking().FirstOrDefaultAsync();
            var file = Request.Form.Files.FirstOrDefault();
            if (file != null && file.Length > 0 && Path.GetExtension(file.FileName) == ".jpg")
            {
                // Crea un cliente del Blob Storage
                BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
                // Crea una referencia al contenedor donde se guardará la imagen
                string containerName = "publication";
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                // Genera un nombre único para la imagen
                string nombreArchivo = person.AvatarUrl;
                // Si el archivo ya existía, se borra para ser reemplazado por la nueva versión
                if (person.AvatarUrl != null)
                {
                    BlobClient oldBlobClient = containerClient.GetBlobClient(nombreArchivo.Substring(nombreArchivo.LastIndexOf('/')+1));
                    await oldBlobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

                    var prueba = oldBlobClient.Exists();
                }
                // Crea un cliente del Blob Storage
                BlobServiceClient blobServiceClient2 = new BlobServiceClient(_connectionString);
                // Crea una referencia al contenedor donde se guardará la imagen
                containerName = "publication";
                BlobContainerClient containerClient2 = blobServiceClient2.GetBlobContainerClient(containerName);
                // Genera un nombre único para la imagen
                nombreArchivo = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)).ToString() + Path.GetExtension(file.FileName);
                BlobClient blobClient = containerClient2.GetBlobClient(nombreArchivo);
                var prueba2 = blobClient.Exists();
                if (prueba2.Value)
                {
                    await blobClient.DeleteAsync(DeleteSnapshotsOption.IncludeSnapshots);
                }
                await blobClient.UploadAsync(file.OpenReadStream());
                prueba2 = blobClient.Exists();
                person.AvatarUrl = blobClient.Uri.ToString();
            }

            // Actualiza la información de la persona
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "Home");

        }
        public IActionResult Register()
        
        
        {

            var person = new Person();

            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            person.FirstName = User.FindFirstValue(ClaimTypes.GivenName);
            person.LastName = User.FindFirstValue(ClaimTypes.Surname);
            person.Email = User.FindFirstValue(ClaimTypes.Email);
            

            return View(person);
        }
        [HttpPost]

        public async Task<IActionResult> Register(Person person,IFormFile archivo)
        {
            
            if (archivo == null || archivo.Length == 0)
            {
                return BadRequest("No se ha enviado ningún archivo");
            }

            // Crea un cliente del Blob Storage
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            // Crea una referencia al contenedor donde se guardará la imagen
            string containerName = "publication";
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            // Genera un nombre único para la imagen
            string nombreArchivo = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)).ToString() + Path.GetExtension(archivo.FileName);

            // Crea un blob con el nombre generado y sube el archivo
            BlobClient blobClient = containerClient.GetBlobClient(nombreArchivo);
            await blobClient.UploadAsync(archivo.OpenReadStream());
            
            person.AvatarUrl = blobClient.Uri.ToString();
            person.UID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            person.IsActive = true;
            _context.Add(person);
            _context.SaveChanges();

            return RedirectToAction("SplashWelcome", "Splash");
        }

        [HttpPost]
        public IActionResult Registrer(Person person)
        {
            return RedirectToAction("SplashWelcomeCundecinos", "Splash");
        }
    }
}
