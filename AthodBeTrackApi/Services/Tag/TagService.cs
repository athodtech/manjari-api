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
    public class TagService : ITagService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly ITagRepository _tagRepository;
        public TagService(IMapper mapper, IGenericRepository genericRepository, ITagRepository tagRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _tagRepository = tagRepository;
        }
        public async Task<List<TagModel>> GetAsync()
        {
            try
            {
                var tags = await _genericRepository.GetAsync<Tag>();
                return _mapper.Map<List<TagModel>>(tags);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TagModel> GetByIdAsync(int id)
        {
            try
            {
                var tag = await _genericRepository.GetByIDAsync<Tag>(id);
                return _mapper.Map<TagModel>(tag);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddAsync(TagModel model)
        {
            try
            {
                var map = _mapper.Map<Tag>(model);
                var res = await _genericRepository.InsertAsync(map);
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

        public async Task<bool> RecordExistAsync(TagModel model)
        {
            try
            {
                var map = _mapper.Map<Tag>(model);
                return await _genericRepository.ExistsAsync<Tag>(x => x.Name == model.Name);
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<bool> UpdateAsync(TagModel request)
        {
            try
            {
                var tag = await _genericRepository.GetByIDAsync<Tag>(request.Id);
                tag.Name = request.Name;
                tag.IsActive = request.IsActive;
                tag.UpdatedOn = request.UpdatedOn;
                tag.UpdatedBy = request.UpdatedBy;
                await _genericRepository.UpdateAsync(tag);
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
                await _genericRepository.DeleteAsync<Tag>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<List<GetQuestionTagMapping_Result>> GetQuestionTagMapping(int tagId)
        {
            try
            {
                var dataTable = await Task.FromResult(_tagRepository.GetQuestionTagMapping(tagId));
                return ExtensionMethods.ConvertToList<GetQuestionTagMapping_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<GetQuestionTagForMapping_Result>> GetQuestionTagForMapping(int tagId)
        {
            try
            {
                var dataTable = await Task.FromResult(_tagRepository.GetQuestionTagForMapping(tagId));
                return ExtensionMethods.ConvertToList<GetQuestionTagForMapping_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateQuestionTagMapping(UpdateQuestionTagMapping_Result request)
        {
            try
            {
                var questionTagMapping = ExtensionMethods.ToDataTable(request.questionIds);
                questionTagMapping.TableName = "questionTagMapping";
                await Task.FromResult(_tagRepository.UpdateQuestionTagMapping
                    (request.tagId, request.CreatedBy, questionTagMapping));
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
