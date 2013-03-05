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
    public partial class Team
    {
        #region Primitive Properties
    
        public virtual long TeamId
        {
            get;
            set;
        }
    
        public virtual string TeamName
        {
            get;
            set;
        }
    
        public virtual int LocationId
        {
            get { return _locationId; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_locationId != value)
                    {
                        if (Location != null && Location.LocationId != value)
                        {
                            Location = null;
                        }
                        _locationId = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _locationId;
    
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
    
        public virtual int TeamTypeId
        {
            get { return _teamTypeId; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_teamTypeId != value)
                    {
                        if (TeamType != null && TeamType.TeamTypeId != value)
                        {
                            TeamType = null;
                        }
                        _teamTypeId = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _teamTypeId;
    
        public virtual bool IsActive
        {
            get;
            set;
        }
    
        public virtual Nullable<long> PhotoId
        {
            get { return _photoId; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_photoId != value)
                    {
                        if (Photo != null && Photo.PhotoId != value)
                        {
                            Photo = null;
                        }
                        _photoId = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<long> _photoId;

        #endregion

        #region Navigation Properties
    
        public virtual Location Location
        {
            get { return _location; }
            set
            {
                if (!ReferenceEquals(_location, value))
                {
                    var previousValue = _location;
                    _location = value;
                    FixupLocation(previousValue);
                }
            }
        }
        private Location _location;
    
        public virtual ICollection<Season> Seasons
        {
            get
            {
                if (_seasons == null)
                {
                    var newCollection = new FixupCollection<Season>();
                    newCollection.CollectionChanged += FixupSeasons;
                    _seasons = newCollection;
                }
                return _seasons;
            }
            set
            {
                if (!ReferenceEquals(_seasons, value))
                {
                    var previousValue = _seasons as FixupCollection<Season>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupSeasons;
                    }
                    _seasons = value;
                    var newValue = value as FixupCollection<Season>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupSeasons;
                    }
                }
            }
        }
        private ICollection<Season> _seasons;
    
        public virtual TeamType TeamType
        {
            get { return _teamType; }
            set
            {
                if (!ReferenceEquals(_teamType, value))
                {
                    var previousValue = _teamType;
                    _teamType = value;
                    FixupTeamType(previousValue);
                }
            }
        }
        private TeamType _teamType;
    
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
    
        public virtual ICollection<Game> Games1
        {
            get
            {
                if (_games1 == null)
                {
                    var newCollection = new FixupCollection<Game>();
                    newCollection.CollectionChanged += FixupGames1;
                    _games1 = newCollection;
                }
                return _games1;
            }
            set
            {
                if (!ReferenceEquals(_games1, value))
                {
                    var previousValue = _games1 as FixupCollection<Game>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupGames1;
                    }
                    _games1 = value;
                    var newValue = value as FixupCollection<Game>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupGames1;
                    }
                }
            }
        }
        private ICollection<Game> _games1;
    
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
    
        public virtual Photo Photo
        {
            get { return _photo; }
            set
            {
                if (!ReferenceEquals(_photo, value))
                {
                    var previousValue = _photo;
                    _photo = value;
                    FixupPhoto(previousValue);
                }
            }
        }
        private Photo _photo;

        #endregion

        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupLocation(Location previousValue)
        {
            if (previousValue != null && previousValue.Teams.Contains(this))
            {
                previousValue.Teams.Remove(this);
            }
    
            if (Location != null)
            {
                if (!Location.Teams.Contains(this))
                {
                    Location.Teams.Add(this);
                }
                if (LocationId != Location.LocationId)
                {
                    LocationId = Location.LocationId;
                }
            }
        }
    
        private void FixupTeamType(TeamType previousValue)
        {
            if (previousValue != null && previousValue.Teams.Contains(this))
            {
                previousValue.Teams.Remove(this);
            }
    
            if (TeamType != null)
            {
                if (!TeamType.Teams.Contains(this))
                {
                    TeamType.Teams.Add(this);
                }
                if (TeamTypeId != TeamType.TeamTypeId)
                {
                    TeamTypeId = TeamType.TeamTypeId;
                }
            }
        }
    
        private void FixupPhoto(Photo previousValue)
        {
            if (previousValue != null && previousValue.Teams.Contains(this))
            {
                previousValue.Teams.Remove(this);
            }
    
            if (Photo != null)
            {
                if (!Photo.Teams.Contains(this))
                {
                    Photo.Teams.Add(this);
                }
                if (PhotoId != Photo.PhotoId)
                {
                    PhotoId = Photo.PhotoId;
                }
            }
            else if (!_settingFK)
            {
                PhotoId = null;
            }
        }
    
        private void FixupSeasons(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Season item in e.NewItems)
                {
                    item.Team = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Season item in e.OldItems)
                {
                    if (ReferenceEquals(item.Team, this))
                    {
                        item.Team = null;
                    }
                }
            }
        }
    
        private void FixupGames(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Game item in e.NewItems)
                {
                    item.Team = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Game item in e.OldItems)
                {
                    if (ReferenceEquals(item.Team, this))
                    {
                        item.Team = null;
                    }
                }
            }
        }
    
        private void FixupGames1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Game item in e.NewItems)
                {
                    item.Team1 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Game item in e.OldItems)
                {
                    if (ReferenceEquals(item.Team1, this))
                    {
                        item.Team1 = null;
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
                    item.Team = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Partnership item in e.OldItems)
                {
                    if (ReferenceEquals(item.Team, this))
                    {
                        item.Team = null;
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
                    item.Team = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (PriceLevel item in e.OldItems)
                {
                    if (ReferenceEquals(item.Team, this))
                    {
                        item.Team = null;
                    }
                }
            }
        }

        #endregion

    }
}
