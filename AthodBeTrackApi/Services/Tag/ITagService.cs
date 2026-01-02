using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface ITagService
    {
        Task<bool> AddAsync(TagModel model);
        Task DeleteAsync(int id);
        Task<List<TagModel>> GetAsync();
        Task<TagModel> GetByIdAsync(int id);
        Task<List<GetQuestionTagForMapping_Result>> GetQuestionTagForMapping(int tagId);
        Task<List<GetQuestionTagMapping_Result>> GetQuestionTagMapping(int tagId);
        Task<bool> RecordExistAsync(TagModel model);
        Task<bool> UpdateAsync(TagModel request);
        Task<bool> UpdateQuestionTagMapping(UpdateQuestionTagMapping_Result request);
    }
}