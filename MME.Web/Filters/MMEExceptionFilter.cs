using Microsoft.AspNetCore.Mvc.Filters;
using MME.Data;
using MME.Model.Shared;

namespace MME.Web.Filters
{
    public class MMEExceptionFilter : ExceptionFilterAttribute
    {
        readonly MMEAppDBContext _context;

        public MMEExceptionFilter(MMEAppDBContext context)
        {
            _context = context;
        }

        public override void OnException(ExceptionContext context)
        {
            string action = context.ActionDescriptor.RouteValues["Action"];
            string controller = context.ActionDescriptor.RouteValues["Controller"];
            var query = context.HttpContext.Request.Path.Value;
            _context.ErrorLogs.Add(new ErrorLogModel
            {
                Action = action,
                Controller = controller,
                Query = query,
                CreatedOn = DateTime.Now,
                ErrorDetails = context.Exception.StackTrace,
                Message = context.Exception.Message,
                Username = string.Empty,
            });
            _context.SaveChanges();
            base.OnException(context);
        }
    }
}
