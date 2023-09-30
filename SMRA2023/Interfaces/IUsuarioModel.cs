using SMRA2023.Entities;

namespace SMRA2023.Interfaces
{
    public interface IUsuarioModel
    {
        public UsuarioEntities? validarUsuario(UsuarioEntities usuario);

        public UsuarioEntities? RegisterUser(UsuarioEntities usuario);

        public UsuarioEntities? UpdateUser(UsuarioEntities usuario);
    }


}
