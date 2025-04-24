using ADMINTORNEO.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace ADMINTORNEO.Controllers
{
    [RoutePrefix("api/Torneos")]
    [Authorize] // Requiere autenticación por Bearer Token
    public class TorneosController : ApiController
    {
        private DBExamenEntities1 db = new DBExamenEntities1();


        [HttpPost]
        [Route("Insertar")]
        public IHttpActionResult Insertar([FromBody] Torneo torneo)
        {
            var admin = db.AdministradorITMs.Find(torneo.idAdministradorITM);
            if (admin == null)
                return BadRequest("Administrador no registrado");

            if (string.IsNullOrWhiteSpace(torneo.TipoTorneo))
                return BadRequest("El tipo de torneo es obligatorio");
            if (string.IsNullOrWhiteSpace(torneo.NombreTorneo))
                return BadRequest("El nombre del torneo es obligatorio");
            if (string.IsNullOrWhiteSpace(torneo.NombreEquipo))
                return BadRequest("El nombre del equipo es obligatorio");
            if (torneo.ValorInscripcion <= 0)
                return BadRequest("El valor de la inscripción debe ser mayor que cero");
            if (torneo.FechaTorneo == default)
                return BadRequest("La fecha del torneo es obligatoria");
            if (string.IsNullOrWhiteSpace(torneo.Integrantes))
                return BadRequest("Debe ingresar los integrantes del equipo");

            torneo.AdministradorITM = null;

            try
            {
                db.Torneos.Add(torneo);
                db.SaveChanges();
                return Ok("Torneo insertado correctamente");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("Consultar")]
        public IHttpActionResult Consultar(string tipo = null, string nombre = null, DateTime? fecha = null)
        {
            var torneos = db.Torneos.AsQueryable();

            if (!string.IsNullOrEmpty(tipo))
                torneos = torneos.Where(t => t.TipoTorneo == tipo);

            if (!string.IsNullOrEmpty(nombre))
                torneos = torneos.Where(t => t.NombreTorneo == nombre);

            if (fecha.HasValue)
                torneos = torneos.Where(t => t.FechaTorneo == fecha.Value);

            var lista = torneos.ToList();

            if (lista == null || lista.Count == 0)
                return NotFound();

            return Ok(lista);
        }


        // PUT: api/Torneos/Actualizar
        [HttpPut]
        [Route("Actualizar")]
        public IHttpActionResult Actualizar([FromBody] Torneo torneo)
        {
            // Buscar el torneo por ID
            var existente = db.Torneos.FirstOrDefault(t => t.idTorneos == torneo.idTorneos);
            if (existente == null)
                return BadRequest("Torneo no encontrado");

            // Validaciones
            if (string.IsNullOrWhiteSpace(torneo.TipoTorneo))
                return BadRequest("El tipo de torneo es obligatorio");
            if (string.IsNullOrWhiteSpace(torneo.NombreEquipo))
                return BadRequest("El nombre del equipo es obligatorio");
            if (torneo.ValorInscripcion <= 0)
                return BadRequest("El valor de la inscripción debe ser mayor que cero");
            if (torneo.FechaTorneo == default)
                return BadRequest("La fecha del torneo es obligatoria");
            if (string.IsNullOrWhiteSpace(torneo.Integrantes))
                return BadRequest("Debe ingresar los integrantes del equipo");

            // Actualización de los valores
            existente.TipoTorneo = torneo.TipoTorneo;
            existente.NombreEquipo = torneo.NombreEquipo;
            existente.ValorInscripcion = torneo.ValorInscripcion;
            existente.FechaTorneo = torneo.FechaTorneo;
            existente.Integrantes = torneo.Integrantes;

            try
            {
                db.SaveChanges();
                return Ok("Torneo actualizado correctamente");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        // DELETE: api/Torneos/Eliminar
        [HttpDelete]
        [Route("Eliminar")]
        public IHttpActionResult Eliminar(int idTorneo)
        {
            var torneo = db.Torneos.FirstOrDefault(t => t.idTorneos == idTorneo);
            if (torneo == null)
                return NotFound();

            try
            {
                db.Torneos.Remove(torneo);
                db.SaveChanges();
                return Ok("Torneo eliminado correctamente");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}