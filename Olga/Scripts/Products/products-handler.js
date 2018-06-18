function removeImages(apprId, images) {
        console.log("removeImages()");
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

        var stringElementValue = stringElement.val();
        var re = /\s*,\s*/;
        var tagList = stringElementValue.split(re);
        for (var k = 0; k < tagList.length; k++) {
            if (tagList[k].includes(fileName)) {
                console.log('tagList[k] contains ' + fileName);
                var result = stringElementValue.replace(tagList[k], '');
                console.log('result = ' + result);
                stringElement.val(result);
            }
        }
    }

    function fileNameInHiddenList(fileName, stringElement) {
        console.log('fileNameInHiddenList:');
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
        var r = confirm("Are you sure you want to delete " + fileName);
        if (r === true) {
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
        var containerBlock = document.getElementById('filesDiv');
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
            '<div name="showImageBlock_' + fileName.replace(".", "") + '" id="showImageBlock_' + fileName.replace(".", "") + '" class="show_blockOfImage">' +
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

        var imageString = imageStringElement.val();
        if (imageString.length > 1) {
            imageString += ',';
        }
        for (i = 0; i < images.length; i++) {
            imageString += fileName + ',';
        }
        imageString = imageString.substring(0, imageString.length - 1);
        imageStringElement.val(imageString);
    }