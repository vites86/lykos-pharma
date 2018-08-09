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

    function removeImages() {
        var elements = document.querySelectorAll('[data-type-marker="image"]');
        if (elements && elements.length) {
            var i = 0;
            for (; i < elements.length; i++) {
                $('form').remove(elements[i]);
            }
            images = [];
            $('#DocumentImagesListString').val('');
        }
    }

    var element = 3;
        $("#dZUpload" + element).dropzone({
            url: "/Product/SaveUploadedFile?apprId=" + element,
            uploadMultiple: true,
            maxFiles: 10,
            acceptedFiles: "image/*,application/pdf, .txt,.xlsx,.docx,.ai,.cdr",
            maxFilesize: 2500000000,
            addRemoveLinks: true,
            init: function() {
                var fancybox = this;

                $('#savePhotoButton' + element).off('click').click(function(clickEvent) {
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

                        var imageString2 = $('#DocumentImagesListStringApprs' + element).val();
                        if (imageString2.length > 1) {
                            imageString2 += ',';
                        }
                        for (i = 0; i < images.length; i++) {
                            imageString2 += element + '__' + images[i].savedName + ',';
                        }
                        imageString2 = imageString2.substring(0, imageString2.length - 1);
                        $('#DocumentImagesListStringApprs' + element).val(imageString2);
                    }
                    fancybox.removeAllFiles();
                });
                $('#deletePhoto' + element).off('click').click(function(clickEvent) {
                    removeImages();
                    fancybox.removeAllFiles();
                });
                $('#cancelAddPhoto' + element).off('click').click(function(clickEvent) {
                    images = [];
                    fancybox.removeAllFiles();
                });

                this.on("removedfile",
                    function(file) {
                        console.log('removed file event');
                        var index = -1;
                        for (var i = 0; i < images.length; i++) {
                            if (images[i].name === file.name) {
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




