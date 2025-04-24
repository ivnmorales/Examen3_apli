using ADMINTORNEO.Models;
using ADMINTORNEO.Clases;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Login = ADMINTORNEO.Models.Login;

namespace ADMINTORNEO.Clases
{
    public class clsAdministrador
    {
        private DBExamenEntities1 db = new DBExamenEntities1();

        public Login login { get; set; }

        public IQueryable<LoginRespuesta> Ingresar()
        {
            // Buscar administrador por usuario
            AdministradorITM admin = db.AdministradorITMs.FirstOrDefault(a => a.Usuario == login.Usuario);

            if (admin == null)
            {
                return new List<LoginRespuesta>
                {
                    new LoginRespuesta
                    {
                        Autenticado = false,
                        Mensaje = "Administrador no existe"
                    }
                }.AsQueryable();
            }

            if (admin.Clave != login.Clave)
            {
                return new List<LoginRespuesta>
                {
                    new LoginRespuesta
                    {
                        Autenticado = false,
                        Mensaje = "La clave no coincide"
                    }
                }.AsQueryable();
            }

            // Generar token
            string token = TokenGenerator.GenerateTokenJwt(admin.Usuario);

            return new List<LoginRespuesta>
            {
                new LoginRespuesta
                {
                    Usuario = admin.Usuario,
                    Autenticado = true,
                    Perfil = "Administrador",
                    PaginaInicio = "/admin",
                    Token = token,
                    Mensaje = ""
                }
            }.AsQueryable();
        }
    }
}
