using System.Runtime.InteropServices;
using System.Text;

namespace Loamen.Common
{
    /// <summary>
    ///     ��дINI�����ļ�
    /// </summary>
    public class IniHelper
    {
        public string path; //INI�ļ���

        public IniHelper(string INIPath)
        {
            path = INIPath;
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
                                                          int size, string filePath);

        //������дINI�ļ���API����

        /// <summary>
        ///     дINI�ļ�
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void write(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        /// <summary>
        ///     ��ȡINI�ļ�
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string read(string Section, string Key)
        {
            try
            {
                var temp = new StringBuilder(255);
                int i = GetPrivateProfileString(Section, Key, "", temp, 255, path);
                return temp.ToString();
            }
            catch
            {
                return "";
            }
        }
    }
}