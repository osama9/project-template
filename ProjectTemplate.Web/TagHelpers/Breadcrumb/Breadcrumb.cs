using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTemplate.Web.TagHelpers.Breadcrumb
{
    public class BreadCrumb
    {
        /// <summary>
        /// A constant URL of the current item
        /// </summary>
        public string Url { set; get; }

        /// <summary>
        /// Title of the current item
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// An optional fontawsome icon of the current item
        /// </summary>
        public string Icon { set; get; }

        /// <summary>
        /// Oder of the current item in the final list
        /// </summary>
        public int Order { set; get; }
    }
}
