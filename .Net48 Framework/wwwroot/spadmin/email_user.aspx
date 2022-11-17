<%@ page title="Email User" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="email_user.aspx.cs" inherits="wwwroot.email_user" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 986;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <span id="failureNotification">
            <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
        </span>

        <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">Email Member</h4>
                <input type="hidden" runat="server" ID="UserID" />

                <div class="mb-3">
                    <label ID="EmailSubjectLabel" clientidmode="Static" runat="server" for="EmailSubject">Email Subject:</label>
                    <input type="text" ID="EmailSubject" runat="server"  class="form-control" ClientIDMode="Static" />
                    <asp:CustomValidator ID="EmailSubjectRequired" runat="server" ControlToValidate="EmailSubject"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Email Subject is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="EmailBodyLabel" clientidmode="Static" runat="server" for="EmailBody">Email Body:</label>
                    <sep:WYSIWYGEditor Runat="server" ID="EmailBody" Width="99%" Height="450" />
                </div>
            </div>
                <div class="mb-3"><asp:Button CssClass="btn btn-primary" ID="SendButton" runat="server" Text="Send Email" onclick="SendButton_Click" /></div>
        </div>
    </asp:Panel>
</asp:content>