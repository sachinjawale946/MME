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
    [Authorize(Policy = "MMEJwtScheme")]
    [ApiController]
    [ServiceFilter(typeof(MMEExceptionFilter))]
    public class StatesApiController : ControllerBase
    {
        readonly MMEAppDBContext _context;

        public StatesApiController(MMEAppDBContext context)
        {
            _context = context;
        }

        [HttpGet, Route("~/api/v1/getstates")]
        public List<StateResponseModel> Get()
        {
            return _context.States.Where(c => c.IsActive)
                   .OrderBy(c => c.DisplayOrder)
                   .Select(o => new StateResponseModel
                   {
                       state = o.State,
                       stateid = o.StateId,
                   }).ToList();
        }
    }
}
