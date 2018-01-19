/* Author: Matthew Baker
 * File : MainActivity.cs
 * Program : Garage Door Opener
 * Description : An Android application that will communicate with an
 *              Arduino using bluetooth to open a garage door. 
 * Date Created : 12/25/2017
 * Date Modified : 1/18/2018
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
            Button OpenButton = FindViewById<Button>(Resource.Id.button1);

            System.Diagnostics.Debug.WriteLine("Before Connection");

            bool connected = ConnectToDevice(EnableBTDevice());

            System.Diagnostics.Debug.WriteLine(connected);

            OpenButton.Click += delegate
             {
                 if (connected == true)
                 {
                     if (status == 0)
                     {
                         status = 1;

                         output.WriteByte(1);
                         System.Diagnostics.Debug.WriteLine("Turned ON");
                     }
                     else if (status == 1)
                     {
                         status = 0;
                         output.WriteByte(0);
                         System.Diagnostics.Debug.WriteLine("Turned OFF");
                     }
                 }
             };
            


          
        }

        private bool EnableBTDevice()
        {
            bool result = true;
  
            if(bluetoothAdapter == null)
            {
                Toast.MakeText(this, "Device does not support Bluetooth", ToastLength.Short).Show();
                result = false;
            }
            else if (!bluetoothAdapter.IsEnabled)
            {
                Intent enableAdapter = new Intent(BluetoothAdapter.ActionRequestEnable);
                StartActivityForResult(enableAdapter, 0);
                result = true;
            }
            return result;
        }

        private bool ConnectToDevice( bool connectionResult)
        {
            bool result = false;
            if (connectionResult == true)
            {
               ICollection<BluetoothDevice> bondedDevice = bluetoothAdapter.BondedDevices;
                    if(bondedDevice != null)
                    {
                        if(bondedDevice.Count >= 1)
                        {
                            foreach(BluetoothDevice device in bondedDevice)
                            {
                                if(device.Name == "GD1")
                                {

                                socket = device.CreateInsecureRfcommSocketToServiceRecord(Java.Util.UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
                               // try
                             //   {
                                    socket.Connect();
                                    System.Console.WriteLine("GD1 found");
                                    output = socket.OutputStream;
                                    input = socket.InputStream;
                                    // Do Stuff
                                    result = true;
                                    return result;
                               // }catch(Exception CloseEx)
                               // {

                                //}
                                }
                            }
                        }
                    }
             }
            return result;
           
        }




    }
}

