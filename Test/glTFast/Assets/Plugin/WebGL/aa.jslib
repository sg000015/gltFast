mergeInto(LibraryManager.library, {
    OpenFileBrowserWebGL: function (callbackObjectName, callbackMethodName) {
        var objectName = UTF8ToString(callbackObjectName);
        var methodName = UTF8ToString(callbackMethodName);
        var function_upload = function () {
            document.removeEventListener("click", function_upload);
            var fileuploader = document.getElementById("fileuploader");
            if (!fileuploader) {
                fileuploader = document.createElement("input");
                fileuploader.setAttribute("style", "display:none;");
                fileuploader.setAttribute("type", "file");
                fileuploader.setAttribute("id", "fileuploader");
                fileuploader.setAttribute("class", "focused");
                fileuploader.setAttribute("accept", ".glb, .gltf");
                document
                    .getElementsByTagName("body")[0]
                    .appendChild(fileuploader);

                fileuploader.onchange = function (e) {
                    var files = e.target.files;
                    for (var i = 0, f; (f = files[i]); i++) {
                        // var filePath = URL.createObjectURL(f);
                        if (
                            f.name.substr(f.name.length - 3) == "glb" ||
                            f.name.substr(f.name.length - 4) == "gltf"
                        ) {
                            //URL.createObjectURL(f);
                            SendMessage(
                                objectName,
                                methodName,
                                URL.createObjectURL(f)
                            );
                        } else {
                            alert("it is not GLB/GLTF file");
                        }
                    }
                };
            }

            if (fileuploader.getAttribute("class") == "focused") {
                fileuploader.setAttribute("class", "");
                fileuploader.click();
            }
        };

        var fileuploader = document.getElementById("fileuploader");
        if (fileuploader) {
            document.getElementById("fileuploader").disabled = true;
            document.removeEventListener("click", function_upload);
            fileuploader.parentNode.removeChild(fileuploader);
        }

        document.addEventListener("click", function_upload);
    },
});
