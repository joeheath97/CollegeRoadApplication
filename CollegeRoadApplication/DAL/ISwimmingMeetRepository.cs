using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeRoadApplication.DAL
{
    interface ISwimmingMeetRepository : IDisposable
    {
        IEnumerable<SwimmingMeet> GetAllSwimmingMeets();

        IEnumerable<PoolSizeType> GetAllPoolSizeTypes();

        SwimmingMeet GetSwimmingMeetById(int id);

        SwimmingMeet GetSwimmingMeetInDB(int id);

        void Add(SwimmingMeet swimmingMeet);

        void Save();

        void Remove(ApplicationUser user);

    }
}
