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
    public class APICollegeCareersController : Controller
    {
        private DataContext _context;

        public APICollegeCareersController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var collegecareer = _context.CollegeCareer.Select(i => new {
                i.CollegeCareerId,
                i.Name,
                i.IsActive
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "CollegeCareerId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(collegecareer, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> GetActive(DataSourceLoadOptions loadOptions)
        {
            var collegecareer = _context.CollegeCareer.Where(x => x.IsActive).Select(i => new {
                i.CollegeCareerId,
                i.Name,
                i.IsActive
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "CollegeCareerId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(collegecareer, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new CollegeCareer();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.CollegeCareer.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.CollegeCareerId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.CollegeCareer.FirstOrDefaultAsync(item => item.CollegeCareerId == key);
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
            var model = await _context.CollegeCareer.FirstOrDefaultAsync(item => item.CollegeCareerId == key);

            _context.CollegeCareer.Remove(model);
            await _context.SaveChangesAsync();
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

        private void PopulateModel(CollegeCareer model, IDictionary values) {
            string COLLEGE_CAREER_ID = nameof(CollegeCareer.CollegeCareerId);
            string NAME = nameof(CollegeCareer.Name);
            string IS_ACTIVE = nameof(CollegeCareer.IsActive);

            if(values.Contains(COLLEGE_CAREER_ID)) {
                model.CollegeCareerId = ConvertTo<System.Guid>(values[COLLEGE_CAREER_ID]);
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