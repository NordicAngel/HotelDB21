﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HotelDBConsole21.Services
{
    public abstract class Connection
    {
        //indsæt din egen connectionstring
        protected String connectionString = @"Data Source=mssql8.unoeuro.com;Initial Catalog=taigas_domain_dk_db_first;User ID=taigas_domain_dk;Password=enFHa6rGyxBR;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}
