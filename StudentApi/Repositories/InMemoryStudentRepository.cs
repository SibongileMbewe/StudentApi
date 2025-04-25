using StudentApi.Models;
using System.Text.Json;
using System.Xml.Serialization;

namespace StudentApi.Repositories
{
    public class InMemoryStudentRepository : IStudentRepository
    {
        private static int _idCounter = 10;
        private List<Student> _students = new();

        public InMemoryStudentRepository()
        {
            LoadFromJson(); 
        }

        public List<Student> GetAll() => _students;

        public Student? GetById(long id) => _students.FirstOrDefault(s => s.Id == id);

        public void Add(Student student)
        {
            student.Id = Interlocked.Increment(ref _idCounter);
            _students.Add(student);
            SaveToJson();
        }

        public void Delete(int id)
        {
            var student = GetById(id);
            if (student != null)
            {
                _students.Remove(student);
                SaveToJson();
            }
        }

        public void Update(Student student)
        {
            var existing = GetById(student.Id);
            if (existing != null)
            {
                existing.FullName = student.FullName;
                existing.Age = student.Age;
                existing.Grade = student.Grade;
                SaveToJson();
            }
        }

        private void SaveToJson()
        {
            var json = JsonSerializer.Serialize(_students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("students.json", json);
        }

        private void LoadFromJson()
        {
            if (!File.Exists("students.json")) return;

            var json = File.ReadAllText("students.json");
            _students = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();

            if (_students.Any())
                _idCounter = _students.Max(s => s.Id);
        }
    }
}
