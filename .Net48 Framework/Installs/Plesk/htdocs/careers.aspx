<%@ page language="C#" viewstatemode="Enabled" masterpagefile="~/skins/template.master"
    codebehind="careers.aspx.cs" inherits="wwwroot.careers" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
</asp:content>

<asp:content id="BodyContent" contentplaceholderid="SiteContent" runat="Server">

    <span ID="PageText" runat="server"></span>

    <div class="ModFormDiv" id="ModFormDiv" runat="server">

        <h4 id="ModifyLegend" runat="server">Search for a Job</h4>
        <div style="float: left; width: 50%;">
            <div class="mb-3">
                <label id="JobTitleLabel" clientidmode="Static" runat="server" for="JobTitle">Job Title:</label>
                <input type="text" id="JobTitle" runat="server" clientidmode="Static" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="KeywordsLabel" clientidmode="Static" runat="server" for="Keywords">Keywords:</label>
                <input type="text" id="Keywords" runat="server" clientidmode="Static" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="LocationLabel" clientidmode="Static" runat="server" for="Location">City, State or Zip/Postal Code:</label>
                <input type="text" id="Location" runat="server" clientidmode="Static" class="form-control" />
            </div>
        </div>
        <div style="float: left; width: 50%;">
            <div class="mb-3">
                <label id="JobCategoryLabel" clientidmode="Static" runat="server" for="JobCategory">Job Category:</label>
                <select id="JobCategory" runat="server" class="form-control" clientidmode="Static">
                    <option value="">Any</option>
                    <option value="Accounting">Accounting</option>
                    <option value="Admin - Clerical">Admin - Clerical</option>
                    <option value="Automotive">Automotive</option>
                    <option value="Banking">Banking</option>
                    <option value="Biotech">Biotech</option>
                    <option value="Business Development">Business Development</option>
                    <option value="Business Opportunity">Business Opportunity</option>
                    <option value="Construction">Construction</option>
                    <option value="Consultant">Consultant</option>
                    <option value="Customer Service">Customer Service</option>
                    <option value="Design">Design</option>
                    <option value="Distribution - Shipping">Distribution - Shipping</option>
                    <option value="Education">Education</option>
                    <option value="Engineering">Engineering</option>
                    <option value="Entry Level">Entry Level</option>
                    <option value="Executive">Executive</option>
                    <option value="Facilities">Facilities</option>
                    <option value="Finance">Finance</option>
                    <option value="Franchise">Franchise</option>
                    <option value="General Business">General Business</option>
                    <option value="General Labor">General Labor</option>
                    <option value="Government">Government</option>
                    <option value="Government - Federal">Government - Federal</option>
                    <option value="Grocery">Grocery</option>
                    <option value="Health Care">Health Care</option>
                    <option value="Hospitality - Hotel">Hospitality - Hotel</option>
                    <option value="Human Resources">Human Resources</option>
                    <option value="Information Technology">Information Technology</option>
                    <option value="Installation - Maint - Repair">Installation - Maint - Repair</option>
                    <option value="Insurance">Insurance</option>
                    <option value="Inventory">Inventory</option>
                    <option value="Legal">Legal</option>
                    <option value="Legal Admin">Legal Admin</option>
                    <option value="Management">Management</option>
                    <option value="Manufacturing">Manufacturing</option>
                    <option value="Marketing">Marketing</option>
                    <option value="Media - Journalism - Newspaper">Media - Journalism - Newspaper</option>
                    <option value="Nonprofit - Social Services">Nonprofit - Social Services</option>
                    <option value="Nurse">Nurse</option>
                    <option value="Other">Other</option>
                    <option value="Pharmaceutical">Pharmaceutical</option>
                    <option value="Professional Services">Professional Services</option>
                    <option value="Purchasing - Procurement">Purchasing - Procurement</option>
                    <option value="QA - Quality Control">QA - Quality Control</option>
                    <option value="Real Estate">Real Estate</option>
                    <option value="Research">Research</option>
                    <option value="Restaurant - Food Service">Restaurant - Food Service</option>
                    <option value="Retail">Retail</option>
                    <option value="Sales">Sales</option>
                    <option value="Science">Science</option>
                    <option value="Skilled Labor - Trades">Skilled Labor - Trades</option>
                    <option value="Strategy - Planning">Strategy - Planning</option>
                    <option value="Supply Chain">Supply Chain</option>
                    <option value="Telecommunications">Telecommunications</option>
                    <option value="Training">Training</option>
                    <option value="Transportation">Transportation</option>
                    <option value="Veterinary Services">Veterinary Services</option>
                    <option value="Warehouse">Warehouse</option>
                </select>
            </div>
            <div class="mb-3">
                <label id="CompanyNameLabel" clientidmode="Static" runat="server" for="CompanyName">Company Name:</label>
                <input type="text" id="CompanyName" runat="server" clientidmode="Static" class="form-control" />
            </div>
            <div class="mb-3">
                <label id="EmploymentTypeLabel" clientidmode="Static" runat="server" for="EmploymentType">Employment Type:</label>
                <select id="EmploymentType" runat="server" class="form-control" clientidmode="Static">
                    <option value="">Any</option>
                    <option value="FTP">Full Time</option>
                    <option value="PTP">Part time</option>
                    <option value="FTT">Full Time (Temporary)</option>
                    <option value="PTT">Part time (Temporary)</option>
                    <option value="CFT">Contractor (Full Time)</option>
                    <option value="CFT">Contractor (Part Time)</option>
                </select>
            </div>
        </div>

        <hr class="mb-4" />
        <div class="mb-3">
            <asp:Button CssClass="btn btn-primary" ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
        </div>
    </div>

    <span id="failureNotification">
        <span id="ErrorMessage" runat="server" clientidmode="Static"></span>
    </span>

    <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="ListContent_PageIndexChanging" AllowSorting="False" ClientIDMode="Static"
        CssClass="GridViewStyle" Caption="Position Search Results" AllowCustomPaging="true">
        <Columns>
        </Columns>
        <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>
</asp:content>