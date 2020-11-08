using System;
using System.Collections.Generic;
using Android.App;
using Android.Widget;
using BellyTrack.Core.Models;
using LiteDB;

namespace BellyTrack.Core
{
    public class DbController
    {
        // Documents folder
        string appPath = System.Environment.GetFolderPath(
            System.Environment.SpecialFolder.Personal);

        private string appDbPath = "";

        /// <summary>
        /// ctor
        /// Opens Connection to Database
        /// or Creates new Database if none exist (first time user)
        /// </summary>
        public DbController()
        {
            try
            {
                appDbPath = System.IO.Path.Combine(appPath, "BellyTrack.db");

                // Open database (or create if not exits)
                using (var db = new LiteDatabase(appDbPath))
                {
                    // works ?
                    Toast.MakeText(Application.Context, "DB Connection Works", ToastLength.Short);
                }
            }
            catch (Exception e)
            {
                Toast.MakeText(Application.Context, e.ToString(), ToastLength.Long);
            }
            
        }



        public void SaveBellyEntry(BellyEntryModel model)
        {
            try
            {
                // Open database (or create if not exits)
                using (var db = new LiteDatabase(appDbPath))
                {
                    var bellyentrys = db.GetCollection<BellyEntryModel>("bellyentrys");
                    bellyentrys.Upsert(model);
                    bellyentrys.EnsureIndex(x => x.IdentifyGuid);

                }
            }
            catch (Exception e)
            {
                Toast.MakeText(Application.Context, e.ToString(), ToastLength.Long);
            }
           
        }



        public BellyEntryModel GetBellyEntryByNames(string Vorname, string Nachname)
        {
            try
            {
                // Open database (or create if not exits)
                using (var db = new LiteDatabase(appDbPath))
                {
                    var bellyentrys = db.GetCollection<BellyEntryModel>("bellyentrys");
                    return bellyentrys.FindOne(x => x.Vorname == Vorname && x.Nachname == Nachname);
                }
            }
            catch (Exception e)
            {
                Toast.MakeText(Application.Context, e.ToString(), ToastLength.Long);
                return new BellyEntryModel();
            }
        }
    }
}