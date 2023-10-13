using Fdm.Ams.Models;
using Fdm.Ams.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface IProgrammeDal
    { 
    Task DeleteAsync(int id);

    Task<IEnumerable<Programme>> GetAllAsync();

    Task<Programme> GetByIdAsync(int id);

    Task<Programme> PostAsync(Programme programme);

    Task<Programme> PutAsync(Programme programme);

    }
}
