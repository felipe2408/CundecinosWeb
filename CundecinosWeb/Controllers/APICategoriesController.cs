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
    public class APICategoriesController : Controller
    {
        private DataContext _context;

        public APICategoriesController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var categories = _context.categories.Select(i => new {
                i.CategoryID,
                i.Name,
                i.IsActive
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "CategoryID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(categories, loadOptions));
        }
        [HttpGet]
        public async Task<IActionResult> GetActive(DataSourceLoadOptions loadOptions)
        {
            var categories =await _context.categories.Where( x=> x.IsActive).ToListAsync();

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "CategoryID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Category();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.categories.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.CategoryID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.categories.FirstOrDefaultAsync(item => item.CategoryID == key);
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
            var model = await _context.categories.FirstOrDefaultAsync(item => item.CategoryID == key);

            _context.categories.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Category model, IDictionary values) {
            string CATEGORY_ID = nameof(Category.CategoryID);
            string NAME = nameof(Category.Name);
            string IS_ACTIVE = nameof(Category.IsActive);

            if(values.Contains(CATEGORY_ID)) {
                model.CategoryID = ConvertTo<System.Guid>(values[CATEGORY_ID]);
            }

            if(values.Contains(NAME)) {
                model.Name = Convert.ToString(values[NAME]);
            }

            if(values.Contains(IS_ACTIVE)) {
                model.IsActive = Convert.ToBoolean(values[IS_ACTIVE]);
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