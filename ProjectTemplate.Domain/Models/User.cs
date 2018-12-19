using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ProjectTemplate.Core.Resources;

namespace ProjectTemplate.Domain.Models
{
    public class User : IdentityUser
    {

        [Required(ErrorMessageResourceName = nameof(ValidationText.Required), ErrorMessageResourceType = typeof(ValidationText))]
        [Display(Name = nameof(DomainText.User_FullName), ResourceType = typeof(DomainText))]
        public string FullName { get; set; }

        [Display(Name = nameof(DomainText.User_Username), ResourceType = typeof(DomainText))]
        public override string UserName { get => base.UserName; set => base.UserName = value; }

        [Display(Name = nameof(DomainText.User_Email), ResourceType = typeof(DomainText))]
        public override string Email { get => base.Email; set => base.Email = value; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();

        [Display(Name = nameof(DomainText.User_CreatedDate), ResourceType = typeof(DomainText))]
        public DateTime? CreatedDate { get; set; }

        public User()
        {
            CreatedDate = DateTime.Now;
        }

        public User Update(User entity)
        {
            this.UserName = entity.UserName;
            this.Email = entity.Email;
            this.FullName = entity.FullName;
            return this;
        }
    }
}