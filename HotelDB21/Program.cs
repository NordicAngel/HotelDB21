using System;
using System.Collections.Generic;
using HotelDBConsole21;
using HotelDBConsole21.Models;
using HotelDBConsole21.Services;

namespace HotelDB21
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showMenu = MainMenu.Menu();

            while (showMenu)
            {
                Console.ReadLine();
                showMenu = MainMenu.Menu();
            }
            //Console.ReadLine();
        }
    }
}
