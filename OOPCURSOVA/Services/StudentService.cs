using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOPCURSOVA.Models;
using OOPCURSOVA.Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;

namespace OOPCURSOVA.Services
{
    internal class StudentService
    {
        private readonly AppDbContext _context;
     

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }


        public List<Student> GetAllStudent()
        {
            return _context.Students
                .Include(s => s.Grades)
                .ToList();
        }


        public Student GetStudentById(int id)
        {

            return _context.Students
                .FirstOrDefault(s => s.Id == id);

        }

        public void UpdateStudents(Student student)
        {
            var existing = _context.Students.Find(student.Id);
            if(existing != null)
            {
                existing.FirstName = student.FirstName;
                existing.LastName = student.LastName;
                existing.Group=student.Group;
                _context.SaveChanges();
            }
        }
        public void DeleteStudent(int Id)
        {
            var student = _context.Students.Find(Id);
            if (student !=null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}
