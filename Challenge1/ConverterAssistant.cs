using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge1
{
    public class ConverterAssistant
    {
        public bool Load()
        {
            if (!Dictionary.Instance.Load())
            {
                Console.WriteLine("Load;Dictionary loading failed.");
                return false;
            }

            translationTable = Dictionary.Instance.TranslationTables.First(i => i.Name == "pt_BR");
            return true;
        }

        public string convertAmount2Words(string m, string n)
        {
            int qTen = m.Length, ten = 0, len;
            bool twoDigits = true;
            string word = "", cents = "", temp;
            string subNumber = m, procNumber;
            string conj = " " + translationTable.Translations.
                First(j => j.InCode.Equals("conjunction")).OutCode + " ";

            // The currency parser
            if (double.Parse(m) > 0)
            {
                // decomposing this number between a single
                // and a double values, to properly evaluate
                // if it is a hundred or a decimal entry
                if (m.Length > 1)
                {
                    do
                    {
                        len = twoDigits ? 2 : 1;
                        twoDigits = !twoDigits;
                        qTen -= len;
                        ten = m.Length - qTen;

                        procNumber = subNumber.Substring(qTen, len);
                        subNumber = subNumber.Substring(0, qTen);

                        temp = BuildNumber(ten, procNumber);
                        word = temp + (temp.Length > 0 && word.Length > 0 && ten > 2 ? conj : "") + word;
                    } while (subNumber.Length > 1);
                }

                // the latest remaining values
                if (subNumber.Length > 0)
                {
                    if (subNumber.Equals("1") && ten == 2 && word.Length > 0)
                    {
                        temp = translationTable.Translations.
                            First(j => j.InCode.Equals("101")).OutCode;
                        word = temp + (word.Length > 0 ? conj : "") + word;
                    }
                    else
                    {
                        temp = BuildNumber(ten + 1, subNumber);

                        // missing thousands multiplier
                        if (word.Length == 0 && (ten + 1) % 3 == 0 && ten > 4)
                        {
                            word = temp + " " + translationTable.Translations.
                                First(j => j.InCode.Equals("1" + new String('0', ten - 2))).OutCode;
                        }
                        else
                            word = temp + (word.Length > 0 ? conj : "") + word;
                    }
                }

                // it is a plural or a singular currency
                if (ten == 0)
                    word += " " + translationTable.Translations.
                        First(j => j.InCode.Equals("curr")).OutCode;
                else
                    word += " " + translationTable.Translations.
                        First(j => j.InCode.Equals("currs")).OutCode;
            }

            // the cents parser
            if(int.Parse(n) > 0)
            { 
                cents = BuildNumber(2, n);

                word += conj + cents;
                if (int.Parse(n) == 1)
                    word += " " + translationTable.Translations.
                        First(j => j.InCode.Equals("cent")).OutCode;
                else
                    word += " " + translationTable.Translations.
                        First(j => j.InCode.Equals("cents")).OutCode;
            }
            
            return char.ToUpper(word[0]) + word.Substring(1);
        }

        private string BuildNumber(int ten, string number)
        {
            if (number.Length == 0 || int.Parse(number) == 0)
                return "";

            int n = int.Parse(number);
            string word = "";
            string conj = " " + translationTable.Translations.
                First(j => j.InCode.Equals("conjunction")).OutCode + " ";

            if (ten % 3 != 0)
            {
                if (n > 0)
                {
                    if (n < 20)
                        word = translationTable.Translations
                            .First(j => j.InCode.Equals(n.ToString())).OutCode;
                    else
                    {
                        string d1 = number.Substring(0, 1) + "0", d2 = number.Substring(1, 1);
                        word = translationTable.Translations.
                                First(j => j.InCode.Equals(d1)).OutCode +
                                (d2.Equals("0") ? "" : conj + translationTable.Translations.
                                    First(j => j.InCode.Equals(d2)).OutCode);
                    }
                }

                bool singular = n == 1;

                switch (ten)
                {
                    case 1:
                    case 2: break;
                    case 4:
                    case 5:
                        word += " " + translationTable.Translations.
                            First(j => j.InCode.Equals("1000")).OutCode;
                        break;
                    case 7:
                    case 8:
                        word += " " + translationTable.Translations.
                            First(j => j.InCode.Equals(
                                "100000" + (singular ? "0" : "1")
                                )).OutCode;
                        break;
                    case 10:
                    case 11:
                        word += " " + translationTable.Translations.
                            First(j => j.InCode.Equals(
                                "1000000000" + (singular ? "0" : "1")
                                )).OutCode;
                        break;
                }
            }
            else
            {
                word += translationTable.Translations.
                            First(j => j.InCode.Equals(
                                number.ToString() + "00")).OutCode;
            }

            return word;
        }

        CTranslationTable translationTable;
    }
}
