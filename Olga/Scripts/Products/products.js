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
        var _apprId = fileName.substr(0, fileName.indexOf("__"));
        var containerBlock = document.getElementById('filesDiv'+_apprId);
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
            '<div name="showImageBlock_' + fileName.replace(".", "").replace(/\s+/g, '').trim() + '" id="showImageBlock_' + fileName.replace(".", "").replace(/\s+/g, '').trim() + '" class="show_blockOfImage">' +
                '<div>' +
            '<img class="shadow" style="height: 50px;" src="' + imagePath + '" alt="' + alt + '" tooltip ="' + alt + '" data-toggle="tooltip" title="' + alt + '" data-placement="top" onmouseover="showFileNameInToast(\'' + fileName +'\');" />' +
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

    $('.delete__files').on('click',
        function (e) {
            console.log('#delete__files click');
            e.preventDefault();
            var fileName = $(this).attr("data-id");
            console.log('data-id = ' + fileName);
            apprId = fileName.substr(0, fileName.indexOf("__"));
            console.log('apprId = ' + apprId);
            var trimFileName = fileName.replace(/\./g, '').replace(/\s+/g, '').trim();
            //trimFileName.replace(/\./g, '');
            console.log('trimFileName = ' + trimFileName);

            var blockElementWithImage = $('#showImageBlock_' + trimFileName);
            blockElementWithImage.removeClass("show_blockOfImage").addClass("hidden");
            console.log('showImageBlock = ' + 'showImageBlock_' + trimFileName);

            var stringElement = $('#DocumentImagesListStringApprs');
            var stringElement2 = $('#DocumentImagesListStringApprs' + apprId);
            deleteFileFromHiddenList(fileName, stringElement);
            deleteFileFromHiddenList(fileName, stringElement2);

            deleteImage(fileName);
        });

    for (var j = 0; j < 5; j++) {
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

                    for (var i = 0; i < images.length; i++) {
                        if (images.length) {
                            var fileName = images[i].savedName;
                            console.log('Saving file in Hidden lists ' + fileName);
                            var imageStringElement1 = $('#DocumentImagesListStringApprs');
                            var imageStringElement2 = $('#DocumentImagesListStringApprs' + apprId);

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
                $('#deletePhoto' + apprId).off('click').click(function (clickEvent) {
                    console.log('#deletePhoto click() ');
                    removeImages(apprId);
                    fancybox.removeAllFiles();
                });
                $('#cancelAddPhoto' + apprId).off('click').click(function (clickEvent) {
                    console.log('#cancelAddPhoto click() ');
                    if (images.length) {
                        for (i = 0; i < images.length; i++) {
                            var fileName = images[i].savedName;
                            console.log('cancelAddPhoto name = ' + fileName);

                            var trimFileName = fileName.replace(/\./g, '').replace(/\s+/g, '').trim();
                            console.log('Hidding #showImageBlock_' + trimFileName);

                            var blockElementWithImage = $('#showImageBlock_' + trimFileName);
                            blockElementWithImage.removeClass("show_blockOfImage").addClass("hidden");

                            var stringElement = $('#DocumentImagesListStringApprs');
                            var stringElement2 = $('#DocumentImagesListStringApprs' + apprId);

                            deleteFileFromHiddenList(fileName, stringElement);
                            deleteFileFromHiddenList(fileName, stringElement2);

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

                                apprId = file.name.substr(0, file.name.indexOf("__"));
                                console.log('file.name to delete = ' + file.name);
                                console.log('apprId = ' + apprId);

                                var fileName = images[i].savedName;
                                var fileNameInListWithoutex = apprId + '__' + file.name.substr(0, file.name.lastIndexOf("."));

                                var blockElementWithImage = $('#showImageBlock_' + fileName.replace(/\./g, '').replace(/\s+/g, '').trim());
                                blockElementWithImage.removeClass("show_blockOfImage").addClass("hidden");
                                var stringElement = $('#DocumentImagesListStringApprs');
                                var stringElement2 = $('#DocumentImagesListStringApprs' + apprId);

                                var fileNameInList = fileNameInHiddenList(fileNameInListWithoutex, stringElement);
                                console.log('fileNameInList = ' + fileNameInList);
                                if (fileNameInList !== null) {
                                    deleteImage(fileNameInList);
                                }

                                deleteFileFromHiddenList(fileNameInListWithoutex, stringElement);
                                deleteFileFromHiddenList(fileNameInListWithoutex, stringElement2);

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

                var imageString = $('#DocumentImagesListStringApprs').val();
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
});
