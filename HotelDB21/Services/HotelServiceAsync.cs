using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    class HotelServiceAsync : Connection, IHotelServiceAsync
    {
        private string queryString = "select * from po22_Hotel";
        private string queryStringFromID = "select * from po22_Hotel where Hotel_No = @ID";
        private string insertSql = "insert into po22_Hotel Values(@ID, @Name, @Address)";
        private string deleteSql = "delete from po22_Hotel where Hotel_No = @ID";
        private string updateSql = "update po22_Hotel set Hotel_No = @NewID," +
                                   " Name = @NewName, Address = @NewAddress" +
                                   " where Hotel_No = @Id";

        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hotels = new List<Hotel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int hotelNr = reader.GetInt32(0);
                        string hotelName = reader.GetString(1);
                        string hotelAddress = reader.GetString(2);
                        Hotel hotel = new Hotel(
                            hotelNr,
                            hotelName,
                            hotelAddress);
                        hotels.Add(hotel);
                    }
                }

                return hotels;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Database error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }

            return hotels;
        }

        public Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@ID", hotel.HotelNum);
                    command.Parameters.AddWithValue("@Name", hotel.Name);
                    command.Parameters.AddWithValue("@Address", hotel.Address);

                    await command.Connection.OpenAsync();

                    int numOfRows = await command.ExecuteNonQueryAsync();
                    return numOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General error: " + ex.Message);
                }
            }

            return false;
        }

        public Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            throw new NotImplementedException();
        }

        public Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            throw new NotImplementedException();
        }

        public Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
