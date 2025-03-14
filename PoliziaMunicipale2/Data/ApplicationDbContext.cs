using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PoliziaMunicipale2.Models;

namespace PoliziaMunicipale2.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anagrafica> Anagraficas { get; set; }

    public virtual DbSet<TipoViolazione> TipoViolaziones { get; set; }

    public virtual DbSet<Verbale> Verbales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-D20BSO4\\SQLEXPRESS;Database=PoliziaMunicipale;User Id=sa;Password=sa;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anagrafica>(entity =>
        {
            entity.HasKey(e => e.Idanagrafica).HasName("PK__ANAGRAFI__7AB1023C3494010D");
        });

        modelBuilder.Entity<TipoViolazione>(entity =>
        {
            entity.HasKey(e => e.Idviolazione).HasName("PK__TIPO_VIO__AF77BD92E295D2A0");
        });

        modelBuilder.Entity<Verbale>(entity =>
        {
            entity.HasKey(e => e.Idverbale).HasName("PK__VERBALE__073D2A450036BD5E");

            entity.HasOne(d => d.IdanagraficaNavigation).WithMany(p => p.Verbales).HasConstraintName("FK__VERBALE__idanagr__4E88ABD4");

            entity.HasOne(d => d.IdviolazioneNavigation).WithMany(p => p.Verbales).HasConstraintName("FK__VERBALE__idviola__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
