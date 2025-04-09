namespace BlazorAppWASM.Services.Interfaces
{
    public interface IStateService
    {
        bool GetIsConnected();
        void SetUserConnected(bool isConnected);
        event Action OnChange;
    }
}
