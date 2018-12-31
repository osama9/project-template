using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjectTemplate.Core.Exceptions;
using ProjectTemplate.Persistance.Repositories;
using ProjectTemplate.Core.Exceptions;
using ProjectTemplate.Persistance.Repositories;

namespace ProjectTemplate.Application.Services
{
   
    public class ServiceBase<TService> where TService : class
    {

        protected internal GenericRepository repository;

        protected string[] Includes { get; set; }

        protected ServiceBase(GenericRepository genericRepository)
        {
            this.repository = genericRepository;
        }

            
        public void ValidateEntity(object entity)
        {
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
            {
                var erros = new List<string>();

                foreach (var item in validationResults)
                    erros.Add(item.ErrorMessage);

                throw new EntityValidationException(erros);
            }
        }

    }
}
