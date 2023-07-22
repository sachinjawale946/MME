﻿using Microsoft.AspNetCore.Http;
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
    public class CastesApiController : ControllerBase
    {
        readonly MMEAppDBContext _context;

        public CastesApiController(MMEAppDBContext context)
        {
            _context = context;
        }

        [HttpGet, Route("~/api/v1/getcasts")]
        public List<CasteResponseModel> Get()
        {
            return _context.Castes.Where(c => c.IsActive)
                   .OrderBy(c => c.DisplayOrder)
                   .Select(o => new CasteResponseModel
                   {
                       caste = o.Caste,
                       casteid = o.CasteId,
                   }).ToList();
        }
    }
}
