using System;
using System.Collections.Generic;

namespace QLRapChieuPhim.Models;

public partial class KhachHang
{
    public int MaKh { get; set; }

    public string? Hoten { get; set; }

    public string? DiaChi { get; set; }

    public string? Sdt { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? Username { get; set; }

    public virtual User? UsernameNavigation { get; set; }

    public virtual ICollection<Ve> Ves { get; } = new List<Ve>();
}
