using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeRoadApplication.DAL
{
    public interface ISwimmingEventRepository : IDisposable
    {
        IEnumerable<SwimmingEvent> GetAllSwimmingEvents();
        IEnumerable<Lane> GetMemberEventLanes(string id);

        IEnumerable<SwimmingEvent> GetAllMeetEvents(int id);

        IEnumerable<SwimmingMeet> GetAllSwimmingMeets();

        ICollection<Lane> GetAllLanesIncludeUser(int id);

        SwimmingEvent GetSwimmingEventById(int id);
        SwimmingMeet GetSwimmingMeetById(int id);

        SwimmingEvent GetSwimmingEventInDb(int id);

        SwimmingEvent GetSwimmingEventIncludeMeetById(int id);

        SwimmingEvent Find(int id);

        void Add(SwimmingEvent swimmingEvent);

        void Save();

        void Remove(SwimmingEvent swimmingEvent);






    }
}
