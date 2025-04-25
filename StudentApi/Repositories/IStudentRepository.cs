using StudentApi.Models;

namespace StudentApi.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student? GetById(long id);
        void Add(Student student);
        void Delete(int id);
        void Update(Student student);

    }
}
