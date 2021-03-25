using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CheckAC.Entitys
{
    public class AutoSizeFormClass
    {
        public struct controlRect
        {
            public int Left;

            public int Top;

            public int Width;

            public int Height;
        }

        public List<controlRect> oldCtrl;

        public void controllInitializeSize(Form mForm)
        {
            oldCtrl = new List<controlRect>();
            controlRect item = default(controlRect);
            item.Left = mForm.Left;
            item.Top = mForm.Top;
            item.Width = mForm.Width;
            item.Height = mForm.Height;
            oldCtrl.Add(item);
            foreach (Control control in mForm.Controls)
            {
                controlRect item2 = default(controlRect);
                item2.Left = control.Left;
                item2.Top = control.Top;
                item2.Width = control.Width;
                item2.Height = control.Height;
                oldCtrl.Add(item2);
            }
        }

        public void controlAutoSize(Form mForm)
        {
            float num = (float)mForm.Width / (float)oldCtrl[0].Width;
            float num2 = (float)mForm.Height / (float)oldCtrl[0].Height;
            int num3 = 1;
            foreach (Control control in mForm.Controls)
            {
                int left = oldCtrl[num3].Left;
                int top = oldCtrl[num3].Top;
                int width = oldCtrl[num3].Width;
                int height = oldCtrl[num3].Height;
                control.Left = (int)((float)left * num);
                control.Top = (int)((float)top * num2);
                control.Width = (int)((float)width * num);
                control.Height = (int)((float)height * num2);
                num3++;
            }
        }
    }


}
