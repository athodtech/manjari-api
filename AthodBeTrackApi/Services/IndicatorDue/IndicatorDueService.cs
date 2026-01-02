using AthodBeTrackApi.Data;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AthodBeTrackApi.Services
{
    public class IndicatorDueService : IIndicatorDueService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly IIndicatorDueRepository _repository;

        public IndicatorDueService(IMapper mapper, IGenericRepository genericRepository, IIndicatorDueRepository repository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _repository = repository;
        }

        public async Task<List<DueReportSummaryModel>> GetDueReportSummary(int activityCategoryMappingId, int groupId, int reportingFrequecyTypeId)
        {
            var dueReportSummaries = await _genericRepository.GetAsync<DueReportSummary>(x => x.ActivityCategoryMappingId == activityCategoryMappingId && x.GroupId == groupId && x.ReportingFrequecyTypeId == reportingFrequecyTypeId);
            return _mapper.Map<List<DueReportSummaryModel>>(dueReportSummaries);
        }

        public async Task<List<IndicatorDueSummary_Result>> GetIndicatorDueSummary(int userId, int activityCategoryMappingId, int? stateId, int? districtId, int? blockId, int? villageId, int? groupId, int? reportingFrequencyId, string status)
        {
            try
            {
                var dataTable = await _repository.GetIndicatorDueSummary(userId, activityCategoryMappingId, stateId, districtId, blockId, villageId, groupId, reportingFrequencyId, status);
                return ExtensionMethods.ConvertToList<IndicatorDueSummary_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DueReportDetailDynamic_Result>> GetIndicatorDueDetailsDynamic(int userId, int? stateId, int? districtId, int? blockId, int? villageId, int activityCategoryMappingId, int ActivityQuestionId, int? groupId, string status, int start, int length, string orderBy, string searchText)
        {
            try
            {
                string whereClause = "";
                string completeQuery = "";
                string limitQuery = "" + orderBy + " OFFSET " + start + " ROWS FETCH FIRST " + length + " ROWS ONLY";
                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = " WHERE [HouseholdName] like '%" + searchText + "%' or MobileNo like '%" + searchText + "%' or UniqueSetCode like '%" + searchText + "%' or DistrictName like '%" + searchText + "%' or BlockName like '%" + searchText + "%' or [VillageName] like '%" + searchText + "%' or [ModifyBy] like '%" + searchText + "%' or [ModifyOn] like '%" + searchText + "%' or [Period] like '%" + searchText + "%' ";
                    completeQuery = string.Format("{0}", whereClause);
                }

                var dataTable = await _repository.GetIndicatorDueDetailsDynamic(userId, stateId, districtId, blockId, villageId, activityCategoryMappingId, ActivityQuestionId, groupId, status, whereClause, limitQuery);

                return ExtensionMethods.ConvertToList<DueReportDetailDynamic_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> GetIndicatorDueDetailsCountDynamic(int userId, int? stateId, int? districtId, int? blockId, int? villageId, int activityCategoryMappingId, int ActivityQuestionId, int? groupId, string status, string searchText)
        {
            try
            {
                int recordsFiltered = 0;
                string whereClause = "";
                string completeQuery = "";

                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = " WHERE [HouseholdName] like '%" + searchText + "%' or MobileNo like '%" + searchText + "%' or UniqueSetCode like '%" + searchText + "%' or DistrictName like '%" + searchText + "%' or BlockName like '%" + searchText + "%' or [VillageName] like '%" + searchText + "%' or [ModifyBy] like '%" + searchText + "%' or [ModifyOn] like '%" + searchText + "%' or [Period] like '%" + searchText + "%' ";
                    completeQuery = string.Format("{0}", whereClause);
                }

                var dataTable = await _repository.GetIndicatorDueDetailsCountDynamic(userId, stateId, districtId, blockId, villageId, activityCategoryMappingId, ActivityQuestionId, groupId, status, whereClause);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    recordsFiltered = Convert.ToString(dataTable.Rows[0][0]) == "" ? 0 : Convert.ToInt32(dataTable.Rows[0][0]);
                }

                return recordsFiltered;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DueReportDetailDynamic_Result>> GetIndicatorDueDetailsExport(int userId, int? stateId, int? districtId, int? blockId, int? villageId, int activityCategoryMappingId, int ActivityQuestionId, int? groupId, string status)
        {
            try
            {
                var dataTable = await _repository.GetIndicatorDueDetailsDynamic(userId, stateId, districtId, blockId, villageId, activityCategoryMappingId, ActivityQuestionId, groupId, status, "", "");

                return ExtensionMethods.ConvertToList<DueReportDetailDynamic_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RefreshDueIndicator()
        {
            try
            {
                await _repository.RefreshDueIndicator();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DateTime?> GetLastUpdateDueIndicator()
        {
            try
            {
                return (await _genericRepository.GetAsync<DueReportDetail>()).FirstOrDefault()?.CreatedOn;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> CheckDueIndicatorReportGenerated()
        {
            try
            {
                return await _repository.CheckDueIndicatorReportGeneratedAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
