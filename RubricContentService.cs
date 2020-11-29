using Microsoft.AspNetCore.Mvc;
using oira.Data;
using oira.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace oira
{
    public class RubricContentService
    {
        public static void ParseRubricContent(UploadRubricContent rubricContentData, AppDbContext context)
        {
            IEnumerable<string> content = Upload.ReadAsList(rubricContentData.Upload);
            foreach (string line in content)
            {
                var column = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                if (column[0].Length == 0) continue; // skip ,,,,
                RubricCriteria crit = new RubricCriteria { CriteriaText = column[0] };
                List<RubricCriteriaElement> elementList = new List<RubricCriteriaElement>();
                for (int i = 1; i < column.Length; i++)
                {
                    if (column[i].Length == 0) continue;
                    var critElement = new RubricCriteriaElement { CriteriaText = column[i] };
                    elementList.Add(critElement);
                }
                crit.RubricCriteriaElements = elementList;
                context.Add(crit);
                context.SaveChanges();

            }
        }
    }
}
