using System;
using System.Collections.Generic;

namespace QLRapChieuPhim.Models;

public partial class DienVienPhim
{
    public string MaDv { get; set; } = null!;

    public string MaPhim { get; set; } = null!;

    public virtual DienVien MaDvNavigation { get; set; } = null!;

    public virtual Phim MaPhimNavigation { get; set; } = null!;
}
