using AthodBeTrackApi.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IReportService
    {
        Task<int> AddReport(ReportModel request);
        Task<List<GetReports_Result>> GetReports(int? reportId, int? activityCategoryMappingId, bool? isActive, int? userId, int? reportStatus);
        Task<string> GetCode(string tableTransaction);
        Task<bool> UpdateReport(ReportModel request);
        Task<bool> DeleteReport(int reportId, int userId, int roleId);
        Task<bool> ActiveReport(int reportId, int userId);
        Task<List<ReportSummaryModel>> GetGraphData(int reportId);
        Task<ReportSummaryViewModel> GetGraphDataWithPagination(int reportId, int pageNumber, int rowsOfPage);
        Task<bool> SaveReportTemplateAsync(ChartTemplate request);
        Task RefreshReportAsync(int reportId);
        Task<List<RPT_GetQuestionDetails_Result>> GetQuestionDetails(int reportId, int activityQuestionId);
        Task<RPT_GetReportLocation_Result> GetReportLocation(int reportId);
        Task<bool> ShareReport(ShareReport shareReport);
        Task<List<RPT_GetUsersForShareReport_Result>> GetUsersForShareReport(string ReportNo, int UserId);
        Task<bool> CloneReport(int reportId, int userId);
        Task<List<RPT_GetUpdatedHousehold_Result>> RPT_GetUpdatedHousehold(int reportId);
        Task<bool> MakeFevoriteReport(int reportId, int userId);
        Task<ReportModel> GetFevoriteReport(int userId);
        Task<List<GetGenerateReportDetails_Result>> GetGenerateReportDetailsAsync(int reportId);
        Task<DataTable> GenerateReportAsync(int reportId, int userId);
        Task<DataSet> GenerateReport(int reportId, int userId);
        Task<List<RPT_GetUpdatedHousehold_Result>> RPT_GetUpdatedHouseholdList(int reportId, int start, int length, string orderBy, string searchText);
        Task<int> RPT_GetUpdatedHouseholdCount(int reportId, string searchText);
        Task<List<RPT_GetQuestionDetails_Result>> RPT_GetQuestionDetailsDynamic(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter, int start, int length, string orderBy, string searchText);
        Task<int> RPT_GetQuestionDetailsCount(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter, string searchText);
        List<RPT_GetQuestionDetails_Result> RPT_GetQuestionDetailsDynamic2(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter, int start, int length, string orderBy, string searchText);
        Task<int> AddReportWithoutGenerateSummary(ReportModel request);
        Task<bool> ResetReportTemplate(int reportId);
        Task DeleteReportTemplate(int reportId);
        Task<int> GetMaximumIndicator(int activityCategoryMappingId);
        Task<List<RPT_GetUpdatedHousehold_Result>> RPT_ExportUpdatedHouseholdList(int reportId);
        Task<List<RPT_GetQuestionDetails_Result>> RPT_ExportQuestionDetails(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter);
        Task DeleteReport(int reportId);
        Task<List<GetAllReports_Result>> GetAllReports(int? activityCategoryMappingId, bool? isActive, int? userId, int? reportStatus);
    }
}