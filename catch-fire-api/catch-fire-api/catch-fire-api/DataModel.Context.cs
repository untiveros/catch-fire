﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace catch_fire_api
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CatchFireEntities : DbContext
    {
        public CatchFireEntities()
            : base("name=CatchFireEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Trait> Traits { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<GoalArea> GoalAreas { get; set; }
    }
}
