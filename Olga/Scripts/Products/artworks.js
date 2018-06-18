$(document).ready(function(event) {
    Dropzone.autoDiscover = false;
    var images = [];
    //var artworkId = 1;
    var accept = ".pdf,.doc,.docx,.odt";

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
    }

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
        var fileExt = fileName.split('.').pop();
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
            default: return fileName;
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
        }
        $(file.previewElement).find(".dz-image img").attr("class", "preview_image");
    }

    function showDocsImageOnPage(response) {
        var fileName = response.Message;
        var apprFolder = response.Folder;
        var _artworkId = fileName.substr(0, fileName.indexOf("__"));
        var containerBlock = document.getElementById('filesArtworkDiv'+_artworkId);
        var blockToInsert = document.createElement('div');
        blockToInsert.style.display = 'inline-block';

        var imageName = getFileImageViaExt(fileName);
        console.log('imageName ' + imageName);

        var imagePath = "";
        if (imageName !== fileName) {
            imagePath = "/Content/images/extentions/" + imageName;
        } else {
            imagePath = '/Upload/Documents/' + apprFolder + '/' + fileName;
        }
        var alt = fileName.substr(fileName.indexOf("_") + 2, fileName.lastIndexOf("_")-3).trim();
        var innerHtml =
            '<div name="showArtworkImageBlock_' + fileName.replace(/\./g, '').replace(/\s+/g, '').trim() + '" id="showArtworkImageBlock_' + fileName.replace(".", "").replace(/\s+/g, '').trim() + '" class="show_blockOfImage">' +
                '<div>' +
                    '<img class="shadow" style="height: 50px;" src="' + imagePath + '" alt="' + alt + '" tooltip ="' + alt + '" data-toggle="tooltip" title="' + alt + '" data-placement="top" />' +
                '</div>' +
                '<div style="margin-top:5px; line-height: 20px;">' +
                '   <a href="/Upload/Documents/' + apprFolder + '/' + fileName + '" target="_blank">Посмотреть</a>' +
                '</div>' +
             '</div>';
        blockToInsert.innerHTML = innerHtml;
        containerBlock.appendChild(blockToInsert);
    }

    function saveFileNameInHiddenList(fileName, imageStringElement) {
        console.log('#saveFileNameInHiddenList(' + fileName+')');
        var imageString = imageStringElement.val();
        if (imageString.includes(fileName)) {
            console.log('imageString already contains(' + fileName + ')');
            return;
        }
        if (imageString.length > 1) {
            imageString += ',';
        }
        imageString += fileName;
        imageStringElement.val(imageString);
    }

    $('.delete__artwork_files').on('click',
        function (e) {
            
            e.preventDefault();
            var fileName = $(this).attr("data-id");
            var artworkId = fileName.substr(0, fileName.indexOf("__"));
            console.log('artworkId = ' + artworkId);
            console.log('#delete__files click() = ' + fileName);
            console.log('artworkId = ' + artworkId);
            var trimFileName = fileName.replace(/\./g, '').replace(/\s+/g, '').trim();
            var blockElementWithImage = $('#showArtworkImageBlock_' + trimFileName);

            var stringElement = $('#DocumentImagesListStringArtworks');
            var stringElement2 = $('#DocumentImagesListStringArtworks' + artworkId);
            blockElementWithImage.removeClass("show_blockOfImage").addClass("hidden");

            console.log('showImageBlock = ' + 'showArtworkImageBlock_' + trimFileName);

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
            maxFilesize: 1500000,
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
                            console.log('fileName = ' + fileName);
                            artworkId = fileName.substr(0, fileName.indexOf("__"));
                            console.log('artworkId = ' + artworkId);

                            console.log('Saving file in Hidden lists ' + fileName);
                            var imageStringElement1 = $('#DocumentImagesListStringArtworks');
                            var imageStringElement2 = $('#DocumentImagesListStringArtworks' + artworkId);

                            if (imageStringElement1.val().includes(fileName)) {
                                console.log('imageString already contains(' + fileName + ')');
                                continue;
                            }
                            saveFileNameInHiddenList(fileName, imageStringElement1);
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
                    console.log('#cancelAddPhoto click() ');
                    if (images.length) {
                        for (i = 0; i < images.length; i++) {
                            var fileName = images[i].savedName;
                            console.log('cancelAddPhoto name = ' + fileName);
                            var artworkId = fileName.substr(0, fileName.indexOf("__"));

                            var trimFileName = fileName.replace(/\./g, '').replace(/\s+/g, '').trim();
                            console.log('Hidding #showArtworkImageBlock_' + trimFileName);

                            var blockElementWithImage = $('#showArtworkImageBlock_' + trimFileName);
                            blockElementWithImage.removeClass("show_blockOfImage").addClass("hidden");

                            var stringElement = $('#DocumentImagesListStringArtworks');
                            var stringElement2 = $('#DocumentImagesListStringArtworks' + artworkId);

                            deleteArtworkFileFromHiddenList(fileName, stringElement);
                            deleteArtworkFileFromHiddenList(fileName, stringElement2);

                            deleteImage(fileName);
                            //images.splice(i, 1);
                        }
                    }
                    images = [];
                    fancybox.removeAllFiles();
                });

                this.on("removedfile",
                    function (file) {
                        console.log('removed file event for ' + file.name);
                        var index = -1;
                        for (var i = 0; i < images.length; i++) {
                            if (images[i].name === file.name) {

                                console.log('file.name to delete = ' + file.name);
                                console.log('artworkId = ' + artworkId);


                                var fileName = images[i].savedName;
                                var fileNameInListWithoutex = artworkId + '__' + file.name.substr(0, file.name.lastIndexOf("."));

                                var trimFileName = fileName.replace(/\./g, '').replace(/\s+/g, '').trim();
                                var blockElementWithImage = $('#showArtworkImageBlock_' + trimFileName);
                                blockElementWithImage.removeClass("show_blockOfImage").addClass("hidden");
                                var stringElement = $('#DocumentImagesListStringArtworks');
                                var stringElement2 = $('#DocumentImagesListStringArtworks' + artworkId);

                                var fileNameInList = fileNameInHiddenList(fileNameInListWithoutex, stringElement);
                                console.log('fileNameInList = ' + fileNameInList);
                                if (fileNameInList !== null) {
                                    deleteImage(fileNameInList);
                                }

                                deleteArtworkFileFromHiddenList(fileNameInListWithoutex, stringElement);
                                deleteArtworkFileFromHiddenList(fileNameInListWithoutex, stringElement2);

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

                var fileSizeMB = file.size / 1024 / 1024;
                if (fileSizeMB > 5) {
                    alertify.error('ErrorMaxUploadFileSize');
                }

            },
            maxfilesexceeded: function (file) {
                this.removeFile(file);
            }
        });
    }
});