
namespace RouteProject.PL.Services
{
    public class Scoped: IScopedService
    {
        public Scoped()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set ; }

        public string GetGuid()
        {
          return Guid.ToString();
        }
    }
}
