using Fdm.Ams.Dal.Interfaces;
using Fdm.Ams.Models;
using Fdm.Ams.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fdm.Ams.Services
{
    public class UserLanguageService : IUserLanguageService
    {
        private readonly IUserLanguageDal userLanguageDal;

        public UserLanguageService(IUserLanguageDal userLanguageDal)
        {
            this.userLanguageDal = userLanguageDal;
        }

        public async Task<IEnumerable<UserLanguage>> GetAll()
        {
            return await userLanguageDal.GetAllAsync();
        }

        public async Task<IEnumerable<UserLanguage>> GetAllByConsultantId(int id)
        {
            return (await userLanguageDal.GetAllAsync()).Where(x => x.ConsultantId == id);
        }

        public async Task<UserLanguage> GetUserLanguageForConsultant(int id)
        {
            return (await userLanguageDal.GetAllAsync()).First(x => x.ConsultantId == id);
        }
    }
}