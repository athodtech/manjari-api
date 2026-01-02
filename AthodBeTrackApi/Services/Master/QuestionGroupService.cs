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
    public class QuestionGroupService : IQuestionGroupService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        public QuestionGroupService(IMapper mapper, IGenericRepository genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }

        public async Task<List<QuestionGroupModel>> GetAsync()
        {
            try
            {
                var groups = await _genericRepository.GetAsync<QuestionGroup>();
                return _mapper.Map<List<QuestionGroupModel>>(groups);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<QuestionGroupModel> GetByIdAsync(int id)
        {
            try
            {
                var group = await _genericRepository.GetByIDAsync<QuestionGroup>(id);
                return _mapper.Map<QuestionGroupModel>(group);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddAsync(QuestionGroupModel model)
        {
            try
            {
                var group = _mapper.Map<QuestionGroup>(model);
                var res = await _genericRepository.InsertAsync(group);
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

        public async Task<bool> RecordExistAsync(QuestionGroupModel model)
        {
            try
            {
                return await _genericRepository.ExistsAsync<QuestionGroup>(x => x.GroupName.ToUpper() == model.GroupName.ToUpper());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(QuestionGroupModel request)
        {
            try
            {
                var question = await _genericRepository.GetByIDAsync<QuestionGroup>(request.GroupId);
                question.GroupName = request.GroupName;
                question.ParentGroupId = request.ParentGroupId;
                question.SortingOrder = request.SortingOrder;
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
                await _genericRepository.DeleteAsync<QuestionGroup>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
