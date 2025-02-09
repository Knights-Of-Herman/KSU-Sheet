using KnightsOfHerman.Backend.Database.Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Database.Testing.UnitTesting
{
    internal class DBFactory
    {
        public static AzureDBContext GetContext()
        {
            string constr = "Server=localhost;Database=KOHDev2;User Id=SA;Password=@serverpassword123;TrustServerCertificate=True";

            var options = new DbContextOptionsBuilder()
                .UseSqlServer(constr);

            return new AzureDBContext(options.Options);

        }
    }
}
