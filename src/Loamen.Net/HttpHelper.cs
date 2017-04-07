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
            Stream stream = DoGet(url).GetResponseStream();
            if (stream != null)
            {
                Image img = Image.FromStream(stream);
                return img;
            }
            return null;
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
            try
            {
                WebResponse response = DoGet(url);
                if (response != null)
                {
                    using (var stream = new StreamReader(response.GetResponseStream(), encode ?? HttpOption.Encoding))
                    {
                        html = stream.ReadToEnd();
                    }
                    response.Close();
                }
            }
            catch
            {
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