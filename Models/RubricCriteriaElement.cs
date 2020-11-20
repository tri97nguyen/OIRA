using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace parser.Models
{
    public class RubricCriteriaElement
    {
        public int Id { get; set; }

        public virtual RubricCriteria RubricCriteria { get; set; }
        public string RubricCriteriaId { get; set; }

        public string CriteriaText { get; set; }

        [Range(0, Int32.MaxValue)]
        public int ScoreValue { get; set; }
    }
}
