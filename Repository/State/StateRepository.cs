using Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository
{
    public class StateRepository
    {
        private SqlConnection connection;

        public StateRepository(SqlConnection connection)
        {
            this.connection = connection;
        }
        public async Task<IEnumerable<State>> GetAll()
        {
            string sp = "GetAllStates";
            List<State> states = new List<State>();

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        State state = new State();
                        state.Id = reader.GetGuid("Id");
                        state.Name = reader.GetString("Name");
                        state.PhotoUrl = reader.GetString("PhotoUrl") ?? string.Empty;
                        state.CountryId = reader.GetGuid("CountryId");

                        if (reader["CountryId"] != DBNull.Value)
                        {
                            state.Country = new Country
                            {
                                Id = reader.GetGuid("CountryId"),
                                Name = reader.GetString("CountryName"),
                                PhotoUrl = reader.GetString("CountryPhotoUrl")
                            };
                        }

                        states.Add(state);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return states;
        }
        public async Task<State> GetOneById(Guid id)
        {
            string sp = "GetOneStateById";
            State state = null;

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
                        state = new State();
                        state.Id = reader.GetGuid("Id");
                        state.Name = reader.GetString("Name");
                        state.PhotoUrl = reader.GetString("PhotoUrl");
                        state.CountryId = reader.GetGuid("CountryId");

                        if (reader["CountryId"] != DBNull.Value)
                        {
                            state.Country = new Country
                            {
                                Id = reader.GetGuid("CountryId"),
                                Name = reader.GetString("CountryName"),
                                PhotoUrl = reader.GetString("CountryPhotoUrl")
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
            return state;
        }

        public async Task<int> Create(State state)
        {
            string sp = "CreateState";
            int cont = 0;

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", state.Id);
            cmd.Parameters.AddWithValue("@Name", state.Name);
            cmd.Parameters.AddWithValue("@PhotoUrl", state.PhotoUrl);
            cmd.Parameters.AddWithValue("@CountryId", state.CountryId);

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

        public async Task<int> Update(State state)
        {
            string sp = "UpdateState";
            int cont = 0;

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", state.Id);
            cmd.Parameters.AddWithValue("@Name", state.Name);
            cmd.Parameters.AddWithValue("@CountryId", state.CountryId);

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
            string sp = "DeleteState";
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
            string sp = "CountStates";
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
