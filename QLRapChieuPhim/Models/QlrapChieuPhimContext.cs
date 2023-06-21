using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLRapChieuPhim.Models;

public partial class QlrapChieuPhimContext : DbContext
{
    public QlrapChieuPhimContext()
    {
    }

    public QlrapChieuPhimContext(DbContextOptions<QlrapChieuPhimContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnhDaiDien> AnhDaiDiens { get; set; }

    public virtual DbSet<ChiTietChieuPhim> ChiTietChieuPhims { get; set; }

    public virtual DbSet<DangPhim> DangPhims { get; set; }

    public virtual DbSet<DienVien> DienViens { get; set; }

    public virtual DbSet<DienVienPhim> DienVienPhims { get; set; }

    public virtual DbSet<Ghe> Ghes { get; set; }

    public virtual DbSet<GioChieu> GioChieus { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LoaiPhim> LoaiPhims { get; set; }

    public virtual DbSet<LoaiVe> LoaiVes { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Phim> Phims { get; set; }

    public virtual DbSet<PhimGioChieu> PhimGioChieus { get; set; }

    public virtual DbSet<PhongChieu> PhongChieus { get; set; }

    public virtual DbSet<QuocGia> QuocGia { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Ve> Ves { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-MP2EPV8\\SQLEXPRESS;Initial Catalog=QLRapChieuPhim;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnhDaiDien>(entity =>
        {
            entity.HasKey(e => e.TenFileAnh);

            entity.ToTable("AnhDaiDien");

            entity.Property(e => e.TenFileAnh).HasMaxLength(200);
            entity.Property(e => e.MaDv).HasMaxLength(20);
            entity.Property(e => e.ViTri).HasMaxLength(50);

            entity.HasOne(d => d.MaDvNavigation).WithMany(p => p.AnhDaiDiens)
                .HasForeignKey(d => d.MaDv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnhDaiDien_DienVien");
        });

        modelBuilder.Entity<ChiTietChieuPhim>(entity =>
        {
            entity.HasKey(e => e.MaVe);

            entity.ToTable("ChiTietChieuPhim");

            entity.Property(e => e.GioChieu).HasMaxLength(20);
            entity.Property(e => e.MaGhe).HasMaxLength(20);
            entity.Property(e => e.MaLv)
                .HasMaxLength(20)
                .HasColumnName("MaLV");
            entity.Property(e => e.MaPhim).HasMaxLength(20);
            entity.Property(e => e.MaPhong).HasMaxLength(20);

            entity.HasOne(d => d.MaGheNavigation).WithMany(p => p.ChiTietChieuPhims)
                .HasForeignKey(d => d.MaGhe)
                .HasConstraintName("FK_ChiTietChieuPhim_Ghe");

            entity.HasOne(d => d.MaLvNavigation).WithMany(p => p.ChiTietChieuPhims)
                .HasForeignKey(d => d.MaLv)
                .HasConstraintName("FK_ChiTietChieuPhim_LoaiVe");

            entity.HasOne(d => d.MaPhimNavigation).WithMany(p => p.ChiTietChieuPhims)
                .HasForeignKey(d => d.MaPhim)
                .HasConstraintName("FK_ChiTietChieuPhim_Phim");

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.ChiTietChieuPhims)
                .HasForeignKey(d => d.MaPhong)
                .HasConstraintName("FK_ChiTietChieuPhim_PhongChieu");
        });

        modelBuilder.Entity<DangPhim>(entity =>
        {
            entity.HasKey(e => e.MaDp);

            entity.ToTable("DangPhim");

            entity.Property(e => e.MaDp)
                .HasMaxLength(20)
                .HasColumnName("MaDP");
            entity.Property(e => e.DangPhim1)
                .HasMaxLength(50)
                .HasColumnName("DangPhim");
        });

        modelBuilder.Entity<DienVien>(entity =>
        {
            entity.HasKey(e => e.MaDv).HasName("PK__DienVien__272585B74CDD0818");

            entity.ToTable("DienVien");

            entity.Property(e => e.MaDv).HasMaxLength(20);
            entity.Property(e => e.ChieuCao).HasMaxLength(5);
            entity.Property(e => e.MaNuoc).HasMaxLength(20);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.TenDv).HasMaxLength(50);

            entity.HasOne(d => d.MaNuocNavigation).WithMany(p => p.DienViens)
                .HasForeignKey(d => d.MaNuoc)
                .HasConstraintName("FK_DienVien_QuocGia");
        });

        modelBuilder.Entity<DienVienPhim>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DienVien_Phim");

            entity.Property(e => e.MaDv).HasMaxLength(20);
            entity.Property(e => e.MaPhim).HasMaxLength(20);

            entity.HasOne(d => d.MaDvNavigation).WithMany()
                .HasForeignKey(d => d.MaDv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DienVien_P__MaDv__245D67DE");

            entity.HasOne(d => d.MaPhimNavigation).WithMany()
                .HasForeignKey(d => d.MaPhim)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DienVien___MaPhi__25518C17");
        });

        modelBuilder.Entity<Ghe>(entity =>
        {
            entity.HasKey(e => e.MaGhe);

            entity.ToTable("Ghe");

            entity.Property(e => e.MaGhe).HasMaxLength(20);
        });

        modelBuilder.Entity<GioChieu>(entity =>
        {
            entity.HasKey(e => e.MaGioChieu);

            entity.ToTable("GioChieu");

            entity.Property(e => e.GioChieu1)
                .HasMaxLength(20)
                .HasColumnName("GioChieu");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh);

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.Hoten).HasMaxLength(50);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .HasColumnName("SDT");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.KhachHangs)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_KhachHang_Users");
        });

        modelBuilder.Entity<LoaiPhim>(entity =>
        {
            entity.HasKey(e => e.MaLp);

            entity.ToTable("LoaiPhim");

            entity.Property(e => e.MaLp)
                .HasMaxLength(20)
                .HasColumnName("MaLP");
            entity.Property(e => e.LoaiPhim1)
                .HasMaxLength(50)
                .HasColumnName("LoaiPhim");
        });

        modelBuilder.Entity<LoaiVe>(entity =>
        {
            entity.HasKey(e => e.MaLv);

            entity.ToTable("LoaiVe");

            entity.Property(e => e.MaLv)
                .HasMaxLength(20)
                .HasColumnName("MaLV");
            entity.Property(e => e.DonGia).HasColumnType("money");
            entity.Property(e => e.TenLv)
                .HasMaxLength(50)
                .HasColumnName("TenLV");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv);

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(20)
                .HasColumnName("MaNV");
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.Hoten).HasMaxLength(50);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .HasColumnName("SDT");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_NhanVien_Users");
        });

        modelBuilder.Entity<Phim>(entity =>
        {
            entity.HasKey(e => e.MaPhim);

            entity.ToTable("Phim");

            entity.Property(e => e.MaPhim).HasMaxLength(20);
            entity.Property(e => e.MaDp)
                .HasMaxLength(20)
                .HasColumnName("MaDP");
            entity.Property(e => e.MaLp)
                .HasMaxLength(20)
                .HasColumnName("MaLP");
            entity.Property(e => e.MaNuoc).HasMaxLength(20);
            entity.Property(e => e.NgayKhoiChieu).HasColumnType("date");
            entity.Property(e => e.Nsx)
                .HasMaxLength(50)
                .HasColumnName("NSX");

            entity.HasOne(d => d.MaDpNavigation).WithMany(p => p.Phims)
                .HasForeignKey(d => d.MaDp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Phim_DangPhim1");

            entity.HasOne(d => d.MaLpNavigation).WithMany(p => p.Phims)
                .HasForeignKey(d => d.MaLp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Phim_LoaiPhim1");

            entity.HasOne(d => d.MaNuocNavigation).WithMany(p => p.Phims)
                .HasForeignKey(d => d.MaNuoc)
                .HasConstraintName("FK_Phim_QuocGia");
        });

        modelBuilder.Entity<PhimGioChieu>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Phim_GioChieu");

            entity.Property(e => e.MaPhim).HasMaxLength(20);

            entity.HasOne(d => d.MaGioChieuNavigation).WithMany()
                .HasForeignKey(d => d.MaGioChieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Phim_GioC__MaGio__16644E42");

            entity.HasOne(d => d.MaPhimNavigation).WithMany()
                .HasForeignKey(d => d.MaPhim)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Phim_GioC__MaPhi__15702A09");
        });

        modelBuilder.Entity<PhongChieu>(entity =>
        {
            entity.HasKey(e => e.MaPhong);

            entity.ToTable("PhongChieu");

            entity.Property(e => e.MaPhong).HasMaxLength(20);
            entity.Property(e => e.TenPhong).HasMaxLength(50);

            entity.HasMany(d => d.MaGhes).WithMany(p => p.MaPhongs)
                .UsingEntity<Dictionary<string, object>>(
                    "PhongChieuGhe",
                    r => r.HasOne<Ghe>().WithMany()
                        .HasForeignKey("MaGhe")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PhongChie__MaGhe__731B1205"),
                    l => l.HasOne<PhongChieu>().WithMany()
                        .HasForeignKey("MaPhong")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PhongChie__MaPho__7226EDCC"),
                    j =>
                    {
                        j.HasKey("MaPhong", "MaGhe").HasName("PK__PhongChi__8370623CD99A082B");
                        j.ToTable("PhongChieu_Ghe");
                        j.IndexerProperty<string>("MaPhong").HasMaxLength(20);
                        j.IndexerProperty<string>("MaGhe").HasMaxLength(20);
                    });
        });

        modelBuilder.Entity<QuocGia>(entity =>
        {
            entity.HasKey(e => e.MaNuoc).HasName("PK__QuocGia__21306FEA131A1762");

            entity.Property(e => e.MaNuoc).HasMaxLength(20);
            entity.Property(e => e.TenNuoc).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Users__F3DBC573E8202256");

            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasColumnName("username");
            entity.Property(e => e.LoaiUser)
                .HasMaxLength(20)
                .HasColumnName("loaiUser");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Ve>(entity =>
        {
            entity.HasKey(e => e.MaVe);

            entity.ToTable("Ve");

            entity.Property(e => e.MaVe).ValueGeneratedNever();
            entity.Property(e => e.Ghe).HasMaxLength(20);
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.MaLv)
                .HasMaxLength(20)
                .HasColumnName("MaLV");
            entity.Property(e => e.MaNv)
                .HasMaxLength(20)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayBanVe).HasColumnType("datetime");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.Ves)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_Ve_KhachHang");

            entity.HasOne(d => d.MaLvNavigation).WithMany(p => p.Ves)
                .HasForeignKey(d => d.MaLv)
                .HasConstraintName("FK_Ve_LoaiVe");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Ves)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_Ve_NhanVien");

            entity.HasOne(d => d.MaVeNavigation).WithOne(p => p.Ve)
                .HasForeignKey<Ve>(d => d.MaVe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ve_ChiTietChieuPhim");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
