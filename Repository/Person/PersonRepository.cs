using Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PersonRepository
    {
        private SqlConnection connection;
        public PersonRepository(SqlConnection connection) {
            this.connection = connection;
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            string sp = "GetAllPeople";
            List<Person> people = new List<Person>();

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Person person = new Person();
                        person.Id = reader.GetGuid("Id");
                        person.StateId = reader.GetGuid("StateId");
                        person.CountryId = reader.GetGuid("CountryId");
                        person.Name = reader.GetString("Name");
                        person.Email = reader.GetString("Email");
                        person.PhoneNumber = reader.GetString("PhoneNumber");
                        person.PhotoUrl = reader.GetString("PhotoUrl");
                        person.Birthday = reader.GetDateTime("Birthday");

                        if (reader["CountryId"] != DBNull.Value)
                        {
                            person.Country = new Country
                            {
                                Id = reader.GetGuid("CountryId"),
                                Name = reader.GetString("CountryName"),
                                PhotoUrl = reader.GetString("CountryPhotoUrl")
                            };
                        }

                        if (reader["StateId"] != DBNull.Value)
                        {
                            person.State = new State
                            {
                                Id = reader.GetGuid("StateId"),
                                Name = reader.GetString("StateName"),
                                PhotoUrl = reader.GetString("StatePhotoUrl"),
                                CountryId = reader.GetGuid("StateCountryId"),
                                Country = person.Country,
                            };
                        }
                        people.Add(person);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return people;
        }

        public async Task<Person> GetOneById(Guid id)
        {
            string sp = "GetOneById";
            Person person = null;

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        person = new Person();
                        person.Id = reader.GetGuid("Id");
                        person.StateId = reader.GetGuid("StateId");
                        person.CountryId = reader.GetGuid("CountryId");
                        person.Name = reader.GetString("Name");
                        person.Email = reader.GetString("Email");
                        person.PhoneNumber = reader.GetString("PhoneNumber");
                        person.PhotoUrl = reader.GetString("PhotoUrl");
                        person.Birthday = reader.GetDateTime("Birthday");

                        if (reader["CountryId"] != DBNull.Value)
                        {
                            person.Country = new Country
                            {
                                Id = reader.GetGuid("CountryId"),
                                Name = reader.GetString("CountryName"),
                                PhotoUrl = reader.GetString("CountryPhotoUrl")
                            };
                        }

                        if (reader["StateId"] != DBNull.Value)
                        {
                            person.State = new State
                            {
                                Id = reader.GetGuid("Id"),
                                Name = reader.GetString("StateName"),
                                PhotoUrl = reader.GetString("StatePhotoUrl"),
                                CountryId = reader.GetGuid("StateCountryId"),
                                Country = person.Country,
                            };
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return person;
        }

        public async Task<int> Create(Person person)
        {
            string sp = "CreatePerson";
            int cont = 0;

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", person.Id);
            cmd.Parameters.AddWithValue("@Name", person.Name);
            cmd.Parameters.AddWithValue("@Email", person.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", person.PhoneNumber);
            cmd.Parameters.AddWithValue("@PhotoUrl", person.PhotoUrl);
            cmd.Parameters.AddWithValue("@Birthday", person.Birthday);
            cmd.Parameters.AddWithValue("@StateId", person.StateId);
            cmd.Parameters.AddWithValue("@CountryId", person.CountryId);

            try
            {
                connection.Open();
                cont = await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return cont;
        }

        public async Task<int> Update(Person person)
        {
            string sp = "UpdatePerson";
            int cont = 0;

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", person.Id);
            cmd.Parameters.AddWithValue("@Name", person.Name);
            cmd.Parameters.AddWithValue("@Email", person.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", person.PhoneNumber);
            cmd.Parameters.AddWithValue("@Birthday", person.Birthday);
            cmd.Parameters.AddWithValue("@StateId", person.StateId);
            cmd.Parameters.AddWithValue("@CountryId", person.CountryId);

            try
            {
                connection.Open();
                cont = await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return cont;
        }

        public async Task<int> Delete(Guid id)
        {
            string sp = "DeletePerson";
            int cont = 0;

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();
                cont = await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return cont;
        }

        public async Task<int> GetCount()
        {
            string sp = "CountPeople";
            int cont = 0;

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                var result = await cmd.ExecuteScalarAsync();
                if (result != null)
                {
                    cont = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();
            return cont;
        }
    }
}
