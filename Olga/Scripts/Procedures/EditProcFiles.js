(function() {

            document.getElementById('load').style.visibility = "hidden";
            document.getElementById("choosenFile").innerHTML = "No file yet";

            var bar = $('.progress-bar');
            var barImage = $('#loading');
            var percent = $('.progress-bar');
            var status = $('#status');
            $(".progress").hide();

            $('form').ajaxForm({
                beforeSend: function(e) {

                    var fileInput = document.getElementById('uploadFiles');

                    if (!fileInput.files[0]) {
                        $('#FileLable').css('background-color', 'lightcoral');
                        console.log("no file chosen");
                        toastr.error("Please select a file before clicking 'Load'!");
                        return false;
                    }

                    status.empty();
                    var percentVal = '0%';
                    bar.width(percentVal);
                    percent.html(percentVal);
                    console.log("beforeSend...");
                    $("#loading").show();

                },
                uploadProgress: function(event, position, total, percentComplete) {
                    $(".progress").show();
                    var percentVal = percentComplete + '%';
                    bar.width(percentVal);
                    percent.html(percentVal);
                    console.log("uploadProgress...");
                },
                success: function (request) {
                    var percentVal = '100%';
                    bar.width(percentVal);
                    percent.html(percentVal);
                    if (request.success === true) {
                        console.log("success downloading");
                        toastr.info(request.responseText);
                        toastr.info("File downloaded successfuly! Wait a few seconds...");
                    }
                    if (request.success === false) {
                        console.log(request.responseText);
                        toastr.error(request.responseText);
                    }
                },
                error: function (request) {
                    var percentVal = '100%';
                    bar.width(percentVal);
                    percent.html(percentVal);
                    console.log(request.responseText);
                    toastr.error(request.responseText);
                },
                complete: function(xhr) {
                    status.html(xhr.responseText);
                    console.log("complete proccessing");
                    $(".progress").hide();
                    $("#loading").hide();
                    toastr.options.timeOut = 30000;
                    toastr.info("Page refreshing! Please wait!");
                    toastr.options.timeOut = 20000;
                    timeRefresh(0);
                }
            });

        })();

        function getFileData(myFile) {
            if (!myFile) {
                $('#FileLable').css('background-color', 'lightcoral');
                toastr.error("Please select a Document to download!");
                console.log("no file chosen");
            } else {
                $('#FileLable').css('background-color', 'lightgrey');
                //var file = myFile.files[0];
                //var filename = file.name;
                //console.log("file chosen = " + filename);
                var key;
                var filelist = "";
                for(key in myFile.files) {
                    if (myFile.files.hasOwnProperty(key)) {
                        filelist += myFile.files[key].name + '<br>';
                        console.log(myFile.files[key].name);
                    }
                }
                document.getElementById("choosenFile").innerHTML = filelist === "" ? "no file chosen" : filelist;
            }
        }

        function timeRefresh(timeoutPeriod) {
            setTimeout("location.reload(true);", timeoutPeriod);
        }


var linkToDelProcFile;

        $(function() {
            $(".delete").click(function() {
                var commentContainer = $(this).parent();
                var link = linkToDelProcFile;
                var documentId = $(this).attr("id");
                var procedureId = $('#procedureId').val();
                console.log("documentId = " + documentId);
                console.log("procedureId = " + procedureId);
                var loaderId = "load_" + documentId;

                $.ajax({
                    type: "POST",
                    url: link,
                    data: { documentId: documentId, procedureId: procedureId },
                    cache: false,
                    beforeSend: function() {
                        document.getElementById(loaderId).style.visibility='visible';
                        console.log(loaderId);
                        toastr.info("Wait a moment! File deleting...");},
                    success: function() {
                        commentContainer.slideUp('slow', function() { $(this).remove(); });
                        document.getElementById('load').style.visibility = "hidden";
                        console.log("deleted success!");
                        toastr.info("File deleted successfuly!");
                    }
                });
                return false;
            });
});

function initScript(_linkToDelProcFile) {
    linkToDelProcFile = _linkToDelProcFile;
}

function getProductInfo(link) {

    
    var queryString = window.location.search;
    var urlParams = new URLSearchParams(queryString);
    var productId = urlParams.get('productId');
    console.log("productId = " + productId);

    $.ajax({
        type: "POST",
        url: link,
        data: { productId: productId },
        cache: false,
        beforeSend: function () {},
        success: function (request) {
            if (request.success === true) {

                console.log("request = " + request.responseText);
                var product = JSON.parse(request.responseText);

                $("#CountryName").text(product.countryName);
                $("#PharmaceuticalForm").text(product.pharmaceuticalForm);
                $("#Strength").text(product.strength);
                $("#MarketingAuthorizNumber").text(product.marketingAuthorizNumber);
                $("#ProductCode").text(product.productCode);
                $('.productInfo img').hide();
                $('#CountryFlag').attr('src', "../../Content/images/countries/" + product.flagSrc);

                var productLink = jQuery('<a>').attr('href', '/Product/ShowDocuments/' + productId + '?countryId=' + product.countryId).text(product.productName);
                jQuery('#ProductNameWithLink').append(productLink);

            } else {
                console.log("request = " + request.responseText);
                toastr.error(request.responseText);
            }
        }
    });
}