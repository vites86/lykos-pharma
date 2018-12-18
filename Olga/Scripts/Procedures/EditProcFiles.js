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

                    var fileInput = document.getElementById('uploads');

                    if (!fileInput.files[0]) {
                        $('#FileLable').css('background-color', 'lightcoral');
                        console.log("no file chosen");
                        toastr.error("Please select a file before clicking 'Load'!");
                        e.preventDefault();
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
                success: function() {
                    var percentVal = '100%';
                    bar.width(percentVal);
                    percent.html(percentVal);
                    console.log("success downloading");
                },
                complete: function(xhr) {
                    status.html(xhr.responseText);
                    console.log("complete downloading");
                    $(".progress").hide();
                    $("#loading").hide();
                    toastr.info("File downloaded successfuly!");
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

        //$(document).ready(function() {
        //    document.getElementById('load').style.visibility = "hidden";
        //});

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