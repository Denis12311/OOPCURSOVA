using OOPCURSOVA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace OOPCURSOVA.Services
{
    internal class SaveService
    {

        public void SaveToJson(List<Student> students, string filePath)
        {
            var json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
        public List<Student> LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath)) return new List<Student>();
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
        }
    }
}
