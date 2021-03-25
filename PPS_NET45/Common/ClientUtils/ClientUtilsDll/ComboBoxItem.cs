namespace ClientUtilsDll
{
	public class ComboBoxItem
	{
		public string Text;

		public string Value;

		public ComboBoxItem(string text, string value)
		{
			Text = text;
			Value = value;
		}

		public override string ToString()
		{
			return Text;
		}
	}
}
