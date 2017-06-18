using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Illustrator;
using log4net;

namespace EtsyListingCreator
{
    class Program
    {
        private static ILog _logger;
        
        static void Main(string[] args)
        {
            try
            {
                var commandLineArgs = ConfigureApp(args);
                _logger.Info("Application started.");

                Illustrator.Application illuApp = new Illustrator.Application();
                Illustrator.Document illuDoc = illuApp.Open(commandLineArgs.WorkingDirectory + "/BaseballMom.svg", Illustrator.AiDocumentColorSpace.aiDocumentRGBColor, null);

                Illustrator.PlacedItem importImage = illuDoc.PlacedItems.Add();
                //importImage.File = importFilePath; // a jpg to place on the document 
                importImage.Top = 50; //move the jpg around 
                importImage.Left = 50;


                //Application illuApp = new Illustrator.Application();
                //Document illuDoc = illuApp.Open(commandLineArgs.WorkingDirectory + "/BaseballMom.svg", AiDocumentColorSpace.aiDocumentRGBColor);

                //PlacedItem importImage = illuDoc.PlacedItems.Add();
                //importImage.File = importFilePath; // a jpg to place on the document 
                //importImage.Top = 50; //move the jpg around 
                //importImage.Left = 50;
                //Open Illustrator
                //Save File as different formats using temp working directory
                //Save files in correct direcotrry
                //use etsy portal to upload listing.

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private static CommandLineArgs ConfigureApp(string[] args)
        {
            //TODO:  Add event log to installer
            if (log4net.LogManager.GetCurrentLoggers().Length == 0)
            {
                log4net.Config.BasicConfigurator.Configure();
            }
            _logger = log4net.LogManager.GetLogger("FileAppender");

            var IOC = new IOC();
            IOC.Configure();

            return AppHelper.ParseCommandLineArguments(args);


        }
    }
}
