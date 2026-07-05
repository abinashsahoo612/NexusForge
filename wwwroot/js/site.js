$(document).ready(function () {
    // WorkspaceJS Start
    $("#btnAddMember").click(function () {
        Swal.fire({
            title: "Add Member?",
            text: "Do you want to add this user to the workspace?",
            icon: "question",
            showCancelButton: true,
            confirmButtonColor: "#198754",
            cancelButtonColor: "#6c757d",
            confirmButtonText: "Yes, Add",
            cancelButtonText: "Cancel"
        }).then((result) => {

            if (!result.isConfirmed)
                return;
            else
                addMember();
        });
    });

    $("#btnCreateMember").click(function () {
        Swal.fire({
            title: "Add Member?",
            text: "Do you want to create this user for this workspace?",
            icon: "question",
            showCancelButton: true,
            confirmButtonColor: "#198754",
            cancelButtonColor: "#6c757d",
            confirmButtonText: "Ok",
            cancelButtonText: "Cancel"
        }).then((result) => {

            if (!result.isConfirmed)
                return;
            else
                createMember();
        });
    });

    function addMember() {

        const workspaceId = $("#WorkspaceId").val();
        const userId = $("#UserId").val();

        if (userId === "") {

            Swal.fire({
                icon: "warning",
                title: "Validation",
                text: "Please select a user."
            });

            return;
        }
        if (workspaceId === "") {

            Swal.fire({
                icon: "warning",
                title: "Validation",
                text: "Workspace is required."
            });

            return;
        }

        $.ajax({

            url: "/Workspace/AddMember",

            type: "POST",

            data: {

                __RequestVerificationToken:
                    $('input[name="__RequestVerificationToken"]').val(),

                WorkspaceId: workspaceId,

                userId: userId
            },

            success: function (response) {

                if (response.success) {

                    Swal.fire({
                        icon: "success",
                        title: "Success",
                        text: response.message
                    }).then(()=>{
                        window.location.reload();
                    });

                    // TODO
                    // refreshMemberTable();

                }
                else {

                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: response.message
                    });

                }

            },

            error: function () {

                Swal.fire({
                    icon: "error",
                    title: "Server Error",
                    text: "Something went wrong."
                });

            }

        });

    }

    function createMember() {

        const workspaceId = $("#WorkspaceId").val();
        const fullName = $("#FullName").val();
        const email = $("#Email").val();
        const password = $("#Password").val();
        const confirmPassword = $("#ConfirmPassword").val();
        if (workspaceId === "" || fullName === "" || email === "" || password === "" || confirmPassword === "") {

            Swal.fire({
                icon: "warning",
                title: "Validation",
                text: "Please fill all the field."
            });
            return;

        }
        if(password !== confirmPassword){
            Swal.fire({
                icon: "warning",
                title: "Validation",
                text: "Password and Confirm Password doesn't match!"
            });
        }

        $.ajax({

            url: "/Workspace/CreateMember",

            type: "POST",

            data: {

                __RequestVerificationToken:
                    $('input[name="__RequestVerificationToken"]').val(),

                WorkspaceId: workspaceId,
                FullName: fullName,
                Email: email,
                Password: password,
                ConfirmPassword: confirmPassword
            },

            success: function (response) {

                if (response.success) {

                    Swal.fire({
                        icon: "success",
                        title: "Success",
                        text: response.message
                    }).then(()=>{
                        window.location.reload();
                    });

                    // TODO
                    // refreshMemberTable();

                }
                else {

                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: response.message
                    });

                }

            },

            error: function () {

                Swal.fire({
                    icon: "error",
                    title: "Server Error",
                    text: "Something went wrong."
                });

            }

        });

    }
    $("#memberTable").DataTable({

        pageLength: 10,

        ordering: true,

        searching: true,

        lengthChange: false,

        info: true,

        responsive: true,

        language: {
            search: "Search:"
        }

    });
    // WorkspaceJS end
});