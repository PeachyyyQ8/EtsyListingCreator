using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Illustrator;
using log4net;

namespace EtsyListingCreator
{
    class Program
    {
        private static ILog _logger;

        //[DllImport("Illustrator.Interop.dll"), STAThread]
        //public static extern Application Application();

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                var properties = ConfigureApp(args);

                try
                {
                    Console.WriteLine("test");
                    Console.ReadLine();
                    _logger.Info("Application started.");

                    Type type = Type.GetTypeFromProgID("Illustrator.Application");
                    dynamic application = Activator.CreateInstance(type);
                    var dir = Directory.GetFiles(properties.WorkingDirectory);
                    foreach (var file in dir)
                    {
                        var baseFileName = Path.GetFileNameWithoutExtension(file);
                        properties.OutputDirectory = properties.OutputDirectory + "\\" + baseFileName;
                        Directory.CreateDirectory(properties.OutputDirectory);
                        dynamic document = application.Open(file,
                            AiDocumentColorSpace.aiDocumentRGBColor, null);
                        SavePDF(baseFileName, properties.OutputDirectory, document);
                        SaveJPEG(baseFileName, properties.OutputDirectory, document);
                        SaveDXF(baseFileName, properties.OutputDirectory, document);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    CleanUp(properties.OutputDirectory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void SaveDXF(string baseFileName, string outputDirectory, dynamic document)
        {
            var fullFileName = outputDirectory + "\\" + baseFileName + ".dxf";
            dynamic dxfOptions = new ExportOptionsAutoCAD();
            Type enumType = typeof(AiAutoCADExportFileFormat);
            dynamic enumValue = enumType.GetField("aiDXF").GetValue(null);
            dxfOptions.ExportFileFormat = enumValue;
            document.Export(fullFileName, AiExportType.aiAutoCAD, dxfOptions);
        }

        private static void SaveJPEG(string baseFileName, string outputDirectory, dynamic document)
        {
            var fullFileName = outputDirectory + "\\" + baseFileName + ".jpg";
            dynamic jpgOptions = new ExportOptionsJPEG();
            jpgOptions.QualitySetting = 100;
            jpgOptions.AntiAliasing = false;
            jpgOptions.Optimization = false;
            document.Export(fullFileName, AiExportType.aiJPEG, jpgOptions);
        }

        private static void CleanUp(string propertiesOutputDirectory)
        {
            Directory.Delete(propertiesOutputDirectory, true);
        }

        private static void SavePDF(string baseFileName, string outputDirectory, dynamic document)
        {
            var fullFileName = outputDirectory + "\\" + baseFileName + ".pdf";
            document.SaveAs(fullFileName, new PDFSaveOptions());
        }

        private static Properties ConfigureApp(string[] args)
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


//Skip to content
//This repository
//Search
//Pull requests
//Issues
//Marketplace
//Gist
//@PeachyyyQ8
//Sign out
//Watch 0
//Star 0
//Fork 0 PeachyyyQ8/Scripts
//Code  Issues 0  Pull requests 0  Projects 0  Wiki Settings Insights
//Branch: master Find file Copy pathScripts/SaveDocs.jsx
//f2d894e  on May 8
//Georgia Mackey Add Jpg
//0 contributors
//RawBlameHistory
//81 lines(64 sloc)  2.33 KB


//try {
//// uncomment to suppress Illustrator warning dialogs
//// app.userInteractionLevel = UserInteractionLevel.DONTDISPLAYALERTS;

//var sourceDoc = '/C:/Users/georg/Google Drive/DigitalListings/CurrentlyListed/NurseSuperpower'
//var destFolder = null;
//var sourceFolder = '/C/Users/georg/Google Drive/DigitalListings/CurrentlyListed';
//////D/somefolder/somefolder/here_we_are"
//destFolder = Folder(sourceFolder).selectDlg();

//    if (destFolder != null) {
//    var pdfOptions, i, sourceDoc, targetFile;

//// Get the PDF options to be used
//    pdfOptions = new PDFSaveOptions();
//// You can tune these by changing the code in the getOptions() function.

//// Get the file to save the document as pdf into
//    targetFile = this.getTargetFile(sourceDoc, '.pdf', destFolder);

//// Save as pdf
//    sourceDoc.saveAs(targetFile, pdfOptions);

//    var type = ExportType.JPEG;
//    var jpgOptions = new ExportOptionsJPEG();
//    jpgOptions.qualitySetting = 80;
//    jpgOptions.horizontalScale = (300 / 72) * 100;
//    jpgOptions.verticalScale = (300 / 72) * 100;
//    jpgOptions.antiAliasing = false;
//    jpgOptions.optimization = false;

//    targetFile = this.getTargetFile(sourceDoc.name, '.jpg', destFolder);

//    sourceDoc.exportFile(targetFile, ExportType.JPG, jpgOptions);


//    alert('Document saved as PDF');
//}
//else{
//    throw new Error('No dest Folder');
//}
//}
//catch(e) {

//alert(e.message, "Script Alert", true);
//}


///** Returns the file to save or export the document into.
//	@param docName the name of the document
//	@param ext the extension the file extension to be applied
//	@param destFolder the output folder
//	@return File object
//*/
//function getTargetFile(docName, ext, destFolder)
//{
//var newName = "";

//    // if name has no dot (and hence no extension),
//    // just append the extension
//    if (docName.indexOf('.') < 0)
//{
//    newName = docName + ext;
//}
//else
//{
//var dot = docName.lastIndexOf('.');
//newName += docName.substring(0, dot);
//newName += ext;
//}

//// Create the file object to save to
//var myFile = new File(destFolder + '/' + newName);

//// Preflight access rights
//if (myFile.open("w"))
//{
//myFile.close();
//}
//else
//{
//throw new Error('Access is denied');
//}
//return myFile;
//}
//Contact GitHub API Training Shop Blog About
//© 2017 GitHub, Inc.Terms Privacy Security Status Help