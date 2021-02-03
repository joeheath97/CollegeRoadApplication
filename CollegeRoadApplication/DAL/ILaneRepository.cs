using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeRoadApplication.DAL
{
    public interface ILaneRepository : IDisposable
    {
        IEnumerable<ApplicationUser> GetAllEligiableSwimmers();

        IEnumerable<Lane> GetAllLanes();
        Lane GetLaneInDb(int id);
        Lane GetLaneById(int id);
        Lane FindById(int id);
        void Add(Lane lane);
        void Save();
        void Remove(Lane lane);

    }
}
