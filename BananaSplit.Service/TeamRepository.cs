using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BananaSplit.Data;

namespace BananaSplit.Service
{
    public class TeamRepository : BaseRepository<Team>
    {
        public Team GetById(int id)
        {
            return GetAll().SingleOrDefault(u => u.TeamId == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public List<Team> GetAllTeams()
        {
            return GetAll().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        /// 
        public List<Team> GetAllTeamsByLocation(int locationId)
        {
            var allTeams = GetAll().Where(t => t.LocationId == locationId);
            return (allTeams.Any()) ? allTeams.ToList() : null;
        }


        public List<Team> GetAllTeamsByTeamType(int teamTypeId)
        {
            var allTeamsForType = GetAll().Where(t => t.TeamTypeId == teamTypeId);
            return (allTeamsForType.Any() ? allTeamsForType.ToList() : null);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        /// 
        public List<Team> GetAllTeamsByState(int stateId)
        {
            var allTeams = GetAll().Where(t => t.Location.StateId == stateId);
            return (allTeams.Any()) ? allTeams.ToList() : null; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// 
        public Boolean Save(Team entity)
        {
            if (entity.TeamId == 0)
            {
                //Insert
                return Add(entity);
            }
            else
            {
                //Update
                return Update(entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Boolean Remove(Team entity)
        {
            return Delete(entity);
        }
    }
}
