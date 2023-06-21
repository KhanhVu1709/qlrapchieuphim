using System;
using System.Collections.Generic;

namespace QLRapChieuPhim.Models;

public partial class ChiTietChieuPhim
{
    public int MaVe { get; set; }

    public string? MaPhong { get; set; }

    public string? MaPhim { get; set; }

    public string? GioChieu { get; set; }

    public string? MaLv { get; set; }

    public string? MaGhe { get; set; }

    public virtual Ghe? MaGheNavigation { get; set; }

    public virtual LoaiVe? MaLvNavigation { get; set; }

    public virtual Phim? MaPhimNavigation { get; set; }

    public virtual PhongChieu? MaPhongNavigation { get; set; }

    public virtual Ve? Ve { get; set; }
}
