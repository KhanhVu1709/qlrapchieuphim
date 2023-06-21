using QLRapChieuPhim.Models;

namespace QLRapChieuPhim.Reponsitory
{
    public class ViTriReponsitory : IViTriReponsiory
    {
        private readonly QlrapChieuPhimContext _context;

        public ViTriReponsitory(QlrapChieuPhimContext context)
        {
            _context = context;
        }

        public GioChieu Add(GioChieu gioChieu)
        {
            _context.GioChieus.Add(gioChieu);
            _context.SaveChanges();
            return gioChieu;
        }

        public GioChieu Delete(GioChieu gioChieu)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GioChieu> GetAllGioChieu()
        {
            return _context.GioChieus;
        }

        public GioChieu GetGioChieu(GioChieu gioChieu)
        {
            return _context.GioChieus.Find(gioChieu);
        }

        public GioChieu Update(GioChieu gioChieu)
        {
            _context.Update(gioChieu);
            _context.SaveChanges();
            return gioChieu;
        }
    }
}
