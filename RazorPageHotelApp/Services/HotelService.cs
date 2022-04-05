using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RazorPageHotelApp.Exceptions;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Services
{
    public class HotelService: Connection, IHotelService
    {
        //string used to command the sql server
        private String queryString = "select * from po22_Hotel";
        private String queryNameString = "select * from po22_Hotel where lower(Name) like lower(@Name)";
        private String queryStringFromID = "select * from po22_Hotel where Hotel_No = @ID";
        private String insertSql = "insert into po22_Hotel Values (@ID, @Name, @Address)";
        private String deleteSql = "delete from po22_Hotel where Hotel_No = @ID";
        private String updateSql = "update po22_Hotel " +
                                   "set Hotel_No= @HotelID, Name=@Name, Address=@Address " +
                                   "where Hotel_No = @ID";


        public HotelService(IConfiguration configuration) : base(configuration)
        { }

        public HotelService(string connectionString) : base(connectionString)
        { }
        
        /// <summary>
        /// Gets all hotels asynchronously
        /// </summary>
        /// <returns>A list af all hotels</returns>
        /// <exception cref="DatabaseException">Thrown if the database has a error</exception>
        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int hotelNr = reader.GetInt32(0);
                            String hotelNavn = reader.GetString(1);
                            String hotelAdr = reader.GetString(2);
                            Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                            hoteller.Add(hotel);
                        }
                    }
                    catch (SqlException sqlEx)  
                    {
                        throw new DatabaseException(sqlEx.Message);
                    }
                }
            }
            return hoteller;
        }

        /// <summary>
        /// Gets the hotel with the corresponding hotelNum asynchronously
        /// </summary>
        /// <param name="hotelNr">The HotelNum of the hotel to get</param>
        /// <returns>A hotel with the same hotelNum</returns>
        /// <exception cref="DatabaseException">Thrown if the database has a error</exception>
        public async Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryStringFromID, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", hotelNr);
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (await reader.ReadAsync())
                        {
                            int hotelNum = reader.GetInt32(0);
                            String hotelNavn = reader.GetString(1);
                            String hotelAdr = reader.GetString(2);
                            return new Hotel(hotelNr, hotelNavn, hotelAdr);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        throw new DatabaseException(sqlEx.Message);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Creates a new hotel asynchronously
        /// </summary>
        /// <param name="hotel">The new hotel must have a uniq number</param>
        /// <returns>true or false depending of the success of the operation</returns>
        /// <exception cref="DatabaseException">Thrown if the database has a error</exception>
        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                        command.Parameters.AddWithValue("@Name", hotel.Navn);
                        command.Parameters.AddWithValue("@Address", hotel.Adresse);
                        await command.Connection.OpenAsync();
                        return 1 == await command.ExecuteNonQueryAsync();
                    }
                    catch (SqlException sqlEx)
                    {
                        throw new DatabaseException(sqlEx.Message);
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Updates the data of a hotel asynchronously
        /// </summary>
        /// <param name="hotel">The new data</param>
        /// <param name="hotelNr">The current id of the hotel to update</param>
        /// <returns>true or false depending of the success of the operation</returns>
        /// <exception cref="DatabaseException">Thrown if the database has a error</exception>
        public async Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@HotelID", hotel.HotelNr);
                        command.Parameters.AddWithValue("@Name", hotel.Navn);
                        command.Parameters.AddWithValue("@Address", hotel.Adresse);
                        command.Parameters.AddWithValue("@ID", hotelNr);
                        await command.Connection.OpenAsync();
                        return 1 == await command.ExecuteNonQueryAsync();
                    }
                    catch (SqlException sqlEx)
                    {
                        throw new DatabaseException(sqlEx.Message);
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Deletes a hotel asynchronously
        /// </summary>
        /// <param name="hotelNr">The hotel number of the hotel to delete</param>
        /// <returns>The deleted hotel</returns>
        /// <exception cref="DatabaseException">Thrown if the database has a error</exception>
        public async Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            Hotel hotel = await GetHotelFromIdAsync(hotelNr);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@ID", hotelNr);
                        await command.Connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        
                    }
                    catch (SqlException sqlEx)
                    {
                        throw new DatabaseException(sqlEx.Message);
                    }
                }
            }

            return hotel;
        }

        /// <summary>
        /// Gets all hotels with a specific name
        /// </summary>
        /// <param name="name">The name to search by</param>
        /// <returns>A list of all hotels with that name</returns>
        /// <exception cref="DatabaseException">Thrown if the database has a error</exception>
        public async Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(queryNameString, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@Name", '%' + name + '%');
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int hotelNr = reader.GetInt32(0);
                            String hotelNavn = reader.GetString(1);
                            String hotelAdr = reader.GetString(2);
                            Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                            hoteller.Add(hotel);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        throw new DatabaseException(sqlEx.Message);
                    }
                }
            }
            return hoteller;
        }
    }
}
