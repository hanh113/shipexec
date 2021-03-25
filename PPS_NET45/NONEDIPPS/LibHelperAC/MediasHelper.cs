using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace LibHelperAC
{
    public partial class MediasHelper
    {
        /// <summary>
        /// 多媒体助手
        /// </summary>
        public MediasHelper()
        { }
        [DllImport("winmm.dll", SetLastError = true, EntryPoint = "PlaySound", CharSet = CharSet.Auto)]
        private static extern bool PlaySoundApi(string szSound, IntPtr hModule, int flags);

        [DllImport("winmm.dll", SetLastError = true, EntryPoint = "GetLastError", CharSet = CharSet.Auto)]
        private static extern int GetLastError();

        /*
        [Flags]
        private  enum PlaySoundFlags
        {
            SND_SYNC = 0,
            SND_ASYNC = 1,
            SND_NODEFAULT = 2,
            SND_MEMORY = 4,
            SND_LOOP = 8,
            SND_NOSTOP = 16,
            SND_NOWAIT = 8192,
            SND_ALIAS = 65536,
            SND_ALIAS_ID = 1114112,
            SND_FILENAME = 131072,
            SND_RESOURCE = 262148
        }
         */

        /// <summary>
        /// 播放一个音频文件（必须是wav格式），并等待播放完成再返回程序继续执行。
        /// </summary>
        /// <param name="fileName">文本：音频文件（必须是wav格式）</param>
        /// <returns>布尔：成功 非o；失败 0</returns>
        public static bool PlaySound(string path)
        {
            return PlaySound(path, false);
            //GetLastError();
        }
        /// <summary>
        /// 播放一个音频文件（必须是wav格式），并等待播放完成再返回程序继续执行。
        /// </summary>
        /// <param name="fileName">文本：音频文件（必须是wav格式）</param>
        /// <param name="Loop">布尔：是否循环播放（SND_LOOP = 8）</param>
        /// <returns>布尔：成功 非o；失败 0</returns>
        public static bool PlaySound(string fileName, bool Loop)
        {
            try
            {
                //return PlaySound(fileName, IntPtr.Zero, (int)PlaySoundFlags.SND_FILENAME);131072
                if (Loop)
                {
                    return PlaySoundApi(fileName, IntPtr.Zero, 131072);
                }
                else
                {
                    return PlaySoundApi(fileName, IntPtr.Zero, 131072);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "播放音频");
                return false;
            }
        }
        /// <summary>
        /// 异步播放一个音频文件（必须是wav格式），播放后，不等待其完成，立即返回程序继续执行。
        /// </summary>
        /// <param name="fileName">文本：音频文件（必须是wav格式）</param>
        /// <returns>布尔：成功 True；失败 False</returns>
        public static bool PlaySoundAsync(string fileName)
        {
            return PlaySoundAsync(fileName, false);
        }

        /// <summary>
        /// 异步播放一个音频文件（必须是wav格式），播放后，不等待其完成，立即返回程序继续执行。
        /// </summary>
        /// <returns>布尔：成功 True；失败 False</returns>
        public static bool PlaySoundAsyncByConts()
        {
            //string audioPath = @"C:\Windows\media\tada.wav";
            string audioPath = string.Empty;
            string dllPath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            audioPath = Path.Combine(dllPath, "Error.wav");
            return PlaySoundAsync(audioPath, false);
        }

        /// <summary>
        /// 播放错误(NG)声音
        /// </summary>
        /// <returns></returns>
        public static bool PlaySoundAsyncByNg()
        {
            //string audioPath = @"C:\Windows\media\tada.wav";
            string audioPath = string.Empty;
            string dllPath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            audioPath = Path.Combine(dllPath, @"voice\NG.wav");
            return PlaySoundAsync(audioPath, false);
        }
        /// <summary>
        /// 播放错误(GS1label)声音
        /// </summary>
        /// <returns></returns>
        public static bool PlaySoundAsyncByGS1()
        {
            //string audioPath = @"C:\Windows\media\tada.wav";
            string audioPath = string.Empty;
            string dllPath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            audioPath = Path.Combine(dllPath, @"voice\GS1.wav");
            return PlaySoundAsync(audioPath, false);
        }
        /// <summary>
        /// 播放Hold(Hold)声音
        /// </summary>
        /// <returns></returns>
        public static bool PlaySoundAsyncByHold()
        {
            //string audioPath = @"C:\Windows\media\tada.wav";
            string audioPath = string.Empty;
            string dllPath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            audioPath = Path.Combine(dllPath, "Hold.wav"); 
            return PlaySoundAsync(audioPath, false);
        }
        /// <summary>
        /// 播放PackList打印(PackList)声音
        /// </summary>
        /// <returns></returns>
        public static bool PlaySoundAsyncByPackList()
        {
            //string audioPath = @"C:\Windows\media\tada.wav";
            string audioPath = string.Empty;
            string dllPath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            audioPath = Path.Combine(dllPath, @"voice\packingList.wav");
            return PlaySoundAsync(audioPath, false);
        }

        /// <summary>
        /// 播放DeliveryNote打印声音
        /// </summary>
        /// <returns></returns>
        public static bool PlaySoundAsyncByDeliveryNote()
        {
            //string audioPath = @"C:\Windows\media\tada.wav";
            string audioPath = string.Empty;
            string dllPath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            audioPath = Path.Combine(dllPath, @"voice\DeliveryNote.wav");
            return PlaySoundAsync(audioPath, false);
        }

        /// <summary>
        /// 播放正确(OK)声音
        /// </summary>
        /// <returns></returns>
        public static bool PlaySoundAsyncByOk()
        {
            //string audioPath = @"C:\Windows\media\tada.wav";
            string audioPath = string.Empty;
            string dllPath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            audioPath = Path.Combine(dllPath, @"voice\OK.wav");
            return PlaySoundAsync(audioPath, false);
        }
        public static bool PlaySoundAsyncByRe()
        {
            string audioPath = string.Empty;
            string dllPath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            audioPath = Path.Combine(dllPath, @"voice\RE.wav");
            return PlaySoundAsync(audioPath, false);
        }
        public static bool PlaySoundAsyncByPartError()
        {
            string audioPath = string.Empty;
            string dllPath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            audioPath = Path.Combine(dllPath, @"voice\PARTERROR.wav");
            return PlaySoundAsync(audioPath, false);
        }

        /// <summary>
        /// 异步播放一个音频文件（必须是wav格式），播放后，不等待其完成，立即返回程序继续执行。
        /// </summary>
        /// <param name="fileName">文本：音频文件（必须是wav格式）</param>
        /// <param name="Loop">布尔：是否循环播放（SND_LOOP = 8）</param>
        /// <returns>布尔：成功 True；失败 False</returns>
        public static bool PlaySoundAsync(string fileName, bool Loop) // SND_FILENAME = 131072,SND_ASYNC = 1,SND_SYNC = 0,SND_LOOP = 8
        {
            try
            {
                //return PlaySound(fileName, IntPtr.Zero, (int)PlaySoundFlags.SND_FILENAME);131072
                if (Loop)
                {
                    return PlaySoundApi(fileName, IntPtr.Zero, 131081);
                }
                else
                {
                    return PlaySoundApi(fileName, IntPtr.Zero, 131073);
                }
            }
            catch (System.Exception)
            {
                //MessageBox.Show(ex.Message, "播放音频");
                return false;
            }

        }
        /// <summary>
        /// 异步播放数字（必须是wav格式，存放于程序目录下\Voice\）
        /// </summary>
        /// <param name="num">长整型：要播放的数字语音</param>
        /// <param name="VoiceFolder">文本：语音文件目录</param>
        /// <returns>布尔：成功 True；失败 False</returns>
        public static bool PlaySoundNumber(long num, string VoiceFolder)
        {
            try
            {
                if (!VoiceFolder.EndsWith("\\")) { VoiceFolder += "\\"; }
                string strNum = num.ToString();

                int dot = strNum.IndexOf('.');
                if (dot == -1) { dot = strNum.Length - 1; }
                string c;
                for (int i = 0; i < strNum.Length; i++)
                {
                    c = strNum.Substring(i, 1);
                    if (c.Equals("."))
                    {
                        PlaySoundAsync(VoiceFolder + "DIAN.wav");
                    }
                    else
                    {
                        if (!(i == dot & c.Equals("0")))
                        {
                            PlaySoundAsync(VoiceFolder + c + ".wav");
                        }
                    }
                    System.Threading.Thread.Sleep(500);
                    if ((dot - i) > 0) 
                    {
                        PlaySoundNumberLocal(dot - i, VoiceFolder);
                    }
                }
                return true;
            }
            catch// (System.Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 异步播放数字的位
        /// </summary>
        /// <param name="num">整型：数字</param>
        /// <param name="VoiceFolder">文本：语音文件目录</param>
        private static void PlaySoundNumberLocal(int num, string VoiceFolder)
        {
            switch (num)
            {
                case 0:
                    break;
                case 1:
                    PlaySoundAsync(VoiceFolder + "SHI.wav");
                    break;

                case 2:
                    PlaySoundAsync(VoiceFolder + "BAI.wav");
                    break;
                case 3:
                    PlaySoundAsync(VoiceFolder + "QIAN.wav");
                    break;
                case 4:
                    PlaySoundAsync(VoiceFolder + "WAN.wav");
                    break;

                default:
                    break;
            }
            //System.Threading.Thread.Sleep(500);
        }



        /// <summary>
        /// 停止所有声音播放
        /// </summary>
        /// <returns>布尔：是否成功</returns>
        public static bool PlaySoundStop()
        {
            try
            {
                //SND_PURGE = &H40:
                return PlaySoundApi(null, IntPtr.Zero, 0x040);
            }
            catch //(Exception)
            {
                return false;
            }
        }

    }
}
