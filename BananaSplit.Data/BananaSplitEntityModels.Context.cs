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
    
        public ObjectSet<PartnerTicket> PartnerTickets
        {
            get { return _partnerTickets  ?? (_partnerTickets = CreateObjectSet<PartnerTicket>("PartnerTickets")); }
        }
        private ObjectSet<PartnerTicket> _partnerTickets;
    
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
    
        public ObjectSet<TicketPrice> TicketPrices
        {
            get { return _ticketPrices  ?? (_ticketPrices = CreateObjectSet<TicketPrice>("TicketPrices")); }
        }
        private ObjectSet<TicketPrice> _ticketPrices;
    
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

        #endregion
    }
}
