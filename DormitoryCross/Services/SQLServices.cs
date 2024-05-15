using DocumentFormat.OpenXml.Office2010.Excel;
using MySql.Data.MySqlClient;
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

        public async Task AddStudent(string fullName, string numberContract, string period, string group, string numberRoom, string telefon, string fullNameParents, string telefoneParents)
        {
            await Init();
            var student = new Student()
            {
                FullName = fullName,
                NumberContract = numberContract,
                Period = period,
                Group = group,
                NumberRoom = numberRoom,
                Telefone = telefon,
                FullNameParents = fullNameParents,
                TelefoneParents = telefoneParents
            };

            var id = await conn.InsertAsync(student);
        }

        public async Task UpdateStudent(int id, string fullName, string numberContract, string period, string group, string numberRoom, string telefon, string fullNameParents, string telefoneParents)
        {
            await Init();
            var student = new Student()
            {
                Id = id,
                FullName = fullName,
                NumberContract = numberContract,
                Period = period,
                Group = group,
                NumberRoom = numberRoom,
                Telefone = telefon,
                FullNameParents = fullNameParents,
                TelefoneParents = telefoneParents
            };

            var st = await conn.UpdateAsync(student);
        }

        public async Task RemoveStudent(int id)
        {
            await Init();

            conn.DeleteAsync<Student>(id);
        }

        public async Task RemoveAllStudent()
        {
            await Init();

            conn.DeleteAllAsync<Student>();
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

        public async Task<IEnumerable<Student>> SearchStudent(string fullName)
        {
            await Init();

            //var students = await conn.Table<Student>().Where(s => s.FullName.Contains(fullName)).ToListAsync();
            //string query = $"SELECT * FROM `Students` WHERE FULLNAME LIKE @value";
            var students = await conn.QueryAsync<Student>($"SELECT * FROM Student WHERE FULLNAME LIKE '%{fullName}%'");

            return students;
        }
    }
}
