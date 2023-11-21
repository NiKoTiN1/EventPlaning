﻿using EventPlaning.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlaning.Domain.Models
{
    public class EventField : IBaseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FieldType Type { get; set; }
    }
}
