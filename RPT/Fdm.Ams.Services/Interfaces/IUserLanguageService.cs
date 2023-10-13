using Fdm.Ams.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Services.Interfaces
{
    public interface IUserLanguageService
    {
        public Task<IEnumerable<UserLanguage>> GetAll();

        public Task<IEnumerable<UserLanguage>> GetAllByConsultantId(int id);

        public Task<UserLanguage> GetUserLanguageForConsultant(int id);
    }
}