using QLRapChieuPhim.Models;
using QLRapChieuPhim.Repository;

namespace QLRapChieuPhim.Model;

public class DangPhimRepository : IDangPhimRepository
{
    private readonly QlrapChieuPhimContext _context;
    public DangPhimRepository(QlrapChieuPhimContext context)
    {
        _context = context;
    }

    public LoaiPhim Add(LoaiPhim loaiphim)
    {
        _context.LoaiPhims.Add(loaiphim);
        _context.SaveChanges();
        return loaiphim;
    }

    public LoaiPhim Delete(string maloaiphim)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<LoaiPhim> GetAllLoaiPhim()
    {
        return _context.LoaiPhims;
    }

    public LoaiPhim GeTLoaiPhim(string maloaiphim)
    {
        return _context.LoaiPhims.Find(maloaiphim);
    }

    public LoaiPhim Update(LoaiPhim loaiphim)
    {
        _context.Update(loaiphim);
        _context.SaveChanges();
        return loaiphim;
    }
}
