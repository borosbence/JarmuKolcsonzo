using CommunityToolkit.Mvvm.Input;

namespace JarmuKolcsonzo.WPF.Commands
{
    public interface IEditCommands<T>
    {
        IRelayCommand NewCmd { get; }
        IAsyncRelayCommand<T> SaveCmdAsync { get; }
        IAsyncRelayCommand<T> DeleteCmdAsync { get; }
    }
}
