function login() {
    var fileName = document.getElementById("fileUploaded").value;
    var fileExtension = fileName.substr(fileName.length - 4);
   // var fileExtensionxlsx = filename.substr(fileName.length - 5);
    console.log(fileExtension);
 //   console.log(fileExtensionxlsx);
    if ((fileExtension != ".xls") /*|| (fileExtension != "xlsx")*/) {
        alert("That ain't no .xls file!");
    }

}