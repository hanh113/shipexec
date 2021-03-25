using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EDIPPS
{
    class ButtonArray : System.Collections.CollectionBase
    {
        private readonly System.Windows.Forms.Panel HostPanel;

        public void AddNewButton(string text, string tag)
        {
            Button button = new Button();
            this.List.Add(button);
            HostPanel.Controls.Add(button);
            button.Name = "Button" + Convert.ToString(Count);
            button.Font = new Font("Tahoma", 9, FontStyle.Bold);
            button.Top = (Count - 1) * button.Height;
            button.Left = 0;
            button.Width = HostPanel.Width;
            button.Tag = tag;
            button.BackColor = Color.Transparent;
            button.Text = text;
            if (File.Exists(Program.skinPath + Program.skinName + @"\button.jpg"))
            {
                button.BackgroundImage = Image.FromFile(Program.skinPath + Program.skinName + Path.DirectorySeparatorChar + "button.jpg");
                button.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        public ButtonArray(System.Windows.Forms.Panel host)
        {
            HostPanel = host;
        }

        public System.Windows.Forms.Button this[int Index]
        {
            get
            {
                return (System.Windows.Forms.Button)this.List[Index];
            }
        }

        public void Remove()
        {
            if (this.Count > 0)
            {
                HostPanel.Controls.Remove(this[this.Count - 1]);
                this.List.RemoveAt(this.Count - 1);
            }
        }
    }
}
