namespace QLRapChieuPhim.Models.LienKetModels
{
    public class PhimVaGioChieuModel
    {
        public string? MaPhim { get; set; }

        public string? TenPhim { get; set; }

        public string? AnhDaiDien { get; set; }

        public string? SuatChieu { get; set; }

        public List<LoaiVe> lv = new List<LoaiVe>();

        public List<Ghe> ghe = new List<Ghe>();
        
    }
}
