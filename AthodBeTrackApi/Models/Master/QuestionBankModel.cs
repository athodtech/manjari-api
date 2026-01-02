using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Models
{
    public class QuestionBankModel : BaseModel
    {
        public int QuestionId { get; set; }
        [Required]
        [StringLength(1000)]
        public string Question { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionType { get; set; }
        public int? ParentQuestionId { get; set; }
        public bool Mandatory { get; set; }
        public int? Sort { get; set; }
        public int? LanguageId { get; set; } = 1;
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }
        [StringLength(200)]
        public string Constraint { get; set; }
        [StringLength(200)]
        public string Format { get; set; }
        public bool? IsFormula { get; set; }
        [StringLength(200)]
        public string Formula { get; set; }
        public bool? HavingChild { get; set; }    
        public int? ReportingFrequencyTypeId { get; set; }
        public bool? TimeStampManual { get; set; }
        public string Help { get; set; }
        public bool? SkipLogic { get; set; }
        public int? SkipQuestionId { get; set; }
        public string SkipQuestionIds { get; set; }
        [StringLength(100)]
        public string SkipLogicDetail { get; set; }
        public string TagIds { get; set; }
        public int? QuestionChoiceId { get; set; }
        [StringLength(150)]
        public string PalceHolder { get; set; }

    }
}
