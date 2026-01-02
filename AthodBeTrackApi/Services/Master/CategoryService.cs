using AthodBeTrackApi.Data;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Category = AthodBeTrackApi.Data.Category;

namespace AthodBeTrackApi.Services
{

    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;

        public CategoryService(IMapper mapper, IGenericRepository genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }
        public async Task<List<CategoryModel>> GetAsync()
        {
            try
            {
                var categories = await _genericRepository.GetAsync<Category>();
                return _mapper.Map<List<CategoryModel>>(categories);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CategoryModel> GetByIdAsync(int id)
        {
            try
            {
                var category = await _genericRepository.GetByIDAsync<Category>(id);
                return _mapper.Map<CategoryModel>(category);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddAsync(CategoryModel model)
        {
            try
            {
                var category = _mapper.Map<Category>(model);
                var res = await _genericRepository.InsertAsync(category);
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

        public async Task<bool> RecordExistAsync(CategoryModel model)
        {
            try
            {
                return await _genericRepository.ExistsAsync<Category>(x => x.CategoryName.ToUpper() == model.CategoryName.ToUpper());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(CategoryModel request)
        {
            try
            {
                var category = await _genericRepository.GetByIDAsync<Category>(request.CategoryId);
                category.CategoryName = request.CategoryName;
                category.Description = request.Description;
                category.IconClass = request.IconClass;
                category.IsActive = request.IsActive;
                category.UpdatedOn = request.UpdatedOn;
                category.UpdatedBy = request.UpdatedBy;
                await _genericRepository.UpdateAsync(category);
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
                await _genericRepository.DeleteAsync<Category>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
