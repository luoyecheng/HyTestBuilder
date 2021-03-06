﻿using System.Drawing;
using HyTestRTDataService.Controls.Base;

namespace HyTestRTDataService.Controls.Meters
{
    public class HTDigitalMeterRenderer : HTRendererBase
    {
        #region (* Constructor *)
        public HTDigitalMeterRenderer()
        {
        }
        #endregion

        #region (* Overrided methods *)
        public override void Draw(Graphics Gr)
        {
            if (this.Meter == null)
                return;

            RectangleF _rc = new RectangleF(0, 0, this.Meter.Width, this.Meter.Height);
            Gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            this.DrawBackground(Gr, _rc);
            this.DrawBorder(Gr, _rc);
        }
        #endregion

        #region (* Properties *)
        public HTDigitalMeter Meter
        {
            get { return this.Control as HTDigitalMeter; }
        }
        #endregion

        #region (* Virtual methods *)
        public virtual bool DrawBackground(Graphics gr, RectangleF rc)
        {
            if (this.Meter == null)
                return false;

            Color c = this.Meter.BackColor;
            SolidBrush br = new SolidBrush(c);
            Pen pen = new Pen(c);

            Rectangle _rcTmp = new Rectangle(0, 0, this.Meter.Width, this.Meter.Height);
            gr.DrawRectangle(pen, _rcTmp);
            gr.FillRectangle(br, rc);

            br.Dispose();
            pen.Dispose();

            return true;
        }

        public virtual bool DrawBorder(Graphics gr, RectangleF rc)
        {
            if (this.Meter == null)
                return false;

            return true;
        }
        #endregion
    }
}
