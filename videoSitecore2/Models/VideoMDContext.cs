using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using videoSitecore2.Models;

namespace videoSitecore2.Models
{
    public partial class VideoMDContext : DbContext
    {

        public VideoMDContext()
        {
        }

        public VideoMDContext(DbContextOptions<VideoMDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Imported2> Imported2 { get; set; }
        public virtual DbSet<Imported4> Imported4 { get; set; }
        public virtual DbSet<MetaData> MetaData { get; set; }
        public virtual DbSet<MetaDataTitles> MetaDataTitles { get; set; }
        public virtual DbSet<MetaDataTypes> MetaDataTypes { get; set; }
        public virtual DbSet<Videos> Videos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //zsm bayad remove she 


            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=172.17.176.211\\RTCLOCAL;Database=VideoMD;User ID=VideoTag;Password=Zaq1;");
            } 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Imported2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("imported_2");

                entity.Property(e => e.Address).HasMaxLength(1000);

                entity.Property(e => e.Bbox).HasMaxLength(255);

                entity.Property(e => e.FileName)
                    .HasColumnName("File_name")
                    .HasMaxLength(1000);

                entity.Property(e => e.Label).HasMaxLength(255);

                entity.Property(e => e.Percentage).HasMaxLength(255);

                entity.Property(e => e.Position).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<Imported4>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("imported_4");

                entity.Property(e => e.Address).HasMaxLength(1000);

                entity.Property(e => e.Bbox).HasMaxLength(255);

                entity.Property(e => e.FileName)
                    .HasColumnName("File_name")
                    .HasMaxLength(1000);

                entity.Property(e => e.Label).HasMaxLength(255);

                entity.Property(e => e.Percentage).HasMaxLength(255);

                entity.Property(e => e.Position).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<MetaData>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MetadataTitleId).HasColumnName("MetadataTitleID");

                entity.Property(e => e.VideoId).HasColumnName("VideoID");

                entity.HasOne(d => d.MetadataTitle)
                    .WithMany(p => p.MetaData)
                    .HasForeignKey(d => d.MetadataTitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MetaData_MetaDataTitles");

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.MetaData)
                    .HasForeignKey(d => d.VideoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MetaData_Videos");
            });

            modelBuilder.Entity<MetaDataTitles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(1000);

                entity.Property(e => e.Entitle)
                    .HasColumnName("ENtitle")
                    .HasMaxLength(1000);

                entity.Property(e => e.MetaDataTypeId).HasColumnName("MetaDataTypeID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(1000);

                entity.HasOne(d => d.MetaDataType)
                    .WithMany(p => p.MetaDataTitles)
                    .HasForeignKey(d => d.MetaDataTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MetaDataTitles_MetaDataTypes");
            });

            modelBuilder.Entity<MetaDataTypes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Videos>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<videoSitecore2.Models.metaVsvideo> metaVsvideo { get; set; }

        //zsm dropdown list
        //pubic Dbset<videoSitecore2.Models.Metadatatype> metadatatype { get; set; }
    }
}
