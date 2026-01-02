using AthodBeTrackApi.Data;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public class DropdownService : IDropdownService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IDropdownRepository _dropdownRepository;
        private readonly IMapper _mapper;

        public DropdownService(IGenericRepository genericRepository, IDropdownRepository dropdownRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _dropdownRepository = dropdownRepository;
            _mapper = mapper;
        }
        public async Task<List<SelectListModel>> GetRolesAsync(int? roleId)
        {
            var dtRole = await _genericRepository.GetAsync<Role>(x => (roleId.HasValue ? x.RoleId == roleId.Value : (1 > 0)) && (x.IsActive == true));
            var RoleList = _mapper.Map<List<SelectListModel>>(dtRole);
            return RoleList;
        }

        public async Task<List<SelectListModel>> GetStateAsync(int? stateId)
        {
            var states = await _genericRepository.GetAsync<State>(x => (stateId.HasValue ? x.StateId == stateId.Value : (1 > 0)) && (x.IsActive == true));
            var stateList = _mapper.Map<List<SelectListModel>>(states);
            return stateList;
        }

        public async Task<List<SelectListModel>> GetDistrictAsync(int? stateId, int? districtId)
        {
            var districts = await _genericRepository.GetAsync<District>(x => (stateId.HasValue ? x.StateId == stateId.Value : (1 > 0)) && (x.IsActive == true) && (districtId.HasValue ? x.DistrictId == districtId.Value : (1 > 0)));
            var districtList = _mapper.Map<List<SelectListModel>>(districts);
            return districtList;

        }

        public async Task<List<SelectListModel>> GetBlockAsync(int? stateId, int? districtId, int? blockId)
        {
            var blocks = await _genericRepository.GetAsync<Block>(x => (stateId.HasValue ? x.StateId == stateId.Value : (1 > 0)) && (x.IsActive == true) && (districtId.HasValue ? x.DistrictId == districtId.Value : (1 > 0)) && (blockId.HasValue ? x.BlockId == blockId.Value : (1 > 0)));
            var blockList = _mapper.Map<List<SelectListModel>>(blocks);
            return blockList;
        }
        public async Task<List<SelectListModel>> GetVillageAsync(int? stateId, int? districtId, int? blockId, int? villageId)
        {
            var villages = await _genericRepository.GetAsync<Village>(x => (stateId.HasValue ? x.StateId == stateId.Value : (1 > 0)) && (x.IsActive == true) && (districtId.HasValue ? x.DistrictId == districtId.Value : (1 > 0)) && (blockId.HasValue ? x.BlockId == blockId.Value : (1 > 0)) && (villageId.HasValue ? x.VillageId == villageId.Value : (1 > 0)));
            var selectLists = _mapper.Map<List<SelectListModel>>(villages);
            return selectLists;
        }

        public async Task<List<SelectListModel>> GetQuestionTypeAsync(int? id)
        {
            var questions = await _genericRepository.GetAsync<QuestionType>(x => (id.HasValue ? x.Id == id.Value : (1 > 0)) && (x.IsActive == true));
            var listModels = _mapper.Map<List<SelectListModel>>(questions);
            return listModels;
        }

        public async Task<List<SelectListModel>> GetQuestionAsync(int? questionId)
        {
            var questions = await _genericRepository.GetAsync<QuestionBank>(x => (questionId.HasValue ? x.QuestionId == questionId.Value : (1 > 0)) && (x.IsActive == true));
            var listModels = _mapper.Map<List<SelectListModel>>(questions);
            return listModels;
        }


        public async Task<List<SelectListModel>> GetQuestionChoiceAsync(int? id)
        {
            var questions = await _genericRepository.GetAsync<QuestionChoice>(x => (id.HasValue ? x.QuestionChoiceId == id.Value : (1 > 0)) && (x.IsActive == true));
            var listModels = _mapper.Map<List<SelectListModel>>(questions);
            return listModels;
        }

        public async Task<List<SelectListModel>> GetQuestionChoiceItemAsync(int id)
        {
            var questions = await _genericRepository.GetAsync<QuestionChoiceItem>(x => x.QuestionChoiceId == id && x.IsActive == true);
            var listModels = _mapper.Map<List<SelectListModel>>(questions);
            return listModels;
        }

        public async Task<List<SelectListModel>> GetActivityAsync(int? activityId)
        {
            var cdate = DateTime.Now;
            var surveys = await _genericRepository.GetAsync<Activity>(x => (activityId.HasValue ? x.ActivityId == activityId.Value : (1 > 0)) && (x.IsActive == true) && (x.EndDate >= cdate));
            var listModels = _mapper.Map<List<SelectListModel>>(surveys);
            return listModels;
        }

        public async Task<List<SelectListModel>> GetUserByRoleIdAsync(int? roleId)
        {
            var users = await _genericRepository.GetAsync<User>(x => (roleId.HasValue ? x.RoleId == roleId.Value : (1 > 0)) && (x.IsActive == true));
            var listModels = _mapper.Map<List<SelectListModel>>(users);
            return listModels;
        }


        public async Task<List<SelectListModel>> GetQuestionGroupAsync(int? groupId, bool? isDefault)
        {
            var groups = await _genericRepository.GetAsync<QuestionGroup>(x => (groupId.HasValue ? x.GroupId == groupId.Value : (1 > 0)) && (isDefault.HasValue ? x.Default == isDefault.Value : (1 > 0)) && (x.IsActive == true));
            var listModels = _mapper.Map<List<SelectListModel>>(groups);
            return listModels;
        }


        public async Task<List<SelectListModel>> GetQuestionGroupExceptIdAsync(int? groupId)
        {
            var groups = await _genericRepository.GetAsync<QuestionGroup>(x => (groupId.HasValue ? x.GroupId != groupId.Value : (1 > 0)) && (x.IsActive == true));
            var listModels = _mapper.Map<List<SelectListModel>>(groups);
            return listModels;
        }

        public async Task<List<SelectListModel>> GetUserLocationLevelDll(int userId)
        {
            var list = await Task.FromResult(_dropdownRepository.GetUserLocationLevel(userId));
            var result = ExtensionMethods.ConvertToList<SelectListModel>(list);
            return result;
        }

        public async Task<List<SelectListModel>> GetUserLocationStateDll(int userId)
        {
            var list = await Task.FromResult(_dropdownRepository.GetUserLocationStates(userId));
            var result = ExtensionMethods.ConvertToList<SelectListModel>(list);
            return result;
        }
        public async Task<List<SelectListModel>> GetUserLocationDistrictDll(int userId, int stateId)
        {
            var list = await Task.FromResult(_dropdownRepository.GetUserLocationDistricts(userId, stateId));
            var result = ExtensionMethods.ConvertToList<SelectListModel>(list);
            return result;
        }
        public async Task<List<SelectListModel>> GetUserLocationBlockDll(int userId, int stateId, int districtId)
        {
            var list = await Task.FromResult(_dropdownRepository.GetUserLocationBlocks(userId, stateId, districtId));
            var result = ExtensionMethods.ConvertToList<SelectListModel>(list);
            return result;
        }
        public async Task<List<SelectListModel>> GetUserLocationVillageDll(int userId, int stateId, int districtId, int blockId)
        {
            var list = await Task.FromResult(_dropdownRepository.GetUserLocationVillages(userId, stateId, districtId, blockId));
            var result = ExtensionMethods.ConvertToList<SelectListModel>(list);
            return result;
        }


        public async Task<List<SelectListModel>> GetQuestionReportingFrequencyAsync(int? reportingFrequencyTypeId)
        {
            var surveys = await _genericRepository.GetAsync<QuestionReportingFrequency>(x => (reportingFrequencyTypeId.HasValue ? x.ReportingFrequencyTypeId == reportingFrequencyTypeId.Value : (1 > 0)));
            var listModels = _mapper.Map<List<SelectListModel>>(surveys);
            return listModels;
        }

        public async Task<List<SelectListActivityCategory>> GetActivityQuestionDll()
        {
            var list = await Task.FromResult(_dropdownRepository.GetActivityQuestionDll());
            var result = ExtensionMethods.ConvertToList<SelectListActivityCategory>(list);
            return result;
        }

        public async Task<List<SelectListModel>> GetActivityQuestionByGroupId(string groupIds)
        {
            var list = await Task.FromResult(_dropdownRepository.GetActivityQuestionByGroupId(groupIds));
            var result = ExtensionMethods.ConvertToList<SelectListModel>(list);
            return result;


        }
        public async Task<List<SelectList2Model>> RPT_GetLocation(int? userId)
        {
            var list = await Task.FromResult(_dropdownRepository.RPT_GetLocation(userId));
            var result = ExtensionMethods.ConvertToList<SelectList2Model>(list);
            return result;
        }


        public async Task<List<SelectListModel>> GetTagAsync(int? tagId)
        {
            var tags = await _genericRepository.GetAsync<Tag>(x => (tagId.HasValue ? x.Id == tagId.Value : (1 > 0)) && (x.IsActive == true));
            var listModels = _mapper.Map<List<SelectListModel>>(tags);
            return listModels;
        }

        public async Task<List<SelectListModel>> GetAssignUserLocationDropdown(string flag, string ids)
        {
            var list = await Task.FromResult(_dropdownRepository.GetAssignUserLocationDropdown(flag, ids));
            var result = ExtensionMethods.ConvertToList<SelectListModel>(list);
            return result;
        }

        public async Task<List<SelectListModel>> RPT_GetUserLocationStates(int userId)
        {
            var list = await Task.FromResult(_dropdownRepository.RPT_GetUserLocationStates(userId));
            var result = ExtensionMethods.ConvertToList<SelectListModel>(list);
            return result;
        }

        public async Task<List<SelectListModel>> RPT_GetAssignUserLocationDropdown(int userId, string flag, string ids)
        {
            var list = await Task.FromResult(_dropdownRepository.RPT_GetAssignUserLocationDropdown(userId, flag, ids));
            var result = ExtensionMethods.ConvertToList<SelectListModel>(list);
            return result;
        }
        public async Task<List<SelectListModel>> RPT_GetAssignUserLocationDropdown2(int userId, string flag, string sIds, string dIds, string bIds)
        {
            var list = await Task.FromResult(_dropdownRepository.RPT_GetAssignUserLocationDropdown2(userId, flag, sIds, dIds, bIds));
            var result = ExtensionMethods.ConvertToList<SelectListModel>(list);
            return result;
        }

        public async Task<List<SelectListModel>> RPT_GetTagsdll(string groupIds)
        {
            var list = await Task.FromResult(_dropdownRepository.RPT_GetTagsdll(groupIds));
            return ExtensionMethods.ConvertToList<SelectListModel>(list);

        }
        public async Task<List<SelectListModel>> RPT_GetQuestiondll(string tagIds, int activityCategoryMapping)
        {

            var list = await Task.FromResult(_dropdownRepository.RPT_GetQuestiondll(tagIds, activityCategoryMapping));
            return ExtensionMethods.ConvertToList<SelectListModel>(list);
        }

        public async Task<List<SelectListModel>> RPT_GetQuestiondll2(string groupIds, string tagIds, int activityCategoryMapping)
        {
            if (string.IsNullOrEmpty(tagIds) && !string.IsNullOrEmpty(groupIds))
            {
                var tagDt = await Task.FromResult(_dropdownRepository.RPT_GetTagsdll(groupIds));
                if (tagDt != null && tagDt.Rows.Count > 0)
                {
                    var tags = tagDt.AsEnumerable()
                     .Select(r => r["id"]?.ToString())  // safely handle nulls
                     .Where(id => !string.IsNullOrEmpty(id))
                     .ToList();
                    tagIds = string.Join(",", tags);
                }
            }
            var list = await Task.FromResult(_dropdownRepository.RPT_GetQuestiondll(tagIds, activityCategoryMapping));
            return ExtensionMethods.ConvertToList<SelectListModel>(list);
        }
        public async Task<List<SelectListModel>> RPT_GetQuestiondll(string tagIds, int activityCategoryMapping, string strWhere)
        {
            var list = await Task.FromResult(_dropdownRepository.RPT_GetQuestiondll(tagIds, activityCategoryMapping, strWhere));
            return ExtensionMethods.ConvertToList<SelectListModel>(list);
        }

        public async Task<List<ChartModel>> GetChartType()
        {
            try
            {
                var charts = await _genericRepository.GetAsync<Chart>(x => x.IsActive == true);
                return _mapper.Map<List<ChartModel>>(charts);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<SelectListModel>> GetRolesExcludingRoleIdAsync(int? roleId)
        {
            var dtRole = await _genericRepository.GetAsync<Role>(x => (roleId.HasValue ? x.RoleId != roleId.Value : (1 > 0)) && (x.IsActive == true));
            var RoleList = _mapper.Map<List<SelectListModel>>(dtRole);
            return RoleList;
        }

        public async Task<List<SelectListModel>> GetUsersExcludingRoleIdAsync(int? roleId)
        {
            var users = await _genericRepository.GetAsync<User>(x => (roleId.HasValue ? x.RoleId != roleId.Value : (1 > 0)) && (x.IsActive == true));
            var listModels = _mapper.Map<List<SelectListModel>>(users);
            return listModels;
        }

        public async Task<List<SelectListModel>> GetApplicationEventTypeAsync(int? id)
        {
            var users = await _genericRepository.GetAsync<ApplicationEventType>(x => (id.HasValue ? x.Id != id.Value : (1 > 0)) && (x.IsActive == true));
            var listModels = _mapper.Map<List<SelectListModel>>(users);
            return listModels;
        }

        public async Task<List<SelectListModel>> RPT_GetQuestionGroupAsync(int userId)
        {
            var list = await _dropdownRepository.RPT_GetQuestionGroupAsync(userId);
            return ExtensionMethods.ConvertToList<SelectListModel>(list);
        }

        public async Task<List<SelectListModel>> RPT_GetTagsAsync(string groupIds, int userId)
        {
            var list = await _dropdownRepository.RPT_GetTagsAsync(groupIds, userId);
            return ExtensionMethods.ConvertToList<SelectListModel>(list);
        }

        public async Task<List<SelectList3Model>> GetAllUsersExcludingRoleIdAsync(int? roleId)
        {
            var users = await _genericRepository.GetAsync<User>(x => (roleId.HasValue ? x.RoleId != roleId.Value : (1 > 0)));
            var listModels = _mapper.Map<List<SelectList3Model>>(users);
            return listModels;
        }

        public async Task<List<SelectListModel>> GetActivityWithMappingidDll(int? userId)
        {
            var list = await Task.FromResult(_dropdownRepository.GetActivityWithMappingidDll(userId));
            var result = ExtensionMethods.ConvertToList<SelectListModel>(list);
            return result;


        }

    }
}
