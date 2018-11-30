using System;
using System.Collections.Generic;
using System.Text;

namespace AppPrefs.Models
{
    public class AppSettings
    {
        private string _AppSettingsVersion { get; set; }
        public string AppSettingsVersion
        {
            get { return this._AppSettingsVersion; }
            set { this._AppSettingsVersion = value; }
        }

        private string _EmailAddress { get; set; }
        public string EmailAddress
        {
            get { return this._EmailAddress; }
            set { this._EmailAddress = value; }
        }

        private bool _AdvtsConsented { get; set; }
        public bool AdvtsConsented
        {
            get { return this._AdvtsConsented; }
            set { this._AdvtsConsented = value; }
        }

        private DateTime _CutoffDate { get; set; }
        public DateTime CutoffDate
        {
            get { return this._CutoffDate; }
            set { this._CutoffDate = value; }
        }

        private string _SelectedSize { get; set; }
        public string SelectedSize
        {
            get { return this._SelectedSize; }
            set { this._SelectedSize = value; }
        }

        private int _EventsDeleteThreshold { get; set; }
        public int EventsDeleteThreshold
        {
            get { return this._EventsDeleteThreshold; }
            set { this._EventsDeleteThreshold = value; }
        }

        private int _MaxDiscountPct { get; set; }
        public int MaxDiscountPct
        {
            get { return this._MaxDiscountPct; }
            set { this._MaxDiscountPct = value; }
        }

        /*
        
        private string _Xyz { get; set; }
        public string Xyz
        {
            get { return this._Xyz; }
            set { this._Xyz = value; }
        }
        */

        public MyAppState GetMyAppState()
        {
            return new MyAppState
            {
                AppSettingsVersion = this.AppSettingsVersion,
                EmailAddress = this.EmailAddress,
                AdvtsConsented = this.AdvtsConsented,
                CutoffDate = this.CutoffDate,
                SelectedSize = this.SelectedSize,
                EventsDeleteThreshold = this.EventsDeleteThreshold,
                MaxDiscountPct = this.MaxDiscountPct,

            };  // end of: return new MyAppState

        }  // end of: GetMyAppState()

        public void SetMyAppState(MyAppState myAppState)
        {
            if (myAppState != null)
            {
                this.AppSettingsVersion = myAppState.AppSettingsVersion;
                this.EmailAddress = myAppState.EmailAddress;
                this.AdvtsConsented = myAppState.AdvtsConsented;
                this.CutoffDate = myAppState.CutoffDate;
                this.SelectedSize = myAppState.SelectedSize;
                this.EventsDeleteThreshold = myAppState.EventsDeleteThreshold;
                this.MaxDiscountPct = myAppState.MaxDiscountPct;

            }  // end of: if (myAppState != null)

        }  // end of: SetMyAppState()

    }  // end of: class AppSettings

    public class MyAppState
    {
        public string AppSettingsVersion { get; set; }
        public string EmailAddress { get; set; }
        public bool AdvtsConsented { get; set; }
        public DateTime CutoffDate { get; set; }
        public string SelectedSize { get; set; }
        public int EventsDeleteThreshold { get; set; }
        public int MaxDiscountPct { get; set; }

    }  // end of: class MyAppState

}  // end of: namespace

