using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyApi2.Models;

public partial class GalDBContext : DbContext
{
    public GalDBContext(DbContextOptions<GalDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account_info> Account_info { get; set; }

    public virtual DbSet<Account_per> Account_per { get; set; }

    public virtual DbSet<Company> Company { get; set; }

    public virtual DbSet<Company_Pic> Company_Pic { get; set; }

    public virtual DbSet<Company_Website> Company_Website { get; set; }

    public virtual DbSet<Company_relation> Company_relation { get; set; }

    public virtual DbSet<Company_relation_info> Company_relation_info { get; set; }

    public virtual DbSet<Company_type> Company_type { get; set; }

    public virtual DbSet<Currency> Currency { get; set; }

    public virtual DbSet<Device> Device { get; set; }

    public virtual DbSet<Export_set_Company> Export_set_Company { get; set; }

    public virtual DbSet<Export_set_Other> Export_set_Other { get; set; }

    public virtual DbSet<Export_set_Other_Product> Export_set_Other_Product { get; set; }

    public virtual DbSet<Export_set_Other_series> Export_set_Other_series { get; set; }

    public virtual DbSet<Export_set_Product> Export_set_Product { get; set; }

    public virtual DbSet<Export_set_Product_series> Export_set_Product_series { get; set; }

    public virtual DbSet<Export_set_batch> Export_set_batch { get; set; }

    public virtual DbSet<Export_set_date> Export_set_date { get; set; }

    public virtual DbSet<Export_type> Export_type { get; set; }

    public virtual DbSet<Permission_set> Permission_set { get; set; }

    public virtual DbSet<Product> Product { get; set; }

    public virtual DbSet<Product_Pic> Product_Pic { get; set; }

    public virtual DbSet<Product_Release_day> Product_Release_day { get; set; }

    public virtual DbSet<Product_Website> Product_Website { get; set; }

    public virtual DbSet<Product_relation> Product_relation { get; set; }

    public virtual DbSet<Product_relation_info> Product_relation_info { get; set; }

    public virtual DbSet<Product_score> Product_score { get; set; }

    public virtual DbSet<Product_score_type> Product_score_type { get; set; }

    public virtual DbSet<Product_type> Product_type { get; set; }

    public virtual DbSet<Product_type_class> Product_type_class { get; set; }

    public virtual DbSet<Product_type_info> Product_type_info { get; set; }

    public virtual DbSet<Rating> Rating { get; set; }

    public virtual DbSet<Rating_type> Rating_type { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Staff_info> Staff_info { get; set; }

    public virtual DbSet<Staff_type> Staff_type { get; set; }

    public virtual DbSet<Translation_team> Translation_team { get; set; }

    public virtual DbSet<Translation_team_batch> Translation_team_batch { get; set; }

    public virtual DbSet<Translation_team_info> Translation_team_info { get; set; }

    public virtual DbSet<Translation_team_type> Translation_team_type { get; set; }

    public virtual DbSet<Voice_type> Voice_type { get; set; }

    public virtual DbSet<Website_Type> Website_Type { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account_info>(entity =>
        {
            entity.HasKey(e => e.Account_id);

            entity.Property(e => e.Account_id).HasMaxLength(32);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(100);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Account_per>(entity =>
        {
            entity.HasIndex(e => e.Account_id, "UK_Account_per_Account_id").IsUnique();

            entity.Property(e => e.Account_id).HasMaxLength(32);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(32);
            entity.Property(e => e.Password_encrypt).HasMaxLength(64);
            entity.Property(e => e.Remark).HasMaxLength(100);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.Account).WithOne(p => p.Account_per)
                .HasForeignKey<Account_per>(d => d.Account_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_per_Account_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.Account_per)
                .HasForeignKey(d => d.Permission_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_per_Permission_id");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasIndex(e => e.C_id, "UK_Company_C_id").IsUnique();

            entity.Property(e => e.C_id).HasMaxLength(10);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Edate).HasMaxLength(8);
            entity.Property(e => e.Intro).HasMaxLength(4000);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Name_origin).HasMaxLength(200);
            entity.Property(e => e.Name_short).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(200);
            entity.Property(e => e.Sdate).HasMaxLength(8);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.C_typeNavigation).WithMany(p => p.Company)
                .HasForeignKey(d => d.C_type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_C_type");
        });

        modelBuilder.Entity<Company_Pic>(entity =>
        {
            entity.Property(e => e.C_id).HasMaxLength(10);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.Type_id).HasMaxLength(3);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
            entity.Property(e => e.Url).HasMaxLength(4000);

            entity.HasOne(d => d.C_idNavigation).WithMany(p => p.Company_Pic)
                .HasPrincipalKey(p => p.C_id)
                .HasForeignKey(d => d.C_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_Pic_C_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Company_Pic)
                .HasForeignKey(d => d.Type_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_Pic_Type_id");
        });

        modelBuilder.Entity<Company_Website>(entity =>
        {
            entity.Property(e => e.C_id).HasMaxLength(10);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.Type_id).HasMaxLength(3);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
            entity.Property(e => e.Url).HasMaxLength(4000);

            entity.HasOne(d => d.C_idNavigation).WithMany(p => p.Company_Website)
                .HasPrincipalKey(p => p.C_id)
                .HasForeignKey(d => d.C_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_Website_C_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Company_Website)
                .HasForeignKey(d => d.Type_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_Website_Type_id");
        });

        modelBuilder.Entity<Company_relation>(entity =>
        {
            entity.Property(e => e.C_id).HasMaxLength(10);
            entity.Property(e => e.C_id_to).HasMaxLength(10);
            entity.Property(e => e.Content).HasMaxLength(100);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.C_idNavigation).WithMany(p => p.Company_relationC_idNavigation)
                .HasPrincipalKey(p => p.C_id)
                .HasForeignKey(d => d.C_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_relation_C_id");

            entity.HasOne(d => d.C_id_toNavigation).WithMany(p => p.Company_relationC_id_toNavigation)
                .HasPrincipalKey(p => p.C_id)
                .HasForeignKey(d => d.C_id_to)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_relation_C_id_to");

            entity.HasOne(d => d.Relation).WithMany(p => p.Company_relation)
                .HasForeignKey(d => d.Relation_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_relation_Relation_id");
        });

        modelBuilder.Entity<Company_relation_info>(entity =>
        {
            entity.HasKey(e => e.Relation_id);

            entity.Property(e => e.Content).HasMaxLength(100);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Company_type>(entity =>
        {
            entity.HasKey(e => e.C_type);

            entity.Property(e => e.C_type_name).HasMaxLength(30);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Remark).HasMaxLength(200);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Currency_id);

            entity.Property(e => e.Currency_id).HasMaxLength(3);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.ShortName).HasMaxLength(20);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Device_id);

            entity.Property(e => e.Device_id).HasMaxLength(5);
            entity.Property(e => e.Content).HasMaxLength(200);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.ShortName).HasMaxLength(20);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Export_set_Company>(entity =>
        {
            entity.Property(e => e.C_id).HasMaxLength(10);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.C_idNavigation).WithMany(p => p.Export_set_Company)
                .HasPrincipalKey(p => p.C_id)
                .HasForeignKey(d => d.C_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Export_set_Company_C_id");

            entity.HasOne(d => d.Export_batchNavigation).WithMany(p => p.Export_set_Company)
                .HasForeignKey(d => d.Export_batch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Export_set_Company_Export_batch");
        });

        modelBuilder.Entity<Export_set_Other>(entity =>
        {
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.Export_batchNavigation).WithMany(p => p.Export_set_Other)
                .HasForeignKey(d => d.Export_batch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Export_set_Other_Export_batch");
        });

        modelBuilder.Entity<Export_set_Other_Product>(entity =>
        {
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.P_id).HasMaxLength(10);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.ESOS).WithMany(p => p.Export_set_Other_Product)
                .HasForeignKey(d => d.ESOS_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Export_set_Other_Product_ESOS_id");

            entity.HasOne(d => d.P_idNavigation).WithMany(p => p.Export_set_Other_Product)
                .HasPrincipalKey(p => p.P_id)
                .HasForeignKey(d => d.P_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Export_set_Other_Product_P_id");
        });

        modelBuilder.Entity<Export_set_Other_series>(entity =>
        {
            entity.Property(e => e.Add_word).HasMaxLength(4000);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.ESO).WithMany(p => p.Export_set_Other_series)
                .HasForeignKey(d => d.ESO_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Export_set_Other_series_ESO_id");
        });

        modelBuilder.Entity<Export_set_Product>(entity =>
        {
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.P_id).HasMaxLength(10);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.ESPS).WithMany(p => p.Export_set_Product)
                .HasForeignKey(d => d.ESPS_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Export_set_Product_ESPS_id");

            entity.HasOne(d => d.P_idNavigation).WithMany(p => p.Export_set_Product)
                .HasPrincipalKey(p => p.P_id)
                .HasForeignKey(d => d.P_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Export_set_Product_P_id");
        });

        modelBuilder.Entity<Export_set_Product_series>(entity =>
        {
            entity.Property(e => e.Add_word).HasMaxLength(4000);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.ESC).WithMany(p => p.Export_set_Product_series)
                .HasForeignKey(d => d.ESC_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Export_set_Product_series_ESC_id");
        });

        modelBuilder.Entity<Export_set_batch>(entity =>
        {
            entity.HasKey(e => e.Export_batch);

            entity.Property(e => e.Export_batch).ValueGeneratedNever();
            entity.Property(e => e.Content).HasMaxLength(100);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Export_set_date>(entity =>
        {
            entity.Property(e => e.Date_mark).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Export_type>(entity =>
        {
            entity.Property(e => e.Content).HasMaxLength(300);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Permission_set>(entity =>
        {
            entity.HasKey(e => e.Permission_id);

            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(32);
            entity.Property(e => e.Remark).HasMaxLength(100);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.P_id, "UK_Product_P_id").IsUnique();

            entity.Property(e => e.C_Name).HasMaxLength(200);
            entity.Property(e => e.C_id).HasMaxLength(10);
            entity.Property(e => e.Content).HasMaxLength(300);
            entity.Property(e => e.Content_style).HasMaxLength(100);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.P_id).HasMaxLength(10);
            entity.Property(e => e.Play_time).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(200);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.C_idNavigation).WithMany(p => p.Product)
                .HasPrincipalKey(p => p.C_id)
                .HasForeignKey(d => d.C_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_C_id");
        });

        modelBuilder.Entity<Product_Pic>(entity =>
        {
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.P_id).HasMaxLength(10);
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.Type_id).HasMaxLength(3);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
            entity.Property(e => e.Url).HasMaxLength(4000);

            entity.HasOne(d => d.P_idNavigation).WithMany(p => p.Product_Pic)
                .HasPrincipalKey(p => p.P_id)
                .HasForeignKey(d => d.P_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Pic_P_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Product_Pic)
                .HasForeignKey(d => d.Type_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Pic_Type_id");
        });

        modelBuilder.Entity<Product_Release_day>(entity =>
        {
            entity.Property(e => e.Content).HasMaxLength(200);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Currency_id).HasMaxLength(3);
            entity.Property(e => e.Device_id).HasMaxLength(5);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.P_id).HasMaxLength(10);
            entity.Property(e => e.Presale_Date).HasMaxLength(8);
            entity.Property(e => e.Price).HasColumnType("smallmoney");
            entity.Property(e => e.Sale_Date).HasMaxLength(8);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.Currency).WithMany(p => p.Product_Release_day)
                .HasForeignKey(d => d.Currency_id)
                .HasConstraintName("FK_Product_Release_day_Currency_id");

            entity.HasOne(d => d.Device).WithMany(p => p.Product_Release_day)
                .HasForeignKey(d => d.Device_id)
                .HasConstraintName("FK_Product_Release_day_Device_id");

            entity.HasOne(d => d.P_idNavigation).WithMany(p => p.Product_Release_day)
                .HasPrincipalKey(p => p.P_id)
                .HasForeignKey(d => d.P_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Release_day_P_id");

            entity.HasOne(d => d.Rating).WithMany(p => p.Product_Release_day)
                .HasForeignKey(d => d.Rating_id)
                .HasConstraintName("FK_Product_Release_day_Rating_id");

            entity.HasOne(d => d.Voice).WithMany(p => p.Product_Release_day)
                .HasForeignKey(d => d.Voice_id)
                .HasConstraintName("FK_Product_Release_day_Voice_id");
        });

        modelBuilder.Entity<Product_Website>(entity =>
        {
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.P_id).HasMaxLength(10);
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.Type_id).HasMaxLength(3);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
            entity.Property(e => e.Url).HasMaxLength(4000);

            entity.HasOne(d => d.P_idNavigation).WithMany(p => p.Product_Website)
                .HasPrincipalKey(p => p.P_id)
                .HasForeignKey(d => d.P_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Website_P_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Product_Website)
                .HasForeignKey(d => d.Type_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Website_Type_id");
        });

        modelBuilder.Entity<Product_relation>(entity =>
        {
            entity.Property(e => e.Content).HasMaxLength(100);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.P_id).HasMaxLength(10);
            entity.Property(e => e.P_id_to).HasMaxLength(10);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.P_idNavigation).WithMany(p => p.Product_relationP_idNavigation)
                .HasPrincipalKey(p => p.P_id)
                .HasForeignKey(d => d.P_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_relation_P_id");

            entity.HasOne(d => d.P_id_toNavigation).WithMany(p => p.Product_relationP_id_toNavigation)
                .HasPrincipalKey(p => p.P_id)
                .HasForeignKey(d => d.P_id_to)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_relation_P_id_to");

            entity.HasOne(d => d.Relation).WithMany(p => p.Product_relation)
                .HasForeignKey(d => d.Relation_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_relation_Relation_id");
        });

        modelBuilder.Entity<Product_relation_info>(entity =>
        {
            entity.HasKey(e => e.Relation_id);

            entity.Property(e => e.Content).HasMaxLength(100);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Product_score>(entity =>
        {
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.P_id).HasMaxLength(10);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.P_idNavigation).WithMany(p => p.Product_score)
                .HasPrincipalKey(p => p.P_id)
                .HasForeignKey(d => d.P_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_score_P_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Product_score)
                .HasForeignKey(d => d.Type_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_score_Type_id");
        });

        modelBuilder.Entity<Product_score_type>(entity =>
        {
            entity.HasKey(e => e.Type_id);

            entity.Property(e => e.Content).HasMaxLength(100);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Product_type>(entity =>
        {
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.P_id).HasMaxLength(10);
            entity.Property(e => e.P_type_id).HasMaxLength(5);
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.P_idNavigation).WithMany(p => p.Product_type)
                .HasPrincipalKey(p => p.P_id)
                .HasForeignKey(d => d.P_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_type_P_id");

            entity.HasOne(d => d.P_type).WithMany(p => p.Product_type)
                .HasForeignKey(d => d.P_type_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_type_P_type_id");
        });

        modelBuilder.Entity<Product_type_class>(entity =>
        {
            entity.HasKey(e => e.P_type_class);

            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Product_type_info>(entity =>
        {
            entity.HasKey(e => e.P_type_id);

            entity.Property(e => e.P_type_id).HasMaxLength(5);
            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.FullName_EN).HasMaxLength(100);
            entity.Property(e => e.FullName_JP).HasMaxLength(100);
            entity.Property(e => e.ShortName).HasMaxLength(20);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.P_type_classNavigation).WithMany(p => p.Product_type_info)
                .HasForeignKey(d => d.P_type_class)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_type_info_P_type_class");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Rating_id);

            entity.Property(e => e.Content).HasMaxLength(100);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Name_EN).HasMaxLength(50);
            entity.Property(e => e.Name_JP).HasMaxLength(50);
            entity.Property(e => e.ShortName).HasMaxLength(20);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.Rating_typeNavigation).WithMany(p => p.Rating)
                .HasForeignKey(d => d.Rating_type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rating_Rating_type");
        });

        modelBuilder.Entity<Rating_type>(entity =>
        {
            entity.HasKey(e => e.Rating_type1);

            entity.Property(e => e.Rating_type1).HasColumnName("Rating_type");
            entity.Property(e => e.Content).HasMaxLength(100);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ShortName).HasMaxLength(20);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.P_id).HasMaxLength(10);
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.Staff_id).HasMaxLength(10);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.P_idNavigation).WithMany(p => p.Staff)
                .HasPrincipalKey(p => p.P_id)
                .HasForeignKey(d => d.P_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_P_id");

            entity.HasOne(d => d.StaffNavigation).WithMany(p => p.Staff)
                .HasPrincipalKey(p => p.Staff_id)
                .HasForeignKey(d => d.Staff_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Staff_id");

            entity.HasOne(d => d.Staff_type).WithMany(p => p.Staff)
                .HasForeignKey(d => d.Staff_typeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Staff_typeid");
        });

        modelBuilder.Entity<Staff_info>(entity =>
        {
            entity.HasIndex(e => e.Staff_id, "UK_Staff_info_Staff_id").IsUnique();

            entity.Property(e => e.Content).HasMaxLength(4000);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Staff_id).HasMaxLength(10);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Staff_type>(entity =>
        {
            entity.Property(e => e.Content).HasMaxLength(100);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Translation_team>(entity =>
        {
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.P_id).HasMaxLength(10);
            entity.Property(e => e.Remark).HasMaxLength(200);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.P_idNavigation).WithMany(p => p.Translation_team)
                .HasPrincipalKey(p => p.P_id)
                .HasForeignKey(d => d.P_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Translation_team_P_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Translation_team)
                .HasForeignKey(d => d.Type_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Translation_team_Type_id");
        });

        modelBuilder.Entity<Translation_team_batch>(entity =>
        {
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.P_id).HasMaxLength(10);
            entity.Property(e => e.T_id).HasMaxLength(6);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);

            entity.HasOne(d => d.TT).WithMany(p => p.Translation_team_batch)
                .HasForeignKey(d => d.TT_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Translation_team_batch_TT_id");

            entity.HasOne(d => d.T_idNavigation).WithMany(p => p.Translation_team_batch)
                .HasPrincipalKey(p => p.T_id)
                .HasForeignKey(d => d.T_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Translation_team_batch_T_id");
        });

        modelBuilder.Entity<Translation_team_info>(entity =>
        {
            entity.HasIndex(e => e.T_id, "UK_Translation_team_info_T_id").IsUnique();

            entity.Property(e => e.Content).HasMaxLength(200);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.T_id).HasMaxLength(6);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Translation_team_type>(entity =>
        {
            entity.HasKey(e => e.Type_id);

            entity.Property(e => e.Content).HasMaxLength(200);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Voice_type>(entity =>
        {
            entity.HasKey(e => e.Voice_id);

            entity.Property(e => e.Content).HasMaxLength(100);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        modelBuilder.Entity<Website_Type>(entity =>
        {
            entity.HasKey(e => e.Type_id);

            entity.Property(e => e.Type_id).HasMaxLength(3);
            entity.Property(e => e.Create_dt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.Upd_date).HasColumnType("datetime");
            entity.Property(e => e.Upd_user).HasMaxLength(32);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
