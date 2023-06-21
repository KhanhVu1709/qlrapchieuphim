using System;
using System.Collections.Generic;

namespace QLRapChieuPhim.Models;

public partial class Ghe
{
    public string MaGhe { get; set; } = null!;

    public virtual ICollection<ChiTietChieuPhim> ChiTietChieuPhims { get; } = new List<ChiTietChieuPhim>();

    public virtual ICollection<PhongChieu> MaPhongs { get; } = new List<PhongChieu>();
}
