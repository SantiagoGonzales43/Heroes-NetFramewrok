using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PracticaAdoNet.Domain;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PracticaAdoNet.DTOs;

namespace PracticaAdoNet.DAL
{
    public class HeroeRepository : IHeroeRepository
    {
        private readonly string _connectionString;

        public HeroeRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AetheriaDB"].ConnectionString;

            if(string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("No se conecto con la base de datos");
            }
        }



        //Get todos los Heroes
        public async Task<List<Heroe>> GetAllHeroes()
        {
            var heroes = new List<Heroe>();

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetAllHeroes",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        await connection.OpenAsync();
                        using(SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while(reader.Read())
                            {
                                heroes.Add(new Heroe
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Clase = reader.GetString(reader.GetOrdinal("Clase")),
                                    Nivel = reader.GetInt32(reader.GetOrdinal("Nivel")),
                                });
                            }
                        }
                    }catch(SqlException ex)
                    {
                        throw new Exception("A ocurrido un error en GetAllHeroes",ex);
                    }

                }
            }

            return heroes;
        }


        //Post
        public async Task<int> CreateHeroe(CreateHeroeDtos heroe)
        {
            var newIdHeroe = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("CreateHeroe", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Nombre", heroe.Nombre);
                    command.Parameters.AddWithValue("@Clase", heroe.Clase);
                    command.Parameters.AddWithValue("@Nivel", heroe.Nivel);

                    SqlParameter idOutPutParam = new SqlParameter {
                        ParameterName = "@NewHeroeId",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(idOutPutParam);

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();

                        if(idOutPutParam.Value != DBNull.Value)
                        {
                            newIdHeroe = Convert.ToInt32(idOutPutParam.Value);
                        }
                    }
                    catch(Exception ex)
                    {
                        throw new Exception($"A ocurrido un error en el servidor a la hora de crear un hero ${ex.Message}",ex);
                    }
                }
            }
            return newIdHeroe;
        }

        //GET por Id
        public async Task<Heroe> GetHeroeById(int id)
        {
            var heroeEncontrado = new Heroe();
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = new SqlCommand("GetHeroeById",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if(await reader.ReadAsync())
                            {
                                heroeEncontrado = new Heroe
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Clase = reader.GetString(reader.GetOrdinal("Clase")),
                                    Nivel = reader.GetInt32(reader.GetOrdinal("Nivel")),
                                };
                            }
                        };
                    }
                    catch(SqlException ex)
                    {
                        throw new Exception($"A ocurrido un error en la base de datos al conseguir el heroe por id ${ex.Message}", ex);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"A ocurrido un error en el servidor al conseguir el heroe por id ${ex.Message}", ex);
                    }
                }

            }
                return heroeEncontrado;
        }

        //Delete heroe
        public async Task<int> DeleteHeroeById(int id)
        {
            var idHeroeEliminado = 0;
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("DeleteHeroeById",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        await connection.OpenAsync();
                        object result = await command.ExecuteScalarAsync();
                        if (result != null && result != DBNull.Value)
                        {
                            idHeroeEliminado = Convert.ToInt32(result);
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"A ocurrido un error en la base de datos al eliminar el heroe por id ${ex.Message}", ex);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"A ocurrido un error en el servidor al eliminar el heroe por id ${ex.Message}", ex);
                    }
                }
            }

            return idHeroeEliminado;
        }
    }
}