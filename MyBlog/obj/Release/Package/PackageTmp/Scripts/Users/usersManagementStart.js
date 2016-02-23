
$(function () {
    var currentPageParam = window.AppU.pageInfo.currentPage;

    $('#loadSearch').hide();
    $('#okSearchUser').on('click', function () {
    $('#loadSearch').show().fadeOut(1000);

        currentPageParam = 1;
        window.pushState(currentPageParam);

        var findParam = $('#findItemFindUsers').val();

        $.ajax({
            //url: "Users/FindUser",
            url: "FindUser",
            type: "POST",
            data: { currentPage: currentPageParam, find: findParam },
            success: function (data) {
                $('#result').html(data);
            }
        });
    });
});

