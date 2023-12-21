using Banco;

namespace BancoProject.Login
{
    public static class DBInstance
    {
        public static DBClass DB { get; set; } = new DBClass();
    }
}