using SQLite;

namespace DormitoryCross.Services
{
    public class SQLServices
    {
        private SQLiteAsyncConnection conn;

        async Task Init()
        {
            if (conn != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyDataDormitory");

            conn = new SQLiteAsyncConnection(databasePath);
            await conn.CreateTableAsync<Student>();
            await conn.CreateTableAsync<User>();
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

            var students = await conn.QueryAsync<Student>($"SELECT * FROM Student WHERE FULLNAME LIKE '%{fullName}%'");

            return students;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            await Init();

            var user = await conn.Table<User>().ToListAsync();
            return user;
        }

        public async Task AddUsers(string name, string email, string password)
        {
            await Init();
            var user = new User()
            {
                Name = name,
                Email = email,
                Password = password
            };

            var id = await conn.InsertAsync(user);
        }

        public async Task RemoveUser(int id)
        {
            await Init();

            conn.DeleteAsync<User>(id);
        }

        public async Task UpdateUser(int id, string name, string email, string password)
        {
            await Init();
            var user = new User()
            {
                Id = id,
                Name = name,
                Email = email,
                Password = password
            };

            var us = await conn.UpdateAsync(user);
        }
    }
}
