using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
namespace INI
{

    /// <summary>
    /// Create a New INI file to store or load data
    /// </summary>
    public class IniFile
    {
        private string _path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public IniFile(string INIPath)
        {
            _path = INIPath;
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void WriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, this._path).ToString();
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <returns>String value of the key is member of section</returns>
        public string ReadValue(string section, string key)
        {
            StringBuilder value = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", value, 255, this._path);
            return value.ToString();
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="path"></PARAM>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <returns>String value of the key is member of section</returns>
        public static string ReadValue(string path, string section, string key)
        {
            StringBuilder value = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", value, 255, path);
            return value.ToString();
        }

        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public static void WriteValue(string path, string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, path).ToString();
        }
    }
    
}
