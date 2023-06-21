using System;
using System.Collections.Generic;

namespace QLRapChieuPhim.Models;

public partial class LoaiVe
{
    public string MaLv { get; set; } = null!;

    public string TenLv { get; set; } = null!;

    public decimal DonGia { get; set; }

    public virtual ICollection<ChiTietChieuPhim> ChiTietChieuPhims { get; } = new List<ChiTietChieuPhim>();

    public virtual ICollection<Ve> Ves { get; } = new List<Ve>();
}
