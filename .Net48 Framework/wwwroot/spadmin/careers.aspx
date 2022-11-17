<%@ page title="Careers" language="C#" viewstatemode="Enabled" masterpagefile="Site.Master"
    codebehind="careers.aspx.cs" inherits="wwwroot.spadmin.careers" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .select-box {
            display: inline-block;
            height: 250px;
            margin-left: 15px;
            vertical-align: top;
            width: 300px;
        }

        .select-overflow {
            border: 1px solid #ddd;
            height: 250px;
            overflow-x: hidden;
            overflow-y: auto;
            width: 300px;
        }

        ul.selector {
            width: 280px;
        }

            ul.selector li {
                cursor: pointer;
            }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("ul.selector").on('click', 'li', function (e) {
                if (e.ctrlKey || e.metaKey) {
                    $(this).toggleClass("selected");
                } else {
                    $(this).addClass("selected").siblings().removeClass('selected');
                }
            }).sortable({
                connectWith: "ul",
                delay: 150, //Needed to prevent accidental drag when trying to select
                revert: 0,
                helper: function (e, item) {
                    //Basically, if you grab an unhighlighted item to drag, it will deselect (unhighlight) everything else
                    if (!item.hasClass('selected')) {
                        item.addClass('selected').siblings().removeClass('selected');
                    }

                    //////////////////////////////////////////////////////////////////////
                    //HERE'S HOW TO PASS THE SELECTED ITEMS TO THE `stop()` FUNCTION:

                    //Clone the selected items into an array
                    var elements = item.parent().children('.selected').clone();

                    //Add a property to `item` called 'multidrag` that contains the
                    //  selected items, then remove the selected items from the source list
                    item.data('multidrag', elements).siblings('.selected').remove();

                    //Now the selected items exist in memory, attached to the `item`,
                    //  so we can access them later when we get to the `stop()` callback

                    //Create the helper
                    var helper = $('<li />');
                    return helper.append(elements);
                },
                stop: function (e, ui) {
                    //Now we access those items that we stored in `item`s data!
                    var elements = ui.item.data('multidrag');

                    //`elements` now contains the originally selected items from the source list (the dragged items)!!

                    //Finally I insert the selected items after the `item`, then remove the `item`, since
                    //  item is a duplicate of one of the selected items.
                    ui.item.after(elements).remove();
                }

            });
        });

        function BuildFieldList() {
            var sSelected = "";
            $(".selectedfieldsDiv ul li").each(function () {
                sSelected += "||" + $(this).attr('data-value') + "||";
            });
            $('#SelectedFIeldValues').val(sSelected);
        }
    </script>
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">

    <asp:Panel ID="UpdatePanel" runat="server">

        <% 
            var cAdminModuleMenu = new SepCityControls.AdminModuleMenu();
            cAdminModuleMenu.ModuleID = 66;
            Response.Write(cAdminModuleMenu.Render()); 
        %>

		<div class="col-md-12 pagecontent">

        <h2>
            <span ID="PageHeader" runat="server" Text="Candidate Detail Fields"></span>
        </h2>

        <p>Drag and drop the fields below with your mouse to order the items or make them available to the user.</p>

        <input type="hidden" runat="server" ID="SelectedFIeldValues" ClientIDMode="Static" />
        <input type="hidden" runat="server" ID="Customize" ClientIDMode="Static" />

        <div class="ModFormDiv">
            <div class="mb-3" style="margin-left:20px;margin-right:0px;">
                <div class="select-box">
                    <strong>Selected Fields</strong>
                    <div class="select-overflow selectedfieldsDiv">
                        <ul class="selector list-group">
                            <span id="selectedFields" runat="server"></span>
                        </ul>
                    </div>
                </div>
                <div class="select-box">
                    <strong>Available Fields</strong>
                    <div class="select-overflow availablefieldsDiv">
                        <ul class="selector list-group">
                            <span id="availableFields" runat="server"></span>
                        </ul>
                    </div>
                </div>
            </div>
            </div>
            <div class="button-to-bottom">
                <asp:Button ID="SaveButton" cssclass="btn btn-primary" runat="server" Text="Save" clientidmode="static" onclientclick="BuildFieldList()" onclick="SaveButton_Click" />
		        <span ID="ErrorMessage" runat="server" clientidmode="Static"></span>
	        </div>
        </div>
    </asp:Panel>
</asp:content>