using QLRapChieuPhim.Models;

namespace QLRapChieuPhim.Repository
{
    public interface IQuocGiaPhimRepository
    {
        QuocGia Add(QuocGia maNuoc);
        QuocGia Update(QuocGia maNuoc);
        QuocGia Delete(QuocGia maNuoc);
        QuocGia GetQuocGium(String maNuoc);
        IEnumerable<QuocGia> GetAllQuocGia();
    }
}
