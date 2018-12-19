using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTemplate.Web.Helpers.Providers
{
    public class DateTimeProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {

            if (context == null) throw new ArgumentNullException(nameof(context));

            if (!context.Metadata.IsComplexType)
            {
                // Look for scrubber attributes
                var underlyingOrModelType = context.Metadata.UnderlyingOrModelType;
                var isDateTime = underlyingOrModelType == typeof(DateTime) || underlyingOrModelType == typeof(DateTime?);

                if (isDateTime)
                    return new DateTimeBinder(underlyingOrModelType);
            }

            return null;
        }
    }
}
