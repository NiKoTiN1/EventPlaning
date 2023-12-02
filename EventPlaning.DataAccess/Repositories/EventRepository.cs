using EventPlanning.DataAccess.Interfaces;
using EventPlanning.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlanning.DataAccess.Repositories
{
    public class EventRepository : BaseRepository<EventModel>, IEventRepository
    {
        public EventRepository(DatabaseContext context)
            : base(context)
        {
        }
    }
}
