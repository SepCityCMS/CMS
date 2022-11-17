<%@ page title="Speaker Bureau" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="speakerbureau_topics_modify.aspx.cs" inherits="wwwroot.speakerbureau_topics_modify" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <script src="../js/gridview.js" type="text/javascript"></script>
    <script src="../js/management.js" type="text/javascript"></script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 50;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontentsave">

            <div class="ModFormDiv" id="ModFormDiv" runat="server">

                <h4 id="ModifyLegend" runat="server">Add Topic</h4>
                <input type="hidden" runat="server" ID="TopicID" />
                <div class="mb-3">
                    <label ID="TopicNameLabel" clientidmode="Static" runat="server" for="TopicName">Topic Name:</label>
                    <input type="text" ID="TopicName" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="TopicNameRequired" runat="server" ControlToValidate="TopicName"
                                            ClientValidationFunction="customFormValidator" ErrorMessage="Topic Name is required."
                                            ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
            </div>
            <div class="button-to-bottom">
		        <button class="btn btn-primary" ID="SaveButton" runat="server" OnServerClick="SaveButton_Click">Save</button>
		        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	        </div>
        </div>

        <div id="ManageSpeeches" runat="server" style="margin-top: 200px;">
            <h2>
                <span ID="PageHeader" runat="server" Text="Manage the Speeches"></span>
            </h2>

            <span class="successNotification" id="successNotification">
                <span ID="DeleteResult" runat="server"></span>
            </span>

            <div class="panel panel-default" id="PageManageGridView" runat="server">
			    <div class="panel-heading">
				    <div class="mb-3">
					    <div class="col-lg-6">
						    <div class="input-group">
							    <select id="FilterDoAction" runat="server" class="form-control" ClientIDMode="Static">
								    <option value="">Select an Action</option>
								    <option value="DeleteSpeeches">Delete Speeches</option>
                                </select>
							    <span class="input-group-btn">
							        <div class="mb-3"><asp:Button ID="RunAction" runat="server" CssClass="btn btn-default" Text="Go!" onclick="RunAction_Click" OnClientClick="return ExecuteAction(this, 'SpeechID')" /></div>
						        </span>
					        </div>
				        </div>
				        <div class="col-lg-6">
				        </div>
			        </div>
		        </div>

                <input type="hidden" ID="UniqueIDs" runat="server" ClientIDMode="Static" Value="" />

                <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                CssClass="GridViewStyle" AllowPaging="false">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="20px">
                            <HeaderTemplate>
                                <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input type="checkbox" id="SpeechID<%#
                Eval("SpeechID").ToString()%>" value="<%#
                Eval("SpeechID").ToString()%>" onclick="gridviewSelectRow(this);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Subject">
                            <ItemTemplate>
                                <%#
                Eval("Subject").ToString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <br />

            <div class="col-md-12" id="AddSpeechForm" runat="server">

                <h4 id="SpeechLegend" runat="server">Add Speech</h4>
                <input type="hidden" runat="server" ID="SpeechID" />
                <div class="col-md-12">
                    <label ID="SubjectLabel" clientidmode="Static" runat="server" for="Subject">Subject:</label>
                    <input type="text" ID="Subject" runat="server"  class="form-control" />
                    <asp:CustomValidator ID="SubjectRequired" runat="server" ControlToValidate="Subject"
                                            ClientValidationFunction="customFormValidator" ErrorMessage="Subject is required."
                                            ValidateEmptyText="true" Display="Dynamic">
                    </asp:CustomValidator>
                </div>
                <div class="col-md-12">
                    <label ID="SpeakerIDLabel" clientidmode="Static" runat="server" for="Subject">Speaker Name:</label>
                    <select ID="SpeakerID" runat="server" class="form-control" />
                </div>
            </div>
            <div class="col-md-12"><asp:Button CssClass="btn btn-secondary" ID="SaveSpeechButton" runat="server" Text="Save" onclick="SaveSpeechButton_Click" /></div>
        </div>
    </asp:Panel>
</asp:content>