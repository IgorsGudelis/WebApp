
function completeAddUserForm(result) {

    var isError = $("span.field-validation-error", result.responseText).length > 0;
    if (isError) {
        // there was an error => we update the container of the form
        $("#addUserForm").html(result.responseText);
    } else {
        // no error => we hide validation errors and update the result container
        $("#addUserForm .field-validation-error").hide();
        $("#addUserForm .input-validation-error").removeClass("input-validation-error");
        $("#result").html(result.responseText);

        var  currentPage = 1;
        pushState(currentPage);

        alert("User is added");
    }
}

function completeEditUserForm(result) {
    var isError = $("span.field-validation-error", result.responseText).length > 0;
    if (isError) {
        // there was an error => we update the container of the form
        $("#editUserForm").html(result.responseText);
    } else {
        // no error => we hide validation errors and update the result container
        $("#editUserForm .field-validation-error").hide();
        $("#editUserForm .input-validation-error").removeClass("input-validation-error");
        $("#result").html(result.responseText);

        alert("User is changed");
    }
}

(function(mainAppObject) { 

    mainAppObject.userPage = {};

    function bindEventsForCloseUserForm($visibleUserForm, $closeUserForm) {
        $visibleUserForm.on(
        {
            "click": function() {
                $visibleUserForm.hide()
                    .find("span").hide()
                    .end()
                    .find("input").removeClass("input-validation-error");
            },
            "mouseover": function() {
                $(this).addClass("btn-danger");
            },
            "mouseout": function() {
                $(this).removeClass("btn-danger");
            }
        }, $closeUserForm);
    }

    function getUserImg($editUserForm, data) {
        var userImg = $("<img/>")
            .attr("src", data.srcImg)
            .css({
                "margin-top": "10px"
            });
        $editUserForm.find("#userImg").empty();
        $editUserForm.find("#userImg").append(userImg);
    }

    function hrefUploadUserImg($linkCallUploadUserImg, id) {
        var src = $linkCallUploadUserImg.attr("data-src");
        src = src.replace("xxx", id);
        $linkCallUploadUserImg.attr("href", src);
    }

    mainAppObject.userPage.addUserForm = function () {

        var $addUserForm = $('#addUserForm');
        var $closeAddUserForm = "#closeAddUserForm";

        $addUserForm.hide();
        
        $("#showFormAddUser").on('click', function () {
            $('#editUserForm')
                .hide()
                .find("input").removeClass("input-validation-error")
                .end()
                .find("span").hide();

            $addUserForm
                .fadeIn()
                .find("input").removeClass("input-validation-error")
                .end()
                .find("span").hide();
        });

        bindEventsForCloseUserForm($addUserForm, $closeAddUserForm); 
    };

    mainAppObject.userPage.editUserForm = function () {

        var currentPageParam = mainAppObject.pageInfo.currentPage;
        var $editUserForm = $('#editUserForm');
        var $closeEditUserForm = "#closeEditUserForm";
        var $linkCallUploadUserImg = $editUserForm.find(".callUploadUserImg");
         
        $editUserForm.hide();
   
        $(".editUserLink").on('click', function (even) {
            $('#addUserForm')
                .hide()
                .find("input").removeClass("input-validation-error")
                .end()
                .find("span").hide();

            $editUserForm
                .fadeIn()
                .find("input").removeClass("input-validation-error")
                .end()
                .find("span").hide();

            bindEventsForCloseUserForm($editUserForm, $closeEditUserForm);
  
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
                //url: "Users/EditUserQuery",
                url: "EditUserQuery",
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
                    //url: "Users/DeleteUser",
                    url: "DeleteUser",
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









