using Android.Bluetooth;
using Android.Content;

namespace LetterStomach.Platforms.Android.Services
{
    public class BluetoothService
    {
        BluetoothAdapter _bluetoothAdapter;

        public BluetoothService() 
        {
            BluetoothManager bluetoothManager = (BluetoothManager)Platform.AppContext.GetSystemService(Context.BluetoothService);
            BluetoothAdapter bluetoothAdapter = bluetoothManager.Adapter;

            if ((!bluetoothAdapter.IsEnabled) && (bluetoothAdapter != null))
            {
                Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                enableBtIntent.SetFlags(ActivityFlags.NewTask); 
                Platform.AppContext.StartActivity(enableBtIntent);
            }
        }

        public void Find()
        {
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
    }
}
