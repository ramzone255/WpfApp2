﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp2
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PR1_chessEntities : DbContext
    {
        private static PR1_chessEntities _context;
        public PR1_chessEntities()
            : base("name=PR1_chessEntities")
        {
        }

        public static PR1_chessEntities GetContext() //метод получения контекста
        {
            if (_context == null)

                _context = new PR1_chessEntities();

            return _context;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ivent> Ivent { get; set; }
        public virtual DbSet<Sportsman> Sportsman { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}