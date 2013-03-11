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
    public partial class GamePrice
    {
        #region Primitive Properties
    
        public virtual long GamePriceId
        {
            get;
            set;
        }
    
        public virtual int GameId
        {
            get { return _gameId; }
            set
            {
                if (_gameId != value)
                {
                    if (Game != null && Game.GameId != value)
                    {
                        Game = null;
                    }
                    _gameId = value;
                }
            }
        }
        private int _gameId;
    
        public virtual long PartnershipId
        {
            get;
            set;
        }
    
        public virtual decimal Price
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties
    
        public virtual Game Game
        {
            get { return _game; }
            set
            {
                if (!ReferenceEquals(_game, value))
                {
                    var previousValue = _game;
                    _game = value;
                    FixupGame(previousValue);
                }
            }
        }
        private Game _game;

        #endregion

        #region Association Fixup
    
        private void FixupGame(Game previousValue)
        {
            if (previousValue != null && previousValue.GamePrices.Contains(this))
            {
                previousValue.GamePrices.Remove(this);
            }
    
            if (Game != null)
            {
                if (!Game.GamePrices.Contains(this))
                {
                    Game.GamePrices.Add(this);
                }
                if (GameId != Game.GameId)
                {
                    GameId = Game.GameId;
                }
            }
        }

        #endregion

    }
}