using Banco;
using DTO.BancoClasses.Login;
using DTO.Login;

namespace BancoProject.Login
{
    public static class LoginDB
    {
        public static DBClass DB = new DBClass();

        public static UsuarioTO Logar(UsuarioTO user){
            Usuario? usuario = DB.Usuario.FirstOrDefault(e =>
             e.Password == user.Password &&
             e.Username == user.Username);

            UsuarioTO usuarioTO = (UsuarioTO) usuario;
            return usuarioTO; 
        }
    }
}
