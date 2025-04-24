using ADMINTORNEO.Models;
using System;
using System.Linq;

namespace ADMINTORNEO.Clases
{
    public class clsTorneo
    {
        private DBExamenEntities1 db = new DBExamenEntities1(); // Contexto BD
        public Torneo torneo { get; set; } // Entidad de torneo

        // Insertar nuevo torneo
        public string Insertar(string usuarioAdmin)
        {
            var admin = db.AdministradorITMs.FirstOrDefault(a => a.Usuario == usuarioAdmin);
            if (admin == null)
                return "Administrador no registrado";

            // Validaciones
            if (string.IsNullOrWhiteSpace(torneo.TipoTorneo))
                return "El tipo de torneo es obligatorio";
            if (string.IsNullOrWhiteSpace(torneo.NombreTorneo))
                return "El nombre del torneo es obligatorio";
            if (string.IsNullOrWhiteSpace(torneo.NombreEquipo))
                return "El nombre del equipo es obligatorio";
            if (torneo.ValorInscripcion <= 0)
                return "El valor de la inscripción debe ser mayor que cero";
            if (torneo.FechaTorneo == default)
                return "La fecha del torneo es obligatoria";
            if (string.IsNullOrWhiteSpace(torneo.Integrantes))
                return "Debe ingresar los integrantes del equipo";

            torneo.idAdministradorITM = admin.idAministradorITM;

            try
            {
                db.Torneos.Add(torneo);
                db.SaveChanges();
                return "Torneo registrado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al registrar el torneo: " + ex.Message;
            }
        }

        // Consultar torneo por tipo, nombre y fecha
        public Torneo Consultar(string tipo, string nombre, DateTime fecha)
        {
            var torneoBuscado = db.Torneos
                .FirstOrDefault(t => t.TipoTorneo == tipo && t.NombreTorneo == nombre && t.FechaTorneo == fecha);

            return torneoBuscado;
        }

        // Actualizar torneo
        public string Actualizar(string nombreTorneo)
        {
            var existente = db.Torneos.FirstOrDefault(t => t.NombreTorneo == nombreTorneo);
            if (existente == null)
                return "Torneo no encontrado";

            // Validaciones
            if (string.IsNullOrWhiteSpace(torneo.TipoTorneo))
                return "El tipo de torneo es obligatorio";
            if (string.IsNullOrWhiteSpace(torneo.NombreEquipo))
                return "El nombre del equipo es obligatorio";
            if (torneo.ValorInscripcion <= 0)
                return "El valor de la inscripción debe ser mayor que cero";
            if (torneo.FechaTorneo == default)
                return "La fecha del torneo es obligatoria";
            if (string.IsNullOrWhiteSpace(torneo.Integrantes))
                return "Debe ingresar los integrantes del equipo";

            // Actualizar campos
            existente.TipoTorneo = torneo.TipoTorneo;
            existente.NombreEquipo = torneo.NombreEquipo;
            existente.ValorInscripcion = torneo.ValorInscripcion;
            existente.FechaTorneo = torneo.FechaTorneo;
            existente.Integrantes = torneo.Integrantes;

            try
            {
                db.SaveChanges();
                return "Torneo actualizado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el torneo: " + ex.Message;
            }
        }

        // Eliminar torneo
        public string Eliminar(string nombreTorneo)
        {
            var torneoEliminar = db.Torneos.FirstOrDefault(t => t.NombreTorneo == nombreTorneo);
            if (torneoEliminar == null)
                return "Torneo no encontrado";

            try
            {
                db.Torneos.Remove(torneoEliminar);
                db.SaveChanges();
                return "Torneo eliminado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el torneo: " + ex.Message;
            }
        }
    }
}
