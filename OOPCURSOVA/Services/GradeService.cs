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
    internal class GradeService
    {
        private readonly AppDbContext _context;

        public GradeService(AppDbContext context)
        {
            _context = context;
        }
        public void AddGrade(Grade grade)
        {
            if (!_context.Students.Any(s => s.Id == grade.StudentId))
                throw new Exception("Студент не найден");

            _context.Grades.Add(grade);
            _context.SaveChanges();
        }

        public List<Grade> GetGradesByStudentId(int studentId)
        {
            return _context.Grades
                .Where(g => g.StudentId == studentId)
                .ToList();
        }

        public void UpgradeGrade(Grade grade)
        {
            var existing = _context.Grades.Find(grade.Id);
            if (existing != null)
            {
                existing.Subject = grade.Subject;
                existing.Score = grade.Score;
                _context.SaveChanges();
            }
        }


        public void DeleteGrade(int gradeId)
        {
            var grade = _context.Grades.Find(gradeId);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                _context.SaveChanges();
            }
        }

        public double GetAverageGrade(int studentId)
        {
            var grades = _context.Grades
                .Where(g => g.StudentId == studentId)
                .Select(g => g.Score)
                .ToList();

            if (!grades.Any()) return 0.0;
            return grades.Average();
        }






    }
}
