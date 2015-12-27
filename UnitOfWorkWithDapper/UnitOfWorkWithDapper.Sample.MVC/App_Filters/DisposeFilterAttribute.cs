using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitOfWorkWithDapper.Sample.Core.Contexts;

namespace UnitOfWorkWithDapper.Sample.MVC
{
    /// <summary>
    /// Filter responsible for disposing instance contexts when finalizing the request
    /// </summary>
    public class DisposeFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var container = UnityConfig.GetConfiguredContainer();

            // get context
            var context = container.Resolve<MyDbContext>();

            // disposes context
            context.Dispose();

            // clean it up context
            container.Teardown(context);
        }
    }
}