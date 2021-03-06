//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BananaSplit.Data
{
    public partial class PriceLevel
    {
        #region Primitive Properties
    
        public virtual int PriceLevelId
        {
            get;
            set;
        }
    
        public virtual string PriceLevelName
        {
            get;
            set;
        }
    
        public virtual string PriceLevelDescription
        {
            get;
            set;
        }
    
        public virtual long TeamId
        {
            get { return _teamId; }
            set
            {
                if (_teamId != value)
                {
                    if (Team != null && Team.TeamId != value)
                    {
                        Team = null;
                    }
                    _teamId = value;
                }
            }
        }
        private long _teamId;
    
        public virtual long SeasonId
        {
            get { return _seasonId; }
            set
            {
                if (_seasonId != value)
                {
                    if (Season != null && Season.SeasonId != value)
                    {
                        Season = null;
                    }
                    _seasonId = value;
                }
            }
        }
        private long _seasonId;
    
        public virtual decimal Price
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties
    
        public virtual Season Season
        {
            get { return _season; }
            set
            {
                if (!ReferenceEquals(_season, value))
                {
                    var previousValue = _season;
                    _season = value;
                    FixupSeason(previousValue);
                }
            }
        }
        private Season _season;
    
        public virtual Team Team
        {
            get { return _team; }
            set
            {
                if (!ReferenceEquals(_team, value))
                {
                    var previousValue = _team;
                    _team = value;
                    FixupTeam(previousValue);
                }
            }
        }
        private Team _team;

        #endregion

        #region Association Fixup
    
        private void FixupSeason(Season previousValue)
        {
            if (previousValue != null && previousValue.PriceLevels.Contains(this))
            {
                previousValue.PriceLevels.Remove(this);
            }
    
            if (Season != null)
            {
                if (!Season.PriceLevels.Contains(this))
                {
                    Season.PriceLevels.Add(this);
                }
                if (SeasonId != Season.SeasonId)
                {
                    SeasonId = Season.SeasonId;
                }
            }
        }
    
        private void FixupTeam(Team previousValue)
        {
            if (previousValue != null && previousValue.PriceLevels.Contains(this))
            {
                previousValue.PriceLevels.Remove(this);
            }
    
            if (Team != null)
            {
                if (!Team.PriceLevels.Contains(this))
                {
                    Team.PriceLevels.Add(this);
                }
                if (TeamId != Team.TeamId)
                {
                    TeamId = Team.TeamId;
                }
            }
        }

        #endregion

    }
}
