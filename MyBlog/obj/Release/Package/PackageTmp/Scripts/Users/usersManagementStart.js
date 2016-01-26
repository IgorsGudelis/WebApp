
function EditUser(data) {
    alert('User changed');
    $('#editUserForm').fadeOut();
}

$(function () {
    var currentPageParam = window.AppU.pageInfo.currentPage;

    $('#loadSearch').hide();
    $('#okSearchUser').on('click', function () {
    $('#loadSearch').show().fadeOut(1000);

        currentPageParam = 1;
        window.pushState(currentPageParam);

        var findParam = $('#findItemFindUsers').val();

        $.ajax({
            //url: 'http://localhost:7738/Users/FindUser',
            url: "Users/FindUser",
            type: "POST",
            data: { currentPage: currentPageParam, find: findParam },
            success: function (data) {
                $('#result').html(data);
            }
        });
    });
});

