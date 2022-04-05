using System;
using System.Collections.Generic;
using System.Text;

namespace HotelDBConsole21.Models
{
    
    public class Hotel
    {
        public int HotelNum { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }

        public Hotel()
        {
        }

        public Hotel(int hotelNum, string name, string address)
        {
            HotelNum = hotelNum;
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(HotelNum)}: {HotelNum}, {nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
        }
    }
}
