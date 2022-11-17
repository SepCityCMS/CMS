<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="realestate.aspx.cs" inherits="wwwroot.realestate1" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document)
            .ready(function () {
                restyleGridView("#NewestProperties");
                restyleGridView("#NewestPropertiesSale");
                restyleGridView("#NewestPropertiesRent");
            });
    </script>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <asp:Panel ID="SearchForm" runat="server">
        <div class="realestate-frm">
            <h3>Price Range:</h3>
            <div class="form-row">
                <div class="col-md-6">
                    <label for="validationServer01">Min Amount</label>
                    <input type="text" id="StartPrice" runat="server" class="inline-block" />                               
                </div>                               
                <div class="col-md-6">
                    <label for="validationServer01">Max Amount</label>
                    <input type="text" id="EndPrice" runat="server" class="inline-block" />                              
                </div>
            </div>
            <h3>Property Types:</h3>
            <div class="form-row">
                <div class="col-md-6">
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="PType" id="PType1" value="PType" checked="checked" />
                        <label class="form-check-label" for="PType1">Buy</label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input ignore" type="checkbox" id="PropertyType2" name="PropertyType" value="2" checked="checked" />
                        <label class="form-check-label" for="PropertyType2">Condo</label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input ignore" type="checkbox" id="PropertyType3" name="PropertyType" value="3" checked="checked" />
                        <label class="form-check-label" for="PropertyType3">House</label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input ignore" type="checkbox" id="PropertyType4" name="PropertyType" value="4" />
                        <label class="form-check-label" for="PropertyType4">Land/Lot</label>
                    </div>           
                </div>
                <div class="col-md-6">
                    <div class="form-check">
                        <input class="form-check-input ignore" type="checkbox" id="PropertyType11" name="PropertyType" value="11" />
                        <label class="form-check-label" for="PropertyType11">Commercial Land/Lot</label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input ignore" type="checkbox" id="PropertyType12" name="PropertyType" value="12" />
                        <label class="form-check-label" for="PropertyType12">Commercial Building</label>
                    </div>          
                </div>
            </div>
                        
            <hr>
            <div class="form-row">
                <div class="col-md-6">
                    <div class="form-check">
                        <input class="form-check-input" type="radio" name="PType" id="PType2" value="PType" />
                        <label class="form-check-label" for="PType2">Rent/Lease</label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input ignore" type="checkbox" id="PropertyType1" name="PropertyType" value="1" checked="checked" />
                        <label class="form-check-label" for="PropertyType1">Apartment</label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input ignore" type="checkbox" id="PropertyType33" name="PropertyType" value="3" checked="checked" />
                        <label class="form-check-label" for="PropertyType33">House</label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input ignore" type="checkbox" id="PropertyType15" name="PropertyType" value="15" />
                        <label class="form-check-label" for="PropertyType15">Furnished Room</label>
                    </div>            
                </div>
                <div class="col-md-6">
                    <div class="form-check">
                        <input class="form-check-input ignore" type="checkbox" id="PropertyType5" name="PropertyType" value="5" />
                        <label class="form-check-label" for="PropertyType5">Town House</label>
                    </div>
                    <div class="form-check">
                        <input class="form-check-input ignore" type="checkbox" id="PropertyType13" name="PropertyType" value="13" />
                        <label class="form-check-label" for="PropertyType13">Commercial Building</label>
                    </div> 
                </div>
            </div>

            <div class="form-row">
                <div class="col-md-6">
                    <select id="Beds" runat="server" class="form-control margin-btm">
                        <option value="1">Beds</option>
                        <option value="1">1+</option>
                        <option value="2">2+</option>
                        <option value="3">3+</option>
                        <option value="4">4+</option>
                        <option value="5">5+</option>
                    </select>
                </div>
                <div class="col-md-6">
                    <select id="Baths" runat="server" class="form-control margin-btm">
                        <option value="1">Baths</option>
                        <option value="1">1</option>
                        <option value="1.5">1.5</option>
                        <option value="2">2</option>
                        <option value="2.5">2.5</option>
                        <option value="3">3</option>
                    </select>
                </div>
            </div>

                        
            <hr>
            <h3>Location Search:</h3>
            <div class="form-row" id="RadiusSearching" runat="server">
                <div class="col-md-6">
                    <label for="validationServer01">Within <span ID="MilesText" runat="server" ClientIDMode="Static"></span> from</label>
			        <select id="Distance" runat="server" class="form-control">
                        <option value="5">5</option>
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                        <option value="ANY">Any</option>
                    </select>      
                </div>
                <div class="col-md-6">
                    <label for="validationServer01">Postal Code</label>
                    <input id="PostalCode" runat="server" type="text" class="form-control" placeholder=""/>        
                </div>
            </div>

            <div id="SearchCountry1" runat="server"><p><strong>Or:</strong></p></div>
                        
            <div class="form-row">
                <div class="col-md-6" id="SearchCountry2" runat="server">
                    <label>Country</label>
                    <sep:CountryDropdown ID="Country" runat="server" CssClass="form-control" ClientIDMode="Static" StateDropdownID="State" />      
                </div>
                <div class="col-md-6">
                    <label>State/Province:</label>
                    <sep:StateDropdown ID="State" runat="server" CssClass="form-control" />     
                </div>
            </div>
            <div class="form-row">
                <div class="col-md-12">
                    <asp:Button ID="SearchButton" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="SearchButton_Click" />
                </div>
            </div>
        </div>
        <br />
    </asp:Panel>

    <span class="successNotification" id="successNotification">
        <span ID="DeleteResult" runat="server"></span>
    </span>

    <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static" ShowHeader="false"
        CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageGridView_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <table class="Table" width="100%">
                        <tr class="TableHeader">
                            <td>
                                <a href="<%= this.GetInstallFolder() %>property/<%#
                this.Eval("PropertyID") %>/<%#
                this.Format_ISAPI(this.Eval("Title")) %>/" class="PropertyTitle"><%#
                this.Eval("Title") %></a>
                            </td>
                            <td align="right">
                                <span class="PropertyHits">Hits: <%#
                this.Eval("Visits") %></span>
                            </td>
                        </tr>
                        <tr class="TableBody2">
                            <td valign="top" width="90%"><%#
                this.Eval("Description") %></td>
                            <td align="right" valign="top" width="10%">
                                <a href="<%= this.GetInstallFolder() %>property/<%#
                this.Eval("PropertyID") %>/<%#
                this.Format_ISAPI(this.Eval("Title")) %>/" class="btn btn-primary" style="width: 90px">Details</a>
                                <a href="<%= this.GetInstallFolder() %>refer.aspx?PageURL=%2fproperty%2f<%#
                this.Eval("PropertyID") %>%2f<%#
                this.Format_ISAPI(this.Eval("Title")) %>%2f" class="btn btn-secondary" style="width: 90px">Refer</a>
                            </td>
                        </tr>
                    </table>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>

    <asp:GridView ID="NewestProperties" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static" ShowHeader="false"
        CssClass="GridViewStyle" AllowPaging="false" Caption="Newest Properties">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <table class="Table" width="100%">
                        <tr class="TableHeader">
                            <td>
                                <a href="<%= this.GetInstallFolder() %>property/<%#
                this.Eval("PropertyID") %>/<%#
                this.Format_ISAPI(this.Eval("Title")) %>/"
                                    class="PropertyTitle"><%#
                this.Eval("Title") %></a>
                            </td>
                            <td align="right">
                                <span class="PropertyHits">Hits: <%#
                this.Eval("Visits") %></span></td>
                        </tr>
                        <tr class="TableBody2">
                            <td valign="top" width="90%"><%#
                this.Eval("Description") %></td>
                            <td align="right" valign="top" width="10%">
                                <a href="<%= this.GetInstallFolder() %>property/<%#
                this.Eval("PropertyID") %>/<%#
                this.Format_ISAPI(this.Eval("Title")) %>/" class="btn btn-primary" style="width: 90px">Details</a>
                                <a href="<%= this.GetInstallFolder() %>refer.aspx?PageURL=%2fproperty%2f<%#
                this.Eval("PropertyID") %>%2f<%#
                this.Format_ISAPI(this.Eval("Title")) %>%2f" class="btn btn-secondary" style="width: 90px">Refer</a>
                            </td>
                        </tr>
                    </table>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:GridView ID="NewestPropertiesSale" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static" ShowHeader="false"
        CssClass="GridViewStyle" AllowPaging="false" Caption="Newest Properties for Sale">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <table class="Table" width="100%">
                        <tr class="TableHeader">
                            <td>
                                <a href="<%= this.GetInstallFolder() %>property/<%#
                this.Eval("PropertyID") %>/<%#
                this.Format_ISAPI(this.Eval("Title")) %>/"
                                    class="PropertyTitle"><%#
                this.Eval("Title") %></a>
                            </td>
                            <td align="right">
                                <span class="PropertyHits">Hits: <%#
                this.Eval("Visits") %></span></td>
                        </tr>
                        <tr class="TableBody2">
                            <td valign="top" width="90%"><%#
                this.Eval("Description") %></td>
                            <td align="right" valign="top" width="10%">
                                <a href="<%= this.GetInstallFolder() %>property/<%#
                this.Eval("PropertyID") %>/<%#
                this.Format_ISAPI(this.Eval("Title")) %>/" class="btn btn-primary" style="width: 90px">Details</a>
                                <a href="<%= this.GetInstallFolder() %>refer.aspx?PageURL=%2fproperty%2f<%#
                this.Eval("PropertyID") %>%2f<%#
                this.Format_ISAPI(this.Eval("Title")) %>%2f" class="btn btn-secondary" style="width: 90px">Refer</a>
                            </td>
                        </tr>
                    </table>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:GridView ID="NewestPropertiesRent" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static" ShowHeader="false"
        CssClass="GridViewStyle" AllowPaging="false" Caption="Newest Properties for Rent">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                <ItemTemplate>
                    <table class="Table" width="100%">
                        <tr class="TableHeader">
                            <td>
                                <a href="<%= this.GetInstallFolder() %>property/<%#
                this.Eval("PropertyID") %>/<%#
                this.Format_ISAPI(this.Eval("Title")) %>/"
                                    class="PropertyTitle"><%#
                this.Eval("Title") %></a>
                            </td>
                            <td align="right">
                                <span class="PropertyHits">Hits: <%#
                this.Eval("Visits") %></span></td>
                        </tr>
                        <tr class="TableBody2">
                            <td valign="top" width="90%"><%#
                this.Eval("Description") %></td>
                            <td align="right" valign="top" width="10%">
                                <a href="<%= this.GetInstallFolder() %>property/<%#
                this.Eval("PropertyID") %>/<%#
                this.Format_ISAPI(this.Eval("Title")) %>/" class="btn btn-primary" style="width: 90px">Details</a>
                                <a href="<%= this.GetInstallFolder() %>refer.aspx?PageURL=%2fproperty%2f<%#
                this.Eval("PropertyID") %>%2f<%#
                this.Format_ISAPI(this.Eval("Title")) %>%2f" class="btn btn-secondary" style="width: 90px">Refer</a>
                            </td>
                        </tr>
                    </table>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:content>