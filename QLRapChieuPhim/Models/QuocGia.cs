using System;
using System.Collections.Generic;

namespace QLRapChieuPhim.Models;

public partial class QuocGia
{
    public string MaNuoc { get; set; } = null!;

    public string TenNuoc { get; set; } = null!;

    public virtual ICollection<DienVien> DienViens { get; } = new List<DienVien>();

    public virtual ICollection<Phim> Phims { get; } = new List<Phim>();
}
