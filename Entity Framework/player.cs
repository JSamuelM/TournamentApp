//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TournamentApp.Entity_Framework
{
    using System;
    using System.Collections.Generic;
    
    public partial class player
    {
        public int id { get; set; }
        public string player_name { get; set; }
        public string playeR_surname { get; set; }
        public sbyte player_age { get; set; }
        public int team_id { get; set; }
    
        public virtual team team { get; set; }
    }
}
