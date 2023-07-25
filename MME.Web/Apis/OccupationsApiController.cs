using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MME.Data;
using MME.Model.Response;
using MME.Web.Filters;

namespace MME.Web.Apis
{
    [Route("api/[controller]")]
    [Authorize(Policy = "MMEJwtScheme")]
    [ApiController]
    [ServiceFilter(typeof(MMEExceptionFilter))]
    public class OccupationsApiController : ControllerBase
    {
        readonly MMEAppDBContext _context;

        public OccupationsApiController(MMEAppDBContext context)
        {
            _context = context;
        }

        [HttpGet, Route("~/api/v1/getoccupations")]
        public List<OccupationResponseModel> Get()
        {
            return _context.Occupations.Where(c => c.IsActive)
                   .OrderBy(c => c.DisplayOrder)
                   .Select(o => new OccupationResponseModel
                   {
                       occupation = o.Occupation,
                       occupationid = o.OccupationId,
                   }).ToList();
        }
    }
}
