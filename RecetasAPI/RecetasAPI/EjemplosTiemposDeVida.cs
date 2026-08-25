namespace RecetasAPI
{
    public class ServicioTransient
    {
        private readonly Guid _guid;
        public ServicioTransient() => _guid = Guid.NewGuid();
        public Guid ObtenerGuid => _guid;
    }

    public class ServicioScoped
    {
        private readonly Guid _guid;
        public ServicioScoped() => _guid = Guid.NewGuid();
        public Guid ObtenerGuid => _guid;
    }

    public class ServicioSingleton
    {
        private readonly Guid _guid;
        public ServicioSingleton() => _guid = Guid.NewGuid();
        public Guid ObtenerGuid => _guid;
    }
}
