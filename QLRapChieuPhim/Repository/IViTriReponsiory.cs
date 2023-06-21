using QLRapChieuPhim.Models;
namespace QLRapChieuPhim.Reponsitory
{
	public interface IViTriReponsiory
	{
        GioChieu Add(GioChieu gioChieu);
        GioChieu Update(GioChieu gioChieu);
        GioChieu Delete(GioChieu gioChieu);
        GioChieu GetGioChieu(GioChieu gioChieu);
        IEnumerable<GioChieu> GetAllGioChieu();
    }
}
