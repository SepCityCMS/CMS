<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="businesses.aspx.cs" inherits="wwwroot.businesses" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>
    
    <%
        var cCategories = new SepCityControls.CategoryLayout();
        cCategories.ModuleID = 20;
        cCategories.CategoryID = SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID"));
        this.Response.Write(cCategories.Render());
    %>

    <br />

    <asp:ListView ID="ListContent" runat="server" ItemPlaceholderID="itemPlaceholder">
        <layouttemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </layouttemplate>
        <itemtemplate>
            <div class="property-bx" style="padding:5px;">
                <span style="display:inline-block;float:left;margin-left:15px;"><strong><%#
                this.Eval("BusinessName") %></strong><span class="badge badge-info" style="display:inline-block;float:right;margin-right:15px;">Hits: <span class="badge badge-light"><%#
                this.Eval("Visits") %></span></span>
				<div style="clear:both;"></div>
                <div class="property-content">
                    <p><%#
                this.Eval("Description") %></p>
                    <p>Member Comments (<%#
                this.Eval("TotalComments") %>)</p>
                    <div class="text-right">
                        <a class="btn btn-primary" href="<%= this.GetInstallFolder() %>business/<%#
                this.Eval("BusinessID") %>/<%#
                this.Format_ISAPI(this.Eval("BusinessName")) %>/">Details</a>
                        <a class="btn btn-secondary" href="<%= this.GetInstallFolder() %>refer.aspx?URL=%2fbusiness%2f<%#
                this.Eval("BusinessID") %>%2f<%#
                this.Format_ISAPI(this.Eval("BusinessName")) %>%2f">Refer</a>
                    </div>
                </div>
            </div>
        </itemtemplate>
    </asp:ListView>

    <br />

    <div class="PagingPanel">
        <asp:DataPager ID="PagerTemplate" runat="server" PagedControlID="ListContent" PageSize="20" OnPreRender="PagerTemplate_PreRender">
            <fields>
                <asp:NextPreviousPagerField ButtonType="Button" FirstPageText="&lt;&lt;" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="True" />
                <asp:TemplatePagerField>
                </asp:TemplatePagerField>
                <asp:NextPreviousPagerField ButtonType="Button" LastPageText="&gt;&gt;" ShowLastPageButton="True" ShowNextPageButton="True" ShowPreviousPageButton="False" />
            </fields>
        </asp:DataPager>
    </div>

    <div runat="server" ID="NewestListings"><h5>Latest Business Listings</h5></div>
    <asp:ListView ID="NewestContent" runat="server" ItemPlaceholderID="itemPlaceholder2">
        <layouttemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder2"></asp:PlaceHolder>
        </layouttemplate>
        <itemtemplate>
            <div class="property-bx" style="padding:5px;">
                <span style="display:inline-block;float:left;margin-left:15px;"><strong><%#
                this.Eval("BusinessName") %></strong></span><span class="badge badge-info" style="display:inline-block;float:right;margin-right:15px;">Hits: <span class="badge badge-light"><%#
                this.Eval("Visits") %></span></span>
				<div style="clear:both;"></div>
                <div class="property-content">
                    <p><%#
                this.Eval("Description") %></p>
                    <p>Member Comments (<%#
                this.Eval("TotalComments") %>)</p>
                    <div class="text-right">
                        <a class="btn btn-primary" href="<%= this.GetInstallFolder() %>business/<%#
                this.Eval("BusinessID") %>/<%#
                this.Format_ISAPI(this.Eval("BusinessName")) %>/">Details</a>
                        <a class="btn btn-secondary" href="<%= this.GetInstallFolder() %>refer.aspx?URL=%2fbusiness%2f<%#
                this.Eval("BusinessID") %>%2f<%#
                this.Format_ISAPI(this.Eval("BusinessName")) %>%2f">Refer</a>
                    </div>
                </div>
            </div>
        </itemtemplate>
    </asp:ListView>
</asp:content>