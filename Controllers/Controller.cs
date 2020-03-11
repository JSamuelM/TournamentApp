using TournamentApp.Model;

namespace TournamentApp.Controllers
{
    interface Controller<T>
    {
        Operation<T> addRecord(T o); // agregar
        Operation<T> updateRecord(T o); // actualizar
        Operation<T> deleteRecord(T o); // eliminar
        Operation<T> getRecords(); // select
    }
}
