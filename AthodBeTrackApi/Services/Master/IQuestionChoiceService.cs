using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IQuestionChoiceService
    {
        Task<bool> AddAsync(QuestionChoiceModel model);
        Task DeleteAsync(int id);
        Task<List<QuestionChoiceModel>> GetAsync();
        Task<QuestionChoiceModel> GetByIdAsync(int id);
        Task<bool> RecordExistAsync(QuestionChoiceModel model);
        Task<bool> UpdateAsync(QuestionChoiceModel request);
    }
}