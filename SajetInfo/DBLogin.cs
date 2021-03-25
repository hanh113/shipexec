using System;
using System.Configuration;
using System.Text;

public class DBLogin
{
    private System.Configuration.Configuration ConfigurationInstance;

    public DBLogin()
    {
        ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
        fileMap.ExeConfigFilename = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "APService.exe.config";
        this.ConfigurationInstance = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
    }

    public string DBPassword()
    {
        return DecodeBase64(this.ConfigurationInstance.AppSettings.Settings["DBPwd"].Value);
    }

    public string DBUser()
    {
        return "ppsuser";
    }

    public static string DecodeBase64(string code)
    {
        byte[] bytes = Convert.FromBase64String(code);
        try
        {
            return Encoding.GetEncoding("utf-8").GetString(bytes);
        }
        catch
        {
            return code;
        }
    }

    public static string EncodeBase64(string code)
    {
        byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(code);
        try
        {
            return Convert.ToBase64String(bytes);
        }
        catch
        {
            return code;
        }
    }

    public string ExportPassword()
    {
        return "tech";
    }

    public string ExportUser()
    {
        return "sajet";
    }
}