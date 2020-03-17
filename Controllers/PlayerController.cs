using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentApp.Entity_Framework;
using TournamentApp.Model;



namespace TournamentApp.Controllers
{
    class PlayerController : Controller<player>
    {
        // Operación para agregar un registro
        public Operation<player> addRecord(player o)
        {
            try
            {
                // Se agrega la entitidad al contexto, el cual sera insertado en la dbo
                EntitySingleton.Context.player.Add(o);
                // Cuando se llama al metodo SaveChanges
                EntitySingleton.Context.SaveChanges();
                return FactoryOperation<player>.getSuccessOperation();
            }
            catch (Exception e)
            {
                return FactoryOperation<player>.getFailOperation(e.Message);
            }
        }

        // No se si vamos a eliminar, yo creo que si
        public Operation<player> deleteRecord(player o)
        {
            try
            {
                player t = EntitySingleton.Context.player.Find(o.id);
                EntitySingleton.Context.player.Remove(o);
                EntitySingleton.Context.SaveChanges();
                return FactoryOperation<player>.getSuccessOperation();
            }
            catch (Exception e)
            {
                return FactoryOperation<player>.getFailOperation(e.Message);
            }
        }

        public Operation<player> getRecords()
        {
            try
            {
                // Los datos en el contexto de torneo, los convierte en lista
                List<player> data = EntitySingleton.Context.player.ToList();
                // Retornamos la data
                return FactoryOperation<player>.getDataOperation(data);
            }
            catch (Exception e)
            {
                return FactoryOperation<player>.getFailOperation(e.Message);
            }
        }

        public Operation<player> updateRecord(player o)
        {
            try
            {
                // ALmacenamos un registro de torneo y lo buscamos por su id
                player t = EntitySingleton.Context.player.Find(o.id);
                /* Obtenemos el objeto para la entidad dada
                 * Donde se obtiene los valores de las propiedades
                 * Luego las almacenamos en un objeto
                 */
                EntitySingleton.Context.Entry(t).CurrentValues.SetValues(o);
                // Actualizamos la información
                EntitySingleton.Context.SaveChanges();
                return FactoryOperation<player>.getSuccessOperation();
            }
            catch (Exception e)
            {
                return FactoryOperation<player>.getFailOperation(e.Message);
            }
        }
    }
}
