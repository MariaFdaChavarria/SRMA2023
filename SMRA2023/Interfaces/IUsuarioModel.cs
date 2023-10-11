using SMRA2023.Entities;

namespace SMRA2023.Interfaces
{
    public interface IUsuarioModel
    {
        public UsuarioEntities? validarUsuario(UsuarioEntities usuario);

        public UsuarioEntities? email_Verification(UsuarioEntities usuario);
        public void Email(string correo);
        public UsuarioEntities? RegisterUser(UsuarioEntities usuario);

        public UsuarioEntities? ConsultAcc(long qIdUser);

        public UsuarioEntities? UpdateUser(UsuarioEntities usuario, long IdUser);

        public UsuarioEntities? RegUserProg(long IdUser);

        public UsuarioEntities? DeleteAcc(long IdUser);

    }


}
