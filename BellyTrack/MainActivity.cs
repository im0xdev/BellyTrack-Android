using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using BellyTrack.Core;
using BellyTrack.Core.Models;

namespace BellyTrack
{
    [Activity(Label = "@string/app_name", Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        DbController dbc = new DbController();

        protected override void OnCreate(Bundle savedInstanceState)
        {

            //var userdata = FindViewById<EditText>(Resource.Id.editText);
            //Android.Support.V7.App.AlertDialog.Builder alertbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            //alertbuilder.SetCancelable(false)
            //    .SetPositiveButton("Submit", delegate
            //    {
            //        Toast.MakeText(this, "Submit Input: " + userdata.Text, ToastLength.Short).Show();
            //    })
            //    .SetNegativeButton("Cancel", delegate
            //    {
            //        alertbuilder.Dispose();
            //    });


            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;


            var testBtn = FindViewById<Button>(Resource.Id.testbtn);
            testBtn.Click += OnTestBtnOnClick;

        }

        private void OnTestBtnOnClick(object sender, EventArgs e)
        {
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            this.Window.ClearFlags(WindowManagerFlags.Fullscreen);


            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View view = layoutInflater.Inflate(Resource.Layout.user_input_dialog, null);
            Android.Support.V7.App.AlertDialog.Builder alertbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            alertbuilder.SetView(view);
            var userdata = view.FindViewById<EditText>(Resource.Id.editText);
            alertbuilder.SetCancelable(false)
                .SetPositiveButton("Speichern", delegate
                {
                    Toast.MakeText(this, "Eingabe Speichern: " + userdata.Text, ToastLength.Short).Show();
                })
                .SetNegativeButton("Abbrechen", delegate
                {
                    alertbuilder.Dispose();
                });
            Android.Support.V7.App.AlertDialog dialog = alertbuilder.Create();
            dialog.Show();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private int count = 1;

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            this.Window.AddFlags(WindowManagerFlags.BlurBehind);
            this.Window.ClearFlags(WindowManagerFlags.BlurBehind);

            View view = (View) sender;


            SaveBellyEntrySample();

            var getItem = dbc.GetBellyEntryByNames("Filip", "Juric");

            Toast.MakeText(ApplicationContext, $"Aus der Datenbank: BDay: {getItem.Geburtsdatum}; Guid: {getItem.IdentifyGuid}; CreatedAt: {getItem.CreatedAt}", ToastLength.Long).Show();


            //Toast.MakeText(ApplicationContext, $"{count} Hier kann man bald Sachen hinzufügen", ToastLength.Long).Show();
            count++;
        }




        private void SaveBellyEntrySample()
        {
            var newModel = new BellyEntryModel();
            newModel.Vorname = "Filip";
            newModel.Nachname = "Juric";
            newModel.Gender = Gender.Male;
            newModel.Geburtsdatum = new DateTime(1996, 2, 7);
            newModel.IdentifyGuid = Guid.NewGuid();
            newModel.CreatedAt = DateTime.Now;

            dbc.SaveBellyEntry(newModel);
        }
	}
}

