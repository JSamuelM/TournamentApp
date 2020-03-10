using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentApp.Entity_Framework;
using TournamentApp.Model;

namespace TournamentApp.Controllers
{
    /* El controlador de torneo, 
     * Una interfaz contiene definiciones para un grupo de funcionalidades relacionadas 
     * que una clase no abstracta o una estructura deben implementar. En este caso las de Controller,
     * Que esta en la carpeta Controllers
     */
    class TournamentController : Controller<tournament>
    {
        // Operación para agregar un registro
        public Operation<tournament> addRecord(tournament o)
        {
            try
            {
                // Se agrega la entitidad al contexto, el cual sera insertado en la dbo
                EntitySingleton.Context.tournament.Add(o);
                // Cuando se llama al metodo SaveChanges
                EntitySingleton.Context.SaveChanges();
                return FactoryOperation<tournament>.getSuccessOperation();
            }
            catch (Exception e)
            {
                return FactoryOperation<tournament>.getFailOperation(e.Message);
            }
        }

        // No se si vamos a eliminar, yo creo que si
        public Operation<tournament> deleteRecord(tournament o)
        {
            try
            {
                tournament t = EntitySingleton.Context.tournament.Find(o.id);
                EntitySingleton.Context.tournament.Remove(o);
                EntitySingleton.Context.SaveChanges();
                return FactoryOperation<tournament>.getSuccessOperation();
            }
            catch (Exception e)
            {
                return FactoryOperation<tournament>.getFailOperation(e.Message);
            }
        }

        public Operation<tournament> getRecords()
        {
            try
            {
                // Los datos en el contexto de torneo, los convierte en lista
                List<tournament> data = EntitySingleton.Context.tournament.ToList();
                // Retornamos la data
                return FactoryOperation<tournament>.getDataOperation(data);
            }
            catch (Exception e)
            {
                return FactoryOperation<tournament>.getFailOperation(e.Message);
            }
        }

        public Operation<tournament> updateRecord(tournament o)
        {
            try
            {
                // ALmacenamos un registro de torneo y lo buscamos por su id
                tournament t = EntitySingleton.Context.tournament.Find(o.id);
                /* Obtenemos el objeto para la entidad dada
                 * Donde se obtiene los valores de las propiedades
                 * Luego las almacenamos en un objeto
                 */
                EntitySingleton.Context.Entry(t).CurrentValues.SetValues(o);
                // Actualizamos la información
                EntitySingleton.Context.SaveChanges();
                return FactoryOperation<tournament>.getSuccessOperation();
            }
            catch (Exception e)
            {
                return FactoryOperation<tournament>.getFailOperation(e.Message);
            }
        }
    }
}
