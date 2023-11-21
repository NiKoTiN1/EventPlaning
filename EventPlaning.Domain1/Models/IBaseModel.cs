using System;

namespace EventPlanning.Domain.Models
{
    public interface IBaseModel
    {
        public Guid Id { get; set; }
    }
}
