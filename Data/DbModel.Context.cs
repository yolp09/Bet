﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NTCBetBDContext : DbContext
    {
        public NTCBetBDContext()
            : base("name=NTCBetBDContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bet> Bets { get; set; }
        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<Logo> Logos { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
