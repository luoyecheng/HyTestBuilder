﻿/*---------------------------------------------------------------------------
 * File: LBDigitalMeter.cs
 * Utente: lucabonotto
 * Date: 05/04/2009
 *-------------------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HyTestRTDataService.Controls.Base;
using HyTestRTDataService.Controls.Leds;
using HyTestRTDataService.RunningMode;

namespace HyTestRTDataService.Controls.Meters
{
    /// <summary>
    /// Class for the digital meter
    /// </summary>
    public partial class HTDigitalMeter : HTIndustrialCtrlBase
    {
        #region(* My Alter *)
        public override void OnDataChanged(object sender, EventArgs e)
        {
            FetchDataAndShow();
        }

        private void FetchDataAndShow()
        {
            if (varName == null && varName == "") return;
            RunningServer server = RunningServer.getServer();
            double value = server.NormalRead<double>(varName);
            this.Value = value;
        }
        #endregion

        #region (* Class variables *)
        protected int _dpPos = 0;
        protected int _numDigits = 0;
        #endregion

        #region (* Constructor *)
        public HTDigitalMeter()
        {
            InitializeComponent();

            // Transparent background
            this.BackColor = Color.Black;

            this.Format = "000";
        }
        #endregion

        #region (* Properties *)
        /// <summary>
        /// Background color
        /// </summary>
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;

                foreach (Control disp in this.Controls)
                {
                    if (disp.GetType() == typeof(HT7SegmentDisplay))
                    {
                        HT7SegmentDisplay d = disp as HT7SegmentDisplay;

                        d.BackColor = value;
                    }
                }
            }
        }

        /// <summary>
        /// Color of the display
        /// </summary>
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;

                foreach (Control disp in this.Controls)
                {
                    if (disp.GetType() == typeof(HT7SegmentDisplay))
                    {
                        HT7SegmentDisplay d = disp as HT7SegmentDisplay;

                        d.ForeColor = value;
                    }
                }
            }
        }

        /// <summary>
        /// Set the sign of the display
        /// </summary>
        private bool _signed = false;
        [
            Category("Digital meter"),
            Description("Set the signed value of the meter")
        ]
        public bool Signed
        {
            set
            {
                if (this._signed == value)
                    return;

                this._signed = value;

                this.UpdateControls();
            }

            get { return this._signed; }
        }

        /// <summary>
        /// Set the format of the display, without the sign
        /// </summary>
        private string _format = string.Empty;
        [
            Category("Digital meter"),
            Description("Format of the display value")
        ]
        public string Format
        {
            set
            {
                if (this._format == value)
                    return;

                this._format = value;

                this.UpdateControls();

                this.Value = this.Value;
            }

            get { return this._format; }
        }

        /// <summary>
        /// Set the value of the display
        /// </summary>
        private double val = 0.0;
        [
            Category("Digital meter"),
            Description("Value to display")
        ]
        public double Value
        {
            set
            {
                this.val = value;

                string str = this.val.ToString(this.Format);
                str = str.Replace ( ".", string.Empty );
                str = str.Replace ( ",", string.Empty );

                bool sign = false;
                if (str[0] == '-')
                {
                    sign = true;
                    str = str.TrimStart(new char[] { '-' });
                }

                if ( str.Length > this._numDigits )
                {
                    foreach (HT7SegmentDisplay d in this.Controls)
                        d.Value = (int)'E';

                    return;
                }

                int idx = 0;
                for (idx = str.Length - 1; idx >= 0; idx--)
                {
                    int id = idx;
                    if (this.Signed != false)
                        id++;
                    HT7SegmentDisplay d = this.Controls[id] as HT7SegmentDisplay;
                    d.Value = Convert.ToInt32(str[idx].ToString());
                }

                HT7SegmentDisplay s = this.Controls["digit_sign"] as HT7SegmentDisplay;
                if (s != null)
                {
                    if (sign != false)
                        s.Value = (int)'-';
                    else
                        s.Value = -1;
                }
            }

            get { return this.val; }
        }
        #endregion

        #region (* Overrided methods *)
        /// <summary>
        /// Create the default renderer
        /// </summary>
        /// <returns></returns>
        protected override IHTRenderer CreateDefaultRenderer()
        {
            return new HTDigitalMeterRenderer();
        }
        /// <summary>
        /// Resize of the control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.RepositionControls();
        }
        #endregion

        #region (* Protected methods *)
        /// <summary>
        /// Update the controls of the meter
        /// </summary>
        protected virtual void UpdateControls()
        {
            int count = this.Format.Length;

            this._dpPos = -1;

            char[] seps = new char[] { '.', ',' };
            int sepIndex = this.Format.IndexOfAny(seps);
            if (sepIndex > 0)
            {
                count--;
                this._dpPos = sepIndex - 1;
                this._numDigits = count;
            }

            this._numDigits = count;

            this.Controls.Clear();

            if (this.Signed != false)
            {
                HT7SegmentDisplay disp = new HT7SegmentDisplay();
                disp.Name = "digit_sign";
                disp.Value = -1;
                this.Controls.Add(disp);
            }

            for (int idx = 0; idx < count; idx++)
            {
                HT7SegmentDisplay disp = new HT7SegmentDisplay();

                disp.Name = "digit_" + idx.ToString();

                disp.Click += this.DisplayClicked;

                if (sepIndex - 1 == idx)
                    disp.ShowDP = true;

                this.Controls.Add(disp);
            }

            this.RepositionControls();
        }

        /// <summary>
        /// Reposition of the digital displaies
        /// </summary>
        protected void RepositionControls()
        {
            Rectangle rc = this.ClientRectangle;

            if (this.Controls.Count <= 0)
                return;

            int digitW = rc.Width / this.Controls.Count;
            bool signFind = false;
            foreach (Control disp in this.Controls)
            {
                if (disp.GetType() == typeof(HT7SegmentDisplay))
                {
                    HT7SegmentDisplay d = disp as HT7SegmentDisplay;

                    int idDigit = 0;
                    if (d.Name.Contains("digit_sign") != false)
                    {
                        signFind = true;
                    }
                    else
                    {
                        if (d.Name.Contains("digit_") != false)
                        {
                            string s = d.Name.Remove(0, 6);
                            idDigit = Convert.ToInt32(s);

                            if (signFind != false)
                                idDigit++;
                        }
                    }

                    Point pos = new Point();
                    pos.X = idDigit * digitW;
                    pos.Y = 0;
                    d.Location = pos;

                    Size dim = new Size();
                    dim.Width = digitW;
                    dim.Height = rc.Height;
                    d.Size = dim;
                }
            }
        }

        /// <summary>
        /// Event generate from the displaies in the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayClicked(object sender, System.EventArgs e)
        {
            this.InvokeOnClick(this, e);
        }
        #endregion
    }
}
