<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="celebrities.aspx.cs" inherits="wwwroot.conference" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <span ID="PageText" runat="server"></span>

    <asp:ListView ID="ListContent" runat="server">
        <LayoutTemplate>
            <div class="profile_box">
                <span ID="itemPlaceholder" runat="server"></span>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="profile_left">
                <div class="box_inner_left">
                    <a href="<%= this.GetInstallFolder() %>celebrity/<%#
                this.Eval("ProfileID") %>/<%#
                this.Format_ISAPI(this.Eval("Username")) %>/">
                        <img src="<%#
                this.Eval("DefaultPicture") %>"
                            border="0" alt="" />
                    </a>
                </div>
                <div class="box_inner_right">
                    <h3>
                        <%#
                this.Eval("FirstName") %> <%#
                this.Eval("LastName") %><br />
                        <span><%#
                this.Eval("Age") %> Years Old</span></h3>
                    <h4><%#
                Convert.ToString(this.Eval("Occupation").ToString() != "" ? this.Eval("Occupation") : "N/A") %></h4>
                    <a href="<%= this.GetInstallFolder() %>celebrity/<%#
                this.Eval("ProfileID") %>/<%#
                this.Format_ISAPI(this.Eval("Username")) %>/">
                        <div class="box_inner_gray">
                            <p>
                                <%#
                this.Eval("Charities") %>
                            </p>
                            <span>CHARITIES</span>
                        </div>
                    </a>
                    <a href="<%= this.GetInstallFolder() %>celebrity/<%#
                this.Eval("ProfileID") %>/<%#
                this.Format_ISAPI(this.Eval("Username")) %>/">
                        <div class="box_inner_gray">
                            <p>
                                <%#
                this.Eval("Causes") %>
                            </p>
                            <span>Causes</span>
                        </div>
                    </a>
                </div>
            </div>
            <div class="profile_right">
                <div class="profile_right_top">
                    <h5>INSIGHTS</h5>
                </div>

                <div class="profile_right_center">
                    <div class="clr"></div>
                    <p>Location</p>
                    <span><%#
                Convert.ToString(this.Eval("Location").ToString() != "" ? this.Eval("Location") : "N/A") %></span>

                    <div class="clr"></div>
                </div>
                <div class="profile_right_bottom">
                    <%# Convert.ToString(Convert.ToBoolean(this.Eval("isOnline")) ? "<a href='" + this.GetInstallFolder() + "video/default.aspx?ProfileID=" + this.Eval("ProfileID") + "&UserID=" + this.Eval("UserID") + "'>Call Now</a> | " : "") %><a href='<%= this.GetInstallFolder() %>celebrities_schedule_call.aspx?ProfileID=<%#
                this.Eval("ProfileID") %>&UserID=<%#
                this.Eval("UserID") %>'>Schedule Call</a> |
                    <a href="<%= this.GetInstallFolder() %>celebrity/<%#
                this.Eval("ProfileID") %>/<%#
                this.Format_ISAPI(this.Eval("Username")) %>/">Details</a>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>

    <br />

    <div class="PagingPanel">
        <asp:DataPager ID="PagerTemplate" runat="server" PagedControlID="ListContent" PageSize="20" OnPreRender="PagerTemplate_PreRender">
            <Fields>
                <asp:NextPreviousPagerField ButtonType="Button" FirstPageText="&lt;&lt;" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="True" />
                <asp:TemplatePagerField>
                </asp:TemplatePagerField>
                <asp:NextPreviousPagerField ButtonType="Button" LastPageText="&gt;&gt;" ShowLastPageButton="True" ShowNextPageButton="True" ShowPreviousPageButton="False" />
            </Fields>
        </asp:DataPager>
    </div>
</asp:content>