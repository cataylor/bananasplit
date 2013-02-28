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
    public partial class Partnership
    {
        #region Primitive Properties
    
        public virtual long PartnershipId
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
    
        public virtual short TotalSeats
        {
            get;
            set;
        }
    
        public virtual string PartnershipName
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

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<Lottery> Lotteries
        {
            get
            {
                if (_lotteries == null)
                {
                    var newCollection = new FixupCollection<Lottery>();
                    newCollection.CollectionChanged += FixupLotteries;
                    _lotteries = newCollection;
                }
                return _lotteries;
            }
            set
            {
                if (!ReferenceEquals(_lotteries, value))
                {
                    var previousValue = _lotteries as FixupCollection<Lottery>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupLotteries;
                    }
                    _lotteries = value;
                    var newValue = value as FixupCollection<Lottery>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupLotteries;
                    }
                }
            }
        }
        private ICollection<Lottery> _lotteries;
    
        public virtual ICollection<MemberPartnership> MemberPartnerships
        {
            get
            {
                if (_memberPartnerships == null)
                {
                    var newCollection = new FixupCollection<MemberPartnership>();
                    newCollection.CollectionChanged += FixupMemberPartnerships;
                    _memberPartnerships = newCollection;
                }
                return _memberPartnerships;
            }
            set
            {
                if (!ReferenceEquals(_memberPartnerships, value))
                {
                    var previousValue = _memberPartnerships as FixupCollection<MemberPartnership>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMemberPartnerships;
                    }
                    _memberPartnerships = value;
                    var newValue = value as FixupCollection<MemberPartnership>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMemberPartnerships;
                    }
                }
            }
        }
        private ICollection<MemberPartnership> _memberPartnerships;
    
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
    
        public virtual ICollection<PartnershipGame> PartnershipGames
        {
            get
            {
                if (_partnershipGames == null)
                {
                    var newCollection = new FixupCollection<PartnershipGame>();
                    newCollection.CollectionChanged += FixupPartnershipGames;
                    _partnershipGames = newCollection;
                }
                return _partnershipGames;
            }
            set
            {
                if (!ReferenceEquals(_partnershipGames, value))
                {
                    var previousValue = _partnershipGames as FixupCollection<PartnershipGame>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupPartnershipGames;
                    }
                    _partnershipGames = value;
                    var newValue = value as FixupCollection<PartnershipGame>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupPartnershipGames;
                    }
                }
            }
        }
        private ICollection<PartnershipGame> _partnershipGames;

        #endregion
        #region Association Fixup
    
        private void FixupSeason(Season previousValue)
        {
            if (previousValue != null && previousValue.Partnerships.Contains(this))
            {
                previousValue.Partnerships.Remove(this);
            }
    
            if (Season != null)
            {
                if (!Season.Partnerships.Contains(this))
                {
                    Season.Partnerships.Add(this);
                }
                if (SeasonId != Season.SeasonId)
                {
                    SeasonId = Season.SeasonId;
                }
            }
        }
    
        private void FixupTeam(Team previousValue)
        {
            if (previousValue != null && previousValue.Partnerships.Contains(this))
            {
                previousValue.Partnerships.Remove(this);
            }
    
            if (Team != null)
            {
                if (!Team.Partnerships.Contains(this))
                {
                    Team.Partnerships.Add(this);
                }
                if (TeamId != Team.TeamId)
                {
                    TeamId = Team.TeamId;
                }
            }
        }
    
        private void FixupLotteries(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Lottery item in e.NewItems)
                {
                    item.Partnership = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Lottery item in e.OldItems)
                {
                    if (ReferenceEquals(item.Partnership, this))
                    {
                        item.Partnership = null;
                    }
                }
            }
        }
    
        private void FixupMemberPartnerships(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MemberPartnership item in e.NewItems)
                {
                    item.Partnership = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MemberPartnership item in e.OldItems)
                {
                    if (ReferenceEquals(item.Partnership, this))
                    {
                        item.Partnership = null;
                    }
                }
            }
        }
    
        private void FixupPartnershipGames(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (PartnershipGame item in e.NewItems)
                {
                    item.Partnership = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (PartnershipGame item in e.OldItems)
                {
                    if (ReferenceEquals(item.Partnership, this))
                    {
                        item.Partnership = null;
                    }
                }
            }
        }

        #endregion
    }
}
