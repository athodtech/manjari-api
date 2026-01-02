using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IQuestionGroupService
    {
        Task<bool> AddAsync(QuestionGroupModel model);
        Task DeleteAsync(int id);
        Task<List<QuestionGroupModel>> GetAsync();
        Task<QuestionGroupModel> GetByIdAsync(int id);
        Task<bool> RecordExistAsync(QuestionGroupModel model);
        Task<bool> UpdateAsync(QuestionGroupModel request);
    }
}