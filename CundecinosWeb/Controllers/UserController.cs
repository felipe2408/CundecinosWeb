using Azure.Storage.Blobs;
using CundecinosWeb.Data;
using CundecinosWeb.Models;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        

        

        public IActionResult Register()
        {
            var person = new Person();

            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            person.FirstName = User.FindFirstValue(ClaimTypes.Name);
            person.LastName = User.FindFirstValue(ClaimTypes.GivenName);
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
            string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);

            // Crea un blob con el nombre generado y sube el archivo
            BlobClient blobClient = containerClient.GetBlobClient(nombreArchivo);
            await blobClient.UploadAsync(archivo.OpenReadStream());
            
            person.AvatarUrl = blobClient.Uri.ToString();
            person.UID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            person.IsActive = true;
            _context.Add(person);
            _context.SaveChanges();

            return View();
        }

        [HttpPost]
        public IActionResult Registrer(Person person)
        {
            return RedirectToAction("SplashWelcomeCundecinos", "Splash");
        }
    }
}
