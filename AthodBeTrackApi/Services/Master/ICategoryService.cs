using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface ICategoryService
    {
        Task<bool> AddAsync(CategoryModel model);
        Task DeleteAsync(int id);
        Task<List<CategoryModel>> GetAsync();
        Task<CategoryModel> GetByIdAsync(int id);
        Task<bool> RecordExistAsync(CategoryModel model);
        Task<bool> UpdateAsync(CategoryModel request);
    }
}