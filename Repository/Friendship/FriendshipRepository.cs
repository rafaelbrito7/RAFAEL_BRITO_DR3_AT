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
    public class FriendshipRepository
    {
        private SqlConnection connection;
        public FriendshipRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public async Task<IEnumerable<Friendship>> GetAll()
        {
            string sp = "GetAllFriendships";
            List<Friendship> friendships = new List<Friendship>();

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Friendship friendship = new Friendship();
                        friendship.Id = reader.GetGuid("Id");
                        friendship.APersonId = reader.GetGuid("APersonId");
                        friendship.BPersonId = reader.GetGuid("BPersonId");

                        friendships.Add(friendship);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return friendships;
        }

        public async Task<Friendship> GetOneFriendshipById(Guid id)
        {
            string sp = "GetOneFriendship";
            Friendship friendship = null;

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
                        friendship = new Friendship();
                        friendship.Id = reader.GetGuid("Id");
                        friendship.APersonId = reader.GetGuid("APersonId");
                        friendship.BPersonId = reader.GetGuid("BPersonId");

                        if (reader["APersonId"] != DBNull.Value)
                        {
                            friendship.APerson = new Person
                            {
                                Id = reader.GetGuid("APersonId"),
                                Name = reader.GetString("APersonName"),
                            };
                        }

                        if (reader["BPersonId"] != DBNull.Value)
                        {
                            friendship.BPerson = new Person
                            {
                                Id = reader.GetGuid("BPersonId"),
                                Name = reader.GetString("BPersonName"),
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
            return friendship;
        }

        public async Task<int> Create(Friendship friendship)
        {
            string sp = "CreateFriendship";
            int cont = 0;

            SqlCommand cmd = new SqlCommand(sp, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", friendship.Id);
            cmd.Parameters.AddWithValue("@APersonId", friendship.APersonId);
            cmd.Parameters.AddWithValue("@BPersonId", friendship.BPersonId);

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
            string sp = "DeleteFriendship";
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
    }
}
