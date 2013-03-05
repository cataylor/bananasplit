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
    public partial class Location
    {
        #region Primitive Properties
    
        public virtual int LocationId
        {
            get;
            set;
        }
    
        public virtual string City
        {
            get;
            set;
        }
    
        public virtual int StateId
        {
            get { return _stateId; }
            set
            {
                if (_stateId != value)
                {
                    if (State != null && State.StateId != value)
                    {
                        State = null;
                    }
                    _stateId = value;
                }
            }
        }
        private int _stateId;

        #endregion

        #region Navigation Properties
    
        public virtual State State
        {
            get { return _state; }
            set
            {
                if (!ReferenceEquals(_state, value))
                {
                    var previousValue = _state;
                    _state = value;
                    FixupState(previousValue);
                }
            }
        }
        private State _state;
    
        public virtual ICollection<Team> Teams
        {
            get
            {
                if (_teams == null)
                {
                    var newCollection = new FixupCollection<Team>();
                    newCollection.CollectionChanged += FixupTeams;
                    _teams = newCollection;
                }
                return _teams;
            }
            set
            {
                if (!ReferenceEquals(_teams, value))
                {
                    var previousValue = _teams as FixupCollection<Team>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTeams;
                    }
                    _teams = value;
                    var newValue = value as FixupCollection<Team>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTeams;
                    }
                }
            }
        }
        private ICollection<Team> _teams;

        #endregion

        #region Association Fixup
    
        private void FixupState(State previousValue)
        {
            if (previousValue != null && previousValue.Locations.Contains(this))
            {
                previousValue.Locations.Remove(this);
            }
    
            if (State != null)
            {
                if (!State.Locations.Contains(this))
                {
                    State.Locations.Add(this);
                }
                if (StateId != State.StateId)
                {
                    StateId = State.StateId;
                }
            }
        }
    
        private void FixupTeams(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Team item in e.NewItems)
                {
                    item.Location = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Team item in e.OldItems)
                {
                    if (ReferenceEquals(item.Location, this))
                    {
                        item.Location = null;
                    }
                }
            }
        }

        #endregion

    }
}
