function prevententertosubmit() {
    $(document).ready(function () {
        $(window).keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });
    });
}

function myjsalternaterecordcolor() {
    var count = 1;
    $("#list .table tbody tr").each(function () {
        var $item = $("td:first", this);
        var $prev = $(this).prev().find('td:first');
        if ($prev.length && $prev.text().trim() != $item.text().trim()) {
            count = count + 1;
        }
        if (count % 2 == 0) {
            $(this).addClass('odd');
        }
        else {
            $(this).addClass('even');
        }
    });
}

$.extend({
    alert: function (message, title) {
        $("<div></div>").dialog({
            buttons: { "Ok": function () { $(this).dialog("close"); } },
            close: function (event, ui) { $(this).remove(); },
            resizable: false,
            title: title,
            modal: true
        }).text(message);
    }
});