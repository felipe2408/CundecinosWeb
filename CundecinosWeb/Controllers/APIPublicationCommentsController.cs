using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CundecinosWeb.Data;
using CundecinosWeb.Models;

namespace CundecinosWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    public class APIPublicationCommentsController : Controller
    {
        private DataContext _context;

        public APIPublicationCommentsController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var publicationcomments = _context.PublicationComments.Select(i => new {
                i.PublicationCommentsID,
                i.PersonID,
                i.PublicationID,
                i.Content,
                i.CommentDate,
                i.EstimatedPrice,
                i.ProductDescription,
                i.ProductUrl
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "PublicationCommentsID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(publicationcomments, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new PublicationComments();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.PublicationComments.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.PublicationCommentsID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.PublicationComments.FirstOrDefaultAsync(item => item.PublicationCommentsID == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(Guid key) {
            var model = await _context.PublicationComments.FirstOrDefaultAsync(item => item.PublicationCommentsID == key);

            _context.PublicationComments.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> PublicationLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Publication
                         orderby i.Qualification
                         select new {
                             Value = i.PublicationID,
                             Text = i.Qualification
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> PeopleLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.People
                         orderby i.FirstName
                         select new {
                             Value = i.PersonID,
                             Text = i.FirstName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(PublicationComments model, IDictionary values) {
            string PUBLICATION_COMMENTS_ID = nameof(PublicationComments.PublicationCommentsID);
            string PERSON_ID = nameof(PublicationComments.PersonID);
            string PUBLICATION_ID = nameof(PublicationComments.PublicationID);
            string CONTENT = nameof(PublicationComments.Content);
            string COMMENT_DATE = nameof(PublicationComments.CommentDate);
            string ESTIMATED_PRICE = nameof(PublicationComments.EstimatedPrice);
            string PRODUCT_DESCRIPTION = nameof(PublicationComments.ProductDescription);
            string PRODUCT_URL = nameof(PublicationComments.ProductUrl);

            if(values.Contains(PUBLICATION_COMMENTS_ID)) {
                model.PublicationCommentsID = ConvertTo<System.Guid>(values[PUBLICATION_COMMENTS_ID]);
            }

            if(values.Contains(PERSON_ID)) {
                model.PersonID = ConvertTo<System.Guid>(values[PERSON_ID]);
            }

            if(values.Contains(PUBLICATION_ID)) {
                model.PublicationID = ConvertTo<System.Guid>(values[PUBLICATION_ID]);
            }

            if(values.Contains(CONTENT)) {
                model.Content = Convert.ToString(values[CONTENT]);
            }

            if(values.Contains(COMMENT_DATE)) {
                model.CommentDate = Convert.ToDateTime(values[COMMENT_DATE]);
            }

            if(values.Contains(ESTIMATED_PRICE)) {
                model.EstimatedPrice = Convert.ToString(values[ESTIMATED_PRICE]);
            }

            if(values.Contains(PRODUCT_DESCRIPTION)) {
                model.ProductDescription = Convert.ToString(values[PRODUCT_DESCRIPTION]);
            }

            if(values.Contains(PRODUCT_URL)) {
                model.ProductUrl = Convert.ToString(values[PRODUCT_URL]);
            }
        }

        private T ConvertTo<T>(object value) {
            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if(converter != null) {
                return (T)converter.ConvertFrom(null, CultureInfo.InvariantCulture, value);
            } else {
                // If necessary, implement a type conversion here
                throw new NotImplementedException();
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}