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

    $("#addProject").click(function () {
        Swal.fire({
            title: "Add Project?",
            text: "Do you want to add this project for this workspace?",
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
                createProject();
        });
    });

    $("#saveTask").click(function () {
        const projectId = $("#ProjectId").val();
        const title = $("#Title").val().trim();
        const description = $("textarea[name='Description']").val().trim();
        const status = $("#Status").val();
        const priority = $("#Priority").val();
        if (projectId === "") {
            Swal.fire({
                icon: "warning",
                title: "Validation",
                text: "Please select a project."
            });
            return;
        }

        if (title === "") {
            Swal.fire({
                icon: "warning",
                title: "Validation",
                text: "Task title is required."
            });
            return;
        }

        if (description === "") {
            Swal.fire({
                icon: "warning",
                title: "Validation",
                text: "Description is required."
            });
            return;
        }
        
        // if (status === "") {
        //     Swal.fire({
        //         icon: "warning",
        //         title: "Validation",
        //         text: "Please select a status."
        //     });
        //     return;
        // }

        // if (priority === "") {
        //     Swal.fire({
        //         icon: "warning",
        //         title: "Validation",
        //         text: "Please select a priority."
        //     });
        //     return;
        // }

        Swal.fire({
            title: "Create Task?",
            text: "Do you want to create this task?",
            icon: "question",
            showCancelButton: true,
            confirmButtonText: "Create",
            confirmButtonColor: "#198754",
            cancelButtonColor: "#6c757d"
        }).then((result) => {

            if (result.isConfirmed) {

                $("#addTaskForm").submit();

            }

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
            search: "Search:",
            emptyTable: "No record found."
        }

    });

    $("#projectListTable").DataTable({

        pageLength: 10,

        ordering: true,

        searching: true,

        lengthChange: false,

        info: true,

        responsive: true,

        language: {
            search: "Search:",
            emptyTable: "No record found."
        }

    });

    $("#unassignedTaskListTable").DataTable({

        pageLength: 10,

        ordering: true,

        searching: true,

        lengthChange: false,

        info: true,

        responsive: true,

        language: {
            search: "Search:",
            emptyTable: "No record found."
        }

    });
    
    function createProject() {

        const workspaceId = $("#WorkspaceId").val();
        const name = $("#Name").val();
        const description = $("#Description").val();
        console.log(name,workspaceId,description);
        if (name === "" || workspaceId === "" || description === "") {

            Swal.fire({
                icon: "warning",
                title: "Validation",
                text: "Please fill all the field."
            });

            return;
        }

        Swal.fire({

            title: "Create Project?",
            text: "Do you want to create this project?",
            icon: "question",

            showCancelButton: true,

            confirmButtonColor: "#0d6efd",
            cancelButtonColor: "#6c757d",

            confirmButtonText: "Yes, Create"

        }).then((result) => {

            if (!result.isConfirmed)
                return;

            $.ajax({

                url: "/Project/Create",

                type: "POST",

                data: {

                    __RequestVerificationToken:
                        $('input[name="__RequestVerificationToken"]').val(),

                    workspaceId: workspaceId,
                    name: name,
                    description: description

                },

                success: function (response) {

                    if (response.success) {

                        $("#addProjectModal").modal("hide");

                        Swal.fire({

                            icon: "success",
                            title: "Success",
                            text: response.message

                        }).then(() => {

                            window.location.reload();

                        });

                    }
                    else {

                        Swal.fire({
                            icon: "error",
                            title: "Oops...",
                            text: response.message
                        });

                    }

                }

            });

        });

    }
    // WorkspaceJS end
});