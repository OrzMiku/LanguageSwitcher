using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using TerrariaApi;
using TerrariaApi.Server;
using TShockAPI;
using Newtonsoft.Json;


namespace LanguageSwitcher
{
    [ApiVersion(2, 1)]
    public class Program : TerrariaPlugin
    {

        /// <summary>
        /// 插件作者
        /// </summary>
        public override string Author => "OrzMiku";

        /// <summary>
        /// 插件描述
        /// </summary>
        public override string Description => "Change TShock's Language Easily.";

        /// <summary>
        /// 插件名称
        /// </summary>
        public override string Name => "LanguageSwitcher";

        /// <summary>
        /// 插件版本
        /// </summary>
        public override Version Version => new Version(1, 0, 0, 0);

        mConfig config = new mConfig();

        List<string> supportLang = new List<string>{
                "English（ID：1）",
                "Deutsch (ID: 2)",
                "italiano (ID: 3)",
                "Français (ID: 4)",
                "español (ID: 5)",
                "Русский (ID: 6)",
                "简体中文 (ID: 7)",
                "Português (ID: 8)",
                "Polski (ID: 9)",
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        public Program(Main game) : base(game)
        {
            switcher(config.Lang);
            int lang = int.Parse(config.Lang)-1;
            Console.WriteLine($"\n[LanguageSwitcher] 你的语言已切换为 “{supportLang[lang]}”");
            Console.WriteLine($"\n[LanguageSwitcher] Your Language Has Been Switched to “{supportLang[lang]}”\n");
            base.Order = int.MinValue;
        }

        /// <summary>
        /// 初始化，服务器启动时加载。
        /// 可以注册钩子，执行加载过程等。
        /// </summary>
        public override void Initialize()
        {
            Commands.ChatCommands.Add(new Command(
                permissions: new List<string> { "LanguageSwitcher.use" },
                cmd: cmd,
                "LanguageSwitcher", "LangSwitcher", "lang"
            ));
        }

        private void cmd(CommandArgs args)
        {
            var param = args.Parameters;
            var player = args.Player;
            if (param.Count == 1)
            {
                if (param[0] == "help")
                {
                    // 输出帮助文档
                    sendHelpDoc(player);
                }
                else
                {
                    // 切换语言
                    try
                    {
                        config.Lang = param[0];
                        player.SendErrorMessage("注意，语言已经修改，请重启服务器以应用新的语言。");
                        player.SendErrorMessage("Note that the language has been changed, please restart the server to apply the new language.");
                    }
                    catch
                    {
                        player.SendErrorMessage("语言切换失败，请检查命令是否正确");
                        player.SendErrorMessage("Language switching failed, please check whether the command is correct.");
                    }
                }
            }
            else if (param.Count != 1)
            {
                // 指令输入错误，输出帮助文档
                sendHelpDoc(player);
            }
        }

        private void sendHelpDoc(TSPlayer p)
        {
            p.SendInfoMessage("/lang help\t获取帮助(Get Help)\n/lang [ID]\t切换语言(Switch Language)\n支持的语言(Support Language): ");
            foreach (string lang in supportLang)
            {
                p.SendInfoMessage(lang);
            }
            p.SendWarningMessage("请勿输入列表外的ID，否则服务器将无法启动（如果无法启动，请删除插件的配置文件）");
            p.SendWarningMessage("Do not enter an ID that is not in the list, otherwise the server will not start (if it does not start, please delete the configuration file of the plugin)");
        }

        private void switcher(string lang)
        {
            LanguageManager.Instance.SetLanguage(int.Parse(lang));
            if (Terraria.Program.LaunchParameters.ContainsKey("-lang"))
            {
                Terraria.Program.LaunchParameters["-lang"] = lang;

            }
            else
            {
                Terraria.Program.LaunchParameters.Add("-lang", lang);

            }
        }

        /// <summary>
        /// 服务器结束时加载。
        /// 应该在此处注销挂钩并释放所有资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // 在这里注销钩子
            }
            base.Dispose(disposing);
        }
    }
}