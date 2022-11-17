<%@ page language="C#" viewstatemode="Enabled" codebehind="shoppingmall_custom_field_modify.aspx.cs" inherits="wwwroot.shoppingmall_custom_field_modify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" integrity="sha384-zCbKRCUGaJDkqS1kPbPd7TveP5iyJE0EjAuZQTgFLD2ylzuqKfdKlfG/eSrtxUkn" crossorigin="anonymous" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-fQybjgWLrvvRgtW6bFlB7jaZrFsaBXjsOMm/tB9LTS58ONXgqbR9W8oWht/amnpF" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-json/2.6.0/jquery.json.min.js" integrity="sha256-Ac6pM19lP690qI07nePO/yY6Ut3c7KM9AgNdnc5LtrI=" crossorigin="anonymous"></script>
    
    <script src="../js/bootbox.min.js" type="text/javascript"></script>
    <script src="../js/main.js" type="text/javascript"></script>
    <style type="text/css">
        .ModFieldset {
            width: 350px;
        }
    </style>
    <script type="text/javascript">
        function applyField() {
            var iRowOffset = <%= SepCommon.SepCore.Request.Item("RowOffset") %>;
            var newRowOffset = parseInt(iRowOffset);
            var fieldId = $("#FieldID").val();
            var row = document.createElement("tr");
            var tdFieldName = document.createElement("td");
            var tdAnswerType = document.createElement("td");
            var tdDelete = document.createElement("td");

            if ($('#FieldMode').val() == 'edit') {
                tdFieldName.innerHTML = '<input type=\"hidden\" name=\"FieldID' +
                    fieldId +
                    '\" id=\"FieldID' +
                    $('#FieldID').val() +
                    '\" value=\"' +
                    $('#FieldID').val() +
                    '\" /><a href=\"javascript:openCustomField(\'' +
                    newRowOffset +
                    '\', \'' +
                    fieldId +
                    '\')\">' +
                    $('#FieldName').val() +
                    '</a>';
                tdAnswerType.innerHTML = $('#AnswerType').val();
                tdDelete.innerHTML = '<a href=\"javascript:deleteField(\'' +
                    newRowOffset +
                    '\', \'' +
                    fieldId +
                    '\')\">Delete</a>';

                row.appendChild(tdFieldName);
                row.appendChild(tdAnswerType);
                row.appendChild(tdDelete);

                parent.$("#CustomFields tr").eq(newRowOffset).replaceWith(row);

                var fieldIndex = 0;
                var array = parent.$('#FieldIDs').val().split("|%|");
                $.each(array,
                    function (i) {
                        if (fieldId == array[i]) {
                            fieldIndex = i;
                        }
                    });
                array = parent.$('#FieldNames').val().split("|%|");
                parent.$("#FieldNames").val('');
                $.each(array,
                    function (i) {
                        if (i > 0) {
                            parent.$("#FieldNames").val(parent.$("#FieldNames").val() + '|%|');
                        };
                        if (fieldIndex == i) {
                            parent.$("#FieldNames").val(parent.$("#FieldNames").val() + $("#FieldName").val());
                        } else {
                            parent.$("#FieldNames").val(parent.$("#FieldNames").val() + array[i]);
                        }
                    });
                array = parent.$('#AnswerTypes').val().split("|%|");
                parent.$("#AnswerTypes").val('');
                $.each(array,
                    function (i) {
                        if (i > 0) {
                            parent.$("#AnswerTypes").val(parent.$("#AnswerTypes").val() + '|%|');
                        };
                        if (fieldIndex == i) {
                            parent.$("#AnswerTypes").val(parent.$("#AnswerTypes").val() + $("#AnswerType").val());
                        } else {
                            parent.$("#AnswerTypes").val(parent.$("#AnswerTypes").val() + array[i]);
                        }
                    });
                array = parent.$('#CustomFieldOptions').val().split("|%|");
                parent.$("#CustomFieldOptions").val('');
                $.each(array,
                    function (i) {
                        if (i > 0) {
                            parent.$("#CustomFieldOptions").val(parent.$("#CustomFieldOptions").val() + '|%|');
                        };
                        if (fieldIndex == i) {
                            parent.$("#CustomFieldOptions")
                                .val(parent.$("#CustomFieldOptions").val() + $("#CustomFieldOptions").val());
                        } else {
                            parent.$("#CustomFieldOptions").val(parent.$("#CustomFieldOptions").val() + array[i]);
                        }
                    });
                array = parent.$('#Orders').val().split("|%|");
                parent.$("#Orders").val('');
                $.each(array,
                    function (i) {
                        if (i > 0) {
                            parent.$("#Orders").val(parent.$("#Orders").val() + '|%|');
                        };
                        if (fieldIndex == i) {
                            parent.$("#Orders").val(parent.$("#Orders").val() + $("#Order").val());
                        } else {
                            parent.$("#Orders").val(parent.$("#Orders").val() + array[i]);
                        }
                    });
                array = parent.$('#Requires').val().split("|%|");
                parent.$("#Requires").val('');
                $.each(array,
                    function (i) {
                        if (i > 0) {
                            parent.$("#Requires").val(parent.$("#Requires").val() + '|%|');
                        };
                        if (fieldIndex == i) {
                            parent.$("#Requires").val(parent.$("#Requires").val() + $("#Require").val());
                        } else {
                            parent.$("#Requires").val(parent.$("#Requires").val() + array[i]);
                        }
                    });
            } else {
                newRowOffset = newRowOffset + 1;

                tdFieldName.innerHTML = '<input type=\"hidden\" name=\"FieldID' +
                    fieldId +
                    '\" id=\"FieldID' +
                    $('#FieldID').val() +
                    '\" value=\"' +
                    $('#FieldID').val() +
                    '\" /><a href=\"javascript:openCustomField(\'' + newRowOffset +
                    '\', \'' + fieldId + '\')\">' +
                    $('#FieldName').val() +
                    '</a>';
                tdAnswerType.innerHTML = $('#AnswerType').val();
                tdDelete.innerHTML = '<a href=\"javascript:deleteField(\'' + newRowOffset +
                    '\', \'' +
                    fieldId +
                    '\')\">Delete</a>';

                row.appendChild(tdFieldName);
                row.appendChild(tdAnswerType);
                row.appendChild(tdDelete);

                if (parent.$("#FieldIDs").val() != '') {
                    parent.$("#FieldIDs").val(parent.$("#FieldIDs").val() + '|%|');
                };
                parent.$("#FieldIDs").val(parent.$("#FieldIDs").val() + fieldId);

                if (parent.$("#FieldNames").val() != '') {
                    parent.$("#FieldNames").val(parent.$("#FieldNames").val() + '|%|');
                };
                parent.$("#FieldNames").val(parent.$("#FieldNames").val() + $("#FieldName").val());

                if (parent.$("#AnswerTypes").val() != '') {
                    parent.$("#AnswerTypes").val(parent.$("#AnswerTypes").val() + '|%|');
                };
                parent.$("#AnswerTypes").val(parent.$("#AnswerTypes").val() + $("#AnswerType").val());

                if (parent.$("#CustomFieldOptions").val() != '') {
                    parent.$("#CustomFieldOptions").val(parent.$("#CustomFieldOptions").val() + '|%|');
                };
                parent.$("#CustomFieldOptions").val(parent.$("#CustomFieldOptions").val() + $("#CustomFieldOptions").val());

                if (parent.$("#Orders").val() != '') {
                    parent.$("#Orders").val(parent.$("#Orders").val() + '|%|');
                };
                parent.$("#Orders").val(parent.$("#Orders").val() + $("#Order").val());

                if (parent.$("#Requires").val() != '') {
                    parent.$("#Requires").val(parent.$("#Requires").val() + '|%|');
                };
                parent.$("#Requires").val(parent.$("#Requires").val() + $("#Require").val());

                parent.$("#CustomFields").append(row);

                parent.document.getElementById("AddCustomField")
                    .setAttribute("href", "javascript:openCustomField('" + newRowOffset + "', '');");
            }

            parent.closeDialog('FieldFilterDiv');
        }

        function editField(fieldId) {
            var fieldIndex = 0;
            var array = parent.$('#FieldIDs').val().split("|%|");
            $.each(array,
                function (i) {
                    if (fieldId == array[i]) {
                        fieldIndex = i;
                    }
                });
            array = parent.$('#FieldNames').val().split("|%|");
            $('#FieldName').val(array[fieldIndex]);

            array = parent.$('#AnswerTypes').val().split("|%|");
            $('#AnswerType').val(array[fieldIndex]);

            array = parent.$('#CustomFieldOptions').val().split("|%|");
            $('#CustomFieldOptions').val(array[fieldIndex]);
            array = parent.$('#CustomFieldOptions').val().split("|%%|");
            $.each(array,
                function (i) {
                    var array2 = array[i].split("|!|");
                    if (fieldId == array2[0]) {
                        $('#OptionID').val(array2[1]);
                        $('#optionList')
                            .html($('#optionList').html() +
                                '<div style="width:190px;margin: 0 auto;" id="Option' +
                                array2[1] +
                                '"><div style="width:120px;float:left;">' +
                                array2[2] +
                                '</div><div style="width:70px;text-align:right;float:right;"><a href="#" onclick="deleteOption(\'' +
                                array2[1] +
                                '\',\'' +
                                array2[2] +
                                '\');return false;">Delete</a></div></div>');
                    }
                });

            array = parent.$('#Orders').val().split("|%|");
            $('#Order').val(array[fieldIndex]);

            array = parent.$('#Requires').val().split("|%|");
            $('#Require').val(array[fieldIndex]);

            changeAnsType();
        }

        function optionAdd() {
            if ($('#OptionName').val() != '') {
                var optionID = parseInt($('#OptionID').val()) - 1;
                $('#OptionID').val(optionID);
                $('#optionList')
                    .html($('#optionList').html() +
                        '<div style="width:190px;margin: 0 auto;" id="Option' +
                        optionID +
                        '"><div style="width:120px;float:left;">' +
                        $('#OptionName').val() +
                        '</div><div style="width:70px;text-align:right;float:right;"><a href="#" onclick="deleteOption(\'' +
                        optionID +
                        '\',\'' +
                        $('#OptionName').val() +
                        '\');return false;">Delete</a></div></div>');

                if ($('#CustomFieldOptions').val() != '') {
                    $('#CustomFieldOptions').val($('#CustomFieldOptions').val() + '|%%|');
                }
                $('#CustomFieldOptions')
                    .val($('#CustomFieldOptions').val() +
                        $('#FieldID').val() +
                        '|!|' +
                        optionID +
                        '|!|' +
                        $('#OptionName').val() +
                        '|!|' +
                        $('#OptionOrder').val() +
                        '|!|' +
                        $('#Price').val() +
                        '|!|' +
                        $('#RecurringPrice').val());

                $('#OptionName').val('');
                $('#Price').val('0');
                $('#RecurringPrice').val('0');
                $('#OptionOrder').val(parseInt($('#OptionOrder').val()) + 1);
            }
        }

        function deleteOption(optionId, optionName) {
            var array = $('#CustomFieldOptions').val().split("|%%|");
            $.each(array,
                function (i) {
                    var array2 = array[i].split("|!|");
                    if (optionId == array2[1]) {
                        $('#Option' + optionId).remove();
                        if (parent.$("#DeleteCustomFieldOptions").val() != '') {
                            parent.$("#DeleteCustomFieldOptions").val(parent.$("#DeleteCustomFieldOptions").val() + '|%|');
                        };
                        parent.$('#DeleteCustomFieldOptions').val(optionId);
                    }
                });
        }

        function changeAnsType() {
            if ($("#AnswerType").val() == 'DropdownM' ||
                $("#AnswerType").val() == 'DropdownS' ||
                $("#AnswerType").val() == 'Radio' ||
                $("#AnswerType").val() == 'Checkbox') {
                document.getElementById('showOptions').style.display = '';
            } else {
                document.getElementById('showOptions').style.display = 'none';
            }
            document.getElementById('OptionName').value = '';
        }

        $(document)
            .ready(function () {
                $('#OptionOrder').val(parseInt($('#optionList div a').length) + 1);
            });
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <asp:panel id="CustomPanel" runat="server">
            <div id="idError" style="display: none;">
                <div class="mb-3" align="center">You must specify a field name.</div>
            </div>

            <input type="hidden" id="FieldID" runat="server" clientidmode="Static" />
            <input type="hidden" id="CustomFieldOptions" runat="server" clientidmode="Static" />
            <input type="hidden" id="OptionID" runat="server" clientidmode="Static" />
            <input type="hidden" id="FieldMode" runat="server" clientidmode="Static" />

            <div class="ModFormDiv">

                <div class="mb-3">
                    <label id="FieldNameLabel" clientidmode="Static" runat="server" for="FieldName">Field Name:</label>
                    <input type="text" id="FieldName" runat="server" class="form-control" clientidmode="Static" />
                </div>
                <div class="mb-3">
                    <label id="AnswerTypeLabel" clientidmode="Static" runat="server" for="AnswerType">Answer Type:</label>
                    <asp:DropDownList ID="AnswerType" runat="server" CssClass="form-control" AutoPostBack="True" EnableViewState="True" OnSelectedIndexChanged="AnswerType_SelectedIndexChanged">
                        <asp:ListItem Text="Short Answer" Value="ShortAnswer" />
                        <asp:ListItem Text="Long Answer" Value="LongAnswer" />
                        <asp:ListItem Text="Dropdown (Multiple Selection)" Value="DropdownM" />
                        <asp:ListItem Text="Dropdown (Single Selection)" Value="DropdownS" />
                        <asp:ListItem Text="Radio Buttons" Value="Radio" />
                        <asp:ListItem Text="Checkboxes" Value="Checkbox" />
                        <asp:ListItem Text="Date" Value="Date" />
                    </asp:DropDownList>
                </div>

                <div id="showOptions" runat="server" clientidmode="Static" style="border: 1px solid; margin-left: 15px; padding: 10px; width: 250px;">
                    <div class="mb-3">
                        <label id="OptionNameLabel" clientidmode="Static" runat="server" for="OptionName">Option Name:</label>
                        <input type="text" id="OptionName" runat="server" class="form-control" clientidmode="Static" />
                    </div>
                    <div class="mb-3">
                        <label id="OptionOrderLabel" clientidmode="Static" runat="server" for="OptionOrder">Order:</label>
                        <input type="text" id="OptionOrder" runat="server" class="form-control" clientidmode="Static" text="1" />
                    </div>
                    <div class="mb-3">
                        <label id="PriceLabel" clientidmode="Static" runat="server" for="Price">Price:</label>
                        <input type="text" id="Price" runat="server" class="form-control" clientidmode="Static" text="0" />
                    </div>
                    <div class="mb-3">
                        <label id="RecurringPriceLabel" clientidmode="Static" runat="server" for="RecurringPrice">Recurring Price:</label>
                        <input type="text" id="RecurringPrice" runat="server" class="form-control" clientidmode="Static" text="0" />
                    </div>
                    <div class="mb-3">
                        <button type="button" id="Add" class="Button" onclick="optionAdd();return false;">Add</button>
                    </div>
                </div>
                <div id="optionList" runat="server" clientidmode="Static" style="margin-bottom: 20px; margin-top: 10px; padding-left: 10px; width: 230px;"></div>
                <div class="mb-3">
                    <label id="OrderLabel" clientidmode="Static" runat="server" for="OrderLabel">Order:</label>
                    <input type="text" id="Order" runat="server" class="form-control" clientidmode="Static" />
                </div>
                <div class="mb-3">
                    <label id="RequiredLabel" clientidmode="Static" runat="server" for="Require">Required:</label>
                    <select id="Require" runat="server" class="form-control">
                        <option value="No">0</option>
                        <option value="Yes">1</option>
                    </select>
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
</html>