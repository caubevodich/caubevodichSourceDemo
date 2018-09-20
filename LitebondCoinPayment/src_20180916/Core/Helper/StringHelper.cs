 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace Core.Helper
{
    public static class StringHelper
    {
        public static string GetPhoneNumber(this string phoneNumber)
        {
            string output = "";
            if (string.IsNullOrWhiteSpace(phoneNumber)) return "";
            string strPattern = @"[\s\:\'\""\,\+\&\?\\\!\(\)\“\”\.\/\–\-]+";
            Regex RegExp = new Regex(strPattern);
            output = RegExp.Replace(phoneNumber, "").Trim();

            if (output.StartsWith("84"))
            {
                string temp = output.Substring(2);
                output = "0" + temp;
            }

            if (output.Length < 9 && output.Length > 14)
            {
                return "ERROR_FORMAT";
            }

            int n;
            bool isNumeric = int.TryParse(output, out n);
            if (isNumeric)
            {
                string pattern = "091|094|0123|0124|0125|0127|0129|090|093|0120|0121|0122|0126|0128|098|097|096|0169|0168|0167|0166|0165|0164|0163|0162|092|0188|095|0993|0994|0995|0996|099|088";
                if (Regex.IsMatch(output, pattern))
                    return output;
                return "ERROR_FORMAT";
            }
            return "ERROR_FORMAT";
        }

        public static bool IsPhoneNumber(this string phoneNumber)
        {
            string detect = GetPhoneNumber(phoneNumber);
            if (detect == "" || detect == "ERROR_FORMAT") return false;
            return true;
        }

        public static string Number2String(this int number, bool isCaps)
        {
            if (number > 0)
            {
                Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
                return c.ToString();
            }
            return string.Empty;
        }

        public static string Number2String(this int number)
        {
            return Number2String(number, true);
        }

        public static string GetFirstName(this string value, bool isFirst = false)
        {
            if (string.IsNullOrEmpty(value)) return "";
            if (isFirst)
                return value.Split(' ').First();
            else return value.Split(' ').Last();
        }

        

        public static bool IsValidEmail(this string email)
        {
            bool result = false;
            if (String.IsNullOrEmpty(email))
                return result;
            email = email.Trim();
            result = Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return result;
        }

        public static bool IsValidPassword(this string password)
        {
            bool result = false;
            if (string.IsNullOrEmpty(password))
                return result;
            password = password.Trim();
            result = Regex.IsMatch(password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&*]).{6,20}.*$");
            return result;
        }

        public static string UrlEncode(this string stringValue)
        {
            return HttpUtility.UrlEncode(stringValue);
        }

        public static string UrlDecode(this string stringValue)
        {
            return HttpUtility.UrlDecode(stringValue);
        }

        public static string Create16DigitString()
        {
            Random RNG = new Random();
            var builder = new StringBuilder();
            while (builder.Length < 16)
            {
                builder.Append(RNG.Next(10).ToString());
            }
            return builder.ToString();
        }

        public static string RandomString(int len)
        {
            string allowedChars = "";
            allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "1,2,3,4,5,6,7,8,9,0,!,@,#,$,%,&,?";

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            StringBuilder randomString = new StringBuilder();
            var temp = string.Empty;
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                randomString.Append(temp);
            }
            return randomString.ToString();
        }

        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        private static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        private static string MyTruncateString(string str, int maxLength)
        {
            if (str.Length < maxLength)
                return str;

            var words = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words[0].Length > maxLength)
                throw new ArgumentException("Từ đầu tiên dài hơn chuỗi cần cắt");
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                if ((sb + word).Length > maxLength)
                    return string.Format("{0}...", sb.ToString().TrimEnd(' '));
                sb.Append(word + " ");
            }
            return string.Format("{0}...", sb.ToString().TrimEnd(' '));
        }

        public static string TruncateString(this string stringValue, int maxLength)
        {
            return MyTruncateString(stringValue, maxLength);
        }

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        public static string TruncateDescription(this string stringData, int maxLength)
        {
            string plainText = StripTagsRegexCompiled(stringData);
            return MyTruncateString(plainText, maxLength);
        }

        /// <summary>
        /// Ham xoa khoang trang trong chuoi
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static string RemoveWhitespace(this string stringValue)
        {
            string strPattern = @"[\s]+";
            Regex RegExp = new Regex(strPattern);
            return RegExp.Replace(stringValue, "").Trim();
        }

        private static string[] VietNamChar =
             {
           "aAeEoOuUiIdDyY",
           "áàạảãâấầậẩẫăắằặẳẵ",
           "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
           "éèẹẻẽêếềệểễ",
           "ÉÈẸẺẼÊẾỀỆỂỄ",
           "óòọỏõôốồộổỗơớờợởỡ",
           "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
           "úùụủũưứừựửữ",
           "ÚÙỤỦŨƯỨỪỰỬỮ",
           "íìịỉĩ",
           "ÍÌỊỈĨ",
           "đ",
           "Đ",
           "ýỳỵỷỹ",
           "ÝỲỴỶỸ"
             };

        public static string RemoveUnicode(this string strInput)
        {
            if (string.IsNullOrEmpty(strInput)) return string.Empty;
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    strInput = strInput.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            return strInput;
        }

        /// <summary>
        /// Ham xoa dau Unicode
        /// </summary>
        /// <param name="content"></param>
        /// <returns>Noi dung da duoc xoa dau</returns>
        public static string RemoveDiacritics(this string content)
        {
            String normalizedString = content.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        public static string RemoveDiacriticsURL(this string url)
        {
            string str = RemoveDiacritics(url).ToLower();

            string strPattern = @"[\s\:\'\""\,\+\&\?\\\!\(\)\“\”\.\/\–]+";
            Regex RegExp = new Regex(strPattern);
            return RegExp.Replace(str, "-").Trim();
        }

        public static string GetImageSrcFromContent(this string content)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string pattern = "<img([^s]|s[^r]|sr[^c]|src[^=]|src=[^'\"])*src=['\"](?<SRC>[^'\"]*)['\"]";
            MatchCollection m = Regex.Matches(content, pattern);

            foreach (Match mm in m)
            {
                stringBuilder.Append(mm.Groups["SRC"].Value + "|");
            }
            return stringBuilder.ToString();
        }

        public static List<string> GetListImageSrcFromContent(this string content)
        {
            string pattern = "<img([^s]|s[^r]|sr[^c]|src[^=]|src=[^'\"])*src=['\"](?<SRC>[^'\"]*)['\"]";
            MatchCollection m = Regex.Matches(content, pattern);
            var list = new List<string>();
            foreach (Match mm in m)
            {
                list.Add(mm.Groups["SRC"].Value);
            }
            return list;
        }

        public static string GetImageUrlFromContent(this string content)
        {
            string matchString = Regex.Match(content, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
            return matchString;
        }

        public static List<string> Find(string file)
        {
            List<string> list = new List<string>();

            MatchCollection m1 = Regex.Matches(file, @"(<a.*?>.*?</a>)",
                RegexOptions.Singleline);

            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;
                string Href = "";

                Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
                RegexOptions.Singleline);
                if (m2.Success)
                {
                    Href = m2.Groups[1].Value;
                }

                list.Add(Href);
            }
            return list;
        }

        public static string GetYoutubeID(this string url)
        {
            string Youtube = @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)";

            Match youtubeMatch = Regex.Match(url, Youtube);

            string id = string.Empty;

            if (youtubeMatch.Success)
                id = youtubeMatch.Groups[1].Value;

            return id;
        }

        public static string GetVimeoID(this string url)
        {
            string Vimeo = @"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)";

            Match vimeoMatch = Regex.Match(url, Vimeo);

            string id = string.Empty;

            if (vimeoMatch.Success)
                id = vimeoMatch.Groups[1].Value;
            return id;
        }

        public static bool IsValidXml(this string xmlString)
        {
            Regex tagsWithData = new Regex("<\\w+>[^<]+</\\w+>");

            if (string.IsNullOrEmpty(xmlString) || tagsWithData.IsMatch(xmlString) == false)
            {
                return false;
            }
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlString);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}