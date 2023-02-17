using PersonDBApp.DataBase;

namespace PersonDBApp.Querys
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
