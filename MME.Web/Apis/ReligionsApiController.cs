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
    public class ReligionsApiController : ControllerBase
    {
        readonly MMEAppDBContext _context;

        public ReligionsApiController(MMEAppDBContext context)
        {
            _context = context;
        }

        [HttpGet, Route("~/api/v1/getreligions")]
        public List<ReligionResponseModel> Get()
        {
            return _context.Religions.Where(c => c.IsActive)
                   .OrderBy(c => c.DisplayOrder)
                   .Select(o => new ReligionResponseModel
                   {
                       religion = o.Religion,
                       religionid = o.ReligionId,
                   }).ToList();
        }
    }
}
