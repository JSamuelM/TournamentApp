using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentApp.Entity_Framework;
using TournamentApp.Model;

namespace TournamentApp.Controllers
{
    /* El controlador de equipos, 
     * Una interfaz contiene definiciones para un grupo de funcionalidades relacionadas 
     * que una clase no abstracta o una estructura deben implementar. En este caso las de Controller,
     * Que esta en la carpeta Controllers
     */
    class TeamController : Controller<team>
    {
        // Operación para agregar un registro
        public Operation<team> addRecord(team o)
        {
            try
            {
                // Se agrega la entitidad al contexto, el cual sera insertado en la dbo
                EntitySingleton.Context.team.Add(o);
                // Cuando se llama al metodo SaveChanges
                EntitySingleton.Context.SaveChanges();
                return FactoryOperation<team>.getSuccessOperation();
            }
            catch (Exception e)
            {
                return FactoryOperation<team>.getFailOperation(e.Message);
            }
        }

        // No se si vamos a eliminar, yo creo que si
        public Operation<team> deleteRecord(team o)
        {
            try
            {
                team t = EntitySingleton.Context.team.Find(o.id);
                EntitySingleton.Context.team.Remove(o);
                EntitySingleton.Context.SaveChanges();
                return FactoryOperation<team>.getSuccessOperation();
            }
            catch (Exception e)
            {
                return FactoryOperation<team>.getFailOperation(e.Message);
            }
        }

        public Operation<team> getRecords()
        {
            try
            {
                // Los datos en el contexto de torneo, los convierte en lista
                List<team> data = EntitySingleton.Context.team.ToList();
                // Retornamos la data
                return FactoryOperation<team>.getDataOperation(data);
            }
            catch (Exception e)
            {
                return FactoryOperation<team>.getFailOperation(e.Message);
            }
        }

     


        public Operation<team> updateRecord(team o)
        {
            try
            {
                // ALmacenamos un registro de torneo y lo buscamos por su id
                team t = EntitySingleton.Context.team.Find(o.id);
                /* Obtenemos el objeto para la entidad dada
                 * Donde se obtiene los valores de las propiedades
                 * Luego las almacenamos en un objeto
                 */
                EntitySingleton.Context.Entry(t).CurrentValues.SetValues(o);
                // Actualizamos la información
                EntitySingleton.Context.SaveChanges();
                return FactoryOperation<team>.getSuccessOperation();
            }
            catch (Exception e)
            {
                return FactoryOperation<team>.getFailOperation(e.Message);
            }
        }
    }
}
