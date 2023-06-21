namespace QLRapChieuPhim.Models.ApiModel
{
    public class PhimCanTim
    {
        public string MaPhim { get; set; } = null!;

        public string TenPhim { get; set; } = null!;

        public string MaLp { get; set; } = null!;

        public string MaDp { get; set; } = null!;

        public string? AnhDaiDien { get; set; }
    }
}
