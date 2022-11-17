<%@ Page Language="C#" CodeBehind="default.aspx.cs" Inherits="wwwroot._default6" %>

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
        function _getSelection(oEl) {
            var bReturn = false;
            var sTmp = "";
            for (var i = 0; i < document.getElementsByName("chkSelect").length; i++) {
                var oInput = document.getElementsByName("chkSelect")[i];
                if (oInput.checked == true) {
                    sTmp += "|" + document.getElementsByName("hidSelect")[i].value;
                    bReturn = true;
                }
            }
            oEl.value = sTmp.substring(1);
            return bReturn;
        }

        function selectImage(nIndex) {
            var s = document.getElementById("hidFileUrl" + nIndex).value;
            document.getElementById("txtURL").value = s;
            document.getElementById("hidThumbnail" + nIndex).value = document.getElementById("hidThumbnail" + nIndex).value;

            if ($("#iimage").length == 0) {
                $("body").append('<img src="' + s + '" id="iimage" style="display:none" />');
            } else {
                $("#iimage").attr('src', s);
            }
        }

        function doClose() {
            parent.tinymce.activeEditor.windowManager.close();
            return false;
        }

        function doOK() {
            var sURL = document.getElementById('txtURL').value;
            if (sURL == '') return false;
            parent.tinymce.activeEditor.insertContent('<img src="' + sURL + '" border="0" class="img-fluid" />');
            parent.tinymce.activeEditor.execCommand('mceAutoResize');
            parent.tinymce.activeEditor.windowManager.close();
            return false;
        }

        function deleteFolder(ele, force) {
            if (force == false) {
                confirm('Are you sure you want to delete this folder?', function () { deleteFolder(ele, true); });
                return false;
            } else {
                $('#' + ele.id).removeAttr("onclick");
                $('#' + ele.id).attr("onclick", "__doPostBack('btnDelete2','')");
                $('#' + ele.id)[0].click();
                return true;
            }
        }

        function deleteFiles(force) {
            if (force == false) {
                confirm('Are you sure you want to delete the selected files?', function () { deleteFiles(true); });
                return false;
            } else {
                $('#btnDelete').removeAttr("onclick");
                $('#btnDelete').click();
                return true;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div class="panel with-nav-tabs panel-info">
            <div class="panel-heading">
                <ul class="nav nav-tabs">
                    <li class="active"><a href="default.aspx?RelativeURLs=<%= SepCommon.SepCore.Request.Item("RelativeURLs") %>">My Photos</a></li>
                    <li><a href="stock_photos.aspx?RelativeURLs=<%= SepCommon.SepCore.Request.Item("RelativeURLs") %>">Photo Gallary</a></li>
                </ul>
            </div>
        </div>

        <asp:Label ID="lblFolder" runat="server" Text="Folder: "></asp:Label>
        <asp:Label ID="lblPath" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblUploadStatus" runat="server" Text="" Style="color: #FF0000; float: right;"></asp:Label>
        <br />
        <br />

        <div runat="server" id="divScroll" style="background: white; border-bottom: #cccccc 1px solid; height: 250px; overflow: auto; padding: 0px; width: 100%;">
            <asp:GridView ID="GridView1" GridLines="None" AlternatingRowStyle-BackColor="#f6f7f8"
                HeaderStyle-BackColor="#d6d7d8" CellPadding="7" runat="server"
                HeaderStyle-HorizontalAlign="left" AllowPaging="True" Width="100%"
                AllowSorting="false" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging">
                <Columns>
                    <asp:TemplateField ItemStyle-VerticalAlign="Middle" HeaderText="" ItemStyle-CssClass="padding2">
                        <ItemTemplate>
                            <input id="hidFileUrl<%#
                this.Eval("index") %>" type="hidden" value="<%#
                this.GetFileUrl(this.Eval("FileUrl", "")) %>" />
                            <input id="hidThumbnail<%#
                this.Eval("index") %>" type="hidden" value="<%#
                this.GetFileUrl(this.Eval("thumbnail", "")) %>" />
                            <%#
                this.ShowCheckBox(this.Eval("FileUrl").ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-VerticalAlign="Middle" HeaderText="File Name" HeaderStyle-Wrap="false" ItemStyle-CssClass="padding2">
                        <ItemTemplate>
                            <%#
                this.ShowLink(this.Eval("FileUrl").ToString(), this.Eval("FileName").ToString(), this.Eval("index", "")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LastUpdated" HeaderText="Last Updated" SortExpression="LastUpdated">
                        <ItemStyle VerticalAlign="Middle" Wrap="false" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Size" HeaderText="Size" SortExpression="Size">
                        <ItemStyle VerticalAlign="Middle" Wrap="false" />
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-VerticalAlign="Middle" HeaderText="Preview">
                        <ItemTemplate>
                            <%#
                this.Preview(this.Eval("Icon").ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <asp:Panel ID="panelManager" runat="server">

            <input type="hidden" id="hidFilesToDel" runat="server" />

            <asp:Panel ID="panelSpecial" runat="server">
                <div id="divManagerContent" style="background: #f6f7f8; border: #d6d7d8 1px solid; border-top: none; height: 70px; padding: 10px;">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete selected files" OnClick="btnDelete_Click" />
                            </td>
                            <td></td>
                            <td>
                                <input type="text" id="txtNewFolder" runat="server" ckass="form-control" />
                            </td>
                            <td>
                                <asp:Button ID="btnNewFolder" runat="server" Text="New Folder" OnClick="btnNewFolder_Click" ValidationGroup="NewFolder" />
                            </td>
                            <td>
                                <asp:CustomValidator ID="rfv1" ControlToValidate="txtNewFolder" ValidationGroup="NewFolder" runat="server" ErrorMessage="*"></asp:CustomValidator>
                            </td>
                        </tr>
                    </table>

                    <table cellpadding="0" cellspacing="0" style="margin-top: 27px">
                        <tr>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </td>
                            <td>
                                <asp:Button ID="btnUpload" runat="server" Text="Upload File" OnClick="btnUpload_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="height: 5px"></div>
                            </td>
                        </tr>
                    </table>

                    <input id="btnDelete2" runat="server" style="display: none" type="button" clientidmode="static" onserverclick="btnDelete2_Click" />
                </div>
            </asp:Panel>

            <table style="margin-bottom: 7px; margin-left: 5px; margin-top: 40px; width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="lblSource" runat="server" Text="Source"></asp:Label>
                    </td>
                    <td>:</td>
                    <td>
                        <input type="text" id="txtURL" runat="server" ckass="form-control" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnClose" CssClass="inpBtn" runat="server" Text=" Close " UseSubmitBehavior="false" OnClientClick="doClose();return false;" />
                        <asp:Button ID="btnOk" CssClass="inpBtn" runat="server" Text="    OK    " UseSubmitBehavior="false" OnClientClick="doOK();return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>