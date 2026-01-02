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
    public class QuestionChoiceItemService : IQuestionChoiceItemService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        public QuestionChoiceItemService(IMapper mapper, IGenericRepository genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }

        public async Task<List<QuestionChoiceItemModel>> GetAsync(int? questionChoiceId)
        {
            try
            {
                var items = await _genericRepository.GetAsync<QuestionChoiceItem>(x=> questionChoiceId.HasValue ? x.QuestionChoiceId == questionChoiceId.Value : (1 > 0));
                return _mapper.Map<List<QuestionChoiceItemModel>>(items);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<QuestionChoiceItemModel> GetByIdAsync(int id)
        {
            try
            {
                var item = await _genericRepository.GetByIDAsync<QuestionChoiceItem>(id);
                return _mapper.Map<QuestionChoiceItemModel>(item);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddAsync(QuestionChoiceItemModel model)
        {
            try
            {
                var questionChoice = _mapper.Map<QuestionChoiceItem>(model);
                questionChoice.QuestionChoice = null;

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


        public async Task<bool> UpdateAsync(QuestionChoiceItemModel request)
        {
            try
            {
                var choiceItem = await _genericRepository.GetByIDAsync<QuestionChoiceItem>(request.Id);
                choiceItem.Item = request.Item;
                choiceItem.Value = request.Value;
                choiceItem.Sort = request.Sort;
                choiceItem.ItemShotName = request.ItemShotName;
                choiceItem.IsActive = request.IsActive;
                choiceItem.UpdatedOn = request.UpdatedOn;
                choiceItem.UpdatedBy = request.UpdatedBy;
                await _genericRepository.UpdateAsync(choiceItem);
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
                await _genericRepository.DeleteAsync<QuestionChoiceItem>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
