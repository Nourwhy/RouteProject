namespace RouteProject.PL.Services
{
    public class Singleton : ISingletonService
    {
        public Singleton()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
