using EventPlanning.BusinessLogic.Interfaces;
using EventPlanning.DataAccess.Interfaces;
using EventPlanning.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanning.BusinessLogic.Services
{
    public class CreatorService : ICreatorService
    {
        private readonly ICreatorRepository _creatorRepository;

        public CreatorService(ICreatorRepository creatorRepository)
        {
            _creatorRepository = creatorRepository;
        }

        public async Task CreateCreator(Creator creator)
        {
            await _creatorRepository.Add(creator);
        }
    }
}
