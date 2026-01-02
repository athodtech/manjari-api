using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IQuestionBankService
    {
        Task<bool> AddAsync(QuestionBankModel model);
        Task DeleteAsync(int id);
        Task<List<GetQuestionBank_Result>> GetAsync(int? questionId);

        //Task<List<QuestionBankModel>> GetAsync();
        Task<QuestionBankModel> GetByIdAsync(int id);
        Task<bool> UpdateAsync(QuestionBankModel request);
    }
}