$(function() {
    $('#UserManagementStartPage').on('click', function () {

        $.ajax(
        {
            url: 'http://localhost:7738/Users/UserManagement',
            type: "POST",
            success: function(data) {
                $('#loadContent').html(data);
            }
        });
    });
});