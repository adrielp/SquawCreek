//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SqualCreekBusinessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventRequest
    {
        public System.Guid Id { get; set; }
        public string RequestedTime { get; set; }
        public Nullable<bool> Approved { get; set; }
        public string EventDescription { get; set; }
        public Nullable<int> NumberOfGuests { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public Nullable<decimal> Budget { get; set; }
        public Nullable<System.Guid> ApprovedEventId { get; set; }
    }
}