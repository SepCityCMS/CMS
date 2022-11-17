<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="games.aspx.cs" inherits="wwwroot.games1" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <span id="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
    </span>

    <div class="row">
        <asp:ListView ID="ListContent" runat="server" ItemPlaceholderID="itemPlaceholder">
            <layouttemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
            </layouttemplate>
            <itemtemplate>
                <div class="col-md-4">
                    <div class="card">
                        <div class="box">
                            <div class="img">
                                <img src="<%= this.GetInstallFolder(true) %>images/games/<%#
                this.Eval("ImageFile") %>" alt="" class="img-fluid" />
                            </div>
                            <h2><%#
                this.Eval("GameName") %> <br /><span><a class="btn btn-primary" href="<%= this.GetInstallFolder(false) %>game/<%#
                this.Eval("GameID") %>/<%#
                this.Format_ISAPI(this.Eval("GameName")) %>/">Play Now</a></span></h2>
                            <p><%#
                this.Eval("Description") %></p>
                            <div class="collapse" id="collapseExample">
                            </div>
                        </div>
                    </div>
                </div>
            </itemtemplate>
        </asp:ListView>
    </div>
</asp:content>