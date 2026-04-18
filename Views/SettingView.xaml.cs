using LetterStomach.Enums;
using LetterStomach.Models;
using LetterStomach.Services;
using LetterStomach.ViewModels;

namespace LetterStomach.Views;

public partial class SettingView : ContentPage
{
    #region ERROR
    private bool _error_on = true;
    private bool _error_off = false;
    private string? _error_message;

    public string? error_message
    {
        get => this._error_message;
        set
        {
            this._error_message = value;
        }
    }

    private async void OnError(object sender, string error_message)
    {
        await DisplayAlert("Error", error_message, "OK");
    }

    private async void OnError(string error_message)
    {
        await DisplayAlert("Error", error_message, "OK");
        await Shell.Current.GoToAsync("..");
    }
    #endregion

    #region VARIABLE
    private bool _update_table = false;
    private bool _upgrade_table = false;
    private bool _drop_table = false;
    private int _selected_table = -1;
    private bool _upgrade_init = false;
    private int _pitch_init = 0;
    private int _volume_init = 0;
    private int _pitch_skeak = 0;
    private int _volume_skeak = 0;

    private SettingService? _settingService;
    #endregion

    #region CONSTRUCTOR
    public SettingView(SQLiteService sQLiteService)
    {
        try
        {
            if (this._error_off) throw new InvalidOperationException("Operation contructor \"Setting\" view failed!");
            else this.error_message = string.Empty;

            this._settingService = SettingService.Instance;

            InitializeComponent();
            SettingViewModel ViewModel = new SettingViewModel(sQLiteService);
            BindingContext = ViewModel;
            ViewModel.OnError += OnError;

            this._upgrade_init = swiSQLite.IsToggled;
            this._pitch_init = (int)sldPich.Value;
            this._volume_init = (int)sldVolume.Value;
            ControlCheck(this._upgrade_table, this._update_table, this._drop_table, this._selected_table, this._upgrade_init, this._pitch_skeak, this._volume_skeak, this._pitch_init, this._volume_init);
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            this.OnError(this.error_message);
        }
    }
    #endregion

    #region BUTTON
    void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this._error_off) throw new InvalidOperationException("Operation selected index changed \"Setting\" view failed!");

            Picker picker = (Picker)sender;
            Hunks hunks = (Hunks)picker.SelectedItem;
            this._selected_table = hunks.Value;
            this._upgrade_init = this._settingService.SQLiteDatabase;
            ControlCheck(this._upgrade_table, this._update_table, this._drop_table, this._selected_table, this._upgrade_init, this._pitch_skeak, this._volume_skeak, this._pitch_init, this._volume_init);
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            this.OnError(this, this.error_message);
        }
    }

    private void OnUpdateToggled(object sender, ToggledEventArgs e)
    {
        try
        {
            if (this._error_off) throw new InvalidOperationException("Operation update toggled \"Setting\" view failed!");

            Switch switchControl = (Switch)sender;
            bool toggled = switchControl.IsToggled;
            this._update_table = toggled;
            this._upgrade_init = this._settingService.SQLiteDatabase;
            ControlCheck(this._upgrade_table, this._update_table, this._drop_table, this._selected_table, this._upgrade_init, this._pitch_skeak, this._volume_skeak, this._pitch_init, this._volume_init);
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            this.OnError(this, this.error_message);
        }
    }

    private void OnSQLiteToggled(object sender, ToggledEventArgs e)
    {
        try
        {
            if (this._error_off) throw new InvalidOperationException("Operation sqlite toggled \"Setting\" view failed!");

            Switch switchControl = (Switch)sender;
            bool toggled = switchControl.IsToggled;
            this._upgrade_table = toggled;
            this._upgrade_init = this._settingService.SQLiteDatabase;
            ControlCheck(this._upgrade_table, this._update_table, this._drop_table, this._selected_table, this._upgrade_init, this._pitch_skeak, this._volume_skeak, this._pitch_init, this._volume_init);
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            this.OnError(this, this.error_message);
        }
    }

    private void OnDropToggled(object sender, ToggledEventArgs e)
    {
        try
        {
            if (this._error_off) throw new InvalidOperationException("Operation drop toggled \"Setting\" view failed!");

            Switch switchControl = (Switch)sender;
            bool toggled = switchControl.IsToggled;
            this._drop_table = toggled;
            this._upgrade_init = this._settingService.SQLiteDatabase;
            ControlCheck(this._upgrade_table, this._update_table, this._drop_table, this._selected_table, this._upgrade_init, this._pitch_skeak, this._volume_skeak, this._pitch_init, this._volume_init);
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            this.OnError(this, this.error_message);
        }
    }

    private void OnCheckSetting(object sender, EventArgs e)
    {
        try
        {
            if (this._error_off) throw new InvalidOperationException("Operation check setting \"Setting\" view failed!");

            this._upgrade_init = this._settingService.SQLiteDatabase;
            ControlCheck(this._upgrade_table, this._update_table, this._drop_table, this._selected_table, this._upgrade_init, this._pitch_skeak, this._volume_skeak, this._pitch_init, this._volume_init);
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            this.OnError(this, this.error_message);
        }
    }

    private void ControlCheck(bool sqlite, bool update, bool drop, int change_select, bool upgrade, int pitch_speak, int volume_speak, int pitch_init, int volume_init)
    {
        try 
        {
            if (this._error_off) throw new InvalidOperationException("Operation control check \"Setting\" view failed!");

            bool nothing = false;
            if ((change_select == (int)Hunk.Null) || (change_select == (int)Hunk.Nothing)) nothing = true;

            bool charge = false;
            if ((update) && (!nothing)) charge = true;

            if (!upgrade)
            {

                if ((!sqlite) && (!charge) && (!drop)) tlbCheck.IsEnabled = false;
                if ((!sqlite) && (!charge) && (drop) && (!update) && (nothing)) tlbCheck.IsEnabled = true;
                if ((!sqlite) && (!charge) && (drop) && (!update) && (!nothing)) tlbCheck.IsEnabled = false;
                if ((!sqlite) && (!charge) && (drop) && (update)) tlbCheck.IsEnabled = false;
                if ((!sqlite) && (charge) && (drop)) tlbCheck.IsEnabled = false;
                if ((!sqlite) && (charge) && (!drop)) tlbCheck.IsEnabled = true;

                if ((sqlite) && (charge) && (drop)) tlbCheck.IsEnabled = false;
                if ((sqlite) && (charge) && (!drop)) tlbCheck.IsEnabled = true;
                if ((sqlite) && (!charge) && (drop)) tlbCheck.IsEnabled = false;
                if ((sqlite) && (!charge) && (!drop) && (update)) tlbCheck.IsEnabled = false;
                if ((sqlite) && (!charge) && (!drop) && (!update) && (nothing)) tlbCheck.IsEnabled = true;
                if ((sqlite) && (!charge) && (!drop) && (!update) && (!nothing)) tlbCheck.IsEnabled = false;
            }
            else
            {
                if ((sqlite) && (!charge) && (!drop)) tlbCheck.IsEnabled = false;
                if ((sqlite) && (!charge) && (drop) && (!update) && (nothing)) tlbCheck.IsEnabled = true;
                if ((sqlite) && (!charge) && (drop) && (!update) && (!nothing)) tlbCheck.IsEnabled = false;
                if ((sqlite) && (!charge) && (drop) && (update)) tlbCheck.IsEnabled = false;
                if ((sqlite) && (charge) && (drop)) tlbCheck.IsEnabled = false;
                if ((sqlite) && (charge) && (!drop)) tlbCheck.IsEnabled = true;

                if ((!sqlite) && (charge) && (drop)) tlbCheck.IsEnabled = false;
                if ((!sqlite) && (charge) && (!drop)) tlbCheck.IsEnabled = true;
                if ((!sqlite) && (!charge) && (drop)) tlbCheck.IsEnabled = false;
                if ((!sqlite) && (!charge) && (!drop) && (update)) tlbCheck.IsEnabled = false;
                if ((!sqlite) && (!charge) && (!drop) && (!update) && (nothing)) tlbCheck.IsEnabled = true;
                if ((!sqlite) && (!charge) && (!drop) && (!update) && (!nothing)) tlbCheck.IsEnabled = false;
            };
            if (pitch_speak != pitch_init) tlbCheck.IsEnabled = true;   
            if (volume_speak != volume_init) tlbCheck.IsEnabled = true;
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            this.OnError(this, this.error_message);
        }
    }

    private void OnSliderPitchChanged(object sender, ValueChangedEventArgs e)
    {
        try 
        {
            if (this._error_off) throw new InvalidOperationException("Operation slider pitch changed \"Setting\" view failed!");

            int quantity = (int)e.NewValue;
            lblPich.Text = $"{quantity}";
            this._pitch_skeak = quantity;
            this._upgrade_init = this._settingService.SQLiteDatabase;
            ControlCheck(this._upgrade_table, this._update_table, this._drop_table, this._selected_table, this._upgrade_init, this._pitch_skeak, this._volume_skeak, this._volume_init, this._volume_init);
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            this.OnError(this, this.error_message);
        }
    }

    private void OnSliderVolumeChanged(object sender, ValueChangedEventArgs e)
    {
        try 
        {
            if (this._error_off) throw new InvalidOperationException("Operation slider volume changed \"Setting\" view failed!");

            int quantity = (int)e.NewValue;
            lblVolume.Text = $"{quantity}";
            this._volume_skeak = quantity;
            this._upgrade_init = this._settingService.SQLiteDatabase;
            ControlCheck(this._upgrade_table, this._update_table, this._drop_table, this._selected_table, this._upgrade_init, this._pitch_skeak, this._volume_skeak, this._volume_init, this._volume_init);
        }
        catch (Exception ex)
        {
            this.error_message = ex.Message;
            this.OnError(this, this.error_message);
        }
    }
    #endregion

    #region EVENT
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        this.Handler?.DisconnectHandler();
    }
    #endregion
}