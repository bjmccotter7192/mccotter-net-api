using System.Collections.Generic;
using mccotter_net_api.Models;

namespace mccotter_net_api.DataAccess
{
    public interface IDataAccessProvider
    {
        void AddDisc(Disc disc);  
        void UpdateDisc(Disc disc);  
        void DeleteDisc(int id);  
        Disc GetDisc(int id);  
        List<Disc> GetDiscs();
    }
}