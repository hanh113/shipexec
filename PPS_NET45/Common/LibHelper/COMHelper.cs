using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace LibHelper
{
    public partial class COMHelper
    {
        /// <summary>初始化串行端口</summary>
        private SerialPort _serialPort;

        public SerialPort serialPort
        {
            get { return _serialPort; }
            set { _serialPort = value; }
        }

        /// <summary>
        /// COM口通信构造函数
        /// </summary>
        /// <param name="PortID">通信端口</param>
        /// <param name="baudRate">波特率</param>
        /// <param name="parity">奇偶校验位</param>
        /// <param name="dataBits">标准数据位长度</param>
        /// <param name="stopBits">每个字节的标准停止位数</param>
        /// <param name="readTimeout">获取或设置读取操作未完成时发生超时之前的毫秒数</param>
        /// <param name="writeTimeout">获取或设置写入操作未完成时发生超时之前的毫秒数</param>
        public void initCOMPort(string PortID, int baudRate, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One, int readTimeout = 100, int writeTimeout = 100)
        {
            try
            {
                serialPort = new SerialPort();
                serialPort.PortName = "COM" + PortID;//通信端口
                serialPort.BaudRate = baudRate;//波特率
                serialPort.Encoding = Encoding.ASCII;
                serialPort.Parity = parity;//奇偶校验位
                serialPort.DataBits = dataBits;//标准数据位长度
                serialPort.StopBits = stopBits;//每个字节的标准停止位数
                serialPort.ReadTimeout = readTimeout;//获取或设置读取操作未完成时发生超时之前的毫秒数
                serialPort.WriteTimeout = writeTimeout;//获取或设置写入操作未完成时发生超时之前的毫秒数
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// 打开COM口
        /// </summary>
        /// <returns>true 打开成功；false 打开失败；</returns>
        public bool Open()
        {
            try
            {
                if (serialPort.IsOpen == false)
                {
                    serialPort.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                //LogImpl.Debug(ex.ToString());
                return false;
            }
            return false;
        }

        /// <summary>
        /// 关闭COM口
        /// </summary>
        /// <returns>true 关闭成功；false 关闭失败；</returns>
        public bool Close()
        {
            try
            {
                serialPort.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断端口是否打开
        /// </summary>
        /// <returns></returns>
        public bool IsOpen()
        {
            try
            {
                return serialPort.IsOpen;
            }
            catch { throw; }
        }

        /// <summary>
        /// 向COM口发送信息
        /// </summary>
        /// <param name="sendData">16进制的字节</param>
        public void WriteData(byte[] sendData)
        {
            try
            {
                if (IsOpen())
                {
                    Thread.Sleep(5);
                    serialPort.Write(sendData, 0, sendData.Length);
                }
            }
            catch { throw; }
        }

        /// <summary>
        /// 接收来自COM的信息
        /// </summary>
        /// <returns>返回收到信息的数组</returns>
        public string[] ReceiveDataArray()
        {

            try
            {
                Thread.Sleep(5);
                if (!serialPort.IsOpen) return null;
                int DataLength = serialPort.BytesToRead;
                byte[] ds = new byte[DataLength];
                int bytecount = serialPort.Read(ds, 0, DataLength);
                return ByteToStringArry(ds);
            }
            catch (Exception)
            {
                //LogImpl.Debug("" + ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// 把字节型转换成十六进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string[] ByteToStringArry(byte[] bytes)
        {
            try
            {
                string[] strArry = new string[bytes.Length];
                for (int i = 0; i < bytes.Length; i++)
                {
                    strArry[i] = String.Format("{0:X2} ", bytes[i]).Trim();
                }
                return strArry;
            }
            catch { throw; }
        }

        /// <summary>
        /// 清除缓存数据
        /// </summary>
        public void ClearDataInBuffer()
        {
            try
            {
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();
            }
            catch { throw; }
        }



        /// <summary>
        /// 注册 数据接收事件，在接收到数据时 触发
        /// </summary>
        /// <param name="serialPort_DataReceived"></param>
        //public void AddReceiveEventHanlder(SerialPortDataReceivedDelegate serialPort_DataReceived)
        //{
        //    try
        //    {
        //        serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
        //    }
        //    catch { throw; }
        //}

        //接收事件是否有效 true开始接收，false停止接收。默认true

        //public static bool ReceiveEventFlag = true;
        /// <summary>
        /// 接收数据触发，将接收的数据，通过一个定义的数据接收事件，传递出去。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    if (ReceiveEventFlag == false)
        //    {
        //        return;
        //    }
        //    string strReceive = ReceiveDataString();
        //    if (!string.IsNullOrEmpty(strReceive))
        //    {
        //        OnReceiveDataHanlder(new ReceiveEventArgs() { ReceiveData = strReceive });
        //    }
        //}

        #region 数据接收事件
        //public event EventHandler<ReceiveEventArgs> ReceiveDataHandler;

        //protected void OnReceiveDataHanlder(ReceiveEventArgs e)
        //{
        //    EventHandler<ReceiveEventArgs> handler = ReceiveDataHandler;
        //    if (handler != null) handler(this, e);
        //}
        #endregion
        //---------------------------------------------------------


        /// <summary>

        /// 获取本机串口列表

        /// </summary>

        /// <param name="isUseReg"></param>

        /// <returns></returns>

        private List<string> GetComlist(bool isUseReg)
        {
            List<string> list = new List<string>();
            try
            {
                if (isUseReg)
                {
                    RegistryKey RootKey = Registry.LocalMachine;
                    RegistryKey Comkey = RootKey.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM");
                    String[] ComNames = Comkey.GetValueNames();
                    foreach (String ComNamekey in ComNames)
                    {
                        string TemS = Comkey.GetValue(ComNamekey).ToString();
                        list.Add(TemS);
                    }
                }
                else
                {
                    foreach (string com in SerialPort.GetPortNames())  //自动获取串行口名称  
                    {
                        list.Add(com);
                    }
                }
            }
            catch
            {
                //MessageBox.Show("串行端口检查异常！", "提示信息");
                //// System.Environment.Exit(0); //彻底退出应用程序   
            }
            return list;
        }

        /// <summary>

        /// 判断是否存在当前串口

        /// </summary>

        private void StartSerialPortMonitor()

        {

            List<string> comList = GetComlist(false); //首先获取本机关联的串行端口列表     

            if (comList.Count == 0)

            {

                //MessageBox.Show("当前设备不存在串行端口！", "提示信息");

                // System.Environment.Exit(0); //彻底退出应用程序   

            }

            else

            {

                string targetCOMPort = "COM8";

                //判断串口列表中是否存在目标串行端口

                if (!comList.Contains(targetCOMPort))

                {

                    //MessageBox.Show("提示信息", "当前设备不存在配置的串行端口！");

                    //System.Environment.Exit(0); //彻底退出应用程序   

                }

            }

        }

        private SerialPort serialPort2;

        /// <summary>

        /// 设置通讯串口

        /// </summary>

        public void setcom()

        {

            try

            {

                StartSerialPortMonitor();

                serialPort2.PortName = "COM8"; //通信端口

                serialPort2.BaudRate = 9600; //串行波特率

                serialPort2.DataBits = 8; //每个字节的标准数据位长度

                serialPort2.StopBits = StopBits.Two; //设置每个字节的标准停止位数

                serialPort2.Parity = Parity.None; //设置奇偶校验检查协议

                //下面这句是当信息中有汉字时，能正确传输，不然会出现问号。

                serialPort2.Encoding = System.Text.Encoding.GetEncoding("GB2312");

                //串口控件成员变量，字面意思为接收字节阀值，

                //串口对象在收到这样长度的数据之后会触发事件处理函数

                //一般都设为1

                serialPort2.ReceivedBytesThreshold = 1;

                serialPort2.DataReceived += new SerialDataReceivedEventHandler(CommDataReceived); //设置数据接收事件（监听）
                serialPort2.Open(); //打开串口

            }

            catch (Exception )

            {

                // MessageBox.Show(ex.Message);

            }
        }

         /// <summary>

            /// 通讯有数据进执行

            /// </summary>

            /// <param name="sender"></param>

            /// <param name="e"></param>

        public void CommDataReceived(Object sender, SerialDataReceivedEventArgs e)

        {

            getcom();

        }

        public void getcom()

        {

            try

            {

                //定义一个字段，来保存串口传来的信息。

                string str = "";



                int len = serialPort.BytesToRead;

                Byte[] readBuffer = new Byte[len];

                serialPort.Read(readBuffer, 0, len);

                str = Encoding.Default.GetString(readBuffer);





                //如果需要和界面上的控件交互显示数据，使用下面的方法。其中ttt是控件的名称。

                //this.tttt.Dispatcher.Invoke(new Action(() =>

                //{

                //    tttt.Text = str ;

                //}));





                serialPort.DiscardInBuffer();  //清空接收缓冲区     

            }

            catch (Exception )

            {

                serialPort.Close();

                //MessageBox.Show(ex.Message);

            }
        } 
      


}
}
