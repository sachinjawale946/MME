﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MME.Data;
using MME.Model.Response;
using MME.Model.Shared;
using MME.Web.Filters;

namespace MME.Web.Apis
{
    [Route("api/[controller]")]
    [Authorize(Policy = "MMEJwtScheme")]
    [ApiController]
    [ServiceFilter(typeof(MMEExceptionFilter))]
    public class LanguagesApiController : ControllerBase
    {
        readonly MMEAppDBContext _context;

        public LanguagesApiController(MMEAppDBContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet, Route("~/api/v1/getlanguages")]
        public List<LanguageResponseModel> Get()
        {
            return _context.Languages.Where(c => c.IsActive)
                   .OrderBy(c => c.DisplayOrder)
                   .Select(o => new LanguageResponseModel
                   {
                       language  = o.Language,
                       languageid = o.LanguageId,
                       languagecode = o.LanguageCode
                   }).ToList();
        }
    }
}
