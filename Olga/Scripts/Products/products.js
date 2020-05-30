$(document).ready(function(event) {
    Dropzone.autoDiscover = false;
    var images = [];
    //var apprId = 1;
    var accept = ".pdf,.doc,.docx,.odt";

    function removeImages(apprId) {
        console.log("removeImages() apprId=" + apprId);
        var elements = document.querySelectorAll('[data-type-marker="image"]');
        if (elements && elements.length) {
            var i = 0;
            for (; i < elements.length; i++) {
                $('form').remove(elements[i]);
                console.log(elements[i].element);
            }
            images = [];
            $('#DocumentImagesListStringApprs').val('');
            $('#DocumentImagesListStringApprs' + apprId).val('');
        }
    }

    function deleteFileFromHiddenList(fileName, stringElement) {

        console.log('deleteFileFromHiddenList(): '+ fileName);
        console.log(stringElement.attr('name')+' = '+stringElement);

        var stringElementValue = stringElement.val();
        var re = /\s*,\s*/;
        var tagList = stringElementValue.split(re);
        for (var k = 0; k < tagList.length; k++) {
            if (tagList[k].includes(fileName)) {
                console.log('tagList[k] == ' + fileName);
                var result = stringElementValue.replace(tagList[k], '');
                console.log('resultHiddenString = ' + result);
                stringElement.val(result);
            }
        }
        console.log('result stringElement.val(): '+ stringElement.val());
    }

    function fileNameInHiddenList(fileName, stringElement) {
        console.log('#fileNameInHiddenList() acted!');
        console.log('fileName:' + fileName);
        console.log('stringElement.val:' + stringElement.val());

        var stringElementValue = stringElement.val();
        var re = /\s*,\s*/;
        var tagList = stringElementValue.split(re);
        for (var k = 0; k < tagList.length; k++) {
            if (tagList[k].includes(fileName)) {
                console.log('fileNameInHiddenList:' + tagList[k]);
                return  tagList[k];
            }
        }
    }

    function getFileImageViaExt(fileName) {
        var fileExt = getExt(fileName);
        console.log('fileExt = ' + fileExt);

        switch (fileExt)
        {
            case 'txt': return "txt.jpg";
            case 'pdf': return "pdf.jpg";
            case 'ai': return "ai.jpg";
            case 'docx': return "docx.jpg";
            case 'xlsx': return "xlsx.jpg";
            case 'cdr': return "cdr.jpg";
            case 'doc': return "docx.jpg";
            case 'xls': return "xlsx.jpg";
            default: return "document.png";
        }
    }

    function deleteImage(fileName) {
            $.ajax({
                url: '/Product/DeleteFile',
                method: 'POST',
                data: { fileName: fileName },
                success: function (response) {
                    toastr.info(response.Message);
                },
                error: function (response) {
                    toastr.error(response.Message);
                }
            });
    }

    function showDocsPreview(file, ext) {
        if (ext === "pdf") {
            $(file.previewElement).find(".dz-image img").attr("src", "/Content/images/extentions/pdf.jpg");
        } else if (ext.indexOf("docx") !== -1) {
            $(file.previewElement).find(".dz-image img").attr("src", "/Content/images/extentions/docx.jpg");
        } else if (ext.indexOf("txt") !== -1) {
            $(file.previewElement).find(".dz-image img").attr("src", "/Content/images/extentions/txt.jpg");
        } else if (ext.indexOf("ai") !== -1) {
            $(file.previewElement).find(".dz-image img").attr("src", "/Content/images/extentions/ai.jpg");
        } else if (ext.indexOf("cdr") !== -1) {
            $(file.previewElement).find(".dz-image img").attr("src", "/Content/images/extentions/cdr.jpg");
        } else if (ext.indexOf("xlsx") !== -1) {
            $(file.previewElement).find(".dz-image img").attr("src", "/Content/images/extentions/xlsx.jpg");
        } else if (ext.indexOf("xls") !== -1) {
            $(file.previewElement).find(".dz-image img").attr("src", "/Content/images/extentions/xlsx.jpg");
        } else if (ext.indexOf("doc") !== -1) {
            $(file.previewElement).find(".dz-image img").attr("src", "/Content/images/extentions/docx.jpg");
        } else {
            $(file.previewElement).find(".dz-image img").attr("src", "/Content/images/extentions/document.png");
        }
        $(file.previewElement).find(".dz-image img").attr("class", "preview_image");
        $(file.previewElement).find(".dz-image img").attr("alt", file.name);
        $(file.previewElement).find(".dz-image img").attr("title", file.name);

        $(file.previewElement).find(".dz-image").append('<span style="display: inline-block;width:150px; overflow: hidden !important;">' + file.name + '</span>');
    }

    function showDocsImageOnPage(response) {
        var fileName = response.Message;
        var apprFolder = response.Folder;
        var _apprId = fileName.substr(0, fileName.indexOf("__"));
        var containerBlock = document.getElementById('filesDiv'+_apprId);
        var blockToInsert = document.createElement('div');
        blockToInsert.style.display = 'block';

        var imageName = getFileImageViaExt(fileName);
        console.log('imageName ' + imageName);

        if (imageName !== fileName) {
            imagePath = "/Content/images/extentions/" + imageName;
        } else {
            imagePath = '/Upload/Documents/' + apprFolder + '/' + fileName;
        }
        //var alt = fileName.substr(fileName.indexOf("_") + 2, fileName.lastIndexOf("_") - 3).trim();
        var trimFileName = getFileNameTrimmed(fileName);
        console.log('fileNameTrimmed=' + trimFileName);
        var innerHtml =
            '<div name="showImageBlock_' + trimFileName + '"'+
            'id = "showImageBlock_' + trimFileName + '" class="show_blockOfImage" > ' +
               '<span class="glyphicon glyphicon-file" />    ' +
                '<a href="/Upload/Documents/' + apprFolder + '/' + fileName + '" target="_blank">' + fileName + '</a>' +
            '</div>';
        blockToInsert.innerHTML = innerHtml;
        containerBlock.appendChild(blockToInsert);
    }

    function saveFileNameInHiddenList(fileName, imageStringElement) {
        var elementName = imageStringElement.name;
        console.log('#saveFileNameInHiddenList() for ' + elementName);
        var imageString = imageStringElement.value;
        console.log(elementName+'.val() = ' + imageString);
        if (imageString.includes(fileName)) {
            console.log('imageString already contains ' + fileName + '');
            return;
        }
        if (imageString.length > 1) {
            imageString += ',';
        }
        imageString += fileName;
        imageStringElement.value = imageString;
        console.log('after adding ' + elementName + '.val() = ' + imageStringElement.value);
    }

    $('.delete__files').on('click',
        function (e) {
            console.log('#delete__files click');
            e.preventDefault();
            var fileName = $(this).attr("data-id");
            console.log('data-id = ' + fileName);
            apprId = fileName.substr(0, fileName.indexOf("__"));
            console.log('apprId = ' + apprId);
            var trimFileName = getFileNameTrimmed(fileName);
            console.log('fileNameTrimmed=' + trimFileName);

            var blockElementWithImage = $('#showImageBlock_' + trimFileName);
            blockElementWithImage.removeClass("show_blockOfImage").addClass("hidden");
            console.log('showImageBlock = ' + 'showImageBlock_' + trimFileName);

            var stringElement = $('#DocumentImagesListStringApprs');
            var stringElement2 = $('#DocumentImagesListStringApprs' + apprId);

            deleteFileFromHiddenList(fileName, stringElement);
            deleteFileFromHiddenList(fileName, stringElement2);

            deleteImage(fileName);
        });

    for (var j = 0; j < 6; j++) {
        var apprId = j;
        $("#dZUpload" + apprId).dropzone({
            url: "/Product/SaveUploadedFile?apprId=" + apprId,
            timeout: 180000,
            uploadMultiple: true,
            maxFiles: 10,
            acceptedFiles: "image/*, .pdf, .txt, .xlsx, .docx, .ai, .cdr, .xls, .doc",
            maxFilesize: 2500000000,
            maxThumbnailFilesize: 2500000000,
            addRemoveLinks: true,
            createImageThumbnails: true,
            init: function () {
                var fancybox = this;
                $('#savePhotoButton' + apprId).off('click').click(function (clickEvent) {
                    console.log('#savePhotoButton click() ');
                    console.log('Count of saved files = ' + images.length);
                    console.log('apprId = ' + apprId);

                    for (var i = 0; i < images.length; i++) {
                        if (images.length) {
                            var fileName = images[i].savedName;
                            var _apprId = fileName.substr(0, fileName.indexOf("__"));
                            console.log('_apprId = ' + _apprId);
                            var imageStringElement1 = document.getElementById('DocumentImagesListStringApprs');    
                            var stringElement2Name = 'DocumentImagesListStringApprs' + _apprId;                           
                            var imageStringElement2 = document.getElementById(stringElement2Name);                            
                            saveFileNameInHiddenList(fileName, imageStringElement1);
                            console.log('StringElement2Name = ' + stringElement2Name);
                            saveFileNameInHiddenList(fileName, imageStringElement2);
                        }
                    }
                });
                $('#deletePhoto' + apprId).off('click').click(function (clickEvent) {
                    console.log('#deletePhoto click() on #deletePhoto for ' + apprId);
                    removeImages(apprId);
                    fancybox.removeAllFiles();
                });
                $('#cancelAddPhoto' + apprId).off('click').click(function (clickEvent) {
                    console.log('#cancel click() on #cancelAddPhoto for ' + apprId);
                    fancybox.removeAllFiles();
                });

                this.on("removedfile",
                    function (file) {
                        console.log('#removed file event for ' + file.name);
                        var index = -1;
                        for (var i = 0; i < images.length; i++) {
                            if (images[i].name.localeCompare(file.name) === 0) {

                                var resultOfCompare = images[i].name === file.name;
                                console.log('resultOfCompare='+resultOfCompare);
                                var fileName = images[i].savedName;
                                //console.log('file.name to delete = ' + file.name);
                                console.log('images[i].savedName to delete = ' + images[i].savedName);

                                apprId = fileName.substr(0, fileName.indexOf("__"));
                                console.log('apprId = ' + apprId);

                                //var fileNameInListWithoutex = apprId + '__' + fileName.substr(0, fileName.lastIndexOf("."));
                                //console.log('fileNameInListWithoutex = ' + fileNameInListWithoutex);

                                var trimFileName = getFileNameTrimmed(fileName);
                                console.log('fileNameTrimmed=' + trimFileName);
                                var blockElementWithImage = document.getElementById('showImageBlock_' + trimFileName);
                                blockElementWithImage.classList.remove("show_blockOfImage");
                                blockElementWithImage.classList.add("hidden");

                                var stringElement = $('#DocumentImagesListStringApprs');
                                var stringElement2 = $('#DocumentImagesListStringApprs' + apprId);
                               
                                deleteImage(fileName);

                                deleteFileFromHiddenList(fileName, stringElement);
                                deleteFileFromHiddenList(fileName, stringElement2);
                                index = i;
                                console.log('index i = ' + index);
                                break;
                            }
                        }
                        if (index !== -1)
                            images.splice(index, 1);
                    });
            },
            success: function (file, response) {

                images.push({ name: file.name, savedName: response.Message });
                console.log('Download on server image with name: ' + file.name);
                console.log('response.Message: ' + response.Message);
                file.previewElement.classList.add("dz-success");

                var ext = getExt(file.name);

                var imageString = $('#DocumentImagesListStringApprs').val();
                if (imageString.includes(file.name)) {
                    console.log('imageString already shows(' + file.name + ')');
                    toastr.error("This file already downloaded on this page!");
                    return;
                }

                showDocsPreview(file, ext);
                showDocsImageOnPage(response);
            },
            error: function (file, response) {
                if (response === "You can not upload any more files.") {
                    this.removeFile(file);
                    return;
                }
                file.previewElement.classList.add("dz-error");

                var fileSizeMB = file.size / 2500000000 / 2500000000;
                if (fileSizeMB > 2500000) {
                    alertify.error('ErrorMaxUploadFileSize');
                    toastr.error('Error in  File Size - Too Big File Size!');
                    console.log('file.size ['+file.size+'] is to big!');
                }
            },
            maxfilesexceeded: function (file) {
                this.removeFile(file);
            }
        });
    }

    function getFileNameTrimmed(fileName) {
        return fileName.replace(/\./g, '').replace(/\s+/g, '').replace(/#/g, '№').replace('&', '_').replace('(', '_').replace(')', '_').trim();
    }

    function getExt(fileName) {
        var lastIndexOfPoint = fileName.lastIndexOf('.')+1;
        if (lastIndexOfPoint >>> 0) {
            var ext = fileName.substring(lastIndexOfPoint);
            console.log('ext=' + ext);
            return ext;
        }
        else {
            console.log('ext= no extention for ' + fileName);
            return "no extention";
        }        
    }
});
