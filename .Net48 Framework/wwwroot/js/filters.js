function runUserSearch(objUsername, objUserID) {
    document.getElementById("UserSearchFrame").src = "../spadmin/filter-usersearch.aspx?PopulateName=" +
        objUsername +
        "&PopulateUserID=" +
        objUserID +
        "&Keywords=" +
        document.getElementById("MemberSearch").value;
}

function openUserSearch(objUsername, objUserID) {

    if ($("#FieldFilterDiv").length === 0) {
        $("body").append('<div id="FieldFilterDiv" title="Member Search"></div>');
        $("#FieldFilterDiv")
            .append('<iframe style="width:100%; height: 330px;" id="UserSearchFrame" src="../spadmin/filter-usersearch.aspx?PopulateUsername=' + objUsername + "&PopulateUserID=" + objUserID + '" frameborder="0" />');
    }
    openModal("FieldFilterDiv", 300, 440);
}

function runProductSearch(irowOffset) {
    document.getElementById("UserSearchFrame").src = "../spadmin/filter-products.aspx?RowOffset=" +
        irowOffset +
        "&Keywords=" +
        document.getElementById("ProductSearch").value;
}

function openProductSearch(irowOffset) {
    if (document.getElementById("FieldFilterDiv") === null) {
        $("body").append('<div id="FieldFilterDiv" title="Product Search"></div>');
        $("#FieldFilterDiv")
            .append('<iframe style="width:100%; height: 330px;" id="UserSearchFrame" src="../spadmin/filter-products.aspx?RowOffset=' + irowOffset + '" frameborder="0" />');
    }
    openModal("FieldFilterDiv", 300, 440);
}