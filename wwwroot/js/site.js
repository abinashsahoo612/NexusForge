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

    $("#workspaceCreate").click(function (e) {

            $.ajax({

            url: "/Workspace/Create/",

            type: "GET",
            success: function (response) {

                $("#workspaceModalContent").html(response);

                $("#workspaceModal").modal("show");

            },
            error: function () {

                Swal.fire({
                    icon: "error",
                    title: "Server Error",
                    text: "Something went wrong."
                });

            }
        });
    });

    $("#workspaceEdit").click(function (e) {
        const workspaceId = $(this).data('id');

            $.ajax({

            url: "/Workspace/Edit/" + workspaceId,

            type: "GET",
            success: function (response) {

                $("#workspaceModalContent").html(response);

                $("#workspaceModal").modal("show");

            },
            error: function () {

                Swal.fire({
                    icon: "error",
                    title: "Server Error",
                    text: "Something went wrong."
                });

            }
        });
    });

    $(document).on("click", "#projectEdit", function () {
        const projectId = $(this).data('id');

            $.ajax({

            url: "/Project/Edit/" + projectId,

            type: "GET",
            success: function (response) {

                $("#commonModalContent").html(response);
                $("#commonModal").modal("show");

            },
            error: function () {

                Swal.fire({
                    icon: "error",
                    title: "Server Error",
                    text: "Something went wrong."
                });

            }
        });
    });

    $(document).on("click", "#taskEdit", function () {
        const taskId = $(this).data('id');

            $.ajax({

            url: "/Task/Edit/" + taskId,

            type: "GET",
            success: function (response) {

                $("#commonModalContent").html(response);
                $("#commonModal").modal("show");

            },
            error: function () {

                Swal.fire({
                    icon: "error",
                    title: "Server Error",
                    text: "Something went wrong."
                });

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

    // $(document).on("click", "#updateWorkspace", function () {
    //     const form = $("#editWorkspaceForm");

    //     $.ajax({

    //         url: form.attr("action"),
    //         type: "POST",
    //         data: form.serialize(),

    //         success: function (response) {

    //             // Validation failed
    //             if (response.status != "success") {
    //                 Swal.fire({
    //                     icon: "error",
    //                     title: "Ooops...",
    //                     text: 'Invali inputs'
    //                 })
    //             }

    //             // Success
    //             Swal.fire({
    //                 icon: "success",
    //                 title: "Success",
    //                 text: response.message
    //             }).then(() => {

    //                 location.reload();

    //             });

    //         },

    //         error: function (xhr) {

    //             if (xhr.status === 400 && xhr.responseJSON) {

    //                 const errors = xhr.responseJSON.errors;

    //                 Swal.fire({
    //                     icon: "warning",
    //                     title: "Validation Error",
    //                     html: errors.join("<br>")
    //                 });

    //                 return;
    //             }

    //             Swal.fire({
    //                 icon: "error",
    //                 title: "Error",
    //                 text: "Something went wrong."
    //             });

    //         }

    //     });

    // });

    function submitAjaxForm(formSelector, successCallback = null) {

        const form = $(formSelector);

        $.ajax({

            url: form.attr("action"),
            type: form.attr("method"),
            data: form.serialize(),

            success: function (response) {

                if (!response.success) {

                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: response.message
                    });

                    return;
                }

                Swal.fire({
                    icon: "success",
                    title: "Success",
                    text: response.message
                }).then(() => {

                    if (successCallback) {
                        successCallback(response);
                    }
                    else {
                        location.reload();
                    }

                });

            },

            error: function (xhr) {

                if (xhr.status === 400 && xhr.responseJSON) {

                    Swal.fire({
                        icon: "warning",
                        title: "Validation Error",
                        html: xhr.responseJSON.errors.join("<br>")
                    });

                    return;
                }

                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: "Something went wrong."
                });

            }

        });

    }

    $(document).on("click", "#createWorkspace", function () {

        submitAjaxForm("#createWorkspaceForm");

    });

    $(document).on("click", "#updateWorkspace", function () {

        submitAjaxForm("#editWorkspaceForm");

    });

    $(document).on("click", "#updateProject", function () {

        submitAjaxForm("#editProjectForm");

    });

    $(document).on("click", "#updateTask", function () {

        submitAjaxForm("#editTaskForm");

    });

    // WorkspaceJS end
});