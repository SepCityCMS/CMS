<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="discounts.aspx.cs" inherits="wwwroot.discounts1" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function Print(a) {
            var row = $(a).closest("div.discount-card").clone(true);
            var printWin = window.open('', '', 'left=0", ",top=0,width=1000,height=600,status=0');
            $(".PrintCoupon", row).remove();
            var sStyle = "<link rel=\"stylesheet\" href=\"<%= this.GetInstallFolder() %>skins/public/styles/public.css\" type=\"text/css\" />";
            var printBut = "<p align=\"center\"><input type=\"button\" value=\"Print\" onclick=\"print()\" /></p>";
            var dv = $("<div />");
            dv.append(sStyle);
            dv.append(row);
            dv.append(printBut);
            printWin.document.write(dv.html());
            printWin.document.close();
            printWin.focus();
        }
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>
    
    <%
        var cCategories = new SepCityControls.CategoryLayout();
        cCategories.ModuleID = 5;
        cCategories.CategoryID = SepCommon.SepFunctions.toLong(SepCommon.SepCore.Request.Item("CatID"));
        this.Response.Write(cCategories.Render());
    %>

    <br />

    <span class="successNotification" id="successNotification">
        <span ID="DeleteResult" runat="server"></span>
    </span>

    <asp:ListView ID="ListContent" runat="server" ItemPlaceholderID="itemPlaceholder">
        <layouttemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
        </layouttemplate>
        <itemtemplate>
            <div class="discount-card">
               <div class="discount-card-body">
                  <div class="table-responsive">
                     <table class="DiscountTable" width="500" height="231" align="center" cellpadding="0" cellspacing="0">
                        <tbody>
                           <tr>
                              <td valign="top">
                                 <br>
                                 <table align="center" height="205" cellpadding="0" cellspacing="0" width="95%">
                                    <tbody>
                                       <tr class="TableHeader">
                                          <td colspan="2" align="center" valign="top">
                                             <table cellpadding="0" cellspacing="0" width="98%">
                                                <tbody>
                                                   <tr>
                                                      <td width="170" style="line-height:26px;">
                                                         <p><b>Code:</b> <%#
                this.Eval("DiscountCode") %><br>
                                                            <b>Expires:</b> <%#
                this.Format_Date(this.Eval("ExpireDate").ToString()) %><br>
                                                            <b>Quantity:</b> <%#
                this.Eval("Quantity") %><br>
                                                            <%# Convert.ToString(this.Eval("CompanyName").ToString() != "" ? this.Eval("CompanyName").ToString() : "") %>
                                                         </p>
                                                         <%# Convert.ToString(this.Eval("BarCodeImage") != null ? "<img src=\"" + this.Eval("BarCodeImage") + "\" border=\"0\" />" : "<img src=\"" + this.GetInstallFolder(true) + "images/admin/barcode.gif\" border=\"0\" />") %>
                                                      </td>
                                                      <td width="120" align="left" valign="top">
                                                         <%# Convert.ToString(this.Eval("ProductImage") != null ? "<img src=\"" + this.Eval("ProductImage") + "\" border=\"0\" class=\"coupon-img img-rounded img-fluid\" />" : "") %>
                                                         <p style="font-size:16px; line-height:16px; padding-top:8px;"><%# Convert.ToString(this.Eval("LabelText").ToString() != "" ? this.Eval("LabelText") : "") %></p>
                                                      </td>
                                                      <td width="28%" align="center" valign="top">
                                                         <div style="background-color:#ff0101; width:125px; height:125px; border-radius:50%; padding:30px 15px 15px; color:#fff; text-align:center">
                                                            <h2 style="margin:0"><%#
                this.FormatPrice(this.Eval("MarkOffType").ToString(), this.Eval("MarkOffPrice").ToString()) %></h2>
                                                         </div>
                                                      </td>
                                                   </tr>
                                                </tbody>
                                             </table>
                                          </td>
                                       </tr>
                                       <tr>
                                          <td colspan="2" height="30">
                                             <table width="100%">
                                                <tbody>
                                                   <tr>
                                                      <td width="100%" valign="Bottom"><span style="color: #F02828"><b>Disclaimer</b></span><b>: </b><%# Convert.ToString(this.Eval("Disclaimer").ToString() != "" ? this.Eval("Disclaimer").ToString() : "N/A") %></td>
                                                   </tr>
                                                </tbody>
                                             </table>
                                          </td>
                                       </tr>
                                    </tbody>
                                 </table>
                              </td>
                           </tr>
                        </tbody>
                     </table>
                  </div>
               </div>
               <div class="discount-card-footer">
                  <a href="javascript:void(0)" onclick="Print(this)" class="card-link">Print this Coupon</a>
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

    <div runat="server" id="NewestListings"><h5>Latest Discount Coupons</h5></div>
    <asp:ListView ID="NewestContent" runat="server" ItemPlaceholderID="itemPlaceholder2">
        <layouttemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder2"></asp:PlaceHolder>
        </layouttemplate>
        <itemtemplate>
            <div class="discount-card">
               <div class="discount-card-body">
                  <div class="table-responsive">
                     <table class="DiscountTable" width="500" height="231" align="center" cellpadding="0" cellspacing="0">
                        <tbody>
                           <tr>
                              <td valign="top">
                                 <br>
                                 <table align="center" height="205" cellpadding="0" cellspacing="0" width="95%">
                                    <tbody>
                                       <tr class="TableHeader">
                                          <td colspan="2" align="center" valign="top">
                                             <table cellpadding="0" cellspacing="0" width="98%">
                                                <tbody>
                                                   <tr>
                                                      <td width="170" style="line-height:26px;">
                                                         <p><b>Code:</b> <%#
                this.Eval("DiscountCode") %><br>
                                                            <b>Expires:</b> <%#
                this.Format_Date(this.Eval("ExpireDate").ToString()) %><br>
                                                            <b>Quantity:</b> <%#
                this.Eval("Quantity") %><br>
                                                            <%# Convert.ToString(this.Eval("CompanyName").ToString() != "" ? this.Eval("CompanyName").ToString() : "") %>
                                                         </p>
                                                         <%# Convert.ToString(this.Eval("BarCodeImage") != null ? "<img src=\"" + this.Eval("BarCodeImage") + "\" border=\"0\" />" : "<img src=\"" + this.GetInstallFolder(true) + "images/admin/barcode.gif\" border=\"0\" />") %>
                                                      </td>
                                                      <td width="120" align="left" valign="top">
                                                         <%# Convert.ToString(this.Eval("ProductImage") != null ? "<img src=\"" + this.Eval("ProductImage") + "\" border=\"0\" class=\"coupon-img img-rounded img-fluid\" />" : "") %>
                                                         <p style="font-size:16px; line-height:16px; padding-top:8px;"><%# Convert.ToString(this.Eval("LabelText").ToString() != "" ? this.Eval("LabelText") : "") %></p>
                                                      </td>
                                                      <td width="28%" align="center" valign="top">
                                                         <div style="background-color:#ff0101; width:125px; height:125px; border-radius:50%; padding:30px 15px 15px; color:#fff; text-align:center">
                                                            <h2 style="margin:0"><%#
                this.FormatPrice(this.Eval("MarkOffType").ToString(), this.Eval("MarkOffPrice").ToString()) %></h2>
                                                         </div>
                                                      </td>
                                                   </tr>
                                                </tbody>
                                             </table>
                                          </td>
                                       </tr>
                                       <tr>
                                          <td colspan="2" height="30">
                                             <table width="100%">
                                                <tbody>
                                                   <tr>
                                                      <td width="100%" valign="Bottom"><span style="color: #F02828"><b>Disclaimer</b></span><b>: </b><%# Convert.ToString(this.Eval("Disclaimer").ToString() != "" ? this.Eval("Disclaimer").ToString() : "N/A") %></td>
                                                   </tr>
                                                </tbody>
                                             </table>
                                          </td>
                                       </tr>
                                    </tbody>
                                 </table>
                              </td>
                           </tr>
                        </tbody>
                     </table>
                  </div>
               </div>
               <div class="discount-card-footer">
                  <a href="javascript:void(0)" onclick="Print(this)" class="card-link">Print this Coupon</a>
               </div>
            </div>
        </itemtemplate>
    </asp:ListView>
</asp:content>