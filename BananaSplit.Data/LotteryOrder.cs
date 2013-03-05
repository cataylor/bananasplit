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
    public partial class LotteryOrder
    {
        #region Primitive Properties
    
        public virtual long LotteryOrderId
        {
            get;
            set;
        }
    
        public virtual long LotteryId
        {
            get { return _lotteryId; }
            set
            {
                if (_lotteryId != value)
                {
                    if (Lottery != null && Lottery.LotteryId != value)
                    {
                        Lottery = null;
                    }
                    _lotteryId = value;
                }
            }
        }
        private long _lotteryId;
    
        public virtual long MemberId
        {
            get { return _memberId; }
            set
            {
                if (_memberId != value)
                {
                    if (Member != null && Member.MemberId != value)
                    {
                        Member = null;
                    }
                    _memberId = value;
                }
            }
        }
        private long _memberId;
    
        public virtual short OrderOfLottery
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties
    
        public virtual Lottery Lottery
        {
            get { return _lottery; }
            set
            {
                if (!ReferenceEquals(_lottery, value))
                {
                    var previousValue = _lottery;
                    _lottery = value;
                    FixupLottery(previousValue);
                }
            }
        }
        private Lottery _lottery;
    
        public virtual Member Member
        {
            get { return _member; }
            set
            {
                if (!ReferenceEquals(_member, value))
                {
                    var previousValue = _member;
                    _member = value;
                    FixupMember(previousValue);
                }
            }
        }
        private Member _member;

        #endregion

        #region Association Fixup
    
        private void FixupLottery(Lottery previousValue)
        {
            if (previousValue != null && previousValue.LotteryOrders.Contains(this))
            {
                previousValue.LotteryOrders.Remove(this);
            }
    
            if (Lottery != null)
            {
                if (!Lottery.LotteryOrders.Contains(this))
                {
                    Lottery.LotteryOrders.Add(this);
                }
                if (LotteryId != Lottery.LotteryId)
                {
                    LotteryId = Lottery.LotteryId;
                }
            }
        }
    
        private void FixupMember(Member previousValue)
        {
            if (previousValue != null && previousValue.LotteryOrders.Contains(this))
            {
                previousValue.LotteryOrders.Remove(this);
            }
    
            if (Member != null)
            {
                if (!Member.LotteryOrders.Contains(this))
                {
                    Member.LotteryOrders.Add(this);
                }
                if (MemberId != Member.MemberId)
                {
                    MemberId = Member.MemberId;
                }
            }
        }

        #endregion

    }
}
