<%@ page title="Custom Reports" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="custom_reports_modify.aspx.cs" inherits="wwwroot.custom_reports_modify" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script type="text/javascript">
        function openHelp() {
            if (document.getElementById('fieldHelp').style.display == '') {
                document.getElementById('fieldHelp').style.display = 'none';
            } else {
                document.getElementById('fieldHelp').style.display = '';
            }
        }

        function showFields(objField) {
            if (document.getElementById('FieldFilterDiv') == null) {
                try {
                    var searchIFrame =
                        '<iframe style="width:100%; height: 310px;" id="UserSearchFrame" src="filter-sqlfields.aspx?PopulateField=' + objField + '&SQLStatement=' + encodeURIComponent(document.getElementById("SQLStatement").value) + '" frameborder="0" />';
                    var $searchDiv = $('<div style="display:none;" title="Selection" id="FieldFilterDiv">' +
                        searchIFrame +
                        '</div>');

                    $('body').append($searchDiv);
                    openDialog('FieldFilterDiv', 270, 400);
                } catch (e) {
                    alert('You must enter a valid SQL Statement.')
                }
            } else {
                $('#FieldFilterDiv').remove();
            }
        }
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 982;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

        <div class="ModFormDiv">

                <h4 id="ModifyLegend" runat="server">Add Custom Report</h4>
                <input type="hidden" runat="server" ID="ReportID" />

                <div class="mb-3">
                    <label ID="ReportTitleLabel" clientidmode="Static" runat="server" for="ReportTitle">Report Title:</label>
                    <input type="text" ID="ReportTitle" runat="server"  class="form-control" MaxLength="100" />
                    <asp:CustomValidator ID="FieldNameRequired" runat="server" ControlToValidate="ReportTitle"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="Report Title is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <label ID="SQLStatementLabel" clientidmode="Static" runat="server" for="SQLStatement">SQL Statement:</label>
                    <textarea ID="SQLStatement" runat="server"  class="form-control" clientidmode="static"></textarea>
                    <asp:CustomValidator ID="SQLStatementRequired" runat="server" ControlToValidate="SQLStatement"
                                         ClientValidationFunction="customFormValidator" ErrorMessage="SQL Statement is required."
                                         ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                    <br />
                    Predifined Reports <select name="Predefined" onchange="document.getElementById('SQLStatement').value = this.value;" class="form-control">
                        <option value="">Select a Predefined Report</option>
                        <option value="SELECT * FROM Members WHERE Status <> '-1' ORDER BY Username">List All Members</option>
                        <option value="SELECT IP.* FROM Invoices AS INV,Invoices_Products AS IP WHERE INV.InvoiceID=IP.InvoiceID AND INV.inCart='0' AND INV.Status > '0' ORDER BY IP.ProductName">All Paid Orders</option>
                        <option value="SELECT *,* FROM Members,Profiles WHERE Profiles.UserID=Members.UserID AND Members.Status <> '-1' ORDER BY Members.Username">All User Profiles</option>
                        <option value="SELECT * FROM Advertisements WHERE Status <> '-1' ORDER BY UserID">All Advertisements</option>
                        <option value="SELECT M2.Username AS Username,M2.EmailAddress,M2.FirstName,M2.LastName,M1.Username AS ReferredBy FROM Members AS M1,Members AS M2 WHERE M1.AffiliateID=M2.ReferralID AND M1.Status <> '-1' ORDER BY M1.Username">Affiliate Referrals</option>
                        <option value="SELECT TOP 100 M.* FROM Members AS M,Activities AS ACT WHERE ACT.UserID=M.UserID AND M.Status <> '-1' ORDER BY ACT.DatePosted DESC">Last 100 Deleted Items</option>
                        <option value="SELECT TOP 100 * FROM Members WHERE Status <> '-1' ORDER BY LastLogin DESC">Last 100 Logins</option>
                        <option value="SELECT TOP 100 * FROM Members WHERE Status <> '-1' ORDER BY CreateDate DESC">Last 100 Signups</option>
                        <option value="SELECT * FROM Members WHERE Status='-1' ORDER BY Username">All Deleted Members</option>
                        <option value="SELECT * FROM Members WHERE UserPoints > '0' AND Status <> '-1' ORDER BY UserPoints DESC,Username">All Members /w Points</option>
                        <option value="SELECT * FROM ReferralAddresses ORDER BY FromEmailAddress">Referral Email Addresses (Refer a Friend)</option>
                    </select> <a href="javascript:openHelp();">SQL Functions Help</a>
                    <br />
                    <span id="fieldHelp" style="display: none; width: 100%;">
                        <b>{NonPaidSales}</b> : Calculate the total amount in the shopping cart that the user has not paid for.<br />
                        <b>Requirements:</b><br />
                        Select the UserID field from the members table.
                        <br /><br />
                        <b>{PaidSales}</b> : Calculate the total sales a user has paid for on your website.<br />
                        <b>Requirements:</b><br />
                        Select the UserID field from the members table.
                        <br /><br />
                        <b>{TotalLogins}</b> : Returns the total number of times a user has logged into your website.<br />
                        <b>Requirements:</b><br />
                        Select the UserID field from the members table.
                        <br /><br />
                        <b>{TotalActivities}</b> : Returns a number with how many activities a user has in the database.<br />
                        <b>Requirements:</b><br />
                        Select the UserID field from the members table.
                    </span>
                </div>

                <table width="480" class="Table" cellpadding="0" cellspacing="1">
                <tr class="TableTitle">
                    <td align="center" colspan="6">
                        <b>Report Header Rows</b>
                    </td>
                </tr><tr class="TableHeader">
                    <td align="center">
                        <b>Header 1</b>
                    </td>
                    <td align="center">
                        <b>Header 2</b>
                    </td>
                    <td align="center">
                        <b>Header 3</b>
                    </td>
                    <td align="center">
                        <b>Header 4</b>
                    </td>
                    <td align="center">
                        <b>Header 5</b>
                    </td>
                    <td align="center">
                        <b>Header 6</b>
                    </td>
                </tr><tr class="TableBody1">
                    <td>
                        <input type="text" id="Head1" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head2" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head3" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head4" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head5" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head6" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                </tr><tr class="TableHeader">
                    <td align="center">
                        <b>Header 7</b>
                    </td>
                    <td align="center">
                        <b>Header 8</b>
                    </td>
                    <td align="center">
                        <b>Header 9</b>
                    </td>
                    <td align="center">
                        <b>Header 10</b>
                    </td>
                    <td align="center">
                        <b>Header 11</b>
                    </td>
                    <td align="center">
                        <b>Header 12</b>
                    </td>
                </tr><tr class="TableBody1">
                    <td>
                        <input type="text" id="Head7" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head8" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head9" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head10" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head11" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head12" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                </tr><tr class="TableHeader">
                    <td align="center">
                        <b>Header 13</b>
                    </td>
                    <td align="center">
                        <b>Header 14</b>
                    </td>
                    <td align="center">
                        <b>Header 15</b>
                    </td>
                    <td align="center">
                        <b>Header 16</b>
                    </td>
                    <td align="center">
                        <b>Header 17</b>
                    </td>
                    <td align="center">
                        <b>Header 18</b>
                    </td>
                </tr><tr class="TableBody1">
                    <td>
                        <input type="text" id="Head13" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head14" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head15" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head16" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head17" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Head18" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                </tr><tr class="TableTitle">
                    <td align="center" colspan="6">
                        <b>Report Body Rows</b>
                    </td>
                </tr><tr class="TableHeader">
                    <td align="center">
                        <b>Body 1</b>
                    </td>
                    <td align="center">
                        <b>Body 2</b>
                    </td>
                    <td align="center">
                        <b>Body 3</b>
                    </td>
                    <td align="center">
                        <b>Body 4</b>
                    </td>
                    <td align="center">
                        <b>Body 5</b>
                    </td>
                    <td align="center">
                        <b>Body 6</b>
                    </td>
                </tr><tr class="TableBody1">
                    <td>
                        <input type="text" id="Body1" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body2" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body3" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body4" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body5" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body6" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                </tr><tr class="TableBody1">
                    <td>
                        <button class="btn btn-light" ID="Change1" onclick="showFields('1');return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change2" onclick="showFields('2');return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change3" onclick="showFields('3');return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change4" onclick="showFields('4');return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change5" onclick="showFields('5');return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change6" onclick="showFields('6');return false;">Change</button>
                    </td>
                </tr><tr class="TableHeader">
                    <td align="center">
                        <b>Body 7</b>
                    </td>
                    <td align="center">
                        <b>Body 8</b>
                    </td>
                    <td align="center">
                        <b>Body 9</b>
                    </td>
                    <td align="center">
                        <b>Body 10</b>
                    </td>
                    <td align="center">
                        <b>Body 11</b>
                    </td>
                    <td align="center">
                        <b>Body 12</b>
                    </td>
                </tr><tr class="TableBody1">
                    <td>
                        <input type="text" id="Body7" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body8" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body9" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body10" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body11" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body12" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                </tr><tr class="TableBody1">
                    <td>
                        <button class="btn btn-light" ID="Change7" onclick="showFields('7');return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change8" onclick="showFields('8');
                return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change9" onclick="showFields('9');return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change10" onclick="showFields('10');return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change11" onclick="showFields('11');return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change12" onclick="showFields('12');return false;">Change</button>
                    </td>
                </tr><tr class="TableHeader">
                    <td align="center">
                        <b>Body 13</b>
                    </td>
                    <td align="center">
                        <b>Body 14</b>
                    </td>
                    <td align="center">
                        <b>Body 15</b>
                    </td>
                    <td align="center">
                        <b>Body 16</b>
                    </td>
                    <td align="center">
                        <b>Body 17</b>
                    </td>
                    <td align="center">
                        <b>Body 18</b>
                    </td>
                </tr><tr class="TableBody1">
                    <td>
                        <input type="text" id="Body13" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body14" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body15" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body16" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body17" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                    <td>
                        <input type="text" id="Body18" runat="server" class="form-control" style="width:80px;" ClientIDMode="Static" />
                    </td>
                </tr><tr class="TableBody1">
                    <td>
                        <button class="btn btn-light" ID="Change13" onclick="showFields('13');return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change14" onclick="showFields('14');return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change15" onclick="showFields('15');return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change16" onclick="showFields('16');return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change17" onclick="showFields('17');return false;">Change</button>
                    </td>
                    <td>
                        <button class="btn btn-light" ID="Change18" onclick="showFields('18');return false;">Change</button>
                    </td>
                </tr>
                </table>
            </div>
                <div class="button-to-bottom">
                    <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
                    <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
                </div>
        </div>
    </asp:Panel>
</asp:content>