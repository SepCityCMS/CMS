<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stock_photos.aspx.cs" Inherits="wwwroot.spadmin.ImageManager.stock_photos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title id="idTitle" runat="server"></title>
    <link type="text/css" rel="stylesheet" integrity="sha384-zCbKRCUGaJDkqS1kPbPd7TveP5iyJE0EjAuZQTgFLD2ylzuqKfdKlfG/eSrtxUkn" crossorigin="anonymous" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-fQybjgWLrvvRgtW6bFlB7jaZrFsaBXjsOMm/tB9LTS58ONXgqbR9W8oWht/amnpF" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-json/2.6.0/jquery.json.min.js" integrity="sha256-Ac6pM19lP690qI07nePO/yY6Ut3c7KM9AgNdnc5LtrI=" crossorigin="anonymous"></script>
    
    <script src="../../js/bootbox.min.js" type="text/javascript"></script>
    <script src="../../js/main.js" type="text/javascript"></script>

    <style type="text/css">
        body {
            background: #eaeaea;
            color: #444444;
            font-family: tahoma, verdana;
            font-size: 8pt;
            margin: 12px;
        }

        td {
            color: #555555;
            font-family: verdana;
            font-size: 11px;
        }

        a:link {
            color: #777777;
        }

        a:visited {
            color: #777777;
        }

        a:hover {
            color: #111111;
        }

        input {
            font-size: 11px;
        }

        .inpBtn, .inpBtnOver, .inpBtnOut {
            background: url('button.png') #EEEEEE;
            border-bottom: 1px solid #AAAAAA;
            border-left: 1px solid #DDDDDD;
            border-right: 1px solid #AAAAAA;
            border-top: 1px solid #DDDDDD;
            color: #000000;
            cursor: pointer;
            font-size: 11px;
            font-weight: bold;
            margin-left: 2px;
            padding: 4px 10px 4px 10px;
        }

        .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover {
            background-color: #eeeeee !important;
            padding: 5px 10px;
        }

        .nav > li > a {
            padding: 5px 10px;
        }

        .panel-heading {
            padding: 5px 10px;
        }
    </style>
	
    <script type="text/javascript">
        var globalWidth = 0;
        var globalHeight = 0;

        function searchPhotos() {
            $("#PhotoList div").remove();
            $("#PhotoList").html('');
            $("#PhotoList").append("<strong>Searching photos ... please wait ...</strong>");
            $('#InsertRow').hide();
            var hasPhotos = false;
            $.getJSON("https://www.sepcity.com/api/photos/search?Keywords=" + $("#PhotoSearch").val(), function (data) {
                var firstRow = true;
                var rowId = "";
                var photoRowCount = 0;
                $("#PhotoList").html('');
                $.each(data.Photos, function (index) {
                    hasPhotos = true;
                    photoRowCount += 1;
                    if (photoRowCount == 4 || firstRow == true) {
                        rowId = "row" + data.Photos[index].PhotoID;
                        $("#PhotoList").append('<div class="row" id="' + rowId + '" style="margin:10px;"></div>');
                        photoRowCount = 0;
                    }
                    $("#PhotoList #" + rowId).append('<div class="col-3"><img src="data:image/png;base64,' + data.Photos[index].ThumbnailBase64 + '" id="Photo' + data.Photos[index].PhotoID + '" border="0" class="thumbnail" style="height: 100px; width: 100%; display: block; cursor: pointer; background-color: #ffffff;" onclick="selectPhoto(\'' + data.Photos[index].PhotoID + '\')" /></div>');
                    firstRow = false;
                });
                if (hasPhotos === false) {
                    $("#PhotoList").append("<div class=\"alert alert-danger\" role=\"alert\">Sorry no photos found matching your search criteria.</div>");
                }
            });
        }

        function selectPhoto(photoId) {
            $('#PhotoList img').css('border', '0px solid');
            $('#InsertRow').show();
            $('#Photo' + photoId).css('border', '4px solid #2e6da4');
            $.getJSON("https://www.sepcity.com/api/photos?ID=" + photoId, function (data) {
                $('#PhotoBase64').val(data.PhotoBase64);
                $('#photoHeight').val(data.Height);
                $('#photoWidth').val(data.Width);
                $('#FileName').val(data.FileName);
                setSelectedPhotoSize(data.Width, data.Height);
                setMaxPhotoSize(false);
            });
        }

        function setMaxPhotoSize(showWarning) {
            if ($('#photoWidth').val() > 1024) {
                if (showWarning === true) {
                    alert('The width of the photo has a max of 1024px.');
                }
                adjustPhoto("w", 1024, $('#photoHeight').val());
                $('#photoWidth').val("1024");
                return;
            }
            if ($('#photoHeight').val() > 1024) {
                if (showWarning === true) {
                    alert('The height of the photo has a max of 1024px.');
                }
                adjustPhoto("h", $('#photoWidth').val(), 1024);
                $('#photoHeight').val("1024");
                return;
            }
            adjustPhoto("w", $('#photoWidth').val(), $('#photoHeight').val());
        }

        function insertPhoto() {
            var PhotoBase64 = $('#PhotoBase64').val();
            if (PhotoBase64 == '') return false;

            async function start() {
                const result = await resizedataURL('data:image/png;base64,' + PhotoBase64, $('#photoWidth').val(), $('#photoHeight').val());
                uploadImage(result, $('#FileName').val());
            }

            start();
        }

        function resizedataURL(datas, wantedWidth, wantedHeight) {
            return new Promise(async function (resolve, reject) {

                // We create an image to receive the Data URI
                var img = document.createElement('img');

                // When the event "onload" is triggered we can resize the image.
                img.onload = function () {
                    // We create a canvas and get its context.
                    var canvas = document.createElement('canvas');
                    var ctx = canvas.getContext('2d');

                    // We set the dimensions at the wanted size.
                    canvas.width = wantedWidth;
                    canvas.height = wantedHeight;

                    // We resize the image with the canvas method drawImage();
                    ctx.drawImage(this, 0, 0, wantedWidth, wantedHeight);

                    var dataURI = canvas.toDataURL();

                    // This is the return of the Promise
                    resolve(dataURI);
                };

                // We put the Data URI in the image's src attribute
                img.src = datas;

            })
        }

        function uploadImage(imageBase64, FileName) {
            $.ajax({
                url: 'save_image.aspx',
                data: { imageBase64: imageBase64, FileName: FileName },
                type: "post",
                success: function (text) {
                    var image = '<img src="' + $('#FilePath').val() + text + '" class="img-fluid" />';
                    parent.tinymce.activeEditor.insertContent(image);
                    parent.tinymce.activeEditor.execCommand('mceAutoResize');
                    parent.tinymce.activeEditor.windowManager.close();
                },
                error: function () {
                    alert('Error saving image');
                }
            });
        }

        function setSelectedPhotoSize(width, height) {
            globalWidth = width;
            globalHeight = height;
        }

        function adjustPhoto(code, width, height) {
            var newPercent = 0;
            var newWidth = 0;
            var newHeight = 0;
            if (code === 'w') {
                newPercent = width / globalWidth;
                newHeight = parseInt(globalHeight * newPercent);
                $('#photoHeight').val(newHeight);
            } else {
                newPercent = height / globalHeight;
                newWidth = parseInt(globalWidth * newPercent);
                $('#photoWidth').val(newWidth);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div class="panel with-nav-tabs panel-info">
            <div class="panel-heading">
                <ul class="nav nav-tabs">
                    <li><a href="default.aspx?RelativeURLs=<%= SepCommon.SepCore.Request.Item("RelativeURLs") %>">My Photos</a></li>
                    <li class="active"><a href="stock_photos.aspx?RelativeURLs=<%= SepCommon.SepCore.Request.Item("RelativeURLs") %>">Photo Gallary</a></li>
                </ul>
            </div>
        </div>

		<div class="input-group">
			<input type="text" ID="PhotoSearch" runat="server" placeholder="Search for..." ClientIDMode="Static" class="form-control" />
			<span class="input-group-btn">
				<button id="ModuleSearchButton" onclick="searchPhotos();return false;" class="btn btn-default">Go!</button>
			</span>
		</div>

        <div id="PhotoList"></div>
		
		<input type="hidden" name="PhotoBase64" id="PhotoBase64" />
		<input type="hidden" name="FileName" id="FileName" />
		<input type="hidden" name="FilePath" id="FilePath" runat="server" />
		
		<div style="height:40px; width: calc(100% - 25px); position:fixed; bottom:0; background-color: #eaeaea; margin: 0 auto;">
			<div id="InsertRow" style="display: none;">
				<strong>Width:</strong> <input type="text" name="width" id="photoWidth" style="width: 80px;display: inline-block;" onchange="setMaxPhotoSize(true);" /> 
				<strong>Height:</strong> <input type="text" name="Height" id="photoHeight" style="width: 80px;display: inline-block;" onchange="setMaxPhotoSize(true);" /> 
				<input type="button" value="Insert Photo" onclick="insertPhoto();" class="btn btn-primary" />
			</div>
		</div>

    </form>
</body>
</html>