using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IRoleService
    {
        Task<bool> AddAsync(RoleModel model);
        Task DeleteAsync(int id);
        Task<List<RoleModel>> GetAsync();
        Task<RoleModel> GetByIdAsync(int id);
        Task<bool> RecordExistAsync(RoleModel model);
        Task<bool> UpdateAsync(RoleModel roleModel);
    }
}