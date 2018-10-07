using CustomerCare.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCare.Data.Dataproviders
{
    internal class EntityFrameworkContext : BaseContext
    {
        static readonly String ConnectionString = $"Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\\LocalDB.mdf;Initial Catalog=LocalDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public EntityFrameworkContext() : base(ConnectionString)
        {

        }
    }
}
