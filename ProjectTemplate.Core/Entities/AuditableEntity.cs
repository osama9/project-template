using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using ProjectTemplate.Core.Extensions;
using ProjectTemplate.Core.Resources;

namespace ProjectTemplate.Core.Entities
{
    public class AuditableEntity
    {
        [Display(Name = nameof(DomainText.AuditableEntity_CreatedDate), ResourceType = typeof(DomainText))]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = nameof(DomainText.AuditableEntity_ModifiedDate), ResourceType = typeof(DomainText))]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = nameof(DomainText.AuditableEntity_CreatedBy), ResourceType = typeof(DomainText))]
        public string CreatedByUserId { get; set; }

        public string ModifiedByUserId { get; set; }

        public void InsertAudit()
        {
            this.CreatedDate = DateTime.Now;
            this.CreatedByUserId = Thread.CurrentPrincipal.GetUserId();
        }

        public void UpdateAudit()
        {
            this.ModifiedDate = DateTime.Now;
            this.ModifiedByUserId = Thread.CurrentPrincipal.GetUserId();
        }
    }
}