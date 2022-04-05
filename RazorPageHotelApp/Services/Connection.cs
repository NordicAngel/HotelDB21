using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RazorPageHotelApp.Services
{
    public abstract class Connection
    {
        protected String ConnectionString;
        public IConfiguration Configuration { get; }

        public Connection(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }

        public Connection(string connectString)
        {
            ConnectionString = connectString;
        }

    }

}
