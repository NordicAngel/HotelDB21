using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotelDBConsole21.Models;
using HotelDBConsole21.Services;

namespace HotelDBConsole21
{
    public static class MainMenu
    {
        //Lav selv flere menupunkter til at vælge funktioner for Rooms
        //public static void showOptions()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Vælg et menupunkt");
        //    Console.WriteLine("1) List hoteller");
        //    Console.WriteLine("1a) List hoteller async");
        //    Console.WriteLine("2) Opret nyt Hotel");
        //    Console.WriteLine("3) Fjern Hotel");
        //    Console.WriteLine("4) Søg efter hotel udfra hotelnr");
        //    Console.WriteLine("5) Opdater et hotel");
        //    Console.WriteLine("6) List alle værelser");
        //    Console.WriteLine("7) List alle værelser til et bestemt hotel");
        //    Console.WriteLine("8) Flere menupunkter kommer snart :) ");
        //    Console.WriteLine("Q) Afslut");
        //}

        public static void showOptions()
        {
            Console.Clear();
            Console.WriteLine("Vælg et menupunkt");
            Console.WriteLine("1) List hoteller");
            Console.WriteLine("1a) List hoteller async");
            Console.WriteLine("2) Opret nyt Hotel");
            Console.WriteLine("2a) Opret nyt Hotel async");
            Console.WriteLine("3) Fjern Hotel");
            Console.WriteLine("4) Søg efter hotel udfra hotelnr");
            Console.WriteLine("5) Opdater et hotel");
            Console.WriteLine("6) List alle værelser");
            Console.WriteLine("7) List alle til et bestemt hotel");
            Console.WriteLine("8) Opret nyt værelse");
            Console.WriteLine("9) Fjern et værelse");
            Console.WriteLine("10) Søg efter et givent værelse");
            Console.WriteLine("11) Opdater et værelse");
            Console.WriteLine("12) kommer snart");
            Console.WriteLine("Q) Afslut");
        }


        public static bool Menu()
        {
            showOptions();
            switch (Console.ReadLine())
            {
                case "1":
                    ShowHotels();
                    return true;
                case "1a":
                    ShowHotelsAsync();
                    DoSomething();
                    return true;
                case "2":
                    CreateHotel();
                    return true;
                case "2a":
                    CreateHotelAsync();
                    return true;
                case "3":
                    RemoveHotel();
                    return true;
                case "4":
                    FindHotelByNum();
                    return true;
                case "5":
                    UpdateHotel();
                    return true;
                case "6":
                    ListAllRooms();
                    return true;
                case "7":
                    ListAllFromHotel();
                    return true;
                case "8":
                    CreateRoom();
                    return true;
                case "9":
                    RemoveRoom();
                    return true;
                case "10":
                    RoomFromId();
                    return true;
                case "11":
                    UpdateRoom();
                    return true;
                case "Q": 
                case "q": return false;
                default: return true;
            }

        }

        private static void UpdateRoom()
        {
            Console.Clear();
            Console.WriteLine("indlæs hotelnr");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("indlæs værelse nr");
            int roomnum = Convert.ToInt32(Console.ReadLine());
            
            RoomService rs = new RoomService();
            Room oldRoom = rs.GetRoomFromId(hotelnr, roomnum);

            Console.WriteLine($"gamle hotel nr var: {oldRoom.HotelNr}\nindlæs det ny");


            Console.WriteLine("indlæs type");
            char type = Console.ReadLine().ToUpper()[0];
            Console.WriteLine("indlæs Price");
            double price = Convert.ToDouble(Console.ReadLine());
            bool success = rs.CreateRoom(
                hotelnr,
                new Room(
                    roomnum,
                    type,
                    price,
                    hotelnr));
            Console.WriteLine(success ?
                "værelset blev succesfult uprettet"
                : "kunne ikke oprette værelse");
        }

        private static void RoomFromId()
        {
            Console.Clear();
            Console.WriteLine("indlæse hotel nr");
            int hotelNr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("indlæse room nr");
            int roomNr = Convert.ToInt32(Console.ReadLine());
            Room room = new RoomService().GetRoomFromId(roomNr, hotelNr);
            Console.WriteLine(room == null
                ? "kunne ikke finde det værelse"
                : $"hotelNr: {room.HotelNr}; værelsesNr: {room.RoomNr}; " +
                  $"type: {room.Types}; pris: {room.Pris}");
        }

        private static void RemoveRoom()
        {
            Console.Clear();
            Console.WriteLine("indlæse hotel nr");
            int hotelNr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("indlæse room nr");
            int roomNr = Convert.ToInt32(Console.ReadLine());
            Room room = new RoomService().DeleteRoom(roomNr, hotelNr);

            Console.WriteLine(room == null
                ? "kunne ikke slætte det værelse"
                : $"værelsete: [hotelNr: {room.HotelNr}; værelsesNr: {room.RoomNr}; " +
                  $"type: {room.Types}; pris: {room.Pris}] er blevet slettet");
        }

        private static void CreateRoom()
        {
            Console.Clear();
            Console.WriteLine("indlæs hotelnr");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("indlæs værelse nr");
            int roomnum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("indlæs type");
            char type = Console.ReadLine().ToUpper()[0];
            Console.WriteLine("indlæs Price");
            double price = Convert.ToDouble(Console.ReadLine());
            bool success = new RoomService().CreateRoom(
                hotelnr, 
                new Room(
                    roomnum,
                    type, 
                    price, 
                    hotelnr));
            Console.WriteLine(success? 
                "værelset blev succesfult uprettet"
                :"kunne ikke oprette værelse");
        }

        private static void ListAllFromHotel()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            List<Room> rooms = new RoomService().
                GetAllRoomsFromHotel(Convert.ToInt32(Console.ReadLine()));
            if (rooms.Count != 0)
            {
                foreach (Room room in rooms)
                {
                    Console.WriteLine($"HotelNr: {room.HotelNr}; RoomNr: {room.RoomNr}; Type: {room.Types}; Price: {room.Pris}");
                }
            }
            else
            {
                Console.WriteLine("Der er ikke nogle værelser på dette hotel");
            }
        }

        private static void ListAllRooms()
        {
            Console.Clear();
            List<Room> rooms = new RoomService().GetAllRooms();
            foreach (Room room in rooms)
            {
                Console.WriteLine($"HotelNr: {room.HotelNr}; RoomNr: {room.RoomNr}; Type: {room.Types}; Price: {room.Pris}");
            } 
        }

        private async static void CreateHotelAsync()
        {
            //Indlæs data
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs hotelnavn");
            string navn = Console.ReadLine();
            Console.WriteLine("Indlæs hotel adresse");
            string adresse = Console.ReadLine();

            //Kalde hotelservice vise resultatet
            HotelServiceAsync hs = new HotelServiceAsync();
            bool ok = await hs.CreateHotelAsync(new Hotel(hotelnr, navn, adresse));
            if (ok)
            {
                Console.WriteLine("Hotellet blev oprettet!");
            }
            else
            {
                Console.WriteLine("Fejl. Hotellet blev ikke oprettet!");
            }
        }

        private static void UpdateHotel()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Hotel hotel = new HotelService().GetHotelFromId(hotelnr);
            
            Console.WriteLine($"Det nuværenede nummer er: {hotel.HotelNum}");
            int newHotelNum = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Det nuværende navn er: {hotel.Name}");
            string newHotelName = Console.ReadLine();

            Console.WriteLine($"Den nuværende adresse er: {hotel.Address}");
            string newHotelAddress = Console.ReadLine();

            Hotel updatedeHotel = new Hotel(newHotelNum, newHotelName, newHotelAddress);

            new HotelService().UpdateHotel(updatedeHotel, hotelnr);
        }

        private static void FindHotelByNum()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Hotel hotel = new HotelService().GetHotelFromId(hotelnr);
            Console.WriteLine(hotel == null
                ? "Der findes ikke nogle hoteller med det ID."
                : $"HotelNr: {hotel.HotelNum}; Name: {hotel.Name}; Address: {hotel.Address}");
        }

        private static void RemoveHotel()
        {
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Hotel hotel = new HotelService().DeleteHotel(hotelnr);
            Console.WriteLine(hotel == null
                ? "Der findes ikke nogle hoteller med det ID."
                : $"HotelNr: {hotel.HotelNum}; Name: {hotel.Name};" +
                  $" Address: {hotel.Address}, has now been deleted");


        }

        private static void ShowHotels()
        {
            Console.Clear();
            List<Hotel> hotels = new HotelService().GetAllHotel();
            foreach (Hotel hotel in hotels)
            {
                Console.WriteLine($"HotelNr: {hotel.HotelNum}; Name: {hotel.Name}; Address: {hotel.Address}");
            }
        }

        private async static Task ShowHotelsAsync()
        {
            Console.Clear();
            List<Hotel> hotels = await new HotelServiceAsync().GetAllHotelAsync();
            foreach (Hotel hotel in hotels)
            {
                Console.WriteLine($"HotelNr: {hotel.HotelNum}; Name: {hotel.Name}; Address: {hotel.Address}");
            }
        }

        private static void CreateHotel()
        {
            //Indlæs data
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs hotelnavn");
            string navn = Console.ReadLine();
            Console.WriteLine("Indlæs hotel adresse");
            string adresse = Console.ReadLine();

            //Kalde hotelservice vise resultatet
            HotelService hs = new HotelService();
            bool ok = hs.CreateHotel(new Hotel(hotelnr, navn, adresse));
            if (ok)
            {
                Console.WriteLine("Hotellet blev oprettet!");
            }
            else
            {
                Console.WriteLine("Fejl. Hotellet blev ikke oprettet!");
            }
        }
        private static void DoSomething()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(25);
                Console.WriteLine(i + " i GUI i main thread");
            }
        }


    }
}
