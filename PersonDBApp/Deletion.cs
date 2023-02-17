using PersonDBApp.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDBApp
{
    internal class Deletion
    {
        public void Delete()
        {
            using (ApplicationContext db = new())
            {
                db.Database.EnsureDeleted();
            };
        }       
    }
}
