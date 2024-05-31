using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryCross.Services
{
    public class ServerServices
    {
        HttpClient httpClient;
        MySqlConnection mySqlConnection;
        string connectionString = "Server=bt6b61lczawghlfwt53w-mysql.services.clever-cloud.com;Port=3306;Database=bt6b61lczawghlfwt53w;Uid=ulokqf0iy4dmxv3d;Pwd=9najDbkHwUnK73sOHJf5;";
        List<Student> students = new();
        //MYSQL_ADDON_HOST = bt6b61lczawghlfwt53w - mysql.services.clever - cloud.com
        //MYSQL_ADDON_DB = bt6b61lczawghlfwt53w
        //MYSQL_ADDON_USER = ulokqf0iy4dmxv3d
        //MYSQL_ADDON_PORT = 3306
        //MYSQL_ADDON_PASSWORD = 9najDbkHwUnK73sOHJf5
        //MYSQL_ADDON_URI = mysql://ulokqf0iy4dmxv3d:9najDbkHwUnK73sOHJf5@bt6b61lczawghlfwt53w-mysql.services.clever-cloud.com:3306/bt6b61lczawghlfwt53w

        public ServerServices() 
        {
            httpClient = new HttpClient();
        }

        async Task Connection()
        {
            if (mySqlConnection != null)
                return;
            
            try
            {
                mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                Console.WriteLine("Connection successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            
        }

        public async Task InsertBD(string fullName, string numberContract, string period, string group, string numberRoom, string telefon, string fullNameParents, string telefoneParents)
        {
            await Connection();

            string query = "INSERT INTO `Students` (`FULLNAME`, `NUMBERCONTRACT`, `PERIOD`, `STGROUP`, `NUMBERROOM`, `TELEFONE`, `FULLNAMEPARENTS`, `TELEFONEPARENTS`) VALUES (@value1, @value2, @value3, @value4, @value5, @value6, @value7, @value8)";

            try
            {
                mySqlConnection.Open();
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@value1", fullName);
                    command.Parameters.AddWithValue("@value2", numberContract);
                    command.Parameters.AddWithValue("@value3", period);
                    command.Parameters.AddWithValue("@value4", group);
                    command.Parameters.AddWithValue("@value5", numberRoom);
                    command.Parameters.AddWithValue("@value6", telefon);
                    command.Parameters.AddWithValue("@value7", fullNameParents);
                    command.Parameters.AddWithValue("@value8", telefoneParents);
                    command.ExecuteNonQuery();
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        public async Task InsertUser(string name, string email, string password)
        {
            await Connection();

            string query = "INSERT INTO `Users` (`NAME`, `EMAIL`, `PASSWORD`) VALUES (@value1, @value2, @value3)";

            try
            {
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@value1", name);
                    command.Parameters.AddWithValue("@value2", email);
                    command.Parameters.AddWithValue("@value3", password);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        public async Task DeleteUser(string id)
        {
            await Connection();

            string query = $"DELETE FROM `Users` WHERE 'Users'.'ID' = {id}";

            try
            {
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    //command.Parameters.AddWithValue("@value1", id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }



        public async Task TruncateBD()
        {
            await Connection();

            string query = "TRUNCATE TABLE `bt6b61lczawghlfwt53w`.`Students`";

            try
            {
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                   
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        public async Task<List<Student>> SelectBD()
        {
            await Connection();

            string query = "SELECT * FROM `Students`";

            try
            {
                using (var command = new MySqlCommand(query, mySqlConnection)) 
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Пример чтения данных из результата запроса
                            students.Add(new Student
                            {
                                Id = reader.GetInt32("ID"),
                                FullName = reader.GetString("FULLNAME"),
                                NumberContract = reader.GetString("NUMBERCONTRACT"),
                                Period = reader.GetString("PERIOD"),
                                Group = reader.GetString("STGROUP"),
                                NumberRoom = reader.GetString("NUMBERROOM"),
                                Telefone = reader.GetString("TELEFONE"),
                                FullNameParents = reader.GetString("FULLNAMEPARENTS"),
                                TelefoneParents = reader.GetString("TELEFONEPARENTS")
                            });
                        }
                    }
                };
                return students;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        //public async Task<List<Student>> GetStudents()
        //{
        //    if (students?.Count > 0)
        //    {
        //        return students;
        //    }

        //    var url = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";

        //    var response = await httpClient.GetAsync(url);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        students = await response.Content.ReadFromJsonAsync<List<Student>>();
        //    }

        //    return students;
        //}
    }
}
