/* Author: Matthew Baker
 * File : MainActivity.cs
 * Program: Garage Door Opener
 * Description: An Android application that will communicate with an
 *              Arduino using bluetooth to open a garage door. 
 */

using Android.App;
using Android.Widget;
using Android.OS;

namespace Garage_Door_Opener
{
    [Activity(Label = "Garage_Door_Opener", MainLauncher = true)]
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

