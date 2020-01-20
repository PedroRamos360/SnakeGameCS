using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace JogoDaCobrinhaCS.Classes {
    class ArquivoIni {
        public string Path { get; set; }

        public string Exe { get; set; }

        [DllImport("kernel32")]

        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32")]

        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder Retval, int Size, string FilePath);

        public ArquivoIni(string path = null) {
            Path = new FileInfo(path ?? Exe + ".ini").FullName.ToString();
        }

        public string Read(string key, string section = null) {
            var RetVal = new StringBuilder(255);

            GetPrivateProfileString(section ?? Exe, key, string.Empty, RetVal, 255, Path);

            return RetVal.ToString();
        }

        public void Write(string key, string value, string section = null) {
            WritePrivateProfileString(section ?? Exe, key, value, Path);
        }

        public void DeleteKey(string key, string section = null) {
            Write(key, null, section ?? Exe);
        }

        public void DeleteSection(string section = null) {
            Write(null, null, section ?? Exe);
        }

        public bool KeyExist(string key, string section = null) {
            return Read(key, section).Length > 0;
        }
    }
}
