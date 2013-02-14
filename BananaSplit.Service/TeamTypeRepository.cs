using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class TeamTypeRepository :BaseRepository<TeamType>
    {

        public TeamType GetById(int id)
        {
            return GetAll().SingleOrDefault(tt => tt.TeamTypeId == id);
        }

        public List<TeamType> GetAllTeamTypes()
        {
            return GetAll().ToList();
        }
    }
}
