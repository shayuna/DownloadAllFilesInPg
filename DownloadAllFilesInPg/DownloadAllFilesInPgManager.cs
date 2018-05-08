using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DownloadAllFilesInPg
{
    class DownloadAllFilesInPgManager
    {
        public void go(string sSrc)
        {
            Helper oHelper = new Helper();
            string sLocal = "blabla.txt";
            oHelper.downloadResource(sSrc, sLocal);
            string sTxt = oHelper.readFile(sLocal);
            string[] refs = oHelper.readAllHRefsInTxt(sTxt);
            for (int ii = 0; ii < refs.Length; ii++)
            {
                string sUrl = refs[ii];
                if (sUrl.IndexOf("/") == 0)
                {
                    sUrl = oHelper.getHostNmFromUrl(sSrc) + refs[ii];
                }
                oHelper.downloadResource(sUrl, oHelper.getFileNmFromResourceUrl(refs[ii]));
                System.Threading.Thread.Sleep(2000);
            }
            MessageBox.Show("סיימנו. עכשיו באמת אפשר ללכת הביתה");

        }
    }
}
