<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="speakers.aspx.cs" inherits="wwwroot.speakers" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <select id="TopicID" runat="server" class="form-control">
        <option value="0">------------- Select a Topic -------------</option>
    </select>

    <select id="SpeakerID" runat="server" class="form-control">
        <option value="0">------------- Select a Speaker -------------</option>
    </select>

    <asp:Button ID="SearchButton" runat="server" CssClass="btn btn-primary" Text="Search Now" OnClick="SearchButton_Click" />

    <br /><br />

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <asp:Panel ID="TopicRow" runat="server" Visible="false">
        <h2 id="TopicName" runat="server"></h2>
        <div id="SpeechList" runat="server"></div>
    </asp:Panel>

    <asp:Panel ID="SpeakersRow" runat="server" Visible="false">
        <h2 id="SpeakerName" runat="server"></h2>
        <div id="Cred" runat="server"></div>
        <span ID="SpeakerImage" runat="server"></span>
        <div id="SpeechList2" runat="server"></div>
        <br />
        <span ID="Bio" runat="server"></span>
        <br />
        <br />
        <asp:HyperLink ID="RequestLink" runat="server" Text="Request this Speaker" NavigateUrl="speakers_schedule.aspx" />
    </asp:Panel>
</asp:content>