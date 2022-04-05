using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    

    public class RoomService : Connection, IRoomService
    {
        private string sqlGetRoomFromId = "select * from po22_Room where Hotel_No = @Hotel_No and Room_No = @Room_No";
        private string sqlGetAllRooms = "select * from po22_Room";
        private string sqlGetAllRoomsFromHotel = "select * from po22_Room where Hotel_No = @ID";
        private string sqlCreateRoom = "insert into po22_Room values(@Room_No, @Hotel_No, @Types, @Price)";
        private string sqlDeleteRoom = "delete from po22_Room where Hotel_No = @Hotel_No and Room_No = @Room_No";

        public List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(sqlGetAllRooms, connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int roomNum = reader.GetInt32(0);
                        int hotelNum = reader.GetInt32(1);
                        char roomType = reader.GetString(2)[0];
                        double roomPrice = reader.GetDouble(3);
                        Room room = new Room(
                            roomNum,
                            roomType,
                            roomPrice,
                            hotelNum);
                        rooms.Add(room);
                    }
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
            return rooms;
        }


        public List<Room> GetAllRoomsFromHotel(int hotelNr)
        {
            List<Room> rooms = new List<Room>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(sqlGetAllRoomsFromHotel, connection);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int roomNum = reader.GetInt32(0);
                        int hotelNum = reader.GetInt32(1);
                        char roomType = reader.GetString(2)[0];
                        double roomPrice = reader.GetDouble(3);
                        Room room = new Room(
                            roomNum,
                            roomType,
                            roomPrice,
                            hotelNum);
                        rooms.Add(room);
                    }
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
            return rooms;
        }

        public Room GetRoomFromId(int roomNr, int hotelNr)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(sqlGetRoomFromId, connection);
                    command.Parameters.AddWithValue("@Hotel_No", hotelNr);
                    command.Parameters.AddWithValue("@Room_No", roomNr);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int roomNum = reader.GetInt32(0);
                        int hotelNum = reader.GetInt32(1);
                        char roomType = reader.GetString(2)[0];
                        double roomPrice = reader.GetDouble(3);
                        return new Room(
                            roomNum,
                            roomType,
                            roomPrice,
                            hotelNum);

                    }
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

        public bool CreateRoom(int hotelNr, Room room)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(sqlCreateRoom, connection);
                    command.Parameters.AddWithValue("@Room_No", room.RoomNr);
                    command.Parameters.AddWithValue("@Hotel_No", hotelNr);
                    command.Parameters.AddWithValue("@Types", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Pris);
                    command.Connection.Open();
                    return 1 == command.ExecuteNonQuery();
                    
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

            return false;
        }

        public bool UpdateRoom(Room room, int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }

        public Room DeleteRoom(int roomNr, int hotelNr)
        {
            Room room = GetRoomFromId(roomNr, hotelNr);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(sqlDeleteRoom, connection);
                    command.Parameters.AddWithValue("@Hotel_No", hotelNr);
                    command.Parameters.AddWithValue("@Room_No", roomNr);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
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

            return room;
        }
    }
}
