/* Author: Matthew Baker
 * File : MainActivity.cs
 * Program : Garage Door Opener
 * Description : An Android application that will communicate with an
 *              Arduino using bluetooth to open a garage door. 
 * Date Created : 12/25/2017
 * Date Modified : 1/19/2018
 * 
 * TO DO : Add Try Catch Blocks. Add the Bluetooth connection in a thread. Allow user to choose a
 *         new device within the app to connect to. Send a status of the door back from the Arduino
 *         to show up on the screen of the application. Disable the Button until bluetooth is connected.
 *         Add Own Icon. Better layout of the Screen. Comment Code Better. 
 * 
 */

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;
using Android.Content;
using System.Collections.Generic;
using Java.IO;
using System;

namespace Garage_Door_Opener
{
    [Activity(Label = "Garage Door Opener", MainLauncher = true)]
    public class MainActivity : Activity
    {

        // Class Variables
        private BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
        private BluetoothSocket socket;
        private System.IO.Stream output;
        private System.IO.Stream input;
        private int status = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Assign Button Variable
            Button OpenButton = FindViewById<Button>(Resource.Id.button1);

            // If the connection was successful
            bool connected = ConnectToDevice(EnableBTDevice());


            OpenButton.Click += delegate
             {
                 if (connected == true)
                 {
                     if (status == 0)
                     {
                         status = 1;

                         // Send a 1 to the Arduino to open the door
                         output.WriteByte(1);
                         System.Diagnostics.Debug.WriteLine("Turned ON");
                     }
                     else if (status == 1)
                     {
                         status = 0;

                         // Send a 0 to the Arduino to close the door
                         output.WriteByte(0);
                         System.Diagnostics.Debug.WriteLine("Turned OFF");
                     }
                 }
             };




        }

        private bool EnableBTDevice()
        {
            // Variable returned
            bool result = true;

            // If the device does not have a bluetooth adapter, return false
            if (bluetoothAdapter == null)
            {
                Toast.MakeText(this, "Device does not support Bluetooth", ToastLength.Short).Show();
                result = false;
            }

            // If the device does not have bluetooth enabled, use system popup to alert user to turn it on.
            else if (!bluetoothAdapter.IsEnabled)
            {
                Intent enableAdapter = new Intent(BluetoothAdapter.ActionRequestEnable);
                StartActivityForResult(enableAdapter, 0);
                result = true;
            }
            return result;
        }

        private bool ConnectToDevice(bool connectionResult)
        {
            // Variable Return
            bool result = false;

            // Check if the EnableBTDevice function returned true. 
            if (connectionResult == true)
            {
                // Get all devices that the device has bonded to in the past.
                ICollection<BluetoothDevice> bondedDevice = bluetoothAdapter.BondedDevices;
                if (bondedDevice != null)
                {
                    // If there is at least one device, continue
                    if (bondedDevice.Count >= 1)
                    {
                        // foreach loop to go through each bonded device
                        foreach (BluetoothDevice device in bondedDevice)
                        {
                            // If the device name is equal to the HC05 adapter for the Arduino, create a connection
                            if (device.Name == "GD1")
                            {
                                socket = device.CreateInsecureRfcommSocketToServiceRecord(Java.Util.UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
                                socket.Connect();

                                // assign the input and output streams
                                output = socket.OutputStream;
                                input = socket.InputStream;

                                // Exit function with result true
                                result = true;
                                return result;

                            }
                        }
                    }
                }
            }
            return result;

        }


    }
}

