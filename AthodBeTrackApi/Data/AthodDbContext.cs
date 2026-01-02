using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace AthodBeTrackApi.Data
{
    public partial class AthodDbContext : DbContext
    {
        public AthodDbContext()
        {
        }

        public AthodDbContext(DbContextOptions<AthodDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityAssign> ActivityAssigns { get; set; }
        public virtual DbSet<ActivityCategoryMapping> ActivityCategoryMappings { get; set; }
        public virtual DbSet<ActivityDocument> ActivityDocuments { get; set; }
        public virtual DbSet<ActivityDocumentTagMapping> ActivityDocumentTagMappings { get; set; }
        public virtual DbSet<ActivityQuestion> ActivityQuestions { get; set; }
        public virtual DbSet<ActivityQuestionGroupMapping> ActivityQuestionGroupMappings { get; set; }
        public virtual DbSet<ActivityQuestionSetGroupMapping> ActivityQuestionSetGroupMappings { get; set; }
        public virtual DbSet<ActivityQuestionSetUniqueIdentity> ActivityQuestionSetUniqueIdentities { get; set; }
        public virtual DbSet<ActivityQuestionSetValue> ActivityQuestionSetValues { get; set; }
        public virtual DbSet<ActivityQuestionSetValue2> ActivityQuestionSetValue2s { get; set; }
        public virtual DbSet<ActivityQuestionSetValueLog> ActivityQuestionSetValueLogs { get; set; }
        public virtual DbSet<ApiUserRefreshToken> ApiUserRefreshTokens { get; set; }
        public virtual DbSet<ApplicationEventType> ApplicationEventTypes { get; set; }
        public virtual DbSet<ApplicationLogging> ApplicationLoggings { get; set; }
        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Chart> Charts { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<DueReportDetail> DueReportDetails { get; set; }
        public virtual DbSet<DueReportSummary> DueReportSummaries { get; set; }
        public virtual DbSet<EmailConfiguration> EmailConfigurations { get; set; }
        public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public virtual DbSet<GenerateReportDetail> GenerateReportDetails { get; set; }
        public virtual DbSet<Hhmistemplate> Hhmistemplates { get; set; }
        public virtual DbSet<HouseholdFilter> HouseholdFilters { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<MailProcessContent> MailProcessContents { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Panchayat> Panchayats { get; set; }
        public virtual DbSet<Process> Processes { get; set; }
        public virtual DbSet<QuestionBank> QuestionBanks { get; set; }
        public virtual DbSet<QuestionChoice> QuestionChoices { get; set; }
        public virtual DbSet<QuestionChoiceItem> QuestionChoiceItems { get; set; }
        public virtual DbSet<QuestionChoiceMapping> QuestionChoiceMappings { get; set; }
        public virtual DbSet<QuestionGroup> QuestionGroups { get; set; }
        public virtual DbSet<QuestionReportingFrequency> QuestionReportingFrequencies { get; set; }
        public virtual DbSet<QuestionTagMapping> QuestionTagMappings { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<ReportChartTemplate> ReportChartTemplates { get; set; }
        public virtual DbSet<ReportItemTemplate> ReportItemTemplates { get; set; }
        public virtual DbSet<ReportSummary> ReportSummaries { get; set; }
        public virtual DbSet<ReportsFavourite> ReportsFavourites { get; set; }
        public virtual DbSet<ReportsShareLog> ReportsShareLogs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleRight> RoleRights { get; set; }
        public virtual DbSet<RoleRightsAttribute> RoleRightsAttributes { get; set; }
        public virtual DbSet<RptGenerateReportTemp> RptGenerateReportTemps { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<SystemConfiguration> SystemConfigurations { get; set; }
        public virtual DbSet<SystemStartingNumber> SystemStartingNumbers { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TblTest> TblTests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserActionLogHh> UserActionLogHhs { get; set; }
        public virtual DbSet<UserLocation> UserLocations { get; set; }
        public virtual DbSet<UserLocationLog> UserLocationLogs { get; set; }
        public virtual DbSet<UserLog> UserLogs { get; set; }
        public virtual DbSet<UserRight> UserRights { get; set; }
        public virtual DbSet<UserRightsAttribute> UserRightsAttributes { get; set; }
        public virtual DbSet<ViewHhquestionValueCheck> ViewHhquestionValueChecks { get; set; }
        public virtual DbSet<Village> Villages { get; set; }
        public virtual DbSet<VillagePanchayatMapping> VillagePanchayatMappings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                  .AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DbConnection"),
                sqlServerOptions => sqlServerOptions.CommandTimeout(900));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ActivityCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ActivityUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Activity_User1");
            });

            modelBuilder.Entity<ActivityAssign>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ActivityAssignCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityAssign_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ActivityAssignUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ActivityAssign_User1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ActivityAssignUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ActivityAssign_User2");
            });

            modelBuilder.Entity<ActivityCategoryMapping>(entity =>
            {
                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.ActivityCategoryMappings)
                    .HasForeignKey(d => d.ActivityId)
                    .HasConstraintName("FK_ActivityCategoryMapping_Activity");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ActivityCategoryMappings)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_ActivityCategoryMapping_Category");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ActivityCategoryMappingCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityCategoryMapping_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ActivityCategoryMappingUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ActivityCategoryMapping_User1");
            });

            modelBuilder.Entity<ActivityDocument>(entity =>
            {
                entity.Property(e => e.DocumentDescription).IsUnicode(false);

                entity.Property(e => e.InternalDocumentName).IsUnicode(false);

                entity.Property(e => e.OriginalDocumentName).IsUnicode(false);

                entity.HasOne(d => d.ActivityQuestionSet)
                    .WithMany(p => p.ActivityDocuments)
                    .HasForeignKey(d => d.ActivityQuestionSetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityDocument_ActivityQuestionSetUniqueIdentity");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ActivityDocumentCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityDocument_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ActivityDocumentUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ActivityDocument_User1");
            });

            modelBuilder.Entity<ActivityDocumentTagMapping>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ActivityDocumentTagMappingCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityDocumentTagMapping_User");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.ActivityDocumentTagMappings)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_ActivityDocumentTagMapping_TagId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ActivityDocumentTagMappingUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ActivityDocumentTagMapping_User1");
            });

            modelBuilder.Entity<ActivityQuestion>(entity =>
            {
                entity.Property(e => e.Primary).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ActivityCategoryMapping)
                    .WithMany(p => p.ActivityQuestions)
                    .HasForeignKey(d => d.ActivityCategoryMappingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityQuestion_Activity");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ActivityQuestionCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityQuestion_User");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.ActivityQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityQuestion_QuestionBank");

                entity.HasOne(d => d.ReportingFrequencyType)
                    .WithMany(p => p.ActivityQuestions)
                    .HasForeignKey(d => d.ReportingFrequencyTypeId)
                    .HasConstraintName("FK_ActivityQuestion_QuestionReportingFrequency");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ActivityQuestionUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ActivityQuestion_User1");
            });

            modelBuilder.Entity<ActivityQuestionGroupMapping>(entity =>
            {
                entity.HasOne(d => d.ActivityQuestion)
                    .WithMany(p => p.ActivityQuestionGroupMappings)
                    .HasForeignKey(d => d.ActivityQuestionId)
                    .HasConstraintName("FK_ActivityQuestionGroupMapping_ActivityQuestion");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ActivityQuestionGroupMappingCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionGroupMapping_User");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ActivityQuestionGroupMappings)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_QuestionGroupMapping_GroupId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ActivityQuestionGroupMappingUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_QuestionGroupMapping_User1");
            });

            modelBuilder.Entity<ActivityQuestionSetGroupMapping>(entity =>
            {
                entity.HasKey(e => e.ActivityQuestionSetGmid)
                    .HasName("PK_QuestionSetGMId");

                entity.HasOne(d => d.ActivityQuestionSet)
                    .WithMany(p => p.ActivityQuestionSetGroupMappings)
                    .HasForeignKey(d => d.ActivityQuestionSetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityQuestionSetGroupMapping_ActivityQuestionSetUniqueIdentity");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ActivityQuestionSetGroupMappingCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityQuestionSetGroupMapping_User1");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ActivityQuestionSetGroupMappings)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityQuestionSetGroupMapping_QuestionGroup");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ActivityQuestionSetGroupMappingUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ActivityQuestionSetGroupMapping_User");
            });

            modelBuilder.Entity<ActivityQuestionSetUniqueIdentity>(entity =>
            {
                entity.HasKey(e => e.ActivityQuestionSetId)
                    .HasName("PK_QuestionSetUniqueIdentity");

                entity.Property(e => e.Column1).HasComment("HouseholdName");

                entity.Property(e => e.Column2).HasComment("MobileNumber");

                entity.Property(e => e.UniqueSetCode).IsUnicode(false);

                entity.HasOne(d => d.ActivityCategoryMapping)
                    .WithMany(p => p.ActivityQuestionSetUniqueIdentities)
                    .HasForeignKey(d => d.ActivityCategoryMappingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityQuestionSetUniqueIdentity_ActivityCategoryMapping");

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.ActivityQuestionSetUniqueIdentities)
                    .HasForeignKey(d => d.BlockId)
                    .HasConstraintName("FK_ActivityQuestionSetUniqueIdentity_Block");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ActivityQuestionSetUniqueIdentityCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionSetUniqueIdentity_User");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.ActivityQuestionSetUniqueIdentities)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_ActivityQuestionSetUniqueIdentity_District");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.ActivityQuestionSetUniqueIdentities)
                    .HasForeignKey(d => d.Stateid)
                    .HasConstraintName("FK_ActivityQuestionSetUniqueIdentity_State");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ActivityQuestionSetUniqueIdentityUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_QuestionSetUniqueIdentity_User1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ActivityQuestionSetUniqueIdentityUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionSetUniqueIdentity_User2");

                entity.HasOne(d => d.Village)
                    .WithMany(p => p.ActivityQuestionSetUniqueIdentities)
                    .HasForeignKey(d => d.VillageId)
                    .HasConstraintName("FK_ActivityQuestionSetUniqueIdentity_Village");
            });

            modelBuilder.Entity<ActivityQuestionSetValue>(entity =>
            {
                entity.HasKey(e => new { e.ActivityQuestionSetId, e.ActivityQuestionId, e.Sno });
            });

            modelBuilder.Entity<ActivityQuestionSetValueLog>(entity =>
            {
                entity.HasKey(e => new { e.ActivityQuestionSetId, e.ActivityQuestionId, e.Sno, e.StartDate, e.EndDate })
                    .HasName("PK_ActivityQuestionSetValueLog_1");
            });

            modelBuilder.Entity<ApiUserRefreshToken>(entity =>
            {
                entity.Property(e => e.IpAddress).IsUnicode(false);

                entity.Property(e => e.SessionId).IsUnicode(false);
            });

            modelBuilder.Entity<ApplicationLogging>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ApplicationLoggingCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationLogging_User");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.ApplicationLoggings)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationLogging_AppliactionEventType");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ApplicationLoggingUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ApplicationLogging_User1");
            });

            modelBuilder.Entity<Block>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.BlockCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Block_User");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Blocks)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Block_District");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Blocks)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Block_State");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.BlockUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Block_User1");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.CategoryCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.CategoryUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Category_User1");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DistrictCode).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.DistrictCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_User_District_CreatedBy");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_State");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.DistrictUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_User_District_UpdatedBy");
            });

            modelBuilder.Entity<DueReportDetail>(entity =>
            {
                entity.Property(e => e.BlockId).IsFixedLength(true);

                entity.Property(e => e.DistrictId).IsFixedLength(true);

                entity.Property(e => e.GroupName).IsUnicode(false);

                entity.Property(e => e.ReportingFrequecyType).IsUnicode(false);

                entity.Property(e => e.Stateid).IsFixedLength(true);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.UniqueSetCode).IsUnicode(false);

                entity.Property(e => e.VillageId).IsFixedLength(true);
            });

            modelBuilder.Entity<DueReportSummary>(entity =>
            {
                entity.Property(e => e.GroupName).IsUnicode(false);

                entity.Property(e => e.ReportingFrequecyType).IsUnicode(false);
            });

            modelBuilder.Entity<EmailConfiguration>(entity =>
            {
                entity.Property(e => e.FriendlyName).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.SenderEmail).IsUnicode(false);

                entity.Property(e => e.Server).IsUnicode(false);

                entity.Property(e => e.Signature).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);
            });

            modelBuilder.Entity<ExceptionLog>(entity =>
            {
                entity.Property(e => e.ErrorMessage).IsUnicode(false);

                entity.Property(e => e.ErrorProcedure).IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<HouseholdFilter>(entity =>
            {
                entity.Property(e => e.Days).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.HouseholdFilterCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HouseholdFilter_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.HouseholdFilterUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_HouseholdFilter_User1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.HouseholdFilterUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HouseholdFilter_User2");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.LanguageName).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.LanguageCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Language_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.LanguageUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Language_User1");
            });

            modelBuilder.Entity<MailProcessContent>(entity =>
            {
                entity.HasOne(d => d.Process)
                    .WithMany(p => p.MailProcessContents)
                    .HasForeignKey(d => d.ProcessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MailProcessContent_Process");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Display).IsUnicode(false);

                entity.Property(e => e.Icon).IsUnicode(false);

                entity.Property(e => e.Label).IsUnicode(false);

                entity.Property(e => e.Link).IsUnicode(false);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.NotificationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Notification_User");

                entity.HasOne(d => d.NotificationToNavigation)
                    .WithMany(p => p.NotificationNotificationToNavigations)
                    .HasForeignKey(d => d.NotificationTo)
                    .HasConstraintName("FK_Notification_User1");
            });

            modelBuilder.Entity<Panchayat>(entity =>
            {
                entity.Property(e => e.PanchayatCode).IsUnicode(false);

                entity.Property(e => e.PanchayatName).IsUnicode(false);
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.Property(e => e.ProcessId).ValueGeneratedNever();

                entity.Property(e => e.ProcessName).IsUnicode(false);
            });

            modelBuilder.Entity<QuestionBank>(entity =>
            {
                entity.Property(e => e.Primary).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.QuestionBankCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionBank_User");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.QuestionBanks)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_QuestionBank_Language");

                entity.HasOne(d => d.QuestionType)
                    .WithMany(p => p.QuestionBanks)
                    .HasForeignKey(d => d.QuestionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionBank_QuestionType");

                entity.HasOne(d => d.ReportingFrequencyType)
                    .WithMany(p => p.QuestionBanks)
                    .HasForeignKey(d => d.ReportingFrequencyTypeId)
                    .HasConstraintName("FK_QuestionBank_QuestionReportingFrequency");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.QuestionBankUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_QuestionBank_User1");
            });

            modelBuilder.Entity<QuestionChoice>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.QuestionChoiceCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionChoice_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.QuestionChoiceUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_QuestionChoice_User1");
            });

            modelBuilder.Entity<QuestionChoiceItem>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.QuestionChoiceItemCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionChoiceItem_User");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.QuestionChoiceItems)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_QuestionChoiceItem_Language");

                entity.HasOne(d => d.QuestionChoice)
                    .WithMany(p => p.QuestionChoiceItems)
                    .HasForeignKey(d => d.QuestionChoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionChoiceItem_QuestionChoice");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.QuestionChoiceItemUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_QuestionChoiceItem_User1");
            });

            modelBuilder.Entity<QuestionChoiceMapping>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.QuestionChoiceMappingCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionChoiceMapping_User");

                entity.HasOne(d => d.QuestionChoice)
                    .WithMany(p => p.QuestionChoiceMappings)
                    .HasForeignKey(d => d.QuestionChoiceId)
                    .HasConstraintName("FK_QuestionChoiceMapping_QuestionChoice");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionChoiceMappings)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionChoiceMapping_QuestionBank");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.QuestionChoiceMappingUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_QuestionChoiceMapping_User1");
            });

            modelBuilder.Entity<QuestionGroup>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.QuestionGroupCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionGroup_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.QuestionGroupUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_QuestionGroup_User1");
            });

            modelBuilder.Entity<QuestionReportingFrequency>(entity =>
            {
                entity.Property(e => e.ReportingFrequencyTypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<QuestionTagMapping>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.QuestionTagMappingCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionTagMapping_User");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.QuestionTagMappings)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_QuestionTagMapping_TagId");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.QuestionTagMappingUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_QuestionTagMapping_User1");
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.Property(e => e.Type).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.QuestionTypeCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionType_CreatedBy");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.QuestionTypeUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_QuestionType_UpdatedBy");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.AllBlock).HasDefaultValueSql("((0))");

                entity.Property(e => e.AllDistrict).HasDefaultValueSql("((0))");

                entity.Property(e => e.AllGroup).HasDefaultValueSql("((0))");

                entity.Property(e => e.AllQuestion).HasDefaultValueSql("((0))");

                entity.Property(e => e.AllState).HasDefaultValueSql("((0))");

                entity.Property(e => e.AllTag).HasDefaultValueSql("((0))");

                entity.Property(e => e.AllVillage).HasDefaultValueSql("((0))");

                entity.Property(e => e.BlockId).IsUnicode(false);

                entity.Property(e => e.DistrictId).IsUnicode(false);

                entity.Property(e => e.ReportFilterEnable).HasDefaultValueSql("((1))");

                entity.Property(e => e.ReportingGroupIds).IsUnicode(false);

                entity.Property(e => e.ReportingTagIds).IsUnicode(false);

                entity.Property(e => e.StateId).IsUnicode(false);

                entity.Property(e => e.VillageId).IsUnicode(false);
            });

            modelBuilder.Entity<ReportChartTemplate>(entity =>
            {
                entity.HasKey(e => new { e.ReportId, e.QuestionId, e.SortingOrder });

                entity.HasOne(d => d.ChartType)
                    .WithMany(p => p.ReportChartTemplates)
                    .HasForeignKey(d => d.ChartTypeId)
                    .HasConstraintName("FK_ReportChartTemplate_Chart");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ReportChartTemplateCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportChartTemplate_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ReportChartTemplateUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ReportChartTemplate_User1");
            });

            modelBuilder.Entity<ReportItemTemplate>(entity =>
            {
                entity.HasKey(e => new { e.ReportId, e.QuestionId });

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.ReportItemTemplateCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportItemTemplate_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.ReportItemTemplateUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ReportItemTemplate_User1");
            });

            modelBuilder.Entity<ReportSummary>(entity =>
            {
                entity.HasOne(d => d.ChartType)
                    .WithMany()
                    .HasForeignKey(d => d.ChartTypeId)
                    .HasConstraintName("FK_ReportSummary_Chart");

                entity.HasOne(d => d.Report)
                    .WithMany()
                    .HasForeignKey(d => d.ReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportSummary_Reports");
            });

            modelBuilder.Entity<ReportsFavourite>(entity =>
            {
                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportsFavourites)
                    .HasForeignKey(d => d.ReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportsFavourite_Reports");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReportsFavourites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportsFavourite_User");
            });

            modelBuilder.Entity<ReportsShareLog>(entity =>
            {
                entity.HasOne(d => d.Report)
                    .WithMany(p => p.ReportsShareLogs)
                    .HasForeignKey(d => d.ReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportsShareLogs_Reports");

                entity.HasOne(d => d.SharedByNavigation)
                    .WithMany(p => p.ReportsShareLogSharedByNavigations)
                    .HasForeignKey(d => d.SharedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportsShareLogs_User1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReportsShareLogUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportsShareLogs_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.RoleCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_CreatedBy");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.RoleUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Role_UpdatedBy");
            });

            modelBuilder.Entity<RoleRight>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.RoleRightCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleRights_CreatedBy_User");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.RoleRights)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleRights_Menu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleRights)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleRights_Role");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.RoleRightUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_RoleRights_UpdatedBy_User");
            });

            modelBuilder.Entity<RoleRightsAttribute>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.RoleRightsAttributeCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleRightsAttribute_User");

                entity.HasOne(d => d.RoleRight)
                    .WithMany(p => p.RoleRightsAttributes)
                    .HasForeignKey(d => d.RoleRightId)
                    .HasConstraintName("FK_RoleRightsAttribute_RoleRights");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.RoleRightsAttributeUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_RoleRightsAttribute_User1");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StateCode).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.StateCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_User_CreatedBy");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.StateUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_User_UpdatedBy");
            });

            modelBuilder.Entity<SystemStartingNumber>(entity =>
            {
                entity.HasKey(e => new { e.TableTransaction, e.Prefix })
                    .HasName("PK_SetupStartingNumber");

                entity.HasComment("Contains the starting number information");

                entity.Property(e => e.TableTransaction).HasComment("The transaction that owns this starting number");

                entity.Property(e => e.Prefix).HasComment("The prefix of the starting number");

                entity.Property(e => e.ColumnName).HasComment("The column name where the starting number will be entered");

                entity.Property(e => e.Counter).ValueGeneratedOnAdd();

                entity.Property(e => e.IsEnabled)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Determines if the starting number is enabled");

                entity.Property(e => e.IsMasterFile)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Determines if the transcation is a master file");

                entity.Property(e => e.IsPostingEntity)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Obsolete");

                entity.Property(e => e.Number)
                    .HasDefaultValueSql("((0))")
                    .HasComment("The current available number");

                entity.Property(e => e.NumberWidth)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Determines the length of the starting number");

                entity.Property(e => e.ParentEntity).HasComment("Obsolete");

                entity.Property(e => e.TableName).HasComment("The table containing the transaction");

                entity.Property(e => e.TransactionDescription).HasComment("Description of the transaction");

                entity.HasOne(d => d.UserCreatedNavigation)
                    .WithMany(p => p.SystemStartingNumberUserCreatedNavigations)
                    .HasForeignKey(d => d.UserCreated)
                    .HasConstraintName("FK_SystemStartingNumber_UserProfile");

                entity.HasOne(d => d.UserModifiedNavigation)
                    .WithMany(p => p.SystemStartingNumberUserModifiedNavigations)
                    .HasForeignKey(d => d.UserModified)
                    .HasConstraintName("FK_SystemStartingNumber_UserProfile1");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TagCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tag_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.TagUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Tag_User1");
            });

            modelBuilder.Entity<TblTest>(entity =>
            {
                entity.Property(e => e.QuestionValue).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailId).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.ImageName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.MobileNo).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<UserActionLogHh>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ActionLogTime, e.ActivityQuestionSetId, e.Status });
            });

            modelBuilder.Entity<UserLocation>(entity =>
            {
                entity.Property(e => e.BlockId).IsUnicode(false);

                entity.Property(e => e.DistrictId).IsUnicode(false);

                entity.Property(e => e.StateId).IsUnicode(false);

                entity.Property(e => e.VillageId).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UserLocationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLocation_User1");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.UserLocationUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_UserLocation_User2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLocationUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLocation_User");
            });

            modelBuilder.Entity<UserLocationLog>(entity =>
            {
                entity.HasOne(d => d.Block)
                    .WithMany(p => p.UserLocationLogs)
                    .HasForeignKey(d => d.BlockId)
                    .HasConstraintName("FK_UserLocationLog_Block");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UserLocationLogCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLocationLog_User");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.UserLocationLogs)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_UserLocationLog_District");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserLocationLogs)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLocationLog_State");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.UserLocationLogUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_UserLocationLog_User1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLocationLogUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLocationLog_User2");

                entity.HasOne(d => d.Village)
                    .WithMany(p => p.UserLocationLogs)
                    .HasForeignKey(d => d.VillageId)
                    .HasConstraintName("FK_UserLocationLog_Village");
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.SessionId).IsUnicode(false);
            });

            modelBuilder.Entity<UserRight>(entity =>
            {
                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UserRightCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRights_CreatedBy_User");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.UserRights)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRights_Menu");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRights)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRights_Role");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.UserRightUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_UserRights_UpdatedBy_User");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRightUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRights_User");
            });

            modelBuilder.Entity<UserRightsAttribute>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UserRightsAttributeCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRightsAttribute_User");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.UserRightsAttributeUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_UserRightsAttribute_User1");

                entity.HasOne(d => d.UserRight)
                    .WithMany(p => p.UserRightsAttributes)
                    .HasForeignKey(d => d.UserRightId)
                    .HasConstraintName("FK_UserRightsAttribute_UserRights");
            });

            modelBuilder.Entity<ViewHhquestionValueCheck>(entity =>
            {
                entity.ToView("view_HHQuestionValueCheck");
            });

            modelBuilder.Entity<Village>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.Villages)
                    .HasForeignKey(d => d.BlockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Village_Block");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.VillageCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Village_User");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Villages)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Village_District");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Villages)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Village_State");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.VillageUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_Village_User1");
            });

            modelBuilder.Entity<VillagePanchayatMapping>(entity =>
            {
                entity.HasKey(e => e.PanchayatId)
                    .HasName("PK_ID_VillagePanchayatMapping");

                entity.Property(e => e.PanchayatId).ValueGeneratedNever();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
