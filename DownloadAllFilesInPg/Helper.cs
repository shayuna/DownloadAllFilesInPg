using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
namespace DownloadAllFilesInPg
{
    class Helper
    {
        public bool downloadResource(string sResourceUrl, string sLocalNm)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");//we must imitate a regular browser, otherwise government sites will return badgateway answer instead of the requested resource
            bool bRslt = true;
            try
            {
                wc.DownloadFile(new Uri(sResourceUrl), sLocalNm);
            }
            catch (WebException ex)
            {
                bRslt = false;
            }
            return bRslt;
        }
        public string readFile(string sFlPath)
        {
            StreamReader oFile = new StreamReader(sFlPath);
            string sTxt = oFile.ReadToEnd();
            return sTxt;
        }
        public string[] readAllHRefsInTxt(string sTxt)
        {
            List<string> refs= new List<string>();
            Regex re = new Regex("href=\"([^\"]+)");
            MatchCollection arMt = re.Matches(sTxt);
            foreach (Match mt in arMt)
            {
                string sRef=mt.Groups[1].ToString();
                string sSuffix = getFlSuffix(sRef);
                if (sSuffix=="doc" || sSuffix=="docx" || sSuffix == "xml" || sSuffix=="txt" || sSuffix=="htm" || sSuffix=="html" || sSuffix=="pdf")
                {
                    refs.Add(sRef);
                }
                
            }
            return refs.ToArray();
        }
        public string getFlSuffix(string sFlNm)
        {
            Regex re = new Regex("[^.]+$");
            Match mt = re.Match(sFlNm);
            return mt.ToString();
        }
        public string getFileNmFromResourceUrl(string sResourceUrl)
        {
            Regex re = new Regex(@"[^\/]+$");
            Match mt = re.Match(sResourceUrl);
            if (mt != null) sResourceUrl = mt.ToString();
            return sResourceUrl;
        }
        public string getHostNmFromUrl(string sUrl)
        {
            string sHostNm = sUrl;
            Regex re = new Regex("(^.+?[^/])[/][^/]");
            Match mt = re.Match(sUrl);
            if (mt != null) sHostNm = mt.Groups[1].ToString();
            return sHostNm;
        }
    }
}
