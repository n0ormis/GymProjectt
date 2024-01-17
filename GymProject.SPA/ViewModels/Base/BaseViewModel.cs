using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ThePowerSPAv2.ViewModels.Base;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private bool _isBusy = false;

    public bool IsBusy
    {
        get => _isBusy;
        set { SetValue(ref _isBusy, value); }
    }

    private bool _isSaving = false;

    public bool IsSaving
    {
        get => _isSaving;
        set { SetValue(ref _isSaving, value); }
    }

    private bool _isLoading = false;

    public bool IsLoading
    {
        get => _isLoading;
        set { SetValue(ref _isLoading, value); }
    }


    protected BaseViewModel()
    {
    }

    public abstract Task OnInitializedAsync();

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged();
    }

    protected void SetValue<T>(ref T backingFiled, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingFiled, value)) return;
        backingFiled = value;

        OnPropertyChanged(propertyName);
    }
}