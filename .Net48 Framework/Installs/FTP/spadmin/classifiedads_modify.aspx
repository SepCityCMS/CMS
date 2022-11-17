<%@ page title="Classified Ads" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="classifiedads_modify.aspx.cs" inherits="wwwroot.classifiedads_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=ExpirationDate.ClientID%>').datetimepicker();
        });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 44;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Classified Ad</h4>
                <input type="hidden" runat="server" ID="AdID" />

                <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="44" CssClass="form-control"></sep:ChangeLogDropdown>

                <div class="mb-3">
                    <label ID="CategoryLabel" ClientIDMode="Static" runat="server">Select a Category in the box below where to list your item:</label>
                    <sep:CategoryDropdown ID="Category" runat="server" ModuleID="44" ClientIDMode="Static" />
                    <asp:CustomValidator ID="CategoryRequired" runat="server" ControlToValidate="Category"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Category is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3" runat="server" id="PortalsRow">
                    <label ID="PortalLabel" clientidmode="Static" runat="server" for="Portal">Portal:</label>
                    <sep:PortalDropdown ID="Portal" runat="server" ClientIDMode="Static" CssClass="form-control" />
                </div>
                <div class="mb-3">
                    <label ID="AdTitleLabel" clientidmode="Static" runat="server" for="AdTitle">Title:</label>
                    <input type="text" ID="AdTitle" runat="server"  class="form-control" ClientIDMode="Static" />
                    <asp:CustomValidator ID="AdTitleRequired" runat="server" ControlToValidate="AdTitle"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Title is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Pictures:</label>
                    <sep:UploadFiles ID="Pictures" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="44" />
                </div>
                <div class="mb-3">
                    <sep:WYSIWYGEditor Runat="server" ID="FullDescription" Width="99%" Height="450" />
                </div>
                <div class="mb-3">
                    <label ID="QuantityLabel" clientidmode="Static" runat="server" for="Quantity">Quantity:</label>
                    <input type="text" ID="Quantity" runat="server"  class="form-control" Text="1" />
                    <asp:CustomValidator ID="QuantityRequired" runat="server" ControlToValidate="Quantity"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Quantity is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="PriceLabel" clientidmode="Static" runat="server" for="Price">Price:</label>
                    <input type="text" ID="Price" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="PriceRequired" runat="server" ControlToValidate="Price"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Price is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="ExpirationDateLabel" clientidmode="Static" runat="server" for="Price">Expiration Date:</label>
                    <div class="form-group">
                        <div class="input-group date" id="ExpirationDateDiv">
                            <input type="text" id="ExpirationDate" class="form-control" runat="server" />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                    </div>
                </div>
                <% 
                    var cCustomFields = new SepCityControls.CustomFields();
                    cCustomFields.ModuleID = 44;
                    cCustomFields.FieldUniqueID = AdID.Value;
                    if(sUserID != "") {
                        cCustomFields.UserID = sUserID;
                    } else {
                        cCustomFields.UserID = SepCommon.SepFunctions.Session_User_ID();
                    }
                    Response.Write(cCustomFields.Render()); 
                %>
            </div>
                <div class="button-to-bottom">
		<button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		<span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	</div>
        </div>
    </asp:Panel>
</asp:content>