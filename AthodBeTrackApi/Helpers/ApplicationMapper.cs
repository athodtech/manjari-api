using AthodBeTrackApi.Data;
using AthodBeTrackApi.Models;
using AutoMapper;
using Category = AthodBeTrackApi.Data.Category;

namespace AthodBeTrackApi.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {

            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, UserModel>()
              .ForMember(d => d.RoleName, o => o.MapFrom(s => s.Role.RoleName))
              .ReverseMap();

            CreateMap<UserLog, UserLogModel>().ReverseMap();

            #region Master
            CreateMap<Role, RoleModel>().ReverseMap();
            CreateMap<EmailConfiguration, EmailConfigModel>().ReverseMap();
            CreateMap<QuestionType, QuestionTypeModel>().ReverseMap();
            CreateMap<QuestionChoice, QuestionChoiceModel>().ReverseMap();
            CreateMap<QuestionChoiceItem, QuestionChoiceItemModel>().ReverseMap();
            CreateMap<QuestionGroup, QuestionGroupModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<QuestionBank, QuestionBankModel>()
             .ForMember(d => d.QuestionType, o => o.MapFrom(s => s.QuestionType.Type))
             .ReverseMap();
            CreateMap<Tag, TagModel>().ReverseMap();
            CreateMap<ApplicationLogging, ApplicationLoggingModel>().ReverseMap();
            #endregion

            #region User Location
            CreateMap<UserLocation, UserLocationModel>().ReverseMap();
            CreateMap<UserLocationLog, UserLocationLogModel>();

            #endregion

            #region Location
            CreateMap<State, StateModel>().ReverseMap();
            CreateMap<District, DistrictModel>()
              .ForMember(d => d.StateName, o => o.MapFrom(s => s.State.StateName))
              .ReverseMap();
            CreateMap<Block, BlockModel>()
              .ForMember(d => d.StateName, o => o.MapFrom(s => s.State.StateName))
              .ForMember(d => d.DistrictName, o => o.MapFrom(s => s.District.DistrictName))
              .ReverseMap();
            CreateMap<Village, VillageModel>()
             .ForMember(d => d.StateName, o => o.MapFrom(s => s.State.StateName))
             .ForMember(d => d.DistrictName, o => o.MapFrom(s => s.District.DistrictName))
             .ForMember(d => d.BlockName, o => o.MapFrom(s => s.Block.BlockName))
             .ReverseMap();

            #endregion

            #region Activity
            CreateMap<Activity, ActivityModel>().ReverseMap();
            CreateMap<ActivityQuestion, ActivityQuestionModel>()
               .ForMember(d => d.QuestionName, o => o.MapFrom(s => s.Question.Question))
               .ForMember(d => d.QuestionType, o => o.MapFrom(s => s.Question.QuestionType.Type))
               .ReverseMap();
            CreateMap<ActivityQuestionSetValue, ActivityQuestionSetValueModel>().ReverseMap();
            CreateMap<ActivityQuestionSetUniqueIdentity, ActivityQuestionSetUniqueIdentityModel>().ReverseMap();
            CreateMap<HouseholdFilter, HouseholdFilterModel>().ReverseMap();

            #endregion

            #region Report
            CreateMap<Report, ReportModel>().ReverseMap();
            CreateMap<ReportSummary, ReportSummaryModel>().ReverseMap();
            #endregion

            #region Indicator Due
            CreateMap<DueReportSummary, DueReportSummaryModel>().ReverseMap();

            #endregion
            #region For Dropdowns

            CreateMap<Role, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.RoleId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.RoleName));

            CreateMap<State, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.StateId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.StateName));

            CreateMap<District, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.DistrictId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.DistrictName));

            CreateMap<Block, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.BlockId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.BlockName));

            CreateMap<Village, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.VillageId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.VillageName));

            CreateMap<QuestionType, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.value, o => o.MapFrom(s => s.Type));

            CreateMap<QuestionChoice, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.QuestionChoiceId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.QuestionChoiceName));

            CreateMap<QuestionChoiceItem, SelectListModel>()
           .ForMember(d => d.id, o => o.MapFrom(s => s.Value))
           .ForMember(d => d.value, o => o.MapFrom(s => s.Item));

            CreateMap<Activity, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.ActivityId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.ActivityName));

            CreateMap<User, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.UserId))
            .ForMember(d => d.value, o => o.MapFrom(s => (s.FirstName + ' ' + s.LastName)));

            CreateMap<User, SelectList3Model>()
           .ForMember(d => d.id, o => o.MapFrom(s => s.UserId))
           .ForMember(d => d.value, o => o.MapFrom(s => (s.FirstName + ' ' + s.LastName)))
           .ForMember(d => d.isActive, o => o.MapFrom(s => s.IsActive));

            CreateMap<QuestionGroup, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.GroupId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.GroupName));

            CreateMap<QuestionBank, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.QuestionId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.Question));

            CreateMap<QuestionReportingFrequency, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.ReportingFrequencyTypeId))
            .ForMember(d => d.value, o => o.MapFrom(s => s.ReportingFrequencyType));

            CreateMap<Tag, SelectListModel>()
            .ForMember(d => d.id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.value, o => o.MapFrom(s => s.Name));

            CreateMap<ApplicationEventType, SelectListModel>()
           .ForMember(d => d.id, o => o.MapFrom(s => s.Id))
           .ForMember(d => d.value, o => o.MapFrom(s => s.Value));

            CreateMap<Chart, ChartModel>().ReverseMap();

            #endregion

            CreateMap<EmailConfiguration, EmailMessage>()
                    .ForMember(d => d.From, o => o.MapFrom(s => $"{s.FriendlyName}<{s.SenderEmail}>"))
                    .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
                    .ForMember(d => d.Password, o => o.MapFrom(s => s.Password))
                    .ForMember(d => d.SmtpClientHost, o => o.MapFrom(s => s.Server))
                    .ForMember(d => d.SmtpPort, o => o.MapFrom(s => s.Port))
                    .ForMember(d => d.enableSSL, o => o.MapFrom(s => s.Sslstatus))
                    .ForMember(d => d.FriendlyName, o => o.MapFrom(s => s.FriendlyName));
        }
    }
}
