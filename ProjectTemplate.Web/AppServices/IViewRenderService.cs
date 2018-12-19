using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTemplate.Web.AppServices
{
    interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model, ViewDataDictionary viewData = null);
    }
}
