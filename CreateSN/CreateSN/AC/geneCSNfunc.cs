using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CreatePPSDataAC
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
            DateTime dtNow = DateTime.Now;

            DateTime startDate = DateTime.Parse("2020-12-29");

            if ((DateTime.Compare(dtInput, startDate) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 1), dtInput) > 0))
            {
                return "C1";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 1)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 2), dtInput) > 0))
            {
                return "C2";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 2)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 3), dtInput) > 0))
            {
                return "C3";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 3)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 4), dtInput) > 0))
            {
                return "C4";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 4)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 5), dtInput) > 0))
            {
                return "C5";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 5)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 6), dtInput) > 0))
            {
                return "C6";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 6)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 7), dtInput) > 0))
            {
                return "C7";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 7)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 8), dtInput) > 0))
            {
                return "C8";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 8)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 9), dtInput) > 0))
            {
                return "C9";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 9)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 10), dtInput) > 0))
            {
                return "CC";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 10)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 11), dtInput) > 0))
            {
                return "CD";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 11)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 12), dtInput) > 0))
            {
                return "CF";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 12)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 13), dtInput) > 0))
            {
                return "CG";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 13)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 14), dtInput) > 0))
            {
                return "CH";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 14)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 15), dtInput) > 0))
            {
                return "CJ";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 15)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 16), dtInput) > 0))
            {
                return "CK";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 16)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 17), dtInput) > 0))
            {
                return "CL";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 17)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 18), dtInput) > 0))
            {
                return "CM";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 18)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 19), dtInput) > 0))
            {
                return "CN";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 19)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 20), dtInput) > 0))
            {
                return "CP";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 20)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 21), dtInput) > 0))
            {
                return "CQ";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 21)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 22), dtInput) > 0))
            {
                return "CR";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 22)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 23), dtInput) > 0))
            {
                return "CT";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 23)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 24), dtInput) > 0))
            {
                return "CV";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 24)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 25), dtInput) > 0))
            {
                return "CW";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 25)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 26), dtInput) > 0))
            {
                return "CX";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 26)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 27), dtInput) > 0))
            {
                return "D1";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 27)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 28), dtInput) > 0))
            {
                return "D2";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 28)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 29), dtInput) > 0))
            {
                return "D3";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 29)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 30), dtInput) > 0))
            {
                return "D4";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 30)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 31), dtInput) > 0))
            {
                return "D5";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 31)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 32), dtInput) > 0))
            {
                return "D6";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 32)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 33), dtInput) > 0))
            {
                return "D7";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 33)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 34), dtInput) > 0))
            {
                return "D8";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 34)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 35), dtInput) > 0))
            {
                return "D9";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 35)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 36), dtInput) > 0))
            {
                return "DC";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 36)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 37), dtInput) > 0))
            {
                return "DD";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 37)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 38), dtInput) > 0))
            {
                return "DF";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 38)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 39), dtInput) > 0))
            {
                return "DG";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 39)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 40), dtInput) > 0))
            {
                return "DH";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 40)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 41), dtInput) > 0))
            {
                return "DJ";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 41)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 42), dtInput) > 0))
            {
                return "DK";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 42)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 43), dtInput) > 0))
            {
                return "DL";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 43)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 44), dtInput) > 0))
            {
                return "DM";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 44)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 45), dtInput) > 0))
            {
                return "DN";
            }

            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 45)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 46), dtInput) > 0))
            {
                return "DP";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 46)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 47), dtInput) > 0))
            {
                return "DQ";

            }

            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 47)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 48), dtInput) > 0))
            {
                return "DR";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 48)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 49), dtInput) > 0))
            {
                return "DT";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 49)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 50), dtInput) > 0))
            {
                return "DV";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 50)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 51), dtInput) > 0))
            {
                return "DW";

            }
            else if ((DateTime.Compare(dtInput, startDate.AddDays(7 * 51)) >= 0) && (DateTime.Compare(startDate.AddDays(7 * 52), dtInput) > 0))
            {
                return "DX";

            }

            else { return string.Empty; }


        }

    }
}
