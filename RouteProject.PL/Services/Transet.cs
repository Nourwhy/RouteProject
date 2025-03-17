namespace RouteProject.PL.Services
{
    public class Transet : ITransetService
    {
        public Transet()
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
