namespace RecetasAPI
{
    public abstract class ServicioConGuid
    {
        public Guid ObtenerGuid { get; } = Guid.NewGuid();
    }

    public class ServicioTransient : ServicioConGuid { }
    public class ServicioScoped : ServicioConGuid { }
    public class ServicioSingleton : ServicioConGuid { }
}
