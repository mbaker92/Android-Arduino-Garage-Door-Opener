/* Author: Matthew Baker
 * File : MainActivity.cs
 * Program : Garage Door Opener
 * Description : An Android application that will communicate with an
 *              Arduino using bluetooth to open a garage door. 
 * Date Created : 12/25/2017
 * Date Modified : 12/29/2017
 * 
 */

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Bluetooth;

namespace Garage_Door_Opener
{
    [Activity(Label = "Garage Door Opener", MainLauncher = true)]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

