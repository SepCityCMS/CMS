<%@ page title="Checkout" language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="viewcart_checkout.aspx.cs" inherits="wwwroot.viewcart_checkout" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        function changePaymentMethod() {
            if (document.getElementById("PaymentMethod").value == "" ||
                document.getElementById("PaymentMethod").value == "Check" ||
                document.getElementById("PaymentMethod").value == "PayPal") {
                document.getElementById("CardTypeRow").style.display = "none";
                document.getElementById("CardNumRow").style.display = "none";
                document.getElementById("CardNameRow").style.display = "none";
                document.getElementById("ExpireDateRow").style.display = "none";
                document.getElementById("SecurityCodeRow").style.display = "none";
            } else {
                document.getElementById("CardTypeRow").style.display = "";
                document.getElementById("CardNumRow").style.display = "";
                document.getElementById("CardNameRow").style.display = "";
                document.getElementById("ExpireDateRow").style.display = "";
                document.getElementById("SecurityCodeRow").style.display = "";
            }
        }
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span id="failureNotification">
        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <div class="ModFormDiv" id="OrderDiv" runat="server">
        <h2>Payment Information</h2>

        <input type="hidden" name="hdnToken" id="hdnToken" runat="server" clientidmode="Static" />

            <div class="mb-3" id="PaymentMethodRow" runat="server">
                <label ID="PaymentMethodLabel" clientidmode="Static" runat="server" for="PaymentMethod">Payment Method:</label>
                <asp:DropDownList ID="PaymentMethod" runat="server" CssClass="form-control" AutoPostBack="True" clientidmode="Static" EnableViewState="True" OnSelectedIndexChanged="PaymentMethod_SelectedIndexChanged">
                    <asp:ListItem Text="--- Make Your Selection ---" Value="" />
                </asp:DropDownList>
            </div>
            <div class="mb-3" id="CardTypeRow" runat="server">
                <label ID="CardTypeLabel" clientidmode="Static" runat="server" for="CardType">Credit / Debit Card Type:</label>
                <select ID="CardType" runat="server" class="form-control">
                    <option value="Visa">Visa</option>
                    <option value="MasterCard">MasterCard</option>
                    <option value="AmericanExpress">American Express</option>
                    <option value="Discover">Discover</option>
                </select>
            </div>
            <div class="mb-3" id="CardNumRow" runat="server">
                <label ID="CardNumberLabel" clientidmode="Static" runat="server" for="CardNumber">Credit / Debit Card Number:</label>
                <input type="text" ID="CardNumber" runat="server" class="form-control" MaxLength="50" ClientIDMode="Static" />
            </div>
            <div class="mb-3" id="CardNameRow" runat="server">
                <label ID="NameOnCardLabel" clientidmode="Static" runat="server" for="NameOnCard">Name on Card:</label>
                <input type="text" ID="NameOnCard" runat="server" class="form-control" MaxLength="50" ClientIDMode="Static" />
            </div>
            <div class="mb-3" id="ExpireDateRow" runat="server">
                <label ID="ExpireDateLabel" clientidmode="Static" runat="server" for="ExpireMonth">Expiration Date:</label>
                <br />
                <select ID="ExpireMonth" runat="server" class="form-control inline-block" clientidmode="Static" style="width:20%"></select>
                <select ID="ExpireYear" runat="server" class="form-control inline-block" clientidmode="Static" style="width:20%"></select>
            </div>
            <div class="mb-3" id="SecurityCodeRow" runat="server">
                <label ID="SecurityCodeLabel" clientidmode="Static" runat="server" for="SecurityCode">Security Code:</label>
                <input type="text" ID="SecurityCode" runat="server"  class="form-control" MaxLength="50" ClientIDMode="Static" />
            </div>
            <div class="mb-3">
                <label ID="CommentsLabel" clientidmode="Static" runat="server" for="Comments">Order Comments: (Optional)</label>
                <textarea ID="Comments" runat="server"  class="form-control"></textarea>
            </div>

        <h2>My Shopping Cart</h2>

        <div class="GridViewStyle">
            <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                          CssClass="GridViewStyle" ShowHeaderWhenEmpty="true" onrowdatabound="ManageGridView_RowDataBound" DataKeyNames="StoreID">
                <Columns>
                    <asp:TemplateField HeaderText="Item Details" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <h3><%#Eval("StoreName")%></h3>
                            <asp:Repeater ID="rtProducts" runat="server">
                                <ItemTemplate>
                                    <%#Eval("ProductName")%>
                                    <br />
                                    <%#Eval("UnitPrice")%>
                                    <%#
                                        Convert.ToString(Format_Currency(DataBinder.Eval(Container.DataItem, "Handling")) != Format_Currency("0") ? "<br />Plus " + DataBinder.Eval(Container.DataItem, "Handling") + " handling fee" : "")
                                    %>
                                    <br />
                                    Quantity: <%#Eval("Quantity")%>
                                    <br />
                                    Total Price: <%#Eval("TotalPrice")%>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shipping Method" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <asp:RadioButtonList ID="rbMethods" runat="server">
                            </asp:RadioButtonList>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <br />

        <hr class="mb-4" />
            <div class="mb-3"><asp:Button CssClass="btn btn-primary" ID="OrderButton" runat="server" Text="Place Order" onclick="OrderButton_Click" clientidmode="Static" /></div>
    </div>
</asp:content>