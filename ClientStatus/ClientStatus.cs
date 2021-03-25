using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Windows.Forms;
using System.Xml;

public class ClientStatus : MarshalByRefObject
{
	public static TcpChannel channel;

	private static int g_iTimeout;

	private static int iPort;

	static ClientStatus()
	{
		ClientStatus.g_iTimeout = 5;
		ClientStatus.iPort = 8086;
	}

	public ClientStatus()
	{
	}

	public bool BroadCastingMessage(int f_iCommandNo)
	{
		return true;
	}

	private static string GetValue(string fileName, string section, string field, string sValue)
	{
		string str;
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(fileName);
		XmlNode xmlNodes = xmlDocument.SelectSingleNode(string.Concat("//", section));
		if (xmlNodes != null)
		{
			str = (xmlNodes.Attributes[field] != null ? xmlNodes.Attributes[field].Value : sValue);
		}
		else
		{
			str = sValue;
		}
		return str;
	}

	public static void RegisterService()
	{
		string str = string.Concat(Application.StartupPath, Path.DirectorySeparatorChar, "PPS.xml");
		ClientStatus.g_iTimeout = int.Parse(ClientStatus.GetValue(str, "Setting", "Timeout", "5"));
		ClientStatus.iPort = int.Parse(ClientStatus.GetValue(str, "Setting", "BroadCast", "8086"));
		try
		{
			BinaryServerFormatterSinkProvider binaryServerFormatterSinkProvider = new BinaryServerFormatterSinkProvider();
			BinaryClientFormatterSinkProvider binaryClientFormatterSinkProvider = new BinaryClientFormatterSinkProvider();
			binaryServerFormatterSinkProvider.TypeFilterLevel = TypeFilterLevel.Full;
			IDictionary hashtables = new Hashtable();
			hashtables["port"] = ClientStatus.iPort + 1;
			hashtables["timeout"] = ClientStatus.g_iTimeout * 1000;
			ClientStatus.channel = new TcpChannel(ClientStatus.iPort);
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(ClientStatus), "ClientStatus", WellKnownObjectMode.Singleton);
		}
		catch
		{
		}
	}

	public static void UnregisterService()
	{
		try
		{
			ClientStatus.channel.StopListening(null);
			ChannelServices.UnregisterChannel(ClientStatus.channel);
			RemotingConfiguration.Configure(null, false);
		}
		catch
		{
		}
	}
}