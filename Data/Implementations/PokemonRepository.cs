using Data.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data.Implementations
{
    public class PokemonRepository : IPokemonRepository
    {
        public bool CreatePokemon(Pokemon pokemon)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    using (SqlCommand command = new SqlCommand("sp_createPokemon", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@name", pokemon.Name);
                        command.Parameters.AddWithValue("@type", pokemon.Type);
                        command.Parameters.AddWithValue("@level", pokemon.Level);
                        command.Parameters.AddWithValue("@urlImage", pokemon.UrlImage ?? (object)DBNull.Value);
                        conn.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public bool DeletePokemon(int pokemonId)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    using (SqlCommand command = new SqlCommand("sp_deletePokemon", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@pokemonId", pokemonId);
                        conn.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public List<Pokemon> GetAllPokemons()
        {
            List<Pokemon> list = new List<Pokemon>();
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    using (SqlCommand command = new SqlCommand("sp_getAllPokemons", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Pokemon pokemon = new Pokemon()
                            {
                                PokemonId = (int)reader["PokemonId"],
                                Name = (string)reader["Name"],
                                Type = (string)reader["Type"],
                                Level = (int)reader["Level"],
                                UrlImage = reader["UrlImage"] as string ?? "D:\\DesktopApp\\PokeApp\\UI\\Resources\\default-image.jpg"
                            };
                            list.Add(pokemon);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return list;
        }

        public bool UpdatePokemon(Pokemon pokemon)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    using (SqlCommand command = new SqlCommand("sp_updatePokemon", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@pokemonId", pokemon.PokemonId);
                        command.Parameters.AddWithValue("@name", pokemon.Name);
                        command.Parameters.AddWithValue("@type", pokemon.Type);
                        command.Parameters.AddWithValue("@level", pokemon.Level);
                        command.Parameters.AddWithValue("@urlImage", pokemon.UrlImage ?? (object)DBNull.Value);
                        conn.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}