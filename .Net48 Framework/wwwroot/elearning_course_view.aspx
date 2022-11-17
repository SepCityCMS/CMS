<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="elearning_course_view.aspx.cs" inherits="wwwroot.elearning_course_view" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <asp:Panel ID="DisplayContent" runat="server" Visible="true">
        <h1 id="CourseName" runat="server"></h1>

        <h2>Course Details</h2>
        <div>
            <label>Instructor:</label>
            <span ID="Instructor" runat="server"></span>
        </div>
        <div>
            <label>Credits:</label>
            <span ID="Credits" runat="server"></span>
        </div>
        <div id="StartDateRow" runat="server">
            <label>Start Date:</label>
            <span ID="StartDate" runat="server"></span>
        </div>
        <div id="EndDateRow" runat="server">
            <label>End Date:</label>
            <span ID="EndDate" runat="server"></span>
        </div>
        <div>
            <label>Cost:</label>
            <span ID="Cost" runat="server"></span>
        </div>

        <br />

        <span ID="Description" runat="server"></span>

        <p align="center">
            <asp:Button Text="Course Registration" ID="RegisterButton" runat="server" OnClick="RegisterButtonClick" />
        </p>
        <%
            var cSocialShare = new SepCityControls.SocialShare();
            this.Response.Write(cSocialShare.Render());
        %>
    </asp:Panel>
</asp:content>