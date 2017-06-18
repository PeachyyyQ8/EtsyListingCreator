using System.Configuration;
using NDesk.Options;

namespace EtsyListingCreator
{
    public class AppHelper
    {
        public static CommandLineArgs ParseCommandLineArguments(string[] args)
        {

            var commandLineArgs = new CommandLineArgs();
            var p = new OptionSet() {
                { "wd|Working Directory=", "Sets or changes the directory of the base file.",
                    v => commandLineArgs.WorkingDirectory = v },
                { "od|Output Directory=",
                    "Sets or changes the directory of the output files",
                    v => commandLineArgs.OutputDirectory = v}
            };
            try
            {
                p.Parse(args);
                if (commandLineArgs.WorkingDirectory.IsNullOrEmpty())
                {
                    if (GetAppSetting("workingDirectory").IsNullOrEmpty())
                    {
                        throw new AppException("User must specify working directory!  Use -wd {directory} to specify.");
                    }
                }

                if (commandLineArgs.OutputDirectory.IsNullOrEmpty())
                {
                    if (GetAppSetting("outputDirectory").IsNullOrEmpty())
                    {
                        throw new AppException("User must specify output directory!  Use -od {directory} to specify.");
                    }
                }
            }
            catch (OptionException e)
            {
                throw new AppException("Unable to parse commandLine args.  OptionsException: {0}".QuickFormat(e.Message));
            }
            return commandLineArgs;
        }

        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static void SetAppSetting(string key, string value)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var settings = configFile.AppSettings.Settings;

            if (settings[key] == null || settings[key].ToString().Trim() == string.Empty)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
    }
}


//bool show_help = false;
//List<string> names = new List<string>();
//int repeat = 1;

//var p = new OptionSet() {
//{ "n|name=", "the {NAME} of someone to greet.",
//    v => names.Add (v) },
//{ "r|repeat=",
//"the number of {TIMES} to repeat the greeting.\n" +
//"this must be an integer.",
//(int v) => repeat = v },
//{ "v", "increase debug message verbosity",
//v => { if (v != null) ++verbosity; } },
//{ "h|help",  "show this message and exit",
//v => show_help = v != null },
//};

//List<string> extra;
//try {
//extra = p.Parse(args);
//}
//catch (OptionException e) {
//Console.Write("greet: ");
//Console.WriteLine(e.Message);
//Console.WriteLine("Try `greet --help' for more information.");
//return;
//}