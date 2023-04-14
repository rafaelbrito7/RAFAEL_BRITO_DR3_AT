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
    public class CountryRepository
    {
        private SqlConnection connection;

        public CountryRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public async Task<IEnumerable<Country>> GetAll()
        {
            string sp = "GetAllCountries";
            List<Country> countries = new List<Country>();

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Country country = new Country();
                        country.Id = reader.GetGuid("Id");
                        country.Name = reader.GetString("Name");
                        country.PhotoUrl = reader.GetString("PhotoUrl") ?? string.Empty;
                        countries.Add(country);
                    }
                }
                connection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return countries;
        }

        public async Task<Country> GetOneById(Guid id)
        {
            string sp = "GetOneCountryById";
            Country country = null;

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Id", id);

            try
            {
                connection.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        country = new Country();
                        country.Id = reader.GetGuid("Id");
                        country.Name = reader.GetString("Name");
                        country.PhotoUrl = reader.GetString("PhotoUrl");
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return country;
        }

        public async Task<int> Create(Country country)
        {
            string sp = "CreateCountry";
            int cont = 0;

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", country.Id);
            cmd.Parameters.AddWithValue("@Name", country.Name);
            cmd.Parameters.AddWithValue("@PhotoUrl", country.PhotoUrl);

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

        public async Task<int> Update(Country country)
        {
            string sp = "UpdateCountry";
            int cont = 0;

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", country.Id);
            cmd.Parameters.AddWithValue("@Name", country.Name);

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
            string sp = "DeleteCountry";
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
            string sp = "CountCountries";
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
