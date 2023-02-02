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
    public class APIPeopleController : Controller
    {
        private DataContext _context;

        public APIPeopleController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var people = _context.People.Select(i => new {
                i.PersonID,
                i.UID,
                i.FirstName,
                i.LastName,
                i.IdentificationNumber,
                i.CellPhone,
                i.Email,
                i.BirthYear,
                i.Company,
                i.CollegeCareerId,
                i.IsActive
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "PersonID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(people, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Person();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.People.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.PersonID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid key, string values) {
            var model = await _context.People.FirstOrDefaultAsync(item => item.PersonID == key);
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
            var model = await _context.People.FirstOrDefaultAsync(item => item.PersonID == key);

            _context.People.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Person model, IDictionary values) {
            string PERSON_ID = nameof(Person.PersonID);
            string UID = nameof(Person.UID);
            string FIRST_NAME = nameof(Person.FirstName);
            string LAST_NAME = nameof(Person.LastName);
            string IDENTIFICATION_NUMBER = nameof(Person.IdentificationNumber);
            string CELL_PHONE = nameof(Person.CellPhone);
            string EMAIL = nameof(Person.Email);
            string BIRTH_YEAR = nameof(Person.BirthYear);
            string COMPANY = nameof(Person.Company);
            string COLLEGE_CAREER_ID = nameof(Person.CollegeCareerId);
            string IS_ACTIVE = nameof(Person.IsActive);

            if(values.Contains(PERSON_ID)) {
                model.PersonID = ConvertTo<System.Guid>(values[PERSON_ID]);
            }

            if(values.Contains(UID)) {
                model.UID = values[UID] != null ? ConvertTo<System.Guid>(values[UID]) : (Guid?)null;
            }

            if(values.Contains(FIRST_NAME)) {
                model.FirstName = Convert.ToString(values[FIRST_NAME]);
            }

            if(values.Contains(LAST_NAME)) {
                model.LastName = Convert.ToString(values[LAST_NAME]);
            }

            if(values.Contains(IDENTIFICATION_NUMBER)) {
                model.IdentificationNumber = Convert.ToString(values[IDENTIFICATION_NUMBER]);
            }

            if(values.Contains(CELL_PHONE)) {
                model.CellPhone = Convert.ToString(values[CELL_PHONE]);
            }

            if(values.Contains(EMAIL)) {
                model.Email = Convert.ToString(values[EMAIL]);
            }

            if(values.Contains(BIRTH_YEAR)) {
                model.BirthYear = values[BIRTH_YEAR] != null ? Convert.ToInt16(values[BIRTH_YEAR]) : (short?)null;
            }

            if(values.Contains(COMPANY)) {
                model.Company = Convert.ToString(values[COMPANY]);
            }

            if(values.Contains(COLLEGE_CAREER_ID)) {
                model.CollegeCareerId = values[COLLEGE_CAREER_ID] != null ? ConvertTo<System.Guid>(values[COLLEGE_CAREER_ID]) : (Guid?)null;
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