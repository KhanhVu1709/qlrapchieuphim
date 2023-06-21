using System;
using System.Collections.Generic;

namespace QLRapChieuPhim.Models;

public partial class PhimGioChieu
{
    public string MaPhim { get; set; } = null!;

    public int MaGioChieu { get; set; }

    public virtual GioChieu MaGioChieuNavigation { get; set; } = null!;

    public virtual Phim MaPhimNavigation { get; set; } = null!;
}
