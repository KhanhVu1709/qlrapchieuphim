using QLRapChieuPhim.Models;

namespace QLRapChieuPhim.Repository
{
    public interface IDangPhimRepository
    {
        LoaiPhim Add(LoaiPhim loaiphim);
        LoaiPhim Update(LoaiPhim loaiphim);
        LoaiPhim Delete(String maloaiphim);
        LoaiPhim GeTLoaiPhim(String maloaiphim);
        IEnumerable<LoaiPhim> GetAllLoaiPhim();
    }
}
