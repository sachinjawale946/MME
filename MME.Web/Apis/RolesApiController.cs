using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MME.Model.Request;
using MME.Model.Response;
using MME.Data;
using Microsoft.AspNetCore.Authorization;
using MME.Web.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MME.Web.Apis
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [ServiceFilter(typeof(MMEExceptionFilter))]
    public class RolesApiController : ControllerBase
    {
        readonly MMEAppDBContext _context;

        public RolesApiController(MMEAppDBContext context)
        {
            _context = context;
        }

        [HttpGet, Route("~/api/v1/getroles")]
        public List<RoleResponseModel> Get()
        {
            return _context.Roles.Where(c => c.IsActive)
                   .OrderBy(c => c.DisplayOrder)
                   .Select(o => new RoleResponseModel
                   {
                       role = o.Role,
                       roleid = o.RoleId,
                   }).ToList();
        }
    }
}
