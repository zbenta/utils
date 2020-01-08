using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChromeDriverUpdater
{
    /// <summary>Class <c>UpdateDriver</c> has a methos that updates the existing chrome driver
    /// to the latest version.</summary>
    ///
    public class UpdateDriver
    {
        /// <summary>This method verifies the existing Chrome version
        ///    downloads the latest chrome driver to a specific folder
        ///    removes the previous version and replaces it with the new one
        /// <param name="driver_src"> Path to the Chrome exe file. </param>
        /// <param name="path_to_download_zip"> Path to where we would like to download the zip with the new chrome driver. </param>
        /// <param name="file_to_delete"> Full path to the previous chrome driver we want to delete. </param>
        /// <param name="path_to_extraction"> Path into wich you want to extract the new chrome driver. </param>
        /// </summary>
        public int Get_New_Driver(string driver_src,string path_to_download_zip, string file_to_delete, string path_to_extraction)
        {
            int result = 0;
            string the_url_retreiver = "https://chromedriver.storage.googleapis.com/LATEST_RELEASE_";
            string browser_version = "";
            // get the browser version
            //string src = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            string src = driver_src;
            browser_version = System.Diagnostics.FileVersionInfo.GetVersionInfo(src).ProductVersion.ToString();
            browser_version = browser_version.Substring(0, browser_version.LastIndexOf("."));
            the_url_retreiver = the_url_retreiver + browser_version;
            //read the data
            WebClient client = new WebClient();
            string latest_version = client.DownloadString(the_url_retreiver);
            //verify if we get the new version
            if (latest_version != "")
            {
                result = 1;
            }
            //append correct version to url
            string the_url_download = "https://chromedriver.storage.googleapis.com/";
            the_url_download = the_url_download + latest_version + "/chromedriver_win32.zip";
            //get the new driver
            //client.DownloadFile(the_url_download, @"C:\Mydir\chromedriver_win32.zip");
            client.DownloadFile(the_url_download, path_to_download_zip);
            //File.Delete(@"C:\Mydrive\chromedriver.exe");
            File.Delete(file_to_delete);
            //ZipFile.ExtractToDirectory(@"C:\Mydir\chromedriver_win32.zip", @"C:\Mydir");
            ZipFile.ExtractToDirectory(path_to_download_zip,path_to_extraction);
            File.Delete(path_to_download_zip);
            return result;
        }
    }
}
