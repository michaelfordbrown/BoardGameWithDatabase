using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BoardGame.Models
{
    public partial class BoardGameContext : DbContext
    {
        public virtual DbSet<Tblboardsquare> Tblboardsquare { get; set; }
        public virtual DbSet<Tblboardsquaresv2> Tblboardsquaresv2 { get; set; }
        public virtual DbSet<Tblpersonnel> Tblpersonnel { get; set; }
        public virtual DbSet<Tblplayers> Tblplayers { get; set; }
        //public virtual DbSet<QPlayers> QPlayers { get; set; }
        public virtual DbSet<Tblplayersv2> Tblplayersv2 { get; set; }
        public virtual DbSet<Tbluseractivity> Tbluseractivity { get; set; }
        public virtual DbSet<Tbluserlogin> Tbluserlogin { get; set; }
        public virtual DbSet<Xrefboardsquareplayer> Xrefboardsquareplayer { get; set; }

        public BoardGameContext(DbContextOptions<BoardGameContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tblboardsquare>(entity =>
            {
                entity.ToTable("tblboardsquare");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Boardspaceid)
                    .HasColumnName("boardspaceid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Colposition)
                    .HasColumnName("colposition")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Eastwall)
                    .HasColumnName("eastwall")
                    .HasColumnType("int(1)");

                entity.Property(e => e.Northwall)
                    .HasColumnName("northwall")
                    .HasColumnType("int(1)");

                entity.Property(e => e.Rowposition)
                    .HasColumnName("rowposition")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Southwall)
                    .HasColumnName("southwall")
                    .HasColumnType("int(1)");

                entity.Property(e => e.Westwall)
                    .HasColumnName("westwall")
                    .HasColumnType("int(1)");
            });

            modelBuilder.Entity<Tblboardsquaresv2>(entity =>
            {
                entity.ToTable("tblboardsquaresv2");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Playerid)
                    .HasName("id_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Colposition)
                    .HasColumnName("colposition")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Eastwall)
                    .HasColumnName("eastwall")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Northwall)
                    .HasColumnName("northwall")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Playerid)
                    .HasColumnName("playerid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Rowposition)
                    .HasColumnName("rowposition")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Southwall)
                    .HasColumnName("southwall")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Westwall)
                    .HasColumnName("westwall")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Tblboardsquaresv2)
                    .HasForeignKey(d => d.Playerid)
                    .HasConstraintName("id");
            });

            modelBuilder.Entity<Tblpersonnel>(entity =>
            {
                entity.ToTable("tblpersonnel");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(45);

                entity.Property(e => e.PayRate).HasColumnType("decimal(10,2)");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Tblplayers>(entity =>
            {
                entity.ToTable("tblplayers");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Colposition)
                    .HasColumnName("colposition")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Facingdirection)
                    .HasColumnName("facingdirection")
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45);

                entity.Property(e => e.Rowposition)
                    .HasColumnName("rowposition")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Tblplayersv2>(entity =>
            {
                entity.ToTable("tblplayersv2");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Playername)
                    .HasName("playername_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Facingdirection)
                    .IsRequired()
                    .HasColumnName("facingdirection")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NORTH'");

                entity.Property(e => e.Playername)
                    .IsRequired()
                    .HasColumnName("playername")
                    .HasMaxLength(45);
            });

/*            modelBuilder.Entity<QPlayers>(entity =>
            {
                entity.ToTable("tblplayersv2");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Playername)
                    .HasName("playername_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Facingdirection)
                    .IsRequired()
                    .HasColumnName("facingdirection")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NORTH'");

                entity.Property(e => e.Playername)
                    .IsRequired()
                    .HasColumnName("playername")
                    .HasMaxLength(45);
            });

*/

            modelBuilder.Entity<Tbluseractivity>(entity =>
            {
                entity.ToTable("tbluseractivity");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateOfActivity).HasColumnType("datetime");

                entity.Property(e => e.FormAccessed).HasMaxLength(100);

                entity.Property(e => e.UserIp)
                    .HasColumnName("UserIP")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Tbluserlogin>(entity =>
            {
                entity.ToTable("tbluserlogin");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SecurityLevel).HasColumnType("char(1)");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.UserPassword).HasMaxLength(50);
            });

            modelBuilder.Entity<Xrefboardsquareplayer>(entity =>
            {
                entity.ToTable("xrefboardsquareplayer");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Boardsquareid)
                    .HasColumnName("boardsquareid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Playerid)
                    .HasColumnName("playerid")
                    .HasColumnType("int(11)");
            });
        }
    }
}
