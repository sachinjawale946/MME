using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MME.Data;
using MME.Model.Shared;

namespace MME.Web.Controllers
{
    public class MembersController : BaseController
    {
        readonly MMEAppDBContext _context;
        readonly IConfiguration _iconfiguration;

        public MembersController(MMEAppDBContext context, IConfiguration iconfiguration)
        {
            _context = context;
            _iconfiguration = iconfiguration;
        }

        public IActionResult All()
        {
            if (TempData["SuccessMessage"] != null && !string.IsNullOrEmpty(TempData["SuccessMessage"].ToString()))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            return View();
        }

        public IActionResult Read(string draw, string start, int length)
        {
            // Sort Column Name  
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            // Sort Column Direction ( asc ,desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            // Search Value from (Search box)  
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            //total number of rows count 
            var members = GetMembers(searchValue, skip, length, out recordsTotal);

            //Sorting  
            if ((!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection)))
            {
                var sortValue = typeof(UserModel).GetProperty(sortColumn.ToUpper(), System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Instance
                    | System.Reflection.BindingFlags.Public);
                if (sortValue != null)
                {
                    if (sortColumnDirection == "asc")
                        members = members.OrderBy(c => sortValue.GetValue(c, null)).ToList();
                    else
                        members = members.OrderByDescending(c => sortValue.GetValue(c, null)).ToList();
                }
            }

            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = members });
        }

        private List<UserModel> GetMembers(string SearchFilter, int skip, int length, out int recordsTotal)
        {
            var members = new List<UserModel>();
            if (!string.IsNullOrEmpty(SearchFilter))
                members = _context.Users.Include("Role").Where(u => u.IsActive == true && (u.FirstName.ToLower().Contains(SearchFilter.ToLower()) || u.LastName.ToLower().Contains(SearchFilter.ToLower()))).ToList();
            else
                members = _context.Users.Include("Role").Where(u => u.IsActive == true).ToList();

            recordsTotal = members.Count;

            return members.Skip(skip).Take(length).OrderByDescending(m => m.Username).ToList();
        }
    }
}
