using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AthodBeTrackApi.Data
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            ActivityAssignCreatedByNavigations = new HashSet<ActivityAssign>();
            ActivityAssignUpdatedByNavigations = new HashSet<ActivityAssign>();
            ActivityAssignUsers = new HashSet<ActivityAssign>();
            ActivityCategoryMappingCreatedByNavigations = new HashSet<ActivityCategoryMapping>();
            ActivityCategoryMappingUpdatedByNavigations = new HashSet<ActivityCategoryMapping>();
            ActivityCreatedByNavigations = new HashSet<Activity>();
            ActivityDocumentCreatedByNavigations = new HashSet<ActivityDocument>();
            ActivityDocumentTagMappingCreatedByNavigations = new HashSet<ActivityDocumentTagMapping>();
            ActivityDocumentTagMappingUpdatedByNavigations = new HashSet<ActivityDocumentTagMapping>();
            ActivityDocumentUpdatedByNavigations = new HashSet<ActivityDocument>();
            ActivityQuestionCreatedByNavigations = new HashSet<ActivityQuestion>();
            ActivityQuestionGroupMappingCreatedByNavigations = new HashSet<ActivityQuestionGroupMapping>();
            ActivityQuestionGroupMappingUpdatedByNavigations = new HashSet<ActivityQuestionGroupMapping>();
            ActivityQuestionSetGroupMappingCreatedByNavigations = new HashSet<ActivityQuestionSetGroupMapping>();
            ActivityQuestionSetGroupMappingUpdatedByNavigations = new HashSet<ActivityQuestionSetGroupMapping>();
            ActivityQuestionSetUniqueIdentityCreatedByNavigations = new HashSet<ActivityQuestionSetUniqueIdentity>();
            ActivityQuestionSetUniqueIdentityUpdatedByNavigations = new HashSet<ActivityQuestionSetUniqueIdentity>();
            ActivityQuestionSetUniqueIdentityUsers = new HashSet<ActivityQuestionSetUniqueIdentity>();
            ActivityQuestionUpdatedByNavigations = new HashSet<ActivityQuestion>();
            ActivityUpdatedByNavigations = new HashSet<Activity>();
            ApplicationLoggingCreatedByNavigations = new HashSet<ApplicationLogging>();
            ApplicationLoggingUpdatedByNavigations = new HashSet<ApplicationLogging>();
            BlockCreatedByNavigations = new HashSet<Block>();
            BlockUpdatedByNavigations = new HashSet<Block>();
            CategoryCreatedByNavigations = new HashSet<Category>();
            CategoryUpdatedByNavigations = new HashSet<Category>();
            DistrictCreatedByNavigations = new HashSet<District>();
            DistrictUpdatedByNavigations = new HashSet<District>();
            HouseholdFilterCreatedByNavigations = new HashSet<HouseholdFilter>();
            HouseholdFilterUpdatedByNavigations = new HashSet<HouseholdFilter>();
            HouseholdFilterUsers = new HashSet<HouseholdFilter>();
            LanguageCreatedByNavigations = new HashSet<Language>();
            LanguageUpdatedByNavigations = new HashSet<Language>();
            NotificationCreatedByNavigations = new HashSet<Notification>();
            NotificationNotificationToNavigations = new HashSet<Notification>();
            QuestionBankCreatedByNavigations = new HashSet<QuestionBank>();
            QuestionBankUpdatedByNavigations = new HashSet<QuestionBank>();
            QuestionChoiceCreatedByNavigations = new HashSet<QuestionChoice>();
            QuestionChoiceItemCreatedByNavigations = new HashSet<QuestionChoiceItem>();
            QuestionChoiceItemUpdatedByNavigations = new HashSet<QuestionChoiceItem>();
            QuestionChoiceMappingCreatedByNavigations = new HashSet<QuestionChoiceMapping>();
            QuestionChoiceMappingUpdatedByNavigations = new HashSet<QuestionChoiceMapping>();
            QuestionChoiceUpdatedByNavigations = new HashSet<QuestionChoice>();
            QuestionGroupCreatedByNavigations = new HashSet<QuestionGroup>();
            QuestionGroupUpdatedByNavigations = new HashSet<QuestionGroup>();
            QuestionTagMappingCreatedByNavigations = new HashSet<QuestionTagMapping>();
            QuestionTagMappingUpdatedByNavigations = new HashSet<QuestionTagMapping>();
            QuestionTypeCreatedByNavigations = new HashSet<QuestionType>();
            QuestionTypeUpdatedByNavigations = new HashSet<QuestionType>();
            ReportChartTemplateCreatedByNavigations = new HashSet<ReportChartTemplate>();
            ReportChartTemplateUpdatedByNavigations = new HashSet<ReportChartTemplate>();
            ReportItemTemplateCreatedByNavigations = new HashSet<ReportItemTemplate>();
            ReportItemTemplateUpdatedByNavigations = new HashSet<ReportItemTemplate>();
            ReportsFavourites = new HashSet<ReportsFavourite>();
            ReportsShareLogSharedByNavigations = new HashSet<ReportsShareLog>();
            ReportsShareLogUsers = new HashSet<ReportsShareLog>();
            RoleCreatedByNavigations = new HashSet<Role>();
            RoleRightCreatedByNavigations = new HashSet<RoleRight>();
            RoleRightUpdatedByNavigations = new HashSet<RoleRight>();
            RoleRightsAttributeCreatedByNavigations = new HashSet<RoleRightsAttribute>();
            RoleRightsAttributeUpdatedByNavigations = new HashSet<RoleRightsAttribute>();
            RoleUpdatedByNavigations = new HashSet<Role>();
            StateCreatedByNavigations = new HashSet<State>();
            StateUpdatedByNavigations = new HashSet<State>();
            SystemStartingNumberUserCreatedNavigations = new HashSet<SystemStartingNumber>();
            SystemStartingNumberUserModifiedNavigations = new HashSet<SystemStartingNumber>();
            TagCreatedByNavigations = new HashSet<Tag>();
            TagUpdatedByNavigations = new HashSet<Tag>();
            UserLocationCreatedByNavigations = new HashSet<UserLocation>();
            UserLocationLogCreatedByNavigations = new HashSet<UserLocationLog>();
            UserLocationLogUpdatedByNavigations = new HashSet<UserLocationLog>();
            UserLocationLogUsers = new HashSet<UserLocationLog>();
            UserLocationUpdatedByNavigations = new HashSet<UserLocation>();
            UserLocationUsers = new HashSet<UserLocation>();
            UserRightCreatedByNavigations = new HashSet<UserRight>();
            UserRightUpdatedByNavigations = new HashSet<UserRight>();
            UserRightUsers = new HashSet<UserRight>();
            UserRightsAttributeCreatedByNavigations = new HashSet<UserRightsAttribute>();
            UserRightsAttributeUpdatedByNavigations = new HashSet<UserRightsAttribute>();
            VillageCreatedByNavigations = new HashSet<Village>();
            VillageUpdatedByNavigations = new HashSet<Village>();
        }

        [Key]
        public int UserId { get; set; }
        public int RoleId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(200)]
        public string Password { get; set; }
        [StringLength(10)]
        public string MobileNo { get; set; }
        [Required]
        [Column("EmailID")]
        [StringLength(50)]
        public string EmailId { get; set; }
        [StringLength(50)]
        public string ImageName { get; set; }
        public int? DefaultMenuId { get; set; }
        [StringLength(500)]
        public string AboutUs { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(256)]
        public string Location { get; set; }
        [StringLength(256)]
        public string Organization { get; set; }
        public DateTime? ResetPasswrodTime { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("Users")]
        public virtual Role Role { get; set; }
        [InverseProperty(nameof(ActivityAssign.CreatedByNavigation))]
        public virtual ICollection<ActivityAssign> ActivityAssignCreatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityAssign.UpdatedByNavigation))]
        public virtual ICollection<ActivityAssign> ActivityAssignUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityAssign.User))]
        public virtual ICollection<ActivityAssign> ActivityAssignUsers { get; set; }
        [InverseProperty(nameof(ActivityCategoryMapping.CreatedByNavigation))]
        public virtual ICollection<ActivityCategoryMapping> ActivityCategoryMappingCreatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityCategoryMapping.UpdatedByNavigation))]
        public virtual ICollection<ActivityCategoryMapping> ActivityCategoryMappingUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(Activity.CreatedByNavigation))]
        public virtual ICollection<Activity> ActivityCreatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityDocument.CreatedByNavigation))]
        public virtual ICollection<ActivityDocument> ActivityDocumentCreatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityDocumentTagMapping.CreatedByNavigation))]
        public virtual ICollection<ActivityDocumentTagMapping> ActivityDocumentTagMappingCreatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityDocumentTagMapping.UpdatedByNavigation))]
        public virtual ICollection<ActivityDocumentTagMapping> ActivityDocumentTagMappingUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityDocument.UpdatedByNavigation))]
        public virtual ICollection<ActivityDocument> ActivityDocumentUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityQuestion.CreatedByNavigation))]
        public virtual ICollection<ActivityQuestion> ActivityQuestionCreatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityQuestionGroupMapping.CreatedByNavigation))]
        public virtual ICollection<ActivityQuestionGroupMapping> ActivityQuestionGroupMappingCreatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityQuestionGroupMapping.UpdatedByNavigation))]
        public virtual ICollection<ActivityQuestionGroupMapping> ActivityQuestionGroupMappingUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityQuestionSetGroupMapping.CreatedByNavigation))]
        public virtual ICollection<ActivityQuestionSetGroupMapping> ActivityQuestionSetGroupMappingCreatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityQuestionSetGroupMapping.UpdatedByNavigation))]
        public virtual ICollection<ActivityQuestionSetGroupMapping> ActivityQuestionSetGroupMappingUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityQuestionSetUniqueIdentity.CreatedByNavigation))]
        public virtual ICollection<ActivityQuestionSetUniqueIdentity> ActivityQuestionSetUniqueIdentityCreatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityQuestionSetUniqueIdentity.UpdatedByNavigation))]
        public virtual ICollection<ActivityQuestionSetUniqueIdentity> ActivityQuestionSetUniqueIdentityUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(ActivityQuestionSetUniqueIdentity.User))]
        public virtual ICollection<ActivityQuestionSetUniqueIdentity> ActivityQuestionSetUniqueIdentityUsers { get; set; }
        [InverseProperty(nameof(ActivityQuestion.UpdatedByNavigation))]
        public virtual ICollection<ActivityQuestion> ActivityQuestionUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(Activity.UpdatedByNavigation))]
        public virtual ICollection<Activity> ActivityUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(ApplicationLogging.CreatedByNavigation))]
        public virtual ICollection<ApplicationLogging> ApplicationLoggingCreatedByNavigations { get; set; }
        [InverseProperty(nameof(ApplicationLogging.UpdatedByNavigation))]
        public virtual ICollection<ApplicationLogging> ApplicationLoggingUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(Block.CreatedByNavigation))]
        public virtual ICollection<Block> BlockCreatedByNavigations { get; set; }
        [InverseProperty(nameof(Block.UpdatedByNavigation))]
        public virtual ICollection<Block> BlockUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(Category.CreatedByNavigation))]
        public virtual ICollection<Category> CategoryCreatedByNavigations { get; set; }
        [InverseProperty(nameof(Category.UpdatedByNavigation))]
        public virtual ICollection<Category> CategoryUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(District.CreatedByNavigation))]
        public virtual ICollection<District> DistrictCreatedByNavigations { get; set; }
        [InverseProperty(nameof(District.UpdatedByNavigation))]
        public virtual ICollection<District> DistrictUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(HouseholdFilter.CreatedByNavigation))]
        public virtual ICollection<HouseholdFilter> HouseholdFilterCreatedByNavigations { get; set; }
        [InverseProperty(nameof(HouseholdFilter.UpdatedByNavigation))]
        public virtual ICollection<HouseholdFilter> HouseholdFilterUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(HouseholdFilter.User))]
        public virtual ICollection<HouseholdFilter> HouseholdFilterUsers { get; set; }
        [InverseProperty(nameof(Language.CreatedByNavigation))]
        public virtual ICollection<Language> LanguageCreatedByNavigations { get; set; }
        [InverseProperty(nameof(Language.UpdatedByNavigation))]
        public virtual ICollection<Language> LanguageUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(Notification.CreatedByNavigation))]
        public virtual ICollection<Notification> NotificationCreatedByNavigations { get; set; }
        [InverseProperty(nameof(Notification.NotificationToNavigation))]
        public virtual ICollection<Notification> NotificationNotificationToNavigations { get; set; }
        [InverseProperty(nameof(QuestionBank.CreatedByNavigation))]
        public virtual ICollection<QuestionBank> QuestionBankCreatedByNavigations { get; set; }
        [InverseProperty(nameof(QuestionBank.UpdatedByNavigation))]
        public virtual ICollection<QuestionBank> QuestionBankUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(QuestionChoice.CreatedByNavigation))]
        public virtual ICollection<QuestionChoice> QuestionChoiceCreatedByNavigations { get; set; }
        [InverseProperty(nameof(QuestionChoiceItem.CreatedByNavigation))]
        public virtual ICollection<QuestionChoiceItem> QuestionChoiceItemCreatedByNavigations { get; set; }
        [InverseProperty(nameof(QuestionChoiceItem.UpdatedByNavigation))]
        public virtual ICollection<QuestionChoiceItem> QuestionChoiceItemUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(QuestionChoiceMapping.CreatedByNavigation))]
        public virtual ICollection<QuestionChoiceMapping> QuestionChoiceMappingCreatedByNavigations { get; set; }
        [InverseProperty(nameof(QuestionChoiceMapping.UpdatedByNavigation))]
        public virtual ICollection<QuestionChoiceMapping> QuestionChoiceMappingUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(QuestionChoice.UpdatedByNavigation))]
        public virtual ICollection<QuestionChoice> QuestionChoiceUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(QuestionGroup.CreatedByNavigation))]
        public virtual ICollection<QuestionGroup> QuestionGroupCreatedByNavigations { get; set; }
        [InverseProperty(nameof(QuestionGroup.UpdatedByNavigation))]
        public virtual ICollection<QuestionGroup> QuestionGroupUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(QuestionTagMapping.CreatedByNavigation))]
        public virtual ICollection<QuestionTagMapping> QuestionTagMappingCreatedByNavigations { get; set; }
        [InverseProperty(nameof(QuestionTagMapping.UpdatedByNavigation))]
        public virtual ICollection<QuestionTagMapping> QuestionTagMappingUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(QuestionType.CreatedByNavigation))]
        public virtual ICollection<QuestionType> QuestionTypeCreatedByNavigations { get; set; }
        [InverseProperty(nameof(QuestionType.UpdatedByNavigation))]
        public virtual ICollection<QuestionType> QuestionTypeUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(ReportChartTemplate.CreatedByNavigation))]
        public virtual ICollection<ReportChartTemplate> ReportChartTemplateCreatedByNavigations { get; set; }
        [InverseProperty(nameof(ReportChartTemplate.UpdatedByNavigation))]
        public virtual ICollection<ReportChartTemplate> ReportChartTemplateUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(ReportItemTemplate.CreatedByNavigation))]
        public virtual ICollection<ReportItemTemplate> ReportItemTemplateCreatedByNavigations { get; set; }
        [InverseProperty(nameof(ReportItemTemplate.UpdatedByNavigation))]
        public virtual ICollection<ReportItemTemplate> ReportItemTemplateUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(ReportsFavourite.User))]
        public virtual ICollection<ReportsFavourite> ReportsFavourites { get; set; }
        [InverseProperty(nameof(ReportsShareLog.SharedByNavigation))]
        public virtual ICollection<ReportsShareLog> ReportsShareLogSharedByNavigations { get; set; }
        [InverseProperty(nameof(ReportsShareLog.User))]
        public virtual ICollection<ReportsShareLog> ReportsShareLogUsers { get; set; }
        [InverseProperty("CreatedByNavigation")]
        public virtual ICollection<Role> RoleCreatedByNavigations { get; set; }
        [InverseProperty(nameof(RoleRight.CreatedByNavigation))]
        public virtual ICollection<RoleRight> RoleRightCreatedByNavigations { get; set; }
        [InverseProperty(nameof(RoleRight.UpdatedByNavigation))]
        public virtual ICollection<RoleRight> RoleRightUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(RoleRightsAttribute.CreatedByNavigation))]
        public virtual ICollection<RoleRightsAttribute> RoleRightsAttributeCreatedByNavigations { get; set; }
        [InverseProperty(nameof(RoleRightsAttribute.UpdatedByNavigation))]
        public virtual ICollection<RoleRightsAttribute> RoleRightsAttributeUpdatedByNavigations { get; set; }
        [InverseProperty("UpdatedByNavigation")]
        public virtual ICollection<Role> RoleUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(State.CreatedByNavigation))]
        public virtual ICollection<State> StateCreatedByNavigations { get; set; }
        [InverseProperty(nameof(State.UpdatedByNavigation))]
        public virtual ICollection<State> StateUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(SystemStartingNumber.UserCreatedNavigation))]
        public virtual ICollection<SystemStartingNumber> SystemStartingNumberUserCreatedNavigations { get; set; }
        [InverseProperty(nameof(SystemStartingNumber.UserModifiedNavigation))]
        public virtual ICollection<SystemStartingNumber> SystemStartingNumberUserModifiedNavigations { get; set; }
        [InverseProperty(nameof(Tag.CreatedByNavigation))]
        public virtual ICollection<Tag> TagCreatedByNavigations { get; set; }
        [InverseProperty(nameof(Tag.UpdatedByNavigation))]
        public virtual ICollection<Tag> TagUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(UserLocation.CreatedByNavigation))]
        public virtual ICollection<UserLocation> UserLocationCreatedByNavigations { get; set; }
        [InverseProperty(nameof(UserLocationLog.CreatedByNavigation))]
        public virtual ICollection<UserLocationLog> UserLocationLogCreatedByNavigations { get; set; }
        [InverseProperty(nameof(UserLocationLog.UpdatedByNavigation))]
        public virtual ICollection<UserLocationLog> UserLocationLogUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(UserLocationLog.User))]
        public virtual ICollection<UserLocationLog> UserLocationLogUsers { get; set; }
        [InverseProperty(nameof(UserLocation.UpdatedByNavigation))]
        public virtual ICollection<UserLocation> UserLocationUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(UserLocation.User))]
        public virtual ICollection<UserLocation> UserLocationUsers { get; set; }
        [InverseProperty(nameof(UserRight.CreatedByNavigation))]
        public virtual ICollection<UserRight> UserRightCreatedByNavigations { get; set; }
        [InverseProperty(nameof(UserRight.UpdatedByNavigation))]
        public virtual ICollection<UserRight> UserRightUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(UserRight.User))]
        public virtual ICollection<UserRight> UserRightUsers { get; set; }
        [InverseProperty(nameof(UserRightsAttribute.CreatedByNavigation))]
        public virtual ICollection<UserRightsAttribute> UserRightsAttributeCreatedByNavigations { get; set; }
        [InverseProperty(nameof(UserRightsAttribute.UpdatedByNavigation))]
        public virtual ICollection<UserRightsAttribute> UserRightsAttributeUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(Village.CreatedByNavigation))]
        public virtual ICollection<Village> VillageCreatedByNavigations { get; set; }
        [InverseProperty(nameof(Village.UpdatedByNavigation))]
        public virtual ICollection<Village> VillageUpdatedByNavigations { get; set; }
    }
}
