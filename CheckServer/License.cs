using System;

public class License
{
	public License()
	{
	}

	public static bool CheckLicense(out string sMsg)
	{
		sMsg = "Chroma License";
		return true;
	}
}