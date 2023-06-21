using System;
using System.Collections.Generic;

namespace QLRapChieuPhim.Models;

public partial class Ve
{
    public int MaVe { get; set; }

    public string? MaNv { get; set; }

    public int? MaKh { get; set; }

    public string? Ghe { get; set; }

    public DateTime? NgayBanVe { get; set; }

    public string? MaLv { get; set; }

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual LoaiVe? MaLvNavigation { get; set; }

    public virtual NhanVien? MaNvNavigation { get; set; }

    public virtual ChiTietChieuPhim? MaVeNavigation { get; set; }
}
