using Microsoft.EntityFrameworkCore;
using parser.Data;
using parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace parser
{
    public class RubricContentService
    {
        public static void ParseRubricContent(UploadRubricContent rubricContentData, AppDbContext context)
        {
            IEnumerable<string> content = Upload.ReadAsList(rubricContentData.Upload);
            string rubricInfo = content.First();
            var matchedRubric = getRubricName(rubricInfo, context);
            content = content.Skip(1).ToList();
            foreach (string line in content)
            {
                // split csv with double quote having double quote "" in column
                var column = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                RubricCriteria crit = new RubricCriteria { CriteriaText = column[0] };
                List<RubricCriteriaElement> elementList = new List<RubricCriteriaElement>();
                for (int i = column.Length-1; i > 0; i--)
                {
                    if (column[i].Length == 0) continue; // skip empty column which is ,,
                    var critElement = new RubricCriteriaElement { CriteriaText = column[i], ScoreValue = 5-i }; // someone please help with this. I dont know why my code worked
                    elementList.Add(critElement);
                }
                
                var existingCriteria = context.RubricCriteria.Include(criteria => criteria.RubricCriteriaElements).Where(criteria => criteria.CriteriaText.Equals(crit.CriteriaText)).FirstOrDefault();
                if (existingCriteria != null)
                {
                    existingCriteria.RubricId = matchedRubric.Id;
                    existingCriteria.RubricCriteriaElements = elementList;
                }
                else
                {
                    crit.RubricId = matchedRubric.Id;
                    crit.RubricCriteriaElements = elementList;
                    context.Add(crit);
                }

                context.SaveChanges();
            }
        }

        private static Rubric getRubricName(string rubricInfo, AppDbContext context)
        {
            var column = Regex.Split(rubricInfo, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            var rubricName = column[0];
            var matchedRubric = context.Rubrics
                                            .Where(rubric => rubric.Name.Equals(rubricName, StringComparison.InvariantCultureIgnoreCase))
                                            .FirstOrDefault();
            if (matchedRubric == null)
            {
                throw new Exception("Rubric metadata is required before rubric content");
            }
            return matchedRubric;
        }
    }
}
