using System.Collections.Generic;
using System.Linq;
using mccotter_net_api.Models;

namespace mccotter_net_api.DataAccess
{
    public class DataAccessProvider: IDataAccessProvider
    {
        private readonly PostgreSqlContext _context;

        public DataAccessProvider(PostgreSqlContext context)  
        {  
            _context = context;  
        }  

        public void AddDisc(Disc disc)  
        {  
            _context.discs.Add(disc);  
            _context.SaveChanges();  
        }  
  
        public void UpdateDisc(Disc disc)  
        {  
            _context.discs.Update(disc);  
            _context.SaveChanges();  
        }  
  
        public void DeleteDisc(int id)  
        {  
            var entity = _context.discs.FirstOrDefault(t => t.id == id);  
            _context.discs.Remove(entity);  
            _context.SaveChanges();  
        }  
  
        public Disc GetDisc(int id)  
        {  
            return _context.discs.FirstOrDefault(t => t.id == id);  
        }  
  
        public List<Disc> GetDiscs()  
        {  
            return _context.discs.ToList();  
        }
    }
}