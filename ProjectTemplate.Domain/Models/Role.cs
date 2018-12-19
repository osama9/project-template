using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ProjectTemplate.Domain.Models
{
    
    public class Role : IdentityRole
    {
        [Required]
        public string NameArabic { get; set; }

        public string Description { get; set; }

        public Role Update(Role entity)
        {
            this.Name = entity.Name;
            this.NormalizedName = entity.NormalizedName;
            this.Id = entity.Id;
            this.ConcurrencyStamp = entity.ConcurrencyStamp;

            return this;
        }
    }
}