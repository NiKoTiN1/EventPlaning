using EventPlanning.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanning.BusinessLogic.Interfaces
{
    public interface ICreatorService
    {
        Task CreateCreator(Creator creator);
    }
}
