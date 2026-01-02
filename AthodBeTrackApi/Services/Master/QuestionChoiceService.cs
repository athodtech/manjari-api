using AthodBeTrackApi.Data;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AthodBeTrackApi.Services
{
    public class QuestionChoiceService : IQuestionChoiceService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        public QuestionChoiceService(IMapper mapper, IGenericRepository genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }

        public async Task<List<QuestionChoiceModel>> GetAsync()
        {
            try
            {
                var questionChoices = await _genericRepository.GetAsync<QuestionChoice>();
                return _mapper.Map<List<QuestionChoiceModel>>(questionChoices);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<QuestionChoiceModel> GetByIdAsync(int id)
        {
            try
            {
                var questionChoice = await _genericRepository.GetByIDAsync<QuestionChoice>(id);
                return _mapper.Map<QuestionChoiceModel>(questionChoice);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddAsync(QuestionChoiceModel model)
        {
            try
            {
                var questionChoice = _mapper.Map<QuestionChoice>(model);
                questionChoice.QuestionChoiceItems = null;                
                var res = await _genericRepository.InsertAsync(questionChoice);
                if (res > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }


        }

        public async Task<bool> RecordExistAsync(QuestionChoiceModel model)
        {
            try
            {
                return await _genericRepository.ExistsAsync<QuestionChoice>(x => x.QuestionChoiceName.ToUpper() == model.QuestionChoiceName.ToUpper());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(QuestionChoiceModel request)
        {
            try
            {
                var question = await _genericRepository.GetByIDAsync<QuestionChoice>(request.QuestionChoiceId);
                question.QuestionChoiceName = request.QuestionChoiceName;
                question.IsActive = request.IsActive;
                question.UpdatedOn = request.UpdatedOn;
                question.UpdatedBy = request.UpdatedBy;
                await _genericRepository.UpdateAsync(question);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _genericRepository.DeleteAsync<QuestionChoice>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
