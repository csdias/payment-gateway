namespace InterfaceAdapters.Interfaces
{
    public interface IClientSession
    {
        string GetClientId();
        IClientSession SetClientId(string clientId);
    }
}