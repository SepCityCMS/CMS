﻿<%@ page title="Forms" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="forms_questions.aspx.cs" inherits="wwwroot.forms_questions" %>

<%@ register tagprefix="sep" namespace="SepControls" assembly="SepControls" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        input[type=submit]:disabled, button:disabled {
            display: none;
        }
    </style>
    <script src="../js/gridview.js" type="text/javascript"></script>
    <script src="../js/management.js" type="text/javascript"></script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 13;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <h2>
            <span ID="PageHeader" runat="server" Text="Manage the Form Questions"></span>
        </h2>

            <div class="panel panel-default">
                <div class="panel-body">
                    <ul class="nav nav-pills">
                        <span id="FormSections" runat="server"></span>
                    </ul>
                </div>
            </div>

            <div class="panel-body">
                <span class="successNotification" id="successNotification">
                    <span ID="DeleteResult" runat="server"></span>
                </span>

                <div class="panel panel-default" id="PageManageGridView" runat="server">
			        <div class="panel-heading">
				        <div class="row">
					        <div class="col-lg-6">
						        <div class="input-group">
							        <select id="FilterDoAction" runat="server" class="form-control" ClientIDMode="Static">
								        <option value="">Select an Action</option>
									        <option value="DeleteForms">Delete Questions</option>
                            </select>
							        <span class="input-group-btn">
							        <button class="btn btn-light" ID="RunAction" runat="server" OnServerClick="RunAction_Click" onclick="if(ExecuteAction(this, 'QuestionID') == false) {return false} else">Go!</button>
						        </span>
					        </div>
				        </div>
				        <div class="col-lg-6">
					        <div class="input-group">
						        <input type="text" ID="ModuleSearch" runat="server" placeholder="Search for..." onKeyPress="if(submitSearch(event) == false){document.getElementById('ModuleSearchButton').click();return submitSearch(event);}"  class="form-control" />
						        <span class="input-group-btn">
							        <button class="btn btn-light" ID="ModuleSearchButton" runat="server" clientidmode="static" OnServerClick="ModuleSearchButton_Click">Go!</button>
						        </span>
					        </div>
				        </div>
			        </div>
		        </div>

                    <input type="hidden" ID="UniqueIDs" runat="server" ClientIDMode="Static" Value="" />

                    <asp:GridView ID="ManageGridView" runat="server" AutoGenerateColumns="False" AllowSorting="True" ClientIDMode="Static"
                                  CssClass="GridViewStyle" AllowPaging="true" OnPageIndexChanging="ManageGridView_PageIndexChanging" PageSize="20" PagerSettings-Mode="NumericFirstLast"
                                  EnableViewState="True" OnRowCommand="ManageGridView_RowCommand">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="20px">
                                <HeaderTemplate>
                                    <input type="checkbox" id="checkAll" onclick="gridviewCheckAll(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input type="checkbox" id="QuestionID<%#
                        Eval("QuestionID").ToString()%>" value="<%#
                        Eval("QuestionID").ToString()%>" onclick="gridviewSelectRow(this);" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:templatefield ItemStyle-Width="20px">
                                <itemtemplate>
                                    <a href="forms_questions_modify.aspx?FormID=<%#
                        Eval("FormID").ToString()%>&QuestionID=<%#
                        Eval("QuestionID").ToString()%>">
                                        <img src="../images/public/edit.png" alt="Edit" />
                                    </a>
                                </itemtemplate>
                            </asp:templatefield>
                            <asp:TemplateField HeaderText="Question" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Question">
                                <ItemTemplate>
                                    <%#
                        Eval("Question").ToString()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Question Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="TypeID">
                                <ItemTemplate>
                                    <%#
                        GetAnswerType(Eval("TypeID").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Required" HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" SortExpression="Mandatory">
                                <ItemTemplate>
                                    <%#
                        Eval("Mandatory").ToString()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <p align="center">
                                        <asp:ImageButton ID="btnUp" runat="Server" ImageUrl="images/moveup.png" CommandName="MoveUp" CommandArgument='<%#
                        Eval("QuestionID").ToString() + "||" + Eval("RowNumber").ToString() + "||" + Eval("FormID").ToString() + "||" + Eval("SectionID").ToString()%>' />
                                        <asp:ImageButton ID="btnDown" runat="Server" ImageUrl="images/movedown.png" CommandName="MoveDown" CommandArgument='<%#
                        Eval("QuestionID").ToString() + "||" + Eval("RowNumber").ToString() + "||" + Eval("FormID").ToString() + "||" + Eval("SectionID").ToString()%>' />
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pagination-ys" />
                    </asp:GridView>
                </div>
			</div>
            </div>
    </asp:Panel>
</asp:content>