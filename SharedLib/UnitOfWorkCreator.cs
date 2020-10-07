namespace SharedLib
{
    public static class UnitOfWorkCreator
    {
        public readonly static DAL.UnitOfWork Instance = new DAL.UnitOfWork();
    }
}
