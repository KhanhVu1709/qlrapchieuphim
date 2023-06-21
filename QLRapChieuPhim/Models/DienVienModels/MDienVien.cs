namespace QLRapChieuPhim.Models.DienVienModels
{
    public class MDienVien
    {
        public string MaDv { get; set; } = null!;

        public string TenDv { get; set; } = null!;

        public string MoTa { get; set; } = null!;

        public DateTime NgaySinh { get; set; }

        public string ChieuCao { get; set; } = null!;

        public string? AnhDaiDien { get; set; }

        public string? MaNuoc { get; set; }
    }
}
