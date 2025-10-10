using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOPCURSOVA.Models;
using OOPCURSOVA.Data;
using Microsoft.Identity.Client;

namespace OOPCURSOVA.Services
{
    internal class FilterService
    {
        public List<Student> Filter(List<Student> students, string? name = null, string? group = null, double? minAverage = null)
        {
            var result = students.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(name))
                result = result.Where(s => s.FirstName.Contains(name) || s.LastName.Contains(name));

            if (!string.IsNullOrWhiteSpace(group))
                result = result.Where(s => s.Group.Contains(group));

            if (minAverage.HasValue)
                result = result.Where(s => s.Grades.Count > 0 && s.Grades.Average(g => g.Score) >= minAverage.Value);

            return result.ToList();
        }
    }
}
