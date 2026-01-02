using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IEmailConfigService
    {
        Task<int> AddAsync(EmailConfigModel request);
        Task DeleteAsync(int id);
        Task<List<EmailConfigModel>> GetAsync();
        Task<EmailConfigModel> GetAsync(int id);
        Task<bool> UpdateAsync(EmailConfigModel request);
    }
}