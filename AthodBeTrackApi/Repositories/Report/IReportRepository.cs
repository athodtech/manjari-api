using System.Data;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories
{
    public interface IReportRepository
    {
        Task<bool> DeleteReport(int reportId);
        Task<bool> DeleteReportTemplate(int reportId);
        Task<bool> DeleteSharedReport(string reportId);
        Task<DataSet> GenerateReport(int reportId, int userId);
        Task<DataTable> GenerateReportAsync(int reportId, int userId);
        Task GenerateReportSummaryAsync(int reportId);
        DataTable GetAllReports(int? activityCategoryMappingId, bool? isActive, int? userId, int? reportStatus);
        string GetCode(string tableTransaction);
        Task<DataTable> GetGenerateReportDetailsAsync(int reportId);
        DataTable GetGraphDataWithPagination(int reportId, int pageNumber, int rowsOfPage);
        DataTable GetQuestionDetails(int reportId, int activityQuestionId);
        DataSet GetReportLocation(int reportId);
        DataTable GetReports(int? reportId, int? activityCategoryMappingId, bool? isActive, int? userId, int? reportStatus);
        DataTable GetUsersForShareReport(string ReportNo, int UserId);
        Task<bool> MakeFevoriteReport(int reportId, int userId);
        Task ResetReportTemplate(int reportId);
        Task<DataTable> RPT_ExportQuestionDetails(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter);
        Task<DataTable> RPT_ExportUpdatedHouseholdList(int reportId);
        Task<DataTable> RPT_GetQuestionDetailsCount(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter, string where);
        Task<DataTable> RPT_GetQuestionDetailsDynamic(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter, string where, string limitQuery);
        DataTable RPT_GetQuestionDetailsDynamic2(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter, string where, string limitQuery);
        DataTable RPT_GetUpdatedHousehold(int reportId);
        Task<DataTable> RPT_GetUpdatedHouseholdCount(int reportId, string where);
        Task<DataTable> RPT_GetUpdatedHouseholdList(int reportId, string where);
        Task<bool> SaveReprtTemplateAsync(int reportId, int userId, DataTable reportChartTemplate, DataTable reportItemTemplate);
    }
}