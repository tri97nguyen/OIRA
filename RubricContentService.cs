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
            foreach (string line in content)
            {
                // split csv with double quote having double quote "" in column
                var column = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                if (column[0].Length == 0) continue; // skip ,,,,
                RubricCriteria crit = new RubricCriteria { CriteriaText = column[0] };
                List<RubricCriteriaElement> elementList = new List<RubricCriteriaElement>();
                for (int i = 1; i < column.Length; i++)
                {
                    if (column[i].Length == 0) continue; // skip empty column which is ,,
                    var critElement = new RubricCriteriaElement { CriteriaText = column[i] };
                    elementList.Add(critElement);
                }
                
                var existingCriteria = context.RubricCriteria.Include(criteria => criteria.RubricCriteriaElements).Where(criteria => criteria.CriteriaText.Equals(crit.CriteriaText)).FirstOrDefault();
                if (existingCriteria != null)
                {
                    Console.WriteLine("before appending");
                    foreach (var ele in existingCriteria.RubricCriteriaElements)
                    {

                        Console.WriteLine($"criteria element is {ele}");
                    }

                    Console.WriteLine("after appending");
                    existingCriteria.RubricCriteriaElements = elementList;
                    Console.WriteLine(existingCriteria.RubricCriteriaElements);
                    foreach (var ele in existingCriteria.RubricCriteriaElements.ToList())
                    {
                        Console.WriteLine($"criteria element is {ele}");
                    }
                }
                else
                {
                    Console.WriteLine($"{crit} is new");
                    crit.RubricCriteriaElements = elementList;
                    context.Add(crit);
                }
                
                context.SaveChanges();

            }
        }
    }
}
