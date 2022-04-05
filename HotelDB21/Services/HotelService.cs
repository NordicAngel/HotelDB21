using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    public class HotelService : Connection, IHotelService
    {
        private string queryString = "select * from po22_Hotel";
        private string queryStringFromID = "select * from po22_Hotel where Hotel_No = @ID";
        private string insertSql = "insert into po22_Hotel Values(@ID, @Name, @Address)";
        private string deleteSql = "delete from po22_Hotel where Hotel_No = @ID";
        private string updateSql = "update po22_Hotel set Hotel_No = @NewID," +
                                   " Name = @NewName, Address = @NewAddress" +
                                   " where Hotel_No = @Id";
        // lav selv sql strengene færdige og lav gerne yderligere sqlstrings


        public List<Hotel> GetAllHotel()
        {
            List<Hotel> hotels = new List<Hotel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
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

        public Hotel GetHotelFromId(int hotelNr)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryStringFromID, connection);
                    command.Parameters.AddWithValue("@ID", hotelNr);

                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (!reader.Read())
                        return null;

                    int hotelNum = reader.GetInt32(0);
                    string hotelName = reader.GetString(1);
                    string hotelAddress = reader.GetString(2);
                    Hotel hotel = new Hotel(
                        hotelNum,
                        hotelName,
                        hotelAddress);
                    return hotel;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Database error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }

            return null;
        }

        public bool CreateHotel(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@ID", hotel.HotelNum);
                    command.Parameters.AddWithValue("@Name", hotel.Name);
                    command.Parameters.AddWithValue("@Address", hotel.Address);

                    command.Connection.Open();

                    int numOfRows = command.ExecuteNonQuery();
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

        public bool UpdateHotel(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@NewID", hotel.HotelNum);
                    command.Parameters.AddWithValue("@NewName", hotel.Name);
                    command.Parameters.AddWithValue("@NewAddress", hotel.Address);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                        
                    command.Connection.Open();

                    int numOfRows = command.ExecuteNonQuery();
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

        public Hotel DeleteHotel(int hotelNr)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Hotel hotel = GetHotelFromId(hotelNr);
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@ID", hotelNr);

                    command.Connection.Open();
                    int reader = command.ExecuteNonQuery();

                    //if (!reader.Read())
                    //    return null;

                    //int hotelNum = reader.GetInt32(0);
                    //string hotelName = reader.GetString(1);
                    //string hotelAddress = reader.GetString(2);
                    //Hotel hotel = new Hotel(
                    //    hotelNum,
                    //    hotelName,
                    //    hotelAddress);
                    return hotel;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Database error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
            }

            return null;
        }

        public List<Hotel> GetHotelsByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
