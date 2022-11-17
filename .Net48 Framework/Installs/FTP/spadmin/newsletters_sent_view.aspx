<%@ page title="Newsletters" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="newsletters_sent_view.aspx.cs" inherits="wwwroot.newsletters_sent_view" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 24;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">View Sent Newsletter</h4>
                <div class="mb-3">
                    <label ID="EmailSubjectLabel" ClientIDMode="Static" runat="server">Email Subject:</label>
                    <span ID="EmailSubject" runat="server"></span>
                </div>
                <div class="mb-3">
                    <label ID="DateSentLabel" ClientIDMode="Static" runat="server">Date Sent:</label>
                    <span ID="DateSent" runat="server"></span>
                </div>
                <div class="mb-3">
                    <label ID="EmailBodyLabel" ClientIDMode="Static" runat="server">Email Body:</label>
                    <span ID="EmailBody" runat="server"></span>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:content>