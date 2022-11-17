<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="advertising.aspx.cs" inherits="wwwroot.user_advertising" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <asp:ListView ID="AdContent" runat="server" ItemPlaceholderID="itemPlaceholder">
        <layouttemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </layouttemplate>
        <itemtemplate>
            <div class="property-bx-bx adv-bx">
                <h3><span>Spot #<%#Container.DataItemIndex + 1 %> </span> <%#
                this.Eval("PlanName") %></h3>
                <p><%#
                this.Eval("LongPrice") %></p>
                <div class="devider"></div>
                <p><%#
                this.Eval("Description") %></p>
                <p>Target Zones: <%#
                this.Eval("Zones") %></p>
                <div class="text-right"><a href="advertising_order.aspx?PlanID=<%#sPriceID %>" class="btn btn-primary">Order Now</a></div>
            </div>
		</itemtemplate>
    </asp:ListView>
</asp:content>