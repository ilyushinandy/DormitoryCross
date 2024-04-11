using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryCross.Services
{
    public class SQLServices
    {
        private SQLiteAsyncConnection conn;

        async Task Init()
        {
            if (conn != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyDataStudent");

            conn = new SQLiteAsyncConnection(databasePath);
            await conn.CreateTableAsync<Student>();
        }

        public async Task AddStudent(string fullName, string numberContract, string period, string group, string telefon,
            string numberRoom)
        {
            await Init();
            var student = new Student()
            {
                FullName = fullName,
                NumberContract = numberContract,
                Period = period,
                Group = group,
                Telefone = telefon,
                NumberRoom = numberRoom
            };

            var id = await conn.InsertAsync(student);
        }

        public async Task RemoveStudent(int id)
        {
            await Init();

            conn.DeleteAsync<Student>(id);
        }

        public async Task<IEnumerable<Student>> GetStudent()
        {
            await Init();

            var students =  await conn.Table<Student>().ToListAsync();
            return students;
        }

        public async Task<IEnumerable<Student>> GetStudentRoom(string numberRoom)
        {
            await Init();

            var students = await conn.Table<Student>().Where(s => s.NumberRoom == numberRoom).ToListAsync();
            return students;
        }
    }
}
