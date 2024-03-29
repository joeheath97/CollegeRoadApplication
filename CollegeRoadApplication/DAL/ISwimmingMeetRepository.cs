﻿using CollegeRoadApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeRoadApplication.DAL
{
    public interface ISwimmingMeetRepository : IDisposable
    {
        IEnumerable<SwimmingMeet> GetAllSwimmingMeets();

        IEnumerable<PoolSizeType> GetAllPoolSizeTypes();

        SwimmingMeet GetSwimmingMeetById(int id);

        SwimmingMeet GetSwimmingMeetInDB(int id);

        SwimmingMeet FindById(int id);

        void Add(SwimmingMeet swimmingMeet);

        void Save();

        void Remove(SwimmingMeet user);

    }
}
