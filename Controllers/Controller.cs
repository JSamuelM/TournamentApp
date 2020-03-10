using TournamentApp.Model;

namespace TournamentApp.Controllers
{
    interface Controller<T>
    {
        Operation<T> addRecord(T o);
        Operation<T> updateRecord(T o);
        Operation<T> deleteRecord(T o);
        Operation<T> getRecords();
    }
}
