
(function(mainAppObject) { 

    mainAppObject.userPage = {};

    function bindEventsForCloseUserForm($closeUserForm, $addUserForm) {
        $closeUserForm
             .mouseover(function () {
                 $(this).addClass("btn-danger");
             })
             .mouseout(function () {
                 $(this).removeClass("btn-danger");
             })
             .click(function () {
                 $addUserForm.fadeOut();
             });
    }

    mainAppObject.userPage.addUserForm = function () {

        var currentPage = mainAppObject.pageInfo.currentPage;
        var $addUserForm = $('#addUserForm');
        var $closeAddUserForm = $('#closeAddUserForm');

        $addUserForm.hide();

        $("#showFormAddUser").on('click', function () {
            $('#editUserForm').hide();
            $addUserForm.fadeIn();
        });

        bindEventsForCloseUserForm($closeAddUserForm, $addUserForm);

        $('#okAddUser').on('click', function () {
            currentPage = 1;
            pushState(currentPage);
        });   
    };

    function getUserImg($editUserForm, data) {
        var userImg = $("<img/>")
            .attr("src", data.srcImg)
            .css({ "margin-top": "10px"
            });
        $editUserForm.find("#userImg").empty();
        $editUserForm.find("#userImg").append(userImg);
    }

    function hrefUploadUserImg($linkCallUploadUserImg, id) {
        var  src = $linkCallUploadUserImg.attr("data-src");
        src = src.replace("xxx", id);
        $linkCallUploadUserImg.attr("href", src);
    }

    mainAppObject.userPage.editUserForm = function () {

        var currentPageParam = mainAppObject.pageInfo.currentPage;
        var $editUserForm = $('#editUserForm');
        var $closeEditUserForm = $('#closeEditUserForm');
        var $linkCallUploadUserImg = $editUserForm.find(".callUploadUserImg");
         
        $editUserForm.hide();
   
        $(".editUserLink").on('click', function (even) {
            $('#addUserForm').hide();

            $editUserForm.fadeIn();

            bindEventsForCloseUserForm($closeEditUserForm, $editUserForm);
  
            //detect id of user and set id in SelectUser.cshtml
            var id = $(this).data('id');
            var $id = $editUserForm.find(".js-Id");
            $id.val(id);
                      
            hrefUploadUserImg($linkCallUploadUserImg, id);

            var $currentPage = $("#inputCurrentPageEditForm");
            var $findUser = $("#inputFindUserEditForm");
            $currentPage.val(currentPageParam);
            //when edit User with findItem
            $findUser.val($('#findItemFindUsers').val());

            $.ajax({
                //url: 'http://localhost:7738/Users/EditUserQuery',
                url: "Users/EditUserQuery",
                type: "POST",
                dataType: 'json',
                data: {
                    idParam: id,
                    currentPage: currentPageParam
                },
                success:
                    function (data) {
                        $editUserForm.find('.js-firstNameEdit').val(data.firstName);
                        $editUserForm.find('.js-lastNameEdit').val(data.lastName);
                        $editUserForm.find('.js-eMailEdit').val(data.eMail);

                        getUserImg($editUserForm, data);
                    }
            });
        });
    };

    mainAppObject.userPage.deleteUser = function () {

        var currentPage = mainAppObject.pageInfo.currentPage;

        $(".deleteUserLink").on('click', function () {
            var id = $(this).data('id');

            if (confirm("Delete User?")) {
                $.ajax({
                    //url: 'http://localhost:7738/Users/DeleteUser',
                    url: "Users/DeleteUser",
                    type: "POST",
                    dataType: 'json',
                    data: {
                        idParam: id,
                        currentPage: currentPage
                    },
                    success:
                        function (data) {
                            $("tr#userRow_" + data.id).fadeOut(1000);
                            alert(data.message);
                        }
                });
            };
        });
    };

    mainAppObject.userPage.initTable =  function() {
        mainAppObject.userPage.addUserForm();
        mainAppObject.userPage.editUserForm();
        mainAppObject.userPage.deleteUser();
    }

    $(function () {
        mainAppObject.userPage.initTable();    
    });

})(AppU);









