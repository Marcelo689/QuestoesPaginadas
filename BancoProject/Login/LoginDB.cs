using DTO.BancoClasses.Login;
using DTO.Login;

namespace BancoProject.Login
{
    public static class LoginDB
    {
        public static UsuarioTO Logar(UsuarioTO user){
            Usuario? usuario =  DBInstance.DB.Usuario.FirstOrDefault(e =>
             e.Password == user.Password &&
             e.Username == user.Username);

            bool usuarioExiste = usuario != null;
            if (usuarioExiste)
                usuario.AtualizaDataLogin();   

            UsuarioTO usuarioTO = (UsuarioTO) usuario;
            usuarioTO.IsTeacher = DBInstance.DB.Professor.FirstOrDefault(user => user.Usuario.Id == usuario.Id) != null;
            return usuarioTO; 
        }
    }
}
