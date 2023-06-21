using System;
using System.Collections.Generic;

namespace QLRapChieuPhim.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? LoaiUser { get; set; }

    public virtual ICollection<KhachHang> KhachHangs { get; } = new List<KhachHang>();

    public virtual ICollection<NhanVien> NhanViens { get; } = new List<NhanVien>();
}
