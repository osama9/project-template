using System;

namespace ProjectTemplate.Core.Entities
{
    [Serializable]
    public class BaseEntity
    {
        public virtual int Id { get; set; }
    }
}