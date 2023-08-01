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
    public class PincodesApiController : ControllerBase
    {
        readonly MMEAppDBContext _context;

        public PincodesApiController(MMEAppDBContext context)
        {
            _context = context;
        }

        [HttpGet, Route("~/api/v1/getpincodes-bystate/{State}")]
        public List<PincodeResponseModel> GetByState(int State)
        {
            return _context.Pincodes.Where(c => c.IsActive && c.State == State)
                   .OrderBy(c => c.DisplayOrder)
                   .Select(o => new PincodeResponseModel
                   {
                       pincode = o.PinCode,
                       pincodeid = o.PinCodeId,
                       stateId = o.State,
                   }).ToList();
        }
    }
}
