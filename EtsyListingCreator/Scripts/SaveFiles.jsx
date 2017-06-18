
try {
    // uncomment to suppress Illustrator warning dialogs
    // app.userInteractionLevel = UserInteractionLevel.DONTDISPLAYALERTS;

    var sourceDoc = '/C:/Users/georg/Google Drive/DigitalListings/CurrentlyListed/NurseSuperpower';
    var destFolder = null;
    var sourceFolder = '/C/Users/georg/Google Drive/DigitalListings/CurrentlyListed';
    ////D/somefolder/somefolder/here_we_are"
    destFolder = Folder(sourceFolder).selectDlg();

    if (destFolder != null) {
        var pdfOptions, i, sourceDoc, targetFile;

        // Get the PDF options to be used
        pdfOptions = new PDFSaveOptions();
        // You can tune these by changing the code in the getOptions() function.

        // Get the file to save the document as pdf into
        targetFile = this.getTargetFile(sourceDoc, '.pdf', destFolder);

        // Save as pdf
        sourceDoc.saveAs(targetFile, pdfOptions);

        var type = ExportType.JPEG;
        var jpgOptions = new ExportOptionsJPEG();
        jpgOptions.qualitySetting = 80;
        jpgOptions.horizontalScale = (300 / 72) * 100;
        jpgOptions.verticalScale = (300 / 72) * 100;
        jpgOptions.antiAliasing = false;
        jpgOptions.optimization = false;

        targetFile = this.getTargetFile(sourceDoc.name, '.jpg', destFolder);

        sourceDoc.exportFile(targetFile, ExportType.JPG, jpgOptions);


        alert('Document saved as PDF');
    }
    else {
        throw new Error('No dest Folder');
    }
}
catch (e) {
    alert(e.message, "Script Alert", true);
}


/** Returns the file to save or export the document into.
	@param docName the name of the document
	@param ext the extension the file extension to be applied
	@param destFolder the output folder
	@return File object
*/
function getTargetFile(docName, ext, destFolder) {
    var newName = "";

    // if name has no dot (and hence no extension),
    // just append the extension
    if (docName.indexOf('.') < 0) {
        newName = docName + ext;
    } else {
        var dot = docName.lastIndexOf('.');
        newName += docName.substring(0, dot);
        newName += ext;
    }

    // Create the file object to save to
    var myFile = new File(destFolder + '/' + newName);

    // Preflight access rights
    if (myFile.open("w")) {
        myFile.close();
    }
    else {
        throw new Error('Access is denied');
    }
    return myFile;
}
