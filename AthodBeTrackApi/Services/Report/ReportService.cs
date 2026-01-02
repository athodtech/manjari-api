using AthodBeTrackApi.Data;
using AthodBeTrackApi.Helpers;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly IReportRepository _reportRepository;
        public ReportService(IMapper mapper, IGenericRepository genericRepository, IReportRepository reportRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _reportRepository = reportRepository;
        }

        public async Task<string> GetCode(string tableTransaction)
        {
            try
            {
                return await Task.FromResult(_reportRepository.GetCode(tableTransaction));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddReport(ReportModel request)
        {
            try
            {
                var report = _mapper.Map<Report>(request);
                report.ReportStatus = (int)ReportStatus.Submitted;
                var reportId = await _genericRepository.InsertAsync(report);
                if (reportId > 0)
                {
                    await _reportRepository.GenerateReportSummaryAsync(reportId);
                    return reportId;
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddReportWithoutGenerateSummary(ReportModel request)
        {
            try
            {
                var report = _mapper.Map<Report>(request);
                report.ReportStatus = (int)ReportStatus.Submitted;
                var reportId = await _genericRepository.InsertAsync(report);
                return reportId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateReport(ReportModel request)
        {
            try
            {
                var report = await _genericRepository.GetFirstOrDefaultAsync<Report>(x => x.ReportId == request.ReportId);
                if (report != null)
                {
                    report.ReportName = request.ReportName;
                    report.Description = request.Description;
                    report.UserId = report.UserId;
                    report.StateId = request.StateId;
                    report.DistrictId = request.DistrictId;
                    report.BlockId = request.BlockId;
                    report.VillageId = request.VillageId;
                    report.ReportingGroupIds = request.ReportingGroupIds;
                    report.ReportingTagIds = request.ReportingTagIds;
                    report.ReportQuestionIds = request.ReportQuestionIds;
                    report.FromDate = request.FromDate;
                    report.ToDate = request.ToDate;
                    report.ReportingFrequnecy = request.ReportingFrequnecy;
                    report.ReportFilterEnable = request.ReportFilterEnable;
                    report.Status = request.Status;
                    report.IsPrimary = request.IsPrimary ?? false;
                    report.IsUnique = request.IsUnique ?? false;
                    report.ReportStatus = (int)ReportStatus.Submitted;
                    report.IsActive = request.IsActive;
                    report.UpdatedBy = request.UpdatedBy;
                    report.UpdatedOn = request.UpdatedOn;
                    await _genericRepository.UpdateAsync(report);

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<GetReports_Result>> GetReports(int? reportId, int? activityCategoryMappingId, bool? isActive, int? userId, int? reportStatus)
        {
            try
            {
                var dataTable = await Task.FromResult(_reportRepository.GetReports(reportId, activityCategoryMappingId, isActive, userId, reportStatus));
                return ExtensionMethods.ConvertToList<GetReports_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetAllReports_Result>> GetAllReports(int? activityCategoryMappingId, bool? isActive, int? userId, int? reportStatus)
        {
            try
            {
                var dataTable = await Task.FromResult(_reportRepository.GetAllReports(activityCategoryMappingId, isActive, userId, reportStatus));
                return ExtensionMethods.ConvertToList<GetAllReports_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteReport(int reportId, int userId, int roleId)
        {
            try
            {
                bool flag;
                if (roleId == 99)
                {
                    //await _genericRepository.DeleteAsync<Report>(reportId);
                    var report = await _genericRepository.GetByIDAsync<Report>(reportId);
                    report.IsActive = false;
                    report.UpdatedBy = userId;
                    report.UpdatedOn = DateTime.Now;
                    await _genericRepository.UpdateAsync(report);
                    flag = true;
                }
                else
                {
                    var report = await _genericRepository.GetByIDAsync<Report>(reportId);
                    report.IsActive = false;
                    report.UpdatedBy = userId;
                    report.UpdatedOn = DateTime.Now;
                    await _genericRepository.UpdateAsync(report);
                    flag = true;
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ActiveReport(int reportId, int userId)
        {
            try
            {
                var report = await _genericRepository.GetByIDAsync<Report>(reportId);
                report.IsActive = true;
                report.UpdatedBy = userId;
                report.UpdatedOn = DateTime.Now;
                await _genericRepository.UpdateAsync(report);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ReportSummaryModel>> GetGraphData(int reportId)
        {
            try
            {
                var reportSummaries = await _genericRepository.GetAsync<ReportSummary>(x => x.ReportId == reportId);
                return _mapper.Map<List<ReportSummaryModel>>(reportSummaries);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ReportSummaryViewModel> GetGraphDataWithPagination(int reportId, int pageNumber, int rowsOfPage)
        {
            try
            {
                var dataTable = await Task.FromResult(_reportRepository.GetGraphDataWithPagination(reportId, pageNumber, rowsOfPage));
                var reportSummaries = ExtensionMethods.ConvertToList<ReportSummaryModel>(dataTable);


                var disReportsByIdSort = reportSummaries.GroupBy(m => new { m.ActivityQuestionId, m.SortingOrder })
                    .Select(group => group.First()).ToList();


                //var questions = reportSummaries.Select(x => x.Question).Distinct().ToList();

                var reportSummaryViewModel = new ReportSummaryViewModel
                {
                    questions = new List<Questions>(),
                    ReportId = reportId
                };

                foreach (var disReportByIdSort in disReportsByIdSort)
                {
                    var questionDetails = new Questions
                    {
                        chartValues = new List<ChartValues>()
                    };
                    var questionSet = reportSummaries.Where(x => x.ActivityQuestionId == disReportByIdSort.ActivityQuestionId && x.SortingOrder == disReportByIdSort.SortingOrder).OrderBy(x => x.Item).ToList();

                    for (int i = 0; i < questionSet.Count; i++)
                    {
                        var values = new ChartValues();
                        if (i == 0)
                        {
                            reportSummaryViewModel.TotalHousehold = questionSet[i].TotalHousehold;
                            reportSummaryViewModel.AffectedHousehold = questionSet[i].AffectedHousehold;
                            reportSummaryViewModel.CreatedBy = questionSet[i].CreatedBy;
                            questionDetails.ActivityQuestionId = questionSet[i].ActivityQuestionId;
                            questionDetails.Question = questionSet[i].Question;
                            questionDetails.Help = questionSet[i].Help;
                            questionDetails.XaxisName = questionSet[i].XaxisName;
                            questionDetails.YaxisName = questionSet[i].YaxisName;
                            questionDetails.RevisedXaxisName = questionSet[i].RevisedXaxisName;
                            questionDetails.RevisedYaxisName = questionSet[i].RevisedYaxisName;
                            questionDetails.ColorPalette = questionSet[i].ColorPalette;
                            questionDetails.RevisedColorPalette = questionSet[i].RevisedColorPalette;
                            questionDetails.RevisedQuestion = questionSet[i].RevisedQuestion;
                            questionDetails.RevisedHelp = questionSet[i].RevisedHelp;
                            questionDetails.Type = questionSet[i].Type;
                            questionDetails.ReportingFrequencyType = questionSet[i].ReportingFrequencyType;
                            questionDetails.SortingOrder = questionSet[i].SortingOrder;
                            questionDetails.RevisedSortingOrder = questionSet[i].RevisedSortingOrder;
                            questionDetails.ChartTypeId = questionSet[i].ChartTypeId;
                            questionDetails.RevisedChartTypeId = questionSet[i].RevisedChartTypeId;
                            questionDetails.ChartSize = questionSet[i].ChartSize;
                            questionDetails.RevisedChartSize = questionSet[i].RevisedChartSize;
                            questionDetails.ChartJson = questionSet[i].ChartJson;

                            questionDetails.LastUpdated = questionSet[i].LastUpdated;
                            values.Item = questionSet[i].Item;
                            values.QuestionValue = questionSet[i].QuestionValue;
                            values.ItemShortName = questionSet[i].ItemShortName;
                            questionDetails.OriginalSortingOrder = questionSet[i].OriginalSortingOrder;
                        }
                        else
                        {
                            values.Item = questionSet[i].Item;
                            values.QuestionValue = questionSet[i].QuestionValue;
                            values.ItemShortName = questionSet[i].ItemShortName;
                        }
                        questionDetails.chartValues.Add(values);
                    }

                    reportSummaryViewModel.questions.Add(questionDetails);
                }
                return reportSummaryViewModel;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> SaveReportTemplateAsync(ChartTemplate request)
        {
            try
            {
                var reportChartTemplate = ExtensionMethods.ToDataTable(request.ChartTemplates);
                reportChartTemplate.TableName = "ReportChartTemplate";
                var reportItemTemplate = ExtensionMethods.ToDataTable(request.ItemTemplates);
                reportItemTemplate.TableName = "ReportItemTemplate";

                await _reportRepository.SaveReprtTemplateAsync(request.ReportId, request.UserId, reportChartTemplate, reportItemTemplate);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ResetReportTemplate(int reportId)
        {
            try
            {
                await _reportRepository.ResetReportTemplate(reportId);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RefreshReportAsync(int reportId)
        {
            try
            {
                await _reportRepository.GenerateReportSummaryAsync(reportId);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteReportTemplate(int reportId)
        {
            await _reportRepository.DeleteReportTemplate(reportId);
        }

        public async Task DeleteReport(int reportId)
        {
            await _reportRepository.DeleteReport(reportId);
        }
        public async Task<List<RPT_GetQuestionDetails_Result>> GetQuestionDetails(int reportId, int activityQuestionId)
        {
            try
            {
                var dataTable = await Task.FromResult(_reportRepository.GetQuestionDetails(reportId, activityQuestionId));
                return ExtensionMethods.ConvertToList<RPT_GetQuestionDetails_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RPT_GetQuestionDetails_Result>> RPT_GetQuestionDetailsDynamic(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter, int start, int length, string orderBy, string searchText)
        {
            try
            {
                string whereClause = "";
                string completeQuery = "";
                string limitQuery = "" + orderBy + " OFFSET " + start + " ROWS FETCH FIRST " + length + " ROWS ONLY";
                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = " WHERE [HouseholdName] like '%" + searchText + "%' or MobileNumber like '%" + searchText + "%' or UniqueSetCode like '%" + searchText + "%' or DistrictName like '%" + searchText + "%' or BlockName like '%" + searchText + "%' or [VillageName] like '%" + searchText + "%' or [UpdatedOn] like '%" + searchText + "%' or [QuestionValue] like '%" + searchText + "%'";
                    completeQuery = string.Format("{0}", whereClause);
                }

                var dataTable = await _reportRepository.RPT_GetQuestionDetailsDynamic(reportId, activityQuestionId, filter, isUniqueFilter, isPrimaryFilter, whereClause, limitQuery);

                return ExtensionMethods.ConvertToList<RPT_GetQuestionDetails_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RPT_GetQuestionDetails_Result> RPT_GetQuestionDetailsDynamic2(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter, int start, int length, string orderBy, string searchText)
        {
            try
            {
                string whereClause = "";
                string completeQuery = "";
                string limitQuery = "" + orderBy + " OFFSET " + start + " ROWS FETCH FIRST " + length + " ROWS ONLY";
                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = " WHERE [HouseholdName] like '%" + searchText + "%' or MobileNumber like '%" + searchText + "%' or UniqueSetCode like '%" + searchText + "%' or DistrictName like '%" + searchText + "%' or BlockName like '%" + searchText + "%' or [VillageName] like '%" + searchText + "%' or [UpdatedOn] like '%" + searchText + "%' or [QuestionValue] like '%" + searchText + "%'";
                    completeQuery = string.Format("{0}", whereClause);
                }

                var dataTable = _reportRepository.RPT_GetQuestionDetailsDynamic2(reportId, activityQuestionId, filter, isUniqueFilter, isPrimaryFilter, whereClause, limitQuery);

                return ExtensionMethods.ConvertToList<RPT_GetQuestionDetails_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> RPT_GetQuestionDetailsCount(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter, string searchText)
        {
            try
            {
                int recordsFiltered = 0;
                string whereClause = "";
                string completeQuery = "";

                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = " WHERE [HouseholdName] like '%" + searchText + "%' or MobileNumber like '%" + searchText + "%' or UniqueSetCode like '%" + searchText + "%' or DistrictName like '%" + searchText + "%' or BlockName like '%" + searchText + "%' or [VillageName] like '%" + searchText + "%' or [UpdatedOn] like '%" + searchText + "%' or [QuestionValue] like '%" + searchText + "%'";
                    completeQuery = string.Format("{0}", whereClause);
                }

                var dataTable = await _reportRepository.RPT_GetQuestionDetailsCount(reportId, activityQuestionId, filter, isUniqueFilter, isPrimaryFilter, whereClause);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    recordsFiltered = dataTable.Rows.Count;
                }

                return recordsFiltered;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<RPT_GetQuestionDetails_Result>> RPT_ExportQuestionDetails(int reportId, int activityQuestionId, string filter, bool isUniqueFilter, bool isPrimaryFilter)
        {
            try
            {
                var dataTable = await _reportRepository.RPT_ExportQuestionDetails(reportId, activityQuestionId, filter, isUniqueFilter, isPrimaryFilter);

                return ExtensionMethods.ConvertToList<RPT_GetQuestionDetails_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RPT_GetReportLocation_Result> GetReportLocation(int reportId)
        {
            try
            {
                var location_Result = new RPT_GetReportLocation_Result()
                {
                    states = new List<RPT_State>(),
                    districts = new List<RPT_District>(),
                    blocks = new List<RPT_Block>(),
                    villages = new List<RPT_Village>(),
                    groups = new List<RPT_Group>(),
                    tags = new List<RPT_Tag>(),
                    questions = new List<RPT_Question>()
                };
                var dataSet = await Task.FromResult(_reportRepository.GetReportLocation(reportId));
                if (dataSet != null)
                {
                    if (dataSet.Tables.Count > 0)
                    {
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            var state = new RPT_State
                            {
                                StateId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["StateId"]),
                                StateName = Convert.ToString(dataSet.Tables[0].Rows[i]["StateName"])
                            };
                            location_Result.states.Add(state);
                        }
                    }
                    if (dataSet.Tables.Count > 1)
                    {
                        for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                        {
                            var district = new RPT_District
                            {
                                DistrictId = Convert.ToInt32(dataSet.Tables[1].Rows[i]["DistrictId"]),
                                DistrictName = Convert.ToString(dataSet.Tables[1].Rows[i]["DistrictName"])
                            };
                            location_Result.districts.Add(district);
                        }
                    }

                    if (dataSet.Tables.Count > 2)
                    {
                        for (int i = 0; i < dataSet.Tables[2].Rows.Count; i++)
                        {
                            var block = new RPT_Block
                            {
                                BlockId = Convert.ToInt32(dataSet.Tables[2].Rows[i]["BlockId"]),
                                BlockName = Convert.ToString(dataSet.Tables[2].Rows[i]["BlockName"])
                            };
                            location_Result.blocks.Add(block);
                        }
                    }
                    if (dataSet.Tables.Count > 3)
                    {
                        for (int i = 0; i < dataSet.Tables[3].Rows.Count; i++)
                        {
                            var village = new RPT_Village
                            {
                                VillageId = Convert.ToInt32(dataSet.Tables[3].Rows[i]["VillageId"]),
                                VillageName = Convert.ToString(dataSet.Tables[3].Rows[i]["VillageName"])
                            };
                            location_Result.villages.Add(village);
                        }
                    }
                    if (dataSet.Tables.Count > 4)
                    {
                        for (int i = 0; i < dataSet.Tables[4].Rows.Count; i++)
                        {
                            var group = new RPT_Group
                            {
                                GroupId = Convert.ToInt32(dataSet.Tables[4].Rows[i]["GroupId"]),
                                GroupName = Convert.ToString(dataSet.Tables[4].Rows[i]["GroupName"])
                            };
                            location_Result.groups.Add(group);
                        }
                    }
                    if (dataSet.Tables.Count > 5)
                    {
                        for (int i = 0; i < dataSet.Tables[5].Rows.Count; i++)
                        {
                            var tag = new RPT_Tag
                            {
                                TagId = Convert.ToInt32(dataSet.Tables[5].Rows[i]["TagId"]),
                                TagName = Convert.ToString(dataSet.Tables[5].Rows[i]["TagName"])
                            };
                            location_Result.tags.Add(tag);
                        }
                    }
                    if (dataSet.Tables.Count > 6)
                    {
                        for (int i = 0; i < dataSet.Tables[6].Rows.Count; i++)
                        {
                            var question = new RPT_Question
                            {
                                Id = Convert.ToInt32(dataSet.Tables[6].Rows[i]["ActivityQuestionId"]),
                                Question = Convert.ToString(dataSet.Tables[6].Rows[i]["Question"])
                            };
                            location_Result.questions.Add(question);
                        }
                    }
                }

                return location_Result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //public async Task<bool> ShareReport(ShareReport shareReport)
        //{
        //    try
        //    {
        //        string msg = "";
        //        string deleteReportIds = "";
        //        var report = await _genericRepository.GetFirstOrDefaultAsync<Report>(x => x.ReportId == shareReport.ReportId);
        //        if (report != null)
        //        {
        //            foreach (var item in shareReport.SharedUsers)
        //            {
        //                if (item.check && !item.shared)
        //                {
        //                    report.ReportId = 0;
        //                    report.ReportStatus = (int)ReportStatus.Shared;
        //                    report.SharedOn = shareReport.SharedOn;
        //                    report.SharedBy = shareReport.SharedBy;
        //                    report.UserId = item.userId;



        //                    int reportId = await _genericRepository.InsertAsync(report);
        //                    await _reportRepository.GenerateReportSummaryAsync(reportId);
        //                    msg = " report has been shared with you by ";
        //                }
        //                else
        //                {
        //                    var deleteReport = await _genericRepository.GetFirstOrDefaultAsync<Report>(x => x.ReportNo == shareReport.ReportNo && x.UserId == item.userId);
        //                    if (deleteReport != null)
        //                        deleteReportIds += deleteReport.ReportId.ToString();

        //                    msg = " report has been unshared with you by";
        //                }

        //                var notification = new Notification()
        //                {
        //                    Notification1 = $"{report.ReportNo} | {report.ReportName} {msg} {shareReport.SharedByName} on {DateTime.Now}",
        //                    NotificationTo = item.userId,
        //                    Url = null,
        //                    Status = (int)notificationStatus.Unread,
        //                    CreatedBy = shareReport.SharedBy,
        //                    CreatedOn = shareReport.SharedOn,
        //                };
        //                await _genericRepository.InsertAsync(notification);
        //            }
        //            await _reportRepository.DeleteSharedReport(deleteReportIds);
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public async Task<bool> ShareReport(ShareReport shareReport)
        {
            try
            {
                string msg = "";
                var report = await _genericRepository.GetFirstOrDefaultAsync<Report>(x => x.ReportId == shareReport.ReportId);
                if (report != null)
                {
                    foreach (var item in shareReport.SharedUsers)
                    {
                        if (item.check && !item.shared)
                        {
                            if (!await _genericRepository.ExistsAsync<ReportsShareLog>(x => x.ReportId == shareReport.ReportId && x.UserId == item.userId))
                            {
                                var reportsShareLog = new ReportsShareLog()
                                {
                                    ReportsShareLogId = 0,
                                    UserId = item.userId,
                                    ReportId = report.ReportId,
                                    ReportNo = report.ReportNo,
                                    SharedOn = DateTime.Now,
                                    SharedBy = shareReport.SharedBy
                                };
                                await _genericRepository.InsertAsync(reportsShareLog);
                                msg = " report has been shared with you by ";
                                var notification = new Notification()
                                {
                                    Notification1 = $"{report.ReportNo} | {report.ReportName} {msg} {shareReport.SharedByName} on {DateTime.Now}",
                                    NotificationTo = item.userId,
                                    Url = null,
                                    Status = (int)notificationStatus.Unread,
                                    CreatedBy = shareReport.SharedBy,
                                    CreatedOn = shareReport.SharedOn,
                                };
                                await _genericRepository.InsertAsync(notification);
                            }
                        }
                        else
                        {
                            var reportsShared = await _genericRepository.GetFirstOrDefaultAsync<ReportsShareLog>(x => x.ReportId == report.ReportId && x.UserId == item.userId);
                            if (reportsShared != null)
                            {
                                await _genericRepository.DeleteAsync<ReportsShareLog>(reportsShared.ReportsShareLogId);
                                msg = " report has been unshared with you by";

                                var notification = new Notification()
                                {
                                    Notification1 = $"{report.ReportNo} | {report.ReportName} {msg} {shareReport.SharedByName} on {DateTime.Now}",
                                    NotificationTo = item.userId,
                                    Url = null,
                                    Status = (int)notificationStatus.Unread,
                                    CreatedBy = shareReport.SharedBy,
                                    CreatedOn = shareReport.SharedOn,
                                };
                                await _genericRepository.InsertAsync(notification);

                            }
                        }
                    }

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> CloneReport(int reportId, int userId)
        {
            try
            {
                var report = await _genericRepository.GetFirstOrDefaultAsync<Report>(x => x.ReportId == reportId);

                if (report != null)
                {
                    report.ReportId = 0;
                    report.ReportNo = await GetCode("Reports");
                    report.ReportName = $"{report.ReportName}";
                    report.ReportStatus = (int)ReportStatus.Draft;
                    report.StateId = null;
                    report.DistrictId = null;
                    report.BlockId = null;
                    report.VillageId = null;
                    report.UserId = userId;
                    report.CreatedBy = userId;
                    report.SharedOn = null;
                    report.SharedBy = null;
                    report.CreatedOn = DateTime.Now;
                    await _genericRepository.InsertAsync(report);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<RPT_GetUsersForShareReport_Result>> GetUsersForShareReport(string ReportNo, int UserId)
        {
            try
            {
                var dataTable = await Task.FromResult(_reportRepository.GetUsersForShareReport(ReportNo, UserId));
                return ExtensionMethods.ConvertToList<RPT_GetUsersForShareReport_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RPT_GetUpdatedHousehold_Result>> RPT_GetUpdatedHousehold(int reportId)
        {
            try
            {
                var dataTable = await Task.FromResult(_reportRepository.RPT_GetUpdatedHousehold(reportId));
                return ExtensionMethods.ConvertToList<RPT_GetUpdatedHousehold_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RPT_GetUpdatedHousehold_Result>> RPT_GetUpdatedHouseholdList(int reportId, int start, int length, string orderBy, string searchText)
        {
            try
            {
                string whereClause = "";
                string completeQuery = "";
                string limitQuery = " OFFSET " + start + " ROWS FETCH FIRST " + length + " ROWS ONLY";
                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = " WHERE HouseholdName like '%" + searchText + "%' or MobileNumber like '%" + searchText + "%' or UniqueSetCode like '%" + searchText + "%' or DistrictName like '%" + searchText + "%' or BlockName like '%" + searchText + "%' or VillageName like '%" + searchText + "%' or UpdatedOn like '%" + searchText + "%'";
                }
                completeQuery = string.Format("{0} {1} {2}", whereClause, orderBy, limitQuery);
                var dataTable = await _reportRepository.RPT_GetUpdatedHouseholdList(reportId, completeQuery);

                return ExtensionMethods.ConvertToList<RPT_GetUpdatedHousehold_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> RPT_GetUpdatedHouseholdCount(int reportId, string searchText)
        {
            int recordsFiltered = 0;
            try
            {
                string whereClause = string.Empty;
                if (!string.IsNullOrEmpty(searchText))
                {
                    whereClause = " WHERE HouseholdName like '%" + searchText + "%' or MobileNumber like '%" + searchText + "%' or UniqueSetCode like '%" + searchText + "%' or DistrictName like '%" + searchText + "%' or BlockName like '%" + searchText + "%' or VillageName like '%" + searchText + "%' or UpdatedOn like '%" + searchText + "%'";
                }

                var dataTable = await _reportRepository.RPT_GetUpdatedHouseholdCount(reportId, whereClause);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    recordsFiltered = Convert.ToString(dataTable.Rows[0][0]) == "" ? 0 : Convert.ToInt32(dataTable.Rows[0][0]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return recordsFiltered;
        }

        public async Task<List<RPT_GetUpdatedHousehold_Result>> RPT_ExportUpdatedHouseholdList(int reportId)
        {
            try
            {
                var dataTable = await _reportRepository.RPT_ExportUpdatedHouseholdList(reportId);
                return ExtensionMethods.ConvertToList<RPT_GetUpdatedHousehold_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> MakeFevoriteReport(int reportId, int userId)
        {
            try
            {
                var reportsFavourite = await _genericRepository.GetFirstOrDefaultAsync<ReportsFavourite>(x => x.UserId == userId);
                if (reportsFavourite != null)
                {
                    reportsFavourite.ReportId = reportId;
                    reportsFavourite.UserId = userId;
                    await _genericRepository.UpdateAsync(reportsFavourite);
                }
                else
                {
                    var favourite = new ReportsFavourite()
                    {
                        ReportId = reportId,
                        UserId = userId
                    };
                    await _genericRepository.InsertAsync(favourite);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<ReportModel> GetFevoriteReport(int userId)
        {
            try
            {
                var reportsFavourite = await _genericRepository.GetFirstOrDefaultAsync<ReportsFavourite>(x => x.UserId == userId);
                if (reportsFavourite != null)
                {
                    var report = await _genericRepository.GetFirstOrDefaultAsync<Report>(x => x.ReportId == reportsFavourite.ReportId);
                    return _mapper.Map<ReportModel>(report);
                }
                else
                    return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataTable> GenerateReportAsync(int reportId, int userId)
        {
            try
            {
                return await _reportRepository.GenerateReportAsync(reportId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataSet> GenerateReport(int reportId, int userId)
        {
            try
            {
                return await _reportRepository.GenerateReport(reportId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetGenerateReportDetails_Result>> GetGenerateReportDetailsAsync(int reportId)
        {
            try
            {
                var dataTable = await _reportRepository.GetGenerateReportDetailsAsync(reportId);
                return ExtensionMethods.ConvertToList<GetGenerateReportDetails_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetMaximumIndicator(int activityCategoryMappingId)
        {
            try
            {
                int maxLimit = 100;
                var hhCount = _genericRepository.GetAsync<ActivityQuestionSetUniqueIdentity>(x => x.ActivityCategoryMappingId == activityCategoryMappingId && x.IsActive == true).Result.Count();

                var systemConfiguration = await _genericRepository.GetFirstOrDefaultAsync<SystemConfiguration>(x => x.LowerRange <= hhCount && x.UpperRange >= hhCount);
                if (systemConfiguration != null)
                {
                    maxLimit = (int)systemConfiguration.MaxLimit;
                }
                return maxLimit;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
