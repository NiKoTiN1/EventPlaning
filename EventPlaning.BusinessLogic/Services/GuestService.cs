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
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;
        public GuestService(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task CreateGuest(Guest guest)
        {
            await _guestRepository.Add(guest);
        }
    }
}
