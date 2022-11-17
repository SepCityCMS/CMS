<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="faq.aspx.cs" inherits="wwwroot.faq1" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var acc = document.getElementsByClassName("accordion");
            var i;

            for (i = 0; i < acc.length; i++) {
              acc[i].addEventListener("click", function() {
                this.classList.toggle("active");
                var panel = this.nextElementSibling;
                if (panel.style.maxHeight){
                  panel.style.maxHeight = null;
                } else {
                  panel.style.maxHeight = panel.scrollHeight + "px";
                } 
              });
            }
            });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>
    
    <%
        var cCategories = new SepCityControls.CategoryLayout();
        cCategories.ModuleID = 9;
        cCategories.CategoryID = SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID"));
        this.Response.Write(cCategories.Render());
    %>
    
    <asp:ListView ID="ListContent" runat="server" ItemPlaceholderID="itemPlaceholder">
        <layouttemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </layouttemplate>
        <itemtemplate>
            <div class="faq-panel">
                <h3><%#
                this.Eval("Question") %></h3>
                <div class="accordion"><span><%#
                this.Format_Date(this.Eval("DatePosted").ToString()) %></span> <abbr><%#
                this.Eval("Question") %></abbr></div>
                <div class="panel">
                    <div class="faq-bx">
                        <h4><%#
                this.Eval("Question") %></h4>
                        <div class="faq-rating">
                            <div class="rating">
                                <sep:ratingstars id="RatingStars1" runat="server" moduleid="9" LookupID='<%#
                this.Eval("FAQID") %>' />
                            </div>
                        </div>
                        <%#
                this.Eval("Answer") %>
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

    <div runat="server" ID="NewestListings"><h5>Newest FAQ's</h5></div>

    <asp:ListView ID="NewestContent" runat="server" ItemPlaceholderID="PlaceHolder1">
        <layouttemplate>
            <asp:PlaceHolder runat="server" ID="PlaceHolder1"></asp:PlaceHolder>
        </layouttemplate>
        <itemtemplate>
            <div class="faq-panel">
                <h3><%#
                this.Eval("Question") %></h3>
                <div class="accordion"><span><%#
                this.Format_Date(this.Eval("DatePosted").ToString()) %></span> <abbr><%#
                this.Eval("Question") %></abbr></div>
                <div class="panel">
                    <div class="faq-bx">
                        <h4><%#
                this.Eval("Question") %></h4>
                        <div class="faq-rating">
                            <div class="rating">
                                <sep:ratingstars id="RatingStars1" runat="server" moduleid="9" LookupID='<%#
                this.Eval("FAQID") %>' />
                            </div>
                        </div>
                        <%#
                this.Eval("Answer") %>
                    </div>
                </div>
            </div>
        </itemtemplate>
    </asp:ListView>
</asp:content>