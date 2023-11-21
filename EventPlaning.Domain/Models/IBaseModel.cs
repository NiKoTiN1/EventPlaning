using System;

namespace EventPlaning.Domain.Models
{
    public interface IBaseModel
    {
        public Guid Id { get; set; }
    }
}
