using DAL.Data;

namespace SharedLib
{
    public static class DBContextCreator
    {
        public readonly static ApplicationDbContext DbContext = new ApplicationDbContext();
    }
}
