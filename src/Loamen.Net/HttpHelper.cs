using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;

namespace Loamen.Net
{
    public class HttpHelper : HttpBase
    {
        #region Methods

        /// <summary>
        ///     �õ�һ������ͼƬ
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Image GetImage(string url)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                Stream stream = DoGet(webRequest).GetResponseStream();
                if (stream != null)
                {
                    Image img = Image.FromStream(stream);
                    return img;
                }
                return null;
            }
            finally
            {
                if(webRequest != null)
                {
                    webRequest.Abort();
                    webRequest = null;
                }
            }
        }

        /// <summary>
        ///     ���ݷ�������ȡ���ص�html����
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public string GetHtml(string url, Encoding encode = null)
        {
            string html = "";
            WebResponse response = null;

            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                response = DoGet(webRequest);
                if (response != null)
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), encode ?? HttpOption.Encoding))
                    {
                        html = stream.ReadToEnd();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if(webRequest != null)
                {
                    webRequest.Abort();
                    webRequest = null;
                }
            }
            return html;
        }

        /// <summary>
        ///     ���ݷ�������ȡ���ص�html����
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public string GetHtml(string url, string postData, Encoding encode = null)
        {
            string html = "";
            try
            {
                WebResponse response = DoPost(url, postData);
                using (var stream = new StreamReader(response.GetResponseStream(), encode ?? HttpOption.Encoding))
                {
                    html = stream.ReadToEnd();
                }
                response.Close();
            }
            catch
            {
            }
            return html;
        }

        #endregion
    }
}