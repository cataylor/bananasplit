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
    public partial class Season
    {
        #region Primitive Properties
    
        public virtual long SeasonId
        {
            get;
            set;
        }
    
        public virtual long TeamId
        {
            get { return _teamId; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_teamId != value)
                    {
                        if (Team != null && Team.TeamId != value)
                        {
                            Team = null;
                        }
                        _teamId = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private long _teamId;
    
        public virtual System.DateTime EventStartDate
        {
            get;
            set;
        }
    
        public virtual string Opponent
        {
            get;
            set;
        }
    
        public virtual string TimeZone
        {
            get;
            set;
        }
    
        public virtual string EventLocation
        {
            get;
            set;
        }
    
        public virtual System.DateTime EventEndDate
        {
            get;
            set;
        }
    
        public virtual bool IsArchived
        {
            get;
            set;
        }
    
        public virtual bool IsActive
        {
            get;
            set;
        }
    
        public virtual System.DateTime DateCreated
        {
            get;
            set;
        }
    
        public virtual System.DateTime DateUpdated
        {
            get;
            set;
        }
    
        public virtual int SeasonYearsId
        {
            get { return _seasonYearsId; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_seasonYearsId != value)
                    {
                        if (SeasonYear != null && SeasonYear.SeasonYearsId != value)
                        {
                            SeasonYear = null;
                        }
                        _seasonYearsId = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _seasonYearsId;
    
        public virtual Nullable<int> SeasonTypeId
        {
            get { return _seasonTypeId; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_seasonTypeId != value)
                    {
                        if (SeasonType != null && SeasonType.SeasonTypeId != value)
                        {
                            SeasonType = null;
                        }
                        _seasonTypeId = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _seasonTypeId;

        #endregion
        #region Navigation Properties
    
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
    
        public virtual SeasonYear SeasonYear
        {
            get { return _seasonYear; }
            set
            {
                if (!ReferenceEquals(_seasonYear, value))
                {
                    var previousValue = _seasonYear;
                    _seasonYear = value;
                    FixupSeasonYear(previousValue);
                }
            }
        }
        private SeasonYear _seasonYear;
    
        public virtual SeasonType SeasonType
        {
            get { return _seasonType; }
            set
            {
                if (!ReferenceEquals(_seasonType, value))
                {
                    var previousValue = _seasonType;
                    _seasonType = value;
                    FixupSeasonType(previousValue);
                }
            }
        }
        private SeasonType _seasonType;
    
        public virtual ICollection<Game> Games
        {
            get
            {
                if (_games == null)
                {
                    var newCollection = new FixupCollection<Game>();
                    newCollection.CollectionChanged += FixupGames;
                    _games = newCollection;
                }
                return _games;
            }
            set
            {
                if (!ReferenceEquals(_games, value))
                {
                    var previousValue = _games as FixupCollection<Game>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupGames;
                    }
                    _games = value;
                    var newValue = value as FixupCollection<Game>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupGames;
                    }
                }
            }
        }
        private ICollection<Game> _games;
    
        public virtual ICollection<Partnership> Partnerships
        {
            get
            {
                if (_partnerships == null)
                {
                    var newCollection = new FixupCollection<Partnership>();
                    newCollection.CollectionChanged += FixupPartnerships;
                    _partnerships = newCollection;
                }
                return _partnerships;
            }
            set
            {
                if (!ReferenceEquals(_partnerships, value))
                {
                    var previousValue = _partnerships as FixupCollection<Partnership>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupPartnerships;
                    }
                    _partnerships = value;
                    var newValue = value as FixupCollection<Partnership>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupPartnerships;
                    }
                }
            }
        }
        private ICollection<Partnership> _partnerships;
    
        public virtual ICollection<PriceLevel> PriceLevels
        {
            get
            {
                if (_priceLevels == null)
                {
                    var newCollection = new FixupCollection<PriceLevel>();
                    newCollection.CollectionChanged += FixupPriceLevels;
                    _priceLevels = newCollection;
                }
                return _priceLevels;
            }
            set
            {
                if (!ReferenceEquals(_priceLevels, value))
                {
                    var previousValue = _priceLevels as FixupCollection<PriceLevel>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupPriceLevels;
                    }
                    _priceLevels = value;
                    var newValue = value as FixupCollection<PriceLevel>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupPriceLevels;
                    }
                }
            }
        }
        private ICollection<PriceLevel> _priceLevels;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupTeam(Team previousValue)
        {
            if (previousValue != null && previousValue.Seasons.Contains(this))
            {
                previousValue.Seasons.Remove(this);
            }
    
            if (Team != null)
            {
                if (!Team.Seasons.Contains(this))
                {
                    Team.Seasons.Add(this);
                }
                if (TeamId != Team.TeamId)
                {
                    TeamId = Team.TeamId;
                }
            }
        }
    
        private void FixupSeasonYear(SeasonYear previousValue)
        {
            if (previousValue != null && previousValue.Seasons.Contains(this))
            {
                previousValue.Seasons.Remove(this);
            }
    
            if (SeasonYear != null)
            {
                if (!SeasonYear.Seasons.Contains(this))
                {
                    SeasonYear.Seasons.Add(this);
                }
                if (SeasonYearsId != SeasonYear.SeasonYearsId)
                {
                    SeasonYearsId = SeasonYear.SeasonYearsId;
                }
            }
        }
    
        private void FixupSeasonType(SeasonType previousValue)
        {
            if (previousValue != null && previousValue.Seasons.Contains(this))
            {
                previousValue.Seasons.Remove(this);
            }
    
            if (SeasonType != null)
            {
                if (!SeasonType.Seasons.Contains(this))
                {
                    SeasonType.Seasons.Add(this);
                }
                if (SeasonTypeId != SeasonType.SeasonTypeId)
                {
                    SeasonTypeId = SeasonType.SeasonTypeId;
                }
            }
            else if (!_settingFK)
            {
                SeasonTypeId = null;
            }
        }
    
        private void FixupGames(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Game item in e.NewItems)
                {
                    item.Season = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Game item in e.OldItems)
                {
                    if (ReferenceEquals(item.Season, this))
                    {
                        item.Season = null;
                    }
                }
            }
        }
    
        private void FixupPartnerships(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Partnership item in e.NewItems)
                {
                    item.Season = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Partnership item in e.OldItems)
                {
                    if (ReferenceEquals(item.Season, this))
                    {
                        item.Season = null;
                    }
                }
            }
        }
    
        private void FixupPriceLevels(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (PriceLevel item in e.NewItems)
                {
                    item.Season = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (PriceLevel item in e.OldItems)
                {
                    if (ReferenceEquals(item.Season, this))
                    {
                        item.Season = null;
                    }
                }
            }
        }

        #endregion
    }
}
