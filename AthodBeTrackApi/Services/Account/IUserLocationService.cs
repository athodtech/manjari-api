using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IUserLocationService
    {
        Task<int> AddAsync(UserLocationModel model);
        Task DeleteAsync(int id);
        Task<List<UserLocationModel>> GetAsync(int userId);
        Task<UserLocationModel> GetByIdAsync(int id);
        Task<bool> UpdateAsync(UserLocationModel request);
        Task<bool> LocationExist(int userId);
    }
}