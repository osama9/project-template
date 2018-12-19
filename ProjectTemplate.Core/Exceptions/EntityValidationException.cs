using System.Collections.Generic;

namespace ProjectTemplate.Core.Exceptions
{
    public class EntityValidationException : BusinessException
    {
        public EntityValidationException(List<string> errors) : base(errors)
        {

        }
    }
}