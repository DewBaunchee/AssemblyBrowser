namespace AssemblyBrowserLib.Entity.Factory
{
    public interface IFactory<K, T>
    {
        T Produce(K typeInfo);
    }
}