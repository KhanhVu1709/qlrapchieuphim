using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QLRapChieuPhim.Models;

public partial class Phim
{
    public string MaPhim { get; set; } = null!;

    public string MaLp { get; set; } = null!;

    public string MaDp { get; set; } = null!;

    public string TenPhim { get; set; } = null!;

    public string Nsx { get; set; } = null!;

    public string? AnhDaiDien { get; set; }

    public string? NoiDung { get; set; }

    public DateTime? NgayKhoiChieu { get; set; }

    public string? MaNuoc { get; set; }

    public virtual ICollection<ChiTietChieuPhim> ChiTietChieuPhims { get; } = new List<ChiTietChieuPhim>();

    public virtual DangPhim? MaDpNavigation { get; set; }

    public virtual LoaiPhim? MaLpNavigation { get; set; }

    public virtual QuocGia? MaNuocNavigation { get; set; }

    [Display(Name = "Front Image")]
    [NotMapped]
    public IFormFile? FrontImage { get; set; }
}
