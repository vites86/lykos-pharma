$(document).ready(function(event) {
    Dropzone.autoDiscover = false;
    var images = [];
    //var artworkId = 1;
    //var accept = ".pdf,.doc,.docx,.odt";

    function removeArtworkImages(artworkId) {
        console.log("removeArtworkImages() artworkId=" + artworkId);
        var elements = document.querySelectorAll('[data-type-marker="image"]');
        if (elements && elements.length) {
            var i = 0;
            for (; i < elements.length; i++) {
                $('form').remove(elements[i]);
                console.log(elements[i].element);
            }
            images = [];
            $('#DocumentImagesListStringArtworks').val('');
            $('#DocumentImagesListStringArtworks' + artworkId).val('');
        }
    }

    function deleteArtworkFileFromHiddenList(fileName, stringElement) {

        console.log('deleteFileFromHiddenList(): '+ fileName);
        console.log('stringElement = ' + stringElement.attr('name') );

        var stringElementValue = stringElement.val();
        var re = /\s*,\s*/;
        var tagList = stringElementValue.split(re);
        for (var k = 0; k < tagList.length; k++) {
            if (tagList[k].includes(fileName)) {
                console.log('tagList[k] == ' + fileName);
                var result = stringElementValue.replace(tagList[k], '');
                console.log('result to delete = ' + result);
                stringElement.val(result);
            }
        }
        console.log('result stringElement.val(): ' + stringElement.val());
    }

    //function deleteFileFromHiddenList(fileName, stringElement) {

    //    var stringElementValue = stringElement.val();
    //    var re = /\s*,\s*/;
    //    var tagList = stringElementValue.split(re);
    //    for (var k = 0; k < tagList.length; k++) {
    //        if (tagList[k].includes(fileName)) {
    //            console.log('tagList[k] contains ' + fileName);
    //            var result = stringElementValue.replace(tagList[k], '');
    //            console.log('result = ' + result);
    //            stringElement.val(result);
    //        }
    //    }
    //}

    function fileNameInHiddenList(fileName, stringElement) {
        console.log('fileNameInHiddenList()');
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
        var _artworkId = fileName.substr(0, fileName.indexOf("__"));
        var containerBlock = document.getElementById('filesArtworkDiv'+_artworkId);
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
        var fileNameTrimmed = getFileNameTrimmed(fileName);
        console.log('fileNameTrimmed=' + fileNameTrimmed);
        var innerHtml =
            '<div name="showArtworkImageBlock_' + fileNameTrimmed +'" ' +
            'id="showArtworkImageBlock_' + fileNameTrimmed + '" class="show_blockOfImage">' +
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
        console.log(elementName + '.val() = ' + imageString);
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

    $('.delete__artwork_files').on('click',
        function (e) {
            console.log('#delete__files click()');
            e.preventDefault();
            var fileName = $(this).attr("data-id");
            console.log('data-id = ' + fileName);
            var _artworkId = fileName.substr(0, fileName.indexOf("__"));
            console.log('artworkId = ' + _artworkId);
            var trimFileName = getFileNameTrimmed(fileName);
            console.log('fileNameTrimmed=' + trimFileName);

            var blockElementWithImage = $('#showArtworkImageBlock_' + trimFileName);
            console.log('showImageBlock = ' + 'showArtworkImageBlock_' + trimFileName);
            console.log(blockElementWithImage.attr('name') + ' before class = ' + blockElementWithImage.attr('class'));
            blockElementWithImage.removeClass("show_blockOfImage").addClass("hidden");
            console.log(blockElementWithImage.attr('name') + ' after class = ' + blockElementWithImage.attr('class'));

            // delete file from HiddenLists
            var stringElement = $('#DocumentImagesListStringArtworks');
            var stringElement2 = $('#DocumentImagesListStringArtworks' + _artworkId);
            deleteArtworkFileFromHiddenList(fileName, stringElement);
            deleteArtworkFileFromHiddenList(fileName, stringElement2);

            deleteImage(fileName);
        });

    for (var p = 0; p < 8; p++) {
        var artworkId = p;
        $("#dZArtworkUpload" + artworkId).dropzone({
            url: "/Product/SaveArtworkUploadedFile?artworkId=" + artworkId,
            uploadMultiple: true,
            maxFiles: 10,
            acceptedFiles: "image/*, .pdf, .txt, .xlsx, .docx, .ai, .cdr, .xls, .doc",
            maxFilesize: 2500000000,
            maxThumbnailFilesize: 2500000000,
            addRemoveLinks: true,
            createImageThumbnails: true,
            init: function () {
                var fancybox = this;
                $('#saveArtworkPhotoButton' + artworkId).off('click').click(function (clickEvent) {
                    console.log('#savePhotoButton click() ');
                    console.log('Count of saved files = ' + images.length);

                    for (var i = 0; i < images.length; i++) {
                        if (images.length) {
                            var fileName = images[i].savedName;
                            var _apprId = fileName.substr(0, fileName.indexOf("__"));
                            console.log('_apprId = ' + _apprId);
                            var imageStringElement1 = document.getElementById('DocumentImagesListStringArtworks');
                            var stringElement2Name = 'DocumentImagesListStringArtworks' + _apprId;
                            var imageStringElement2 = document.getElementById(stringElement2Name);
                            saveFileNameInHiddenList(fileName, imageStringElement1);
                            console.log('StringElement2Name = ' + stringElement2Name);
                            saveFileNameInHiddenList(fileName, imageStringElement2);                            
                        }
                    }
                    //var i = 0;
                });
                $('#deleteArtworkPhoto' + artworkId).off('click').click(function (clickEvent) {
                    console.log('#deletePhoto click() ');
                    //var artworkId = fileName.substr(0, fileName.indexOf("__"));
                    removeArtworkImages(artworkId);
                    fancybox.removeAllFiles();
                });
                $('#cancelArtworkAddPhoto' + artworkId).off('click').click(function (clickEvent) {
                    console.log('#cancel click()');                   
                    fancybox.removeAllFiles();
                });

                this.on("removedfile",
                    function (file) {
                        console.log('#removed file event for ' + file.name);
                        var index = -1;
                        for (var i = 0; i < images.length; i++) {
                            if (images[i].name.localeCompare(file.name) === 0) {

                                console.log('file.name to delete = ' + file.name);

                                var fileName = images[i].savedName;
                                console.log('images[i].savedName = ' + fileName);

                                var artworkId = fileName.substr(0, fileName.indexOf("__"));
                                console.log('artworkId = ' + artworkId);

                                //var fileNameInListWithoutex = artworkId + '__' + file.name.substr(0, file.name.lastIndexOf("."));
                                var trimFileName = getFileNameTrimmed(fileName);
                                console.log('fileNameTrimmed=' + trimFileName);

                                var blockElementWithImage = document.getElementById('showArtworkImageBlock_' + trimFileName);
                                blockElementWithImage.classList.remove("show_blockOfImage");
                                blockElementWithImage.classList.add("hidden");
                                
                                var stringElement = $('#DocumentImagesListStringArtworks');
                                var stringElement2 = $('#DocumentImagesListStringArtworks' + artworkId);

                                deleteImage(fileName);

                                deleteArtworkFileFromHiddenList(fileName, stringElement);
                                deleteArtworkFileFromHiddenList(fileName, stringElement2);

                                index = i;
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

                var ext = file.name.split('.').pop();

                var imageString = $('#DocumentImagesListStringArtworks').val();
                if (imageString.includes(file.name)) {
                    console.log('imageString already shows(' + file.name + ')');
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

                var fileSizeMB = file.size / 2500000 / 2500000;
                if (fileSizeMB > 2500000) {
                    toastr.error('Error in  File Size - Too Big File Size!');
                }
            },
            maxfilesexceeded: function (file) {
                this.removeFile(file);
            }
        });
    }

    function getFileNameTrimmed(fileName)
    {
        return fileName.replace(/\./g, '').replace(/\s+/g, '').replace(/#/g, '№').replace('&', '_').trim();
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