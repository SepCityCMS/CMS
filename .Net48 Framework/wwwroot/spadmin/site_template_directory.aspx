<%@ page title="Site Template" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="site_template_directory.aspx.cs" inherits="wwwroot.site_template_directory" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/site_template.js" type="text/javascript"></script>
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script src="../js/gridview.js" type="text/javascript"></script>
    <script src="../js/management.js" type="text/javascript"></script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 984;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <h2>
            <span ID="PageHeader" runat="server" Text="Advance File Manager"></span>
        </h2>

        <span class="successNotification" id="successNotification">
            <span ID="DeleteResult" runat="server"></span>
        </span>

        <div class="GridViewStyle">
            <div class="GridViewFilter">
                <div class="GridViewFilterLeft">
                    <select id="FilterDoAction" runat="server" Class="GridViewAction" ClientIDMode="Static">
                        <option value="">Select an Action</option>
                        <option value="DeleteItems">Delete Items</option>
                    </select>
                    <button class="btn btn-light" ID="RunAction" runat="server" OnServerClick="RunAction_Click" onclick="if(ExecuteAction(this, 'DeleteItem') == false) {return false} else">GO</button>
                </div>
                <div class="GridViewFilterRight">
                    <asp:Panel ID="UploadFilePanel" DefaultButton="UploadFileButton" runat="server" CssClass="PanelButton">
                        Upload File: <asp:FileUpload ID="UploadFile" runat="server" CssClass="GridViewAction inline-block" Width="150px" /> <asp:Button ID="UploadFileButton" runat="server" Text="Upload" onclick="UploadFileButton_Click" />
                    </asp:Panel>
                    <asp:Panel ID="CreateFolderPanel" DefaultButton="CreateFolderButton" runat="server" CssClass="PanelButton">
                        Create Folder:  <input type="text" id="FolderName" runat="server" class="GridViewAction inline-block" style="width:100px" /> <asp:Button ID="CreateFolderButton" runat="server" Text="Create" onclick="CreateFolderButton_Click" />
                    </asp:Panel>
                    <asp:Panel ID="CreateFilePanel" DefaultButton="CreateFileButton" runat="server" CssClass="PanelButton">
                        Create File:  <input type="text" id="FileName" runat="server" class="GridViewAction inline-block" style="width:100px" /> <asp:Button ID="CreateFileButton" runat="server" Text="Create" onclick="CreateFileButton_Click" />
                    </asp:Panel>
                </div>
            </div>

            <input type="hidden" ID="UniqueIDs" runat="server" ClientIDMode="Static" Value="" />

            <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                          CssClass="GridViewStyle">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <HeaderTemplate>
                            <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" id="DeleteItem<%#
                Eval("FileName")%>" value="<%#
                Eval("FileName")%>" onclick="gridviewSelectRow(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                                Convert.ToString(SepCommon.SepCore.Strings.Right(Eval("Folder").ToString(), 3) == "%5C" ? "<a href=\"site_template_directory.aspx?Folder=" + Eval("Folder") + "\">" + Eval("Image") + Eval("FileName") + "</a>" : "<a href=\"site_template_download.aspx?File=" + Eval("Folder") + "\" target=\"_blank\">" + Eval("Image") + " " + Eval("FileName") + "</a>")
                            %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="View/Download" ItemStyle-Width="50px">
                        <ItemTemplate>
                            <%#
                                Convert.ToString(!string.IsNullOrWhiteSpace(Convert.ToString(Eval("AllowEdit")))
                                ? "<a href=\"" + Convert.ToString(Convert.ToString(Eval("AllowEdit")) == "editor" ? "site_template_open_file.aspx?File=" + Eval("Folder").ToString()
                                : Convert.ToString(Convert.ToString(Eval("AllowEdit")) == "Zip" ? GetInstallFolder() + "spadmin/site_template_directory.aspx?DoAction=DownloadZip&Folder=" + Eval("Folder").ToString() : GetInstallFolder() + "skins" + Eval("Folder").ToString()) + "\" target=\"_blank")
                                + "\"><img src=\"" + GetInstallFolder() + "images/public/" +
                                Convert.ToString(Convert.ToString(Eval("AllowEdit")) == "editor" ? "edit.png" : Convert.ToString(Convert.ToString(Eval("AllowEdit")) == "Zip" ? "software-128.png" : "view-image.png")) +
                                "\" border=\"0\" width=\"16\" height=\"16\" alt=\"" +
                                Convert.ToString(Convert.ToString(Eval("AllowEdit")) == "editor" ? "Edit File" : Convert.ToString(Convert.ToString(Eval("AllowEdit")) == "Zip" ? "Download Zip" : "View Image")) +
                                "\" /></a>" : "")
                            %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Size" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                Eval("Size")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Created" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                Eval("DateCreated")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Last Modified" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <%#
                Eval("DateModified")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
</div>
    </asp:Panel>
</asp:content>