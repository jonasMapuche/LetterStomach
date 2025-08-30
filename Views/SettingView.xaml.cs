using LetterStomach.Models;
using LetterStomach.ViewModels;

namespace LetterStomach.Views;

public partial class SettingView : ContentPage
{
    private bool _error_test = false;
    private string _error_message;

    public string error_message
    {
        get => _error_message;
        set
        {
            _error_message = value;
        }
    }

    private async void OnError(object sender, string error_message)
    {
        await DisplayAlert("Erro", error_message, "OK");
    }

    private async void OnError(string error_message)
    {
        await DisplayAlert("Erro", error_message, "OK");
    }

    private bool _update_table = false;
    private bool _upgrade_table = false;
    private int _selected_table = -1;
    private bool _upgrade_init = false;
    private int _pitch_init = 0;
    private int _volume_init = 0;
    private int _pitch_skeak = 0;
    private int _volume_skeak = 0;
        
    public SettingView(SettingViewModel ViewModel)
    {
        try
        { 
            InitializeComponent();
            ViewModel.OnError += OnError;
            BindingContext = ViewModel;
            this._upgrade_init = swiSQLite.IsToggled;
            this._pitch_init = (int)sldPich.Value;
            this._volume_init = (int)sldVolume.Value;
            ControlCheck(this._upgrade_table, this._update_table, this._selected_table, this._upgrade_init, this._pitch_skeak, this._volume_skeak, this._pitch_init, this._volume_init);
            if (_error_test) throw new InvalidOperationException("Falha na operação!");
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            OnError(this.error_message);
        }
    }

    void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        { 
            Picker picker = (Picker)sender;
            Hunks hunks = (Hunks)picker.SelectedItem;
            this._selected_table = hunks.Value;
            ControlCheck(this._upgrade_table, this._update_table, this._selected_table, this._upgrade_init, this._pitch_skeak, this._volume_skeak, this._pitch_init, this._volume_init);
            if (_error_test) throw new InvalidOperationException("Falha na operação!");
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            OnError(this.error_message);
        }
    }

    private void OnUpdateToggled(object sender, ToggledEventArgs e)
    {
        try
        { 
            Switch switchControl = (Switch)sender;
            bool toggled = switchControl.IsToggled;
            this._update_table = toggled;
            ControlCheck(this._upgrade_table, this._update_table, this._selected_table, this._upgrade_init, this._pitch_skeak, this._volume_skeak, this._pitch_init, this._volume_init);
            if (_error_test) throw new InvalidOperationException("Falha na operação!");
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            OnError(this.error_message);
        }
    }

    private void OnSQLiteToggled(object sender, ToggledEventArgs e)
    {
        try
        { 
            Switch switchControl = (Switch)sender;
            bool toggled = switchControl.IsToggled;
            this._upgrade_table = toggled;
            ControlCheck(this._upgrade_table, this._update_table, this._selected_table, this._upgrade_init, this._pitch_skeak, this._volume_skeak, this._pitch_init, this._volume_init);
            if (_error_test) throw new InvalidOperationException("Falha na operação!");
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            OnError(this.error_message);
        }
    }

    private void ControlCheck(bool sqlite, bool update, int change_select, bool upgrade, int pitch_speak, int volume_speak, int pitch_init, int volume_init)
    {
        try
        { 
            if (!upgrade)
            {
                if ((!sqlite) && (!update) && (change_select == -1)) tlbCheck.IsEnabled = false;
                if ((!sqlite) && (!update) && (change_select != -1)) tlbCheck.IsEnabled = false;
                if ((!sqlite) && (update) && (change_select == -1)) tlbCheck.IsEnabled = false;
                if ((!sqlite) && (update) && (change_select != -1)) tlbCheck.IsEnabled = true;
                if ((sqlite) && (update) && (change_select == -1)) tlbCheck.IsEnabled = false;
                if ((sqlite) && (update) && (change_select != -1)) tlbCheck.IsEnabled = true;
                if ((sqlite) && (!update) && (change_select != -1)) tlbCheck.IsEnabled = true;
                if ((sqlite) && (!update) && (change_select == -1)) tlbCheck.IsEnabled = true;
            } else
            {
                if ((sqlite) && (!update) && (change_select == -1)) tlbCheck.IsEnabled = false;
                if ((sqlite) && (!update) && (change_select != -1)) tlbCheck.IsEnabled = false;
                if ((sqlite) && (update) && (change_select == -1)) tlbCheck.IsEnabled = false;
                if ((sqlite) && (update) && (change_select != -1)) tlbCheck.IsEnabled = true;
                if ((!sqlite) && (update) && (change_select == -1)) tlbCheck.IsEnabled = false;
                if ((!sqlite) && (update) && (change_select != -1)) tlbCheck.IsEnabled = true;
                if ((!sqlite) && (!update) && (change_select == -1)) tlbCheck.IsEnabled = true;
                if ((!sqlite) && (!update) && (change_select != -1)) tlbCheck.IsEnabled = true;
            };
            if (pitch_speak != pitch_init) tlbCheck.IsEnabled = true;   
            if (volume_speak != volume_init) tlbCheck.IsEnabled = true;
            if (_error_test) throw new InvalidOperationException("Falha na operação!");
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
        }
    }

    private void OnSliderPitchChanged(object sender, ValueChangedEventArgs e)
    {
        try 
        { 
            int quantity = (int)e.NewValue;
            lblPich.Text = $"{quantity}";
            this._pitch_skeak = quantity;
            ControlCheck(this._upgrade_table, this._update_table, this._selected_table, this._upgrade_init, this._pitch_skeak, this._volume_skeak, this._volume_init, this._volume_init);
            if (_error_test) throw new InvalidOperationException("Falha na operação!");
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            OnError(this.error_message);
        }
    }

    private void OnSliderVolumeChanged(object sender, ValueChangedEventArgs e)
    {
        try 
        { 
            int quantity = (int)e.NewValue;
            lblVolume.Text = $"{quantity}";
            this._volume_skeak = quantity;
            ControlCheck(this._upgrade_table, this._update_table, this._selected_table, this._upgrade_init, this._pitch_skeak, this._volume_skeak, this._volume_init, this._volume_init);
            if (_error_test) throw new InvalidOperationException("Falha na operação!");
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            OnError(this.error_message);
        }
    }
}