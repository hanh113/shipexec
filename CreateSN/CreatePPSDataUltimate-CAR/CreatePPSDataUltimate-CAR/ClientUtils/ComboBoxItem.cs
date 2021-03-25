using System;

namespace ClientUtilsDll
{
    public class ComboBoxItem
    {
        public string Text;

        public string Value;

        public ComboBoxItem(string text, string value)
        {
            this.Text = text;
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}