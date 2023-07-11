using Azure.Storage.Blobs;
using CundecinosWeb.Data;
using CundecinosWeb.Models;
using CundecinosWeb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace CundecinosWeb.Controllers
{
    [Authorize]
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
            var model = _context.People.Include(x => x.CollegeCareer).Where(x => x.UID == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).FirstOrDefault();
            if (model == null)
            {
                return RedirectToAction("Register", "User");
            }

            vmodel.Person = model;
            vmodel.Publication = new Publication();
            vmodel.PublicationUsers = _context.Publication.Include(x => x.Person).Include(x => x.PublicationComments).Include(x => x.Person.Califications).Include(x => x.PublicationAttachment).Where(x => x.PublicationDate >= DateTime.Now || x.PublicationDate <= DateTime.Now && x.IsActive == true).ToList();
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
                publication.IsActive = true;
                publication.Status = Enum.Status.Negotiation;
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
                return View("Ocurrio un error " + e);
            }

            return RedirectToAction("Index", "Home");
        }
        [Route("/SearchPublication")]
        [HttpGet]
        public async Task<IActionResult> ObtenerPublicaciones(string busqueda)
        {
            try
            {
                var publicaciones = _context.Publication
                    .Include(x => x.Person)
                    .Include(x => x.PublicationComments)
                    .Include(x => x.PublicationAttachment)
                    .Where(x => x.PublicationDate >= DateTime.Now || x.PublicationDate <= DateTime.Now && x.IsActive == true && (x.Qualification.ToLower().Contains(busqueda.Trim()) || x.Content.ToLower().Contains(busqueda.Trim()) || x.ProductDescription.ToLower().Contains(busqueda.Trim())))
                    .ToList();

                string html = "";
                foreach (var publicacion in publicaciones)
                {
                    string plantilla = $@"
                         <div class=""card mb-5 mb-xl-8"">
                            <div class=""card-body pb-0"">
                                <div class=""d-flex align-items-center mb-5"">
                                    <div class=""d-flex align-items-center flex-grow-1"">
                                        <div class=""symbol symbol-45px me-5"">
                                            <img class=""imagen-avatar"" src=""{publicacion.Person.AvatarUrl}"" alt=""Avatar Usuario"" />
                                        </div>
                                        <div class=""d-flex flex-column"">
                                            <a href=""#"" class=""text-gray-800 text-hover-primary fs-6 fw-bolder"">{publicacion.Person.FirstName + " " + publicacion.Person.LastName}</a>
                                            <span class=""text-gray-400 fw-bold"">{publicacion.Person.Email}</span>
                                        </div>
                                    </div>
                ";
                    if (publicacion.PublicationType == Enum.PublicationType.Sale)
                    {
                        plantilla += "<span class=\"badge badge-light-success\">En Venta</span>";
                    }
                    else
                    {
                        plantilla += "<span class=\"badge badge-light-warning\">En Trueque</span>";
                    }
                    plantilla += @$"</div>
                                <div class=""row g-5 g-xl-8"">
                                    <div class=""col-xl-4"">
                                        <div class=""card card-xl-stretch mb-xl-8"">
                                            <div class=""card-header align-items-center border-0 mt-4"">
                                                <h3 class=""card-title align-items-start flex-column"">
                                                    <span class=""fw-bolder text-dark"">{publicacion.Qualification}</span>
                                                    <span class=""text-muted mt-1 fw-bold fs-7"">{publicacion.Content}</span>
                                                    <span class=""text-muted mt-1 fw-bold fs-7"">Descripción del Producto : {publicacion.ProductDescription}</span>
                                                    <span class=""text-muted mt-1 fw-bold fs-7"">Precio Estimado : {publicacion.EstimatedPrice}</span>
                                                </h3>
                                                <div class=""card-toolbar"">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class=""col-xl-8"">

                                        <div class=""card card-xl-stretch mb-5 mb-xl-8"">
                                            <div class=""card-header align-items-center border-0 mt-4"">
                ";

                    foreach (var imageUrl in publicacion.PublicationAttachment)
                    {
                        plantilla += @$"
                           <div class=""symbol symbol-60px symbol-2by3 me-4"">
                                <div class=""symbol-label"" style=""background-image: url('{imageUrl.ImageThumbNail}'); height: 130px !important; width: 130px !important;""></div>
                           </div>
                    ";
                    }
                    plantilla += $@"
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class=""mb-5"">
                                    <div class=""d-flex align-items-center mb-5"">
                                        <a href=""/Chat/ChatUser/{publicacion.Person.UID}"" class=""btn btn-sm btn-light btn-color-muted btn-active-light-success px-4 py-2 me-4"">
                                            <span class=""svg-icon svg-icon-3"">
                                                <svg xmlns=""http://www.w3.org/2000/svg"" width=""24px"" height=""24px"" viewBox=""0 0 24 24"" version=""1.1"">
                                                    <path d=""M16,15.6315789 L16,12 C16,10.3431458 14.6568542,9 13,9 L6.16183229,9 L6.16183229,5.52631579 C6.16183229,4.13107011 7.29290239,3 8.68814808,3 L20.4776218,3 C21.8728674,3 23.0039375,4.13107011 23.0039375,5.52631579 L23.0039375,13.1052632 L23.0206157,17.786793 C23.0215995,18.0629336 22.7985408,18.2875874 22.5224001,18.2885711 C22.3891754,18.2890457 22.2612702,18.2363324 22.1670655,18.1421277 L19.6565168,15.6315789 L16,15.6315789 Z"" fill=""#000000"" />
                                                    <path d=""M1.98505595,18 L1.98505595,13 C1.98505595,11.8954305 2.88048645,11 3.98505595,11 L11.9850559,11 C13.0896254,11 13.9850559,11.8954305 13.9850559,13 L13.9850559,18 C13.9850559,19.1045695 13.0896254,20 11.9850559,20 L4.10078614,20 L2.85693427,21.1905292 C2.65744295,21.3814685 2.34093638,21.3745358 2.14999706,21.1750444 C2.06092565,21.0819836 2.01120804,20.958136 2.01120804,20.8293182 L2.01120804,18.32426 C1.99400175,18.2187196 1.98505595,18.1104045 1.98505595,18 Z M6.5,14 C6.22385763,14 6,14.2238576 6,14.5 C6,14.7761424 6.22385763,15 6.5,15 L11.5,15 C11.7761424,15 12,14.7761424 12,14.5 C12,14.2238576 11.7761424,14 11.5,14 L6.5,14 Z M9.5,16 C9.22385763,16 9,16.2238576 9,16.5 C9,16.7761424 9.22385763,17 9.5,17 L11.5,17 C11.7761424,17 12,16.7761424 12,16.5 C12,16.2238576 11.7761424,16 11.5,16 L9.5,16 Z"" fill=""#000000"" opacity=""0.3"" />
                                                </svg>
                                            </span>
                                            Contactar
                                        </a>
                                        <a href=""/PublicationComment/PublicationDescription/{publicacion.PublicationID}"" class=""btn btn-sm btn-light btn-color-muted btn-active-light-success px-4 py-2 me-4"">
                                            <span class=""svg-icon svg-icon-3"">
                                                <svg xmlns=""http://www.w3.org/2000/svg"" width=""24px"" height=""24px"" viewBox=""0 0 24 24"" version=""1.1"">
                                                    <path d=""M16,15.6315789 L16,12 C16,10.3431458 14.6568542,9 13,9 L6.16183229,9 L6.16183229,5.52631579 C6.16183229,4.13107011 7.29290239,3 8.68814808,3 L20.4776218,3 C21.8728674,3 23.0039375,4.13107011 23.0039375,5.52631579 L23.0039375,13.1052632 L23.0206157,17.786793 C23.0215995,18.0629336 22.7985408,18.2875874 22.5224001,18.2885711 C22.3891754,18.2890457 22.2612702,18.2363324 22.1670655,18.1421277 L19.6565168,15.6315789 L16,15.6315789 Z"" fill=""#000000"" />
                                                    <path d=""M1.98505595,18 L1.98505595,13 C1.98505595,11.8954305 2.88048645,11 3.98505595,11 L11.9850559,11 C13.0896254,11 13.9850559,11.8954305 13.9850559,13 L13.9850559,18 C13.9850559,19.1045695 13.0896254,20 11.9850559,20 L4.10078614,20 L2.85693427,21.1905292 C2.65744295,21.3814685 2.34093638,21.3745358 2.14999706,21.1750444 C2.06092565,21.0819836 2.01120804,20.958136 2.01120804,20.8293182 L2.01120804,18.32426 C1.99400175,18.2187196 1.98505595,18.1104045 1.98505595,18 Z M6.5,14 C6.22385763,14 6,14.2238576 6,14.5 C6,14.7761424 6.22385763,15 6.5,15 L11.5,15 C11.7761424,15 12,14.7761424 12,14.5 C12,14.2238576 11.7761424,14 11.5,14 L6.5,14 Z M9.5,16 C9.22385763,16 9,16.2238576 9,16.5 C9,16.7761424 9.22385763,17 9.5,17 L11.5,17 C11.7761424,17 12,16.7761424 12,16.5 C12,16.2238576 11.7761424,16 11.5,16 L9.5,16 Z"" fill=""#000000"" opacity=""0.3"" />
                                                </svg>
                                            </span>
                                            Ofertas {publicacion.PublicationComments.Count()}
                                        </a>
                                    </div>
                                </div>
                                <div class=""separator mb-4""></div>

                            </div>
                        </div>
                ";
                    html += plantilla;
                }
                return Content(html, "text/html");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
