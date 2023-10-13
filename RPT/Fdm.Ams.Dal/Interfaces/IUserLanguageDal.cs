using Fdm.Ams.Models;
using Fdm.Ams.Models.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fdm.Ams.Dal.Interfaces
{
    public interface IUserLanguageDal
    {
        Task DeleteAsync(int id);

        Task<IEnumerable<UserLanguage>> GetAllAsync();

        Task<UserLanguage> GetByIdAsync(int id);

        Task<UserLanguage> PostAsync(PostUserLanguageDto postDto);

        Task PutAsync(UserLanguage userLanguage);
    }
}