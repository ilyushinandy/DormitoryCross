using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryCross.Services
{
    public class ServerServices
    {
        HttpClient httpClient;

        public ServerServices() 
        {
            httpClient = new HttpClient();
        }

        List<Student> students = new ();

        public async Task<List<Student>> GetStudents()
        {
            if(students?.Count > 0)
            {
                return students;
            }

            var url = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                students = await response.Content.ReadFromJsonAsync<List<Student>>();
            }

            return students;
        }
    }
}
