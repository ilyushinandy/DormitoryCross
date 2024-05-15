using SQLite;

namespace DormitoryCross.Model
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NumberContract { get; set; }
        public string Period { get; set; }
        public string Group { get; set; }
        public string NumberRoom { get; set; }
        public string Telefone { get; set; }
        public string FullNameParents { get; set; }
        public string TelefoneParents { get; set; }
    }
}
