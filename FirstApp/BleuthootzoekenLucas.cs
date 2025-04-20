using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;

namespace FirstApp
{
    class BleuthootzoekenLucas
    {
        private BluetoothLEAdvertisementWatcher _watcher;

        public BleuthootzoekenLucas()
        {
            _watcher = new BluetoothLEAdvertisementWatcher
            {
                ScanningMode = BluetoothLEScanningMode.Active
            };

            _watcher.Received += (sender, args) =>
            {
                Debug.WriteLine($"Device gevonden: {args.Advertisement.LocalName ?? "Unknown"}");
            };
        }

        public void BLE_Start()
        {
            if (_watcher.Status != BluetoothLEAdvertisementWatcherStatus.Started)
            {
                _watcher.Start();
                Debug.WriteLine("BLE scan gestart");
            }
        }

        public void BLE_Stop()
        {
            if (_watcher.Status == BluetoothLEAdvertisementWatcherStatus.Started)
            {
                _watcher.Stop();
                Debug.WriteLine("BLE scan gestopt");
            }
        }
    }
}

