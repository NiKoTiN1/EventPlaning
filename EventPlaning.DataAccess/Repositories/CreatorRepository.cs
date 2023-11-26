using EventPlanning.DataAccess.Interfaces;
using EventPlanning.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanning.DataAccess.Repositories
{
    public class CreatorRepository : BaseRepository<Creator>, ICreatorRepository
    {
        public CreatorRepository(DatabaseContext context)
            : base(context)
        {
        }
    }
}
