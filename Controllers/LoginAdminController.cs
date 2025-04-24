using ADMINTORNEO.Models;
using ADMINTORNEO.Clases;
using System.Linq;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace Servicios_6_8.Controllers
{
    [RoutePrefix("api/LoginAdmin")]
    public class LoginAdminController : ApiController
    {
        [HttpPost]
        [Route("IngresarAdministrador")]
        public IQueryable<LoginRespuesta> IngresarAdministrador(ADMINTORNEO.Models.Login login)
        {
            clsAdministrador _admin = new clsAdministrador();
            _admin.login = login;
            return _admin.Ingresar();
        }
    }
}
