using Android.Bluetooth;
using Android.Content;

namespace LetterStomach.Platforms.Android.Services
{
    public class BluetoothService
    {
        #region ERROR
        private bool _error_on = true;
        private bool _error_off = false;
        private string _error_message;

        public string error_message
        {
            get => this._error_message;
            set
            {
                this._error_message = value;
            }
        }

        public event EventHandler<string> OnError;
        #endregion

        #region VARIABLE
        BluetoothAdapter _bluetoothAdapter;
        #endregion

        #region CONSTRUCTOR
        public BluetoothService() 
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation contructor \"Bluetooth\" service failed!");
                else this.error_message = string.Empty;

                BluetoothManager bluetoothManager = (BluetoothManager)Platform.AppContext.GetSystemService(Context.BluetoothService);
                BluetoothAdapter bluetoothAdapter = bluetoothManager.Adapter;

                if ((!bluetoothAdapter.IsEnabled) && (bluetoothAdapter != null))
                {
                    Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                    enableBtIntent.SetFlags(ActivityFlags.NewTask); 
                    Platform.AppContext.StartActivity(enableBtIntent);
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
            }
        }
        #endregion

        #region BUTTON
        public void Find()
        {
            try
            { 
                if (this._error_off) throw new InvalidOperationException("Operation find \"Bluetooth\" service failed!");

                ICollection<BluetoothDevice> pairedDevices = _bluetoothAdapter.BondedDevices;
                if (pairedDevices.Count > 0)
                {
                    foreach (BluetoothDevice device in pairedDevices)
                    {
                        String deviceName = device.Name;
                        String deviceHardwareAddress = device.Address;
                    }
                }
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
            }
        }
        #endregion
    }
}
