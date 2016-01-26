
(function (mainAppObject) {
    mainAppObject.userPage.pagination = function () {
        var currentPageParam = mainAppObject.pageInfo.currentPage;
        var totalPagesParam = mainAppObject.pageInfo.totalPages;

        DisabledPrevNext(totalPagesParam, currentPageParam);

        var findParam = $('#findItemFindUsers').val();

        var url = "Users/FindUser";
        var getSetupPaginationAjaxRequest = function(currentPageParam, findUser) {
           return {
                url: AppU.urls.getUserByPage + url,
                type: "POST",
                data: { currentPage: currentPageParam, find: findUser},
                success: function (data) {
                    $('#result').html(data);
                }
            };
        };

        $('#nextUsersPage').on('click', function () {
            currentPageParam++;

            pushState(currentPageParam);

            var setup = getSetupPaginationAjaxRequest(currentPageParam, findParam);
            $.ajax(setup);
        });

        $('#prevUsersPage').on('click', function () {
            currentPageParam--;

            pushState(currentPageParam);

            var setup = getSetupPaginationAjaxRequest(currentPageParam, findParam);
            $.ajax(setup);
        });

        
        $('.linkCurrentPageUsers').on('click', function () {
            currentPageParam = $(this).text();

            pushState(currentPageParam);

            var findParam = $('#findItemFindUsers').val();

            var setup = getSetupPaginationAjaxRequest(currentPageParam, findParam);
            $.ajax(setup);
        });
    };

    $(function () {
        mainAppObject.userPage.pagination();
    });

})(AppU);



