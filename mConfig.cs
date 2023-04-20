using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LanguageSwitcher
{
    internal class mConfig
    {
        private static string dir = "tshock/LanguageSwitcher/";
        private static string fn = "config.json";
        private static string path = Path.Combine(dir, fn);
        internal string? lang = null;
        public string Lang
        {
            get { return lang ?? "1"; } 
            set { lang = value; fWriteConfig(); }
        }
        internal mConfig() {
            fReadConfig();
        }

        private void fReadConfig()
        {
            if (lang == null)
            {
                if (!File.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                    lang = "1";
                }
                else {
                    lang = File.ReadAllText(path);
                }
                fWriteConfig();
            }
        }

        private void fWriteConfig() {
            if (!File.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }
            if (!File.Exists(path)) {
                File.Create(path).Close();
            }
            File.WriteAllText(path, lang);
        }
    }
}
