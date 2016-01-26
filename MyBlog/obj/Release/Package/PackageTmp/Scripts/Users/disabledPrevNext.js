
function DisabledPrevNext(totalPages, currentPage) {
    var $prevUsersPage = $('#prevUsersPage');
    var $nextUsersPage = $('#nextUsersPage');

    if (currentPage > 1) {
        $(function() {
            $prevUsersPage.attr('disabled');
        });
    } else {
        $(function() {
            $prevUsersPage.attr('disabled', true);
        });
    }

    if (currentPage < totalPages) {
        $(function() {
            $nextUsersPage.attr('disabled');
        });
    } else {
        $(function() {
            $nextUsersPage.attr('disabled', true);
        });
    }
};