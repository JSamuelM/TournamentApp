using TournamentApp.Entity_Framework;

namespace TournamentApp
{
    public sealed class EntitySingleton
    {
        private readonly static Entities context = new Entities();

        private EntitySingleton()
        {

        }

        public static Entities Context
        {
            get { return EntitySingleton.context; }
        }
    }
}
