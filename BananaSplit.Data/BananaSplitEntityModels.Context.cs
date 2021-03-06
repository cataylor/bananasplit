//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace BananaSplit.Data
{
    public partial class BananaSplitEntityModels : ObjectContext
    {
        public const string ConnectionString = "name=BananaSplitEntityModels";
        public const string ContainerName = "BananaSplitEntityModels";
    
        #region Constructors
    
        public BananaSplitEntityModels()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public BananaSplitEntityModels(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public BananaSplitEntityModels(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<ApiAccess> ApiAccesses
        {
            get { return _apiAccesses  ?? (_apiAccesses = CreateObjectSet<ApiAccess>("ApiAccesses")); }
        }
        private ObjectSet<ApiAccess> _apiAccesses;
    
        public ObjectSet<Invoice> Invoices
        {
            get { return _invoices  ?? (_invoices = CreateObjectSet<Invoice>("Invoices")); }
        }
        private ObjectSet<Invoice> _invoices;
    
        public ObjectSet<Location> Locations
        {
            get { return _locations  ?? (_locations = CreateObjectSet<Location>("Locations")); }
        }
        private ObjectSet<Location> _locations;
    
        public ObjectSet<Member> Members
        {
            get { return _members  ?? (_members = CreateObjectSet<Member>("Members")); }
        }
        private ObjectSet<Member> _members;
    
        public ObjectSet<MemberType> MemberTypes
        {
            get { return _memberTypes  ?? (_memberTypes = CreateObjectSet<MemberType>("MemberTypes")); }
        }
        private ObjectSet<MemberType> _memberTypes;
    
        public ObjectSet<Partnership> Partnerships
        {
            get { return _partnerships  ?? (_partnerships = CreateObjectSet<Partnership>("Partnerships")); }
        }
        private ObjectSet<Partnership> _partnerships;
    
        public ObjectSet<State> States
        {
            get { return _states  ?? (_states = CreateObjectSet<State>("States")); }
        }
        private ObjectSet<State> _states;
    
        public ObjectSet<Team> Teams
        {
            get { return _teams  ?? (_teams = CreateObjectSet<Team>("Teams")); }
        }
        private ObjectSet<Team> _teams;
    
        public ObjectSet<Season> Seasons
        {
            get { return _seasons  ?? (_seasons = CreateObjectSet<Season>("Seasons")); }
        }
        private ObjectSet<Season> _seasons;
    
        public ObjectSet<TeamType> TeamTypes
        {
            get { return _teamTypes  ?? (_teamTypes = CreateObjectSet<TeamType>("TeamTypes")); }
        }
        private ObjectSet<TeamType> _teamTypes;
    
        public ObjectSet<SeasonYear> SeasonYears
        {
            get { return _seasonYears  ?? (_seasonYears = CreateObjectSet<SeasonYear>("SeasonYears")); }
        }
        private ObjectSet<SeasonYear> _seasonYears;
    
        public ObjectSet<SeasonType> SeasonTypes
        {
            get { return _seasonTypes  ?? (_seasonTypes = CreateObjectSet<SeasonType>("SeasonTypes")); }
        }
        private ObjectSet<SeasonType> _seasonTypes;
    
        public ObjectSet<Game> Games
        {
            get { return _games  ?? (_games = CreateObjectSet<Game>("Games")); }
        }
        private ObjectSet<Game> _games;
    
        public ObjectSet<GamePrice> GamePrices
        {
            get { return _gamePrices  ?? (_gamePrices = CreateObjectSet<GamePrice>("GamePrices")); }
        }
        private ObjectSet<GamePrice> _gamePrices;
    
        public ObjectSet<Lottery> Lotteries
        {
            get { return _lotteries  ?? (_lotteries = CreateObjectSet<Lottery>("Lotteries")); }
        }
        private ObjectSet<Lottery> _lotteries;
    
        public ObjectSet<LotteryOrder> LotteryOrders
        {
            get { return _lotteryOrders  ?? (_lotteryOrders = CreateObjectSet<LotteryOrder>("LotteryOrders")); }
        }
        private ObjectSet<LotteryOrder> _lotteryOrders;
    
        public ObjectSet<MemberPartnership> MemberPartnerships
        {
            get { return _memberPartnerships  ?? (_memberPartnerships = CreateObjectSet<MemberPartnership>("MemberPartnerships")); }
        }
        private ObjectSet<MemberPartnership> _memberPartnerships;
    
        public ObjectSet<PartnershipGame> PartnershipGames
        {
            get { return _partnershipGames  ?? (_partnershipGames = CreateObjectSet<PartnershipGame>("PartnershipGames")); }
        }
        private ObjectSet<PartnershipGame> _partnershipGames;
    
        public ObjectSet<Photo> Photos
        {
            get { return _photos  ?? (_photos = CreateObjectSet<Photo>("Photos")); }
        }
        private ObjectSet<Photo> _photos;
    
        public ObjectSet<PreLotteryOrder> PreLotteryOrders
        {
            get { return _preLotteryOrders  ?? (_preLotteryOrders = CreateObjectSet<PreLotteryOrder>("PreLotteryOrders")); }
        }
        private ObjectSet<PreLotteryOrder> _preLotteryOrders;
    
        public ObjectSet<PriceLevel> PriceLevels
        {
            get { return _priceLevels  ?? (_priceLevels = CreateObjectSet<PriceLevel>("PriceLevels")); }
        }
        private ObjectSet<PriceLevel> _priceLevels;

        #endregion

    }
}
