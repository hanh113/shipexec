using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CreatePPSDataPpart
{
    class geneCSNfunc
    {
        public Int64 ValB(string aa)
        {
            return Convert.ToInt64(Get34toTen(aa.ToUpper()));
        }

        public string StrA(Int64 aa)
        {
            return aa.ToString();
        }


        public void CheckStrLen(TextBox s, int n)
        {
            int a = 0;
            a = s.Text.Length;
            if (a != n)
            {
                MessageBox.Show("The length of " + s.Name.ToString() + " text is error,it is " + n.ToString() + " digits", "See you later.I'm going to go to sleep,BYEBYE!");

            }
        }
        public string Get31Daycode(string day)
        {
            switch (day)
            {
                case "0":
                    {
                        return "0";
                    }

                case "1":
                    {
                        return "1";
                    }

                case "2":
                    {
                        return "2";
                    }

                case "3":
                    {
                        return "3";
                    }

                case "4":
                    {
                        return "4";
                    }

                case "5":
                    {
                        return "5";
                    }

                case "6":
                    {
                        return "6";
                    }

                case "7":
                    {
                        return "7";
                    }

                case "8":
                    {
                        return "8";
                    }

                case "9":
                    {
                        return "9";
                    }

                case "10":
                    {
                        return "A";
                    }

                case "11":
                    {
                        return "B";
                    }

                case "12":
                    {
                        return "C";
                    }

                case "13":
                    {
                        return "D";
                    }

                case "14":
                    {
                        return "E";
                    }

                case "15":
                    {
                        return "F";
                    }

                case "16":
                    {
                        return "G";
                    }

                case "17":
                    {
                        return "H";
                    }

                case "18":
                    {
                        return "J";
                    }

                case "19":
                    {
                        return "K";
                    }

                case "20":
                    {
                        return "L";
                    }

                case "21":
                    {
                        return "M";
                    }

                case "22":
                    {
                        return "N";
                    }

                case "23":
                    {
                        return "P";
                    }

                case "24":
                    {
                        return "R";
                    }

                case "25":
                    {
                        return "S";
                    }

                case "26":
                    {
                        return "T";
                    }

                case "27":
                    {
                        return "V";
                    }

                case "28":
                    {
                        return "W";
                    }

                case "29":
                    {
                        return "X";
                    }

                case "30":
                    {
                        return "Y";
                    }

                case "31":
                    {
                        return "Z";
                    }

                case "32":
                    {
                        return "-";
                    }

                default:
                    {
                        MessageBox.Show("error");
                        return null;

                    }
            }

        }
        public string Get34toTen(string num)
        {
            switch (num)
            {
                case "0":
                    {
                        return "0";
                    }

                case "1":
                    {
                        return "1";
                    }

                case "2":
                    {
                        return "2";
                    }

                case "3":
                    {
                        return "3";
                    }

                case "4":
                    {
                        return "4";
                    }

                case "5":
                    {
                        return "5";
                    }

                case "6":
                    {
                        return "6";
                    }

                case "7":
                    {
                        return "7";
                    }

                case "8":
                    {
                        return "8";
                    }

                case "9":
                    {
                        return "9";
                    }

                case "A":
                    {
                        return "10";
                    }

                case "B":
                    {
                        return "11";
                    }

                case "C":
                    {
                        return "12";
                    }

                case "D":
                    {
                        return "13";
                    }

                case "E":
                    {
                        return "14";
                    }

                case "F":
                    {
                        return "15";
                    }

                case "G":
                    {
                        return "16";
                    }

                case "H":
                    {
                        return "17";
                    }

                case "J":
                    {
                        return "18";
                    }

                case "K":
                    {
                        return "19";
                    }

                case "L":
                    {
                        return "20";
                    }

                case "M":
                    {
                        return "21";
                    }

                case "N":
                    {
                        return "22";
                    }

                case "P":
                    {
                        return "23";
                    }

                case "Q":
                    {
                        return "24";
                    }

                case "R":
                    {
                        return "25";
                    }

                case "S":
                    {
                        return "26";
                    }

                case "T":
                    {
                        return "27";
                    }

                case "U":
                    {
                        return "28";
                    }

                case "V":
                    {
                        return "29";
                    }

                case "W":
                    {
                        return "30";
                    }

                case "X":
                    {
                        return "31";
                    }

                case "Y":
                    {
                        return "32";
                    }

                case "Z":
                    {
                        return "33";
                    }

                case "-":
                    {
                        return "34";
                    }

                default:
                    {
                        MessageBox.Show("errorC");
                        return null;
                    }
            }




        }

        public string GetNumtoWvV(string num, string wv, string towv)
        {
            if (ValB(wv) < 2 | ValB(wv) > 34)
            {
                MessageBox.Show("Weight value is error");
                return null;
            }
            if (ValB(towv) < 2 | ValB(towv) > 34)
            {
                MessageBox.Show(" the change  Weight value is error");
                return null;
            }

            string numstr;
            numstr = num;
            Int64 numten = 0;
            int i = 0;
            while (numstr.Length > 0)
            {
                string a;
                a = numstr.Substring(numstr.Length - 1, 1);

                //VB:
                //numten = numten + ValB(a) * ValB(wv) ^ i
                numten = numten + ValB(a) * Convert.ToInt32(Math.Pow(ValB(wv), i));
                i = i + 1;
                numstr = numstr.Substring(0, numstr.Length - 1);

            }
            // 这时numten 是转换后的十进制

            string resultStr;
            resultStr = "";
            // 'notice : is '/'
            while ((numten / (double)ValB(towv) > 0))
            {
                resultStr = Get10to34(StrA(numten % ValB(towv))) + resultStr;

                // notice: is '\'
                numten = numten / ValB(towv);
            }

            return resultStr;
        }

        public string Get10to34(string day)
        {
            switch (day)
            {
                case "0":
                    {
                        return "0";
                    }

                case "1":
                    {
                        return "1";
                    }

                case "2":
                    {
                        return "2";
                    }

                case "3":
                    {
                        return "3";
                    }

                case "4":
                    {
                        return "4";
                    }

                case "5":
                    {
                        return "5";
                    }

                case "6":
                    {
                        return "6";
                    }

                case "7":
                    {
                        return "7";
                    }

                case "8":
                    {
                        return "8";
                    }

                case "9":
                    {
                        return "9";
                    }

                case "10":
                    {
                        return "A";
                    }

                case "11":
                    {
                        return "B";
                    }

                case "12":
                    {
                        return "C";
                    }

                case "13":
                    {
                        return "D";
                    }

                case "14":
                    {
                        return "E";
                    }

                case "15":
                    {
                        return "F";
                    }

                case "16":
                    {
                        return "G";
                    }

                case "17":
                    {
                        return "H";
                    }

                case "18":
                    {
                        return "J";
                    }

                case "19":
                    {
                        return "K";
                    }

                case "20":
                    {
                        return "L";
                    }

                case "21":
                    {
                        return "M";
                    }

                case "22":
                    {
                        return "N";
                    }

                case "23":
                    {
                        return "P";
                    }

                case "24":
                    {
                        return "Q";
                    }

                case "25":
                    {
                        return "R";
                    }

                case "26":
                    {
                        return "S";
                    }

                case "27":
                    {
                        return "T";
                    }

                case "28":
                    {
                        return "U";
                    }

                case "29":
                    {
                        return "V";
                    }

                case "30":
                    {
                        return "W";
                    }

                case "31":
                    {
                        return "X";
                    }

                case "32":
                    {
                        return "Y";
                    }

                case "33":
                    {
                        return "Z";
                    }

                case "34":
                    {
                        return "-";
                    }

                default:
                    {
                        MessageBox.Show("error");
                        return null;
                    }
            }
        }

        public string CheckSum(string a)
        {
            if (a.Length != 16)
            {
                MessageBox.Show("the serilNo is error!");
                System.Environment.Exit(0);
            }

            int EEnum = 0;
            int OOnum = 0;
            int i;

            for (i = 0; i <= a.Length - 1; i += 1)
            {
                string mm;
                mm = a.Substring(i, 1);
                EEnum = EEnum + Convert.ToInt32(Get34toTen(a.Substring(i, 1)));
                i = i + 1;
                string nn;
                nn = a.Substring(i, 1);
                OOnum = OOnum + Convert.ToInt32(Get34toTen(a.Substring(i, 1))) * 3;
            }

            if ((EEnum + OOnum) % 34 == 0)
                return "0";
            else
            {
                string s;
                // 'the  str((34 - (EEnum + OOnum) Mod 34))  is error code .  need to used ToString
                s = (34 - (EEnum + OOnum) % 34).ToString();
                return GetNumtoWvV(s, "A", "-");
            }
        }

        public string GetYW(string indatetime)
        {
            DateTime dtInput = DateTime.Now;
            if (!string.IsNullOrEmpty(indatetime))
            {
                try
                {
                    dtInput = DateTime.Parse(indatetime);
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }


            //DateTime dtNow = DateTime.Now;
            DateTime startDate = DateTime.Parse("2020-12-20");
            int i = 0;
            int j = 0;
            if ((DateTime.Compare(dtInput, startDate) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Y1";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Y2";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Y3";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Y4";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Y5";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Y6";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Y7";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Y8";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Y9";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YC";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YD";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YF";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YG";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YH";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YJ";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YK";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YL";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YM";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YN";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YP";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YQ";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YR";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YT";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YV";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YW";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "YX";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Z1";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Z2";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Z3";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Z4";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Z5";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Z6";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Z7";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Z8";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "Z9";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZC";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZD";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZF";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZG";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZH";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZJ";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZK";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZL";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZM";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZN";
            }

            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZP";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZQ";

            }

            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZR";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZT";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZV";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZW";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * ++i)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * ++j), dtInput) > 0))
            {
                return "ZX";

            }

            else { return string.Empty; }


        }

    }
}
