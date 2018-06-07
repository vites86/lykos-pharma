function deleteProductDocument(event, id) {
  $.ajax({
    url: '/Product/Delete',
    data: { id: id },
    success: function(responce) {
     
      var row = event.target.closest('[data-rowid]');
      if (row) {
        var tbody = row.closest('tbody');
        tbody.removeChild(row);
      }
      //alert('success in deleting');
    },
    error: function(error) {
      //alert(error in delete); we should show message
    }
  });
}

$(document).ready(function(event) {
    Dropzone.autoDiscover = false;
    var images = [];
    var apprId = 1;
    var accept = ".pdf,.doc,.docx,.odt";

    function removeImages() {
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

    
    $("#dZUpload" + apprId).dropzone({
        url: "/Product/SaveUploadedFile?apprId=" + apprId,
            uploadMultiple: true,
            maxFiles: 10,
            acceptedFiles: "image/*, application/pdf, .txt,.xlsx,.docx,.ai,.cdr",
            maxFilesize: 1500000,
            addRemoveLinks: true,
            createImageThumbnails: true,
            init: function() {
                var fancybox = this;

                $('#savePhotoButton' + apprId).off('click').click(function(clickEvent) {
                    var i = 0;

                    if (images.length) {
                        var imageString = $('#DocumentImagesListStringApprs').val();
                        if (imageString.length > 1) {
                            imageString += ',';
                        }
                        for (i = 0; i < images.length; i++) {
                                imageString += images[i].savedName + ',';
                        }
                        imageString = imageString.substring(0, imageString.length - 1);
                        $('#DocumentImagesListStringApprs').val(imageString);

                        var imageString2 = $('#DocumentImagesListStringApprs' + apprId).val();
                        if (imageString2.length > 1) {
                            imageString2 += ',';
                        }
                        for (i = 0; i < images.length; i++) {
                                imageString2 += images[i].savedName + ',';
                        }
                        imageString2 = imageString2.substring(0, imageString2.length - 1);
                        $('#DocumentImagesListStringApprs' + apprId).val(imageString2);
                    }
                    //fancybox.removeAllFiles();
                });
                $('#deletePhoto' + apprId).off('click').click(function(clickEvent) {
                    removeImages();
                    fancybox.removeAllFiles();
                });
                $('#cancelAddPhoto' + apprId).off('click').click(function(clickEvent) {
                    images = [];
                    fancybox.removeAllFiles();
                });

                this.on("removedfile",
                    function(file) {
                        console.log('removed file event for ' + file.name);
                        var index = -1;
                        var imageString = $('#DocumentImagesListStringApprs').val();
                        var imageString2 = $('#DocumentImagesListStringApprs' + apprId).val();
                        for (var i = 0; i < images.length; i++) {
                            if (images[i].name === file.name) {
                                console.log('file.name: ' + file.name);
                                console.log('imageString before delete: ' + imageString);
                                var fileNameInList = apprId + '__' + file.name;
                                imageString.replace(fileNameInList, '');
                                imageString.replace(',,', ',');
                                imageString2.replace(fileNameInList, '');
                                imageString2.replace(',,', ',');
                                console.log('fileNameInList: ' + fileNameInList);
                                console.log('imageString after delete: ' + imageString);
                                index = i;
                                break;
                            }
                        }
                        if (index !== -1)
                            images.splice(index, 1);
                    });
            },
            success: function(file, response) {

                images.push({ name: file.name, savedName: response.Message });
                console.log('added image with name ' + file.name);
                file.previewElement.classList.add("dz-success");

            },
            error: function(file, response) {
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
            maxfilesexceeded: function(file) {
                this.removeFile(file);
            }
        });
});



function deleteImage() {
    console.log('removed file image');
    };




