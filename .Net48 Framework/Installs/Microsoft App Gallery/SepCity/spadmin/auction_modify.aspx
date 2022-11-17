<%@ page title="Auction" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="auction_modify.aspx.cs" inherits="wwwroot.auction_modify" %>
<%@ import namespace="SepCommon" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.min.css" integrity="sha256-DOS9W6NR+NFe1fUhEE0PGKY/fubbUCnOfTje2JMDw3Y=" crossorigin="anonymous" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-datetimepicker/2.5.20/jquery.datetimepicker.full.min.js" integrity="sha256-FEqEelWI3WouFOo2VWP/uJfs1y8KJ++FLh2Lbqc8SJk=" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            <%=SepFunctions.Date_Picker(ExpirationDate.ClientID, "false", "true", "")%>;
        });
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 31;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Auction</h4>
                <input type="hidden" runat="server" ID="AdID" />

                <sep:ChangeLogDropdown id="ChangeLog" runat="server" ModuleID="31" CssClass="form-control"></sep:ChangeLogDropdown>

                <div class="mb-3">
                    <label ID="CategoryLabel" ClientIDMode="Static" runat="server">Select a Category in the box below where to list your item:</label>
                    <sep:CategoryDropdown ID="Category" runat="server" ModuleID="31" ClientIDMode="Static" />
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
                    <label ID="StartingBidLabel" clientidmode="Static" runat="server" for="StartingBid">Starting Bid:</label>
                    <input type="text" ID="StartingBid" runat="server"  class="form-control" ClientIDMode="Static" />
                    <asp:CustomValidator ID="StartingBidRequired" runat="server" ControlToValidate="StartingBid"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Starting Bid is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="IncreaseBidsLabel" clientidmode="Static" runat="server" for="IncreaseBids">Money To Increase Bids By:</label>
                    <input type="text" ID="IncreaseBids" runat="server"  class="form-control" ClientIDMode="Static" />
                    <asp:CustomValidator ID="IncreaseBidsRequired" runat="server" ControlToValidate="IncreaseBids"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Increase Bids is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="PicturesLabel" clientidmode="Static" runat="server" for="Pictures">Pictures:</label>
                    <sep:UploadFiles ID="Pictures" runat="server" Mode="MultipleFiles" FileType="Images" ModuleID="31" />
                </div>
                <div class="mb-3">
                    <sep:WYSIWYGEditor Runat="server" ID="FullDescription" Width="99%" Height="450" />
                </div>
                <div class="mb-3">
                    <label ID="ExpirationDateLabel" clientidmode="Static" runat="server" for="ExpirationDate">Expiration Date:</label>
                    <div class="form-group">
                        <div class="input-group date" id="datetimepicker1">
                            <input type="text" id="ExpirationDate" class="form-control" runat="server" />
                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
                    </div>
                </div>
                <% 
                    var cCustomFields = new SepCityControls.CustomFields();
                    cCustomFields.ModuleID = 31;
                    cCustomFields.FieldUniqueID = SepCommon.SepCore.Request.Item("AdID");
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