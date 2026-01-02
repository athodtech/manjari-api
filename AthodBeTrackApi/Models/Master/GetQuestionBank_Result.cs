using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class GetQuestionBank_Result
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionType { get; set; }
        public int? ParentQuestionId { get; set; }
        public bool Mandatory { get; set; }
        public int? Sort { get; set; }
        public int? LanguageId { get; set; } = 1;
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        public string Constraint { get; set; }
        public string Format { get; set; }
        public bool? IsFormula { get; set; }
        public string Formula { get; set; }
        public bool? HavingChild { get; set; }
        public int? ReportingFrequencyTypeId { get; set; }
        public bool? TimeStampManual { get; set; }
        public string Help { get; set; }
        public bool? SkipLogic { get; set; }
        public int? SkipQuestionId { get; set; }
        public string SkipLogicDetail { get; set; }
        public int? QuestionChoiceId { get; set; }
        public string QuestionChoiceName { get; set; }
        public string TagIds { get; set; }
        public string Tags { get; set; }
        public bool IsActive { get; set; }
        public string PalceHolder { get; set; }
        public string Theme { get; set; }

    }
}
