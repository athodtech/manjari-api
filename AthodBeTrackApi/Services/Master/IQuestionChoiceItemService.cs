using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IQuestionChoiceItemService
    {
        Task<bool> AddAsync(QuestionChoiceItemModel model);
        Task DeleteAsync(int id);
        Task<List<QuestionChoiceItemModel>> GetAsync(int? questionChoiceId);
        Task<QuestionChoiceItemModel> GetByIdAsync(int id);
        Task<bool> UpdateAsync(QuestionChoiceItemModel request);
    }
}