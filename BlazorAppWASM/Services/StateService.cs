using BlazorAppWASM.Services.Interfaces;

public class StateService : IStateService
{
    private bool isConnected = false;

    public event Action OnChange;

    public bool GetIsConnected() => isConnected;

    public void SetUserConnected(bool isConnected)
    {
        this.isConnected = isConnected;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
