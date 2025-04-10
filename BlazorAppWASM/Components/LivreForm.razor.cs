using BlazorAppWASM.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace BlazorAppWASM.Components;

public partial class LivreForm
{
    

    [Parameter]
    public Livre Livre { get; set; } = new();

    [Parameter]
    public EventCallback<Livre> OnSave { get; set; }

    [Parameter]
    public EventCallback OnCancel { get; set; }

    private async Task HandleSubmit(EditContext context)
    {
        if (context.Validate())
        {
            await OnSave.InvokeAsync(Livre);
        }
    }

    private async Task HandleCancel()
    {
        await OnCancel.InvokeAsync();
    }
} 