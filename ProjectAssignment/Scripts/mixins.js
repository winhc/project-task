var accessToken = $("input[name=__RequestVerificationToken]").val();

// login
$(document).on("click", "#btnLogin", function () {
    var employeeID = $('#employeeID').val();
    var employeePassword = $('#employeePassword').val();

    if (employeeID == '') {
        var header = "Message";
        var type = "warning";

        MessageBox("Please enter Employee ID!", header, "", type);
    }
    else if (employeePassword == '') {
        var header = "Message";
        var type = "warning";

        MessageBox("Please enter Password!", header, "", type);
    }
    else {
        $.ajax({
            url: '/LoginAPI/Login',
            type: 'POST',
            data: { "__RequestVerificationToken": accessToken, "employeeID": employeeID, "employeePassword": employeePassword },
            bDestroy: true,
            responsive: true,

            success: function (data) {
                if (data.result == "success") {
                    window.location.href = '/Employee/Index';
                }
                else {
                    var header = "Error";
                    var type = "error";

                    MessageBox("Login fail.", header, "", type);
                }
            }
        });
    }

});

// logout
$(document).on('click', '#employeeLogout', function () {
    accessToken = $("input[name=__RequestVerificationToken]").val();
    console.log("employeeLogout");
    var header = "Confirmation";
    var type = "confirm";
    var btn = '<button id="btnLogoutConfirm" class="btn btn-min btn-info" type="button" data-dismiss="modal">Yes</button>';
    btn = btn + '<button class="btn btn-min btn-secondary" type="button" data-dismiss="modal">No</button>';
    MessageBox("Are you sure to logout?", header, btn, type);
});

$(document).on('click', '#btnLogoutConfirm', function () {
    $('#MyModel').on('hidden.bs.modal', function (e) {
        $.post("/LoginAPI/Logout", { "__RequestVerificationToken": accessToken }, function (data) {
            window.location.href = '/Login/Login';
        });
    });
});



function MessageBox(msg, header, btn, type) {
    var vheader = header;
    if (header === "") { vheader = "Message"; }
    if (btn === "") { btn = '<button class="btn btn-min btn-info" type="button" data-dismiss="modal">Ok</button>'; }
    var icon = "<i class='fa fa-info-circle fa-stack-3x text-info'></i>";
    
    if (type.toLowerCase() === "warning") {
        if (header === "") { vheader = "Warning"; }
        icon = "<i class='fa fa-warning fa-stack-3x text-primary'></i>";
    }
    if (type.toLowerCase() === "confirm") {
        if (header === "") { vheader = "Confirmation"; }
        icon = "<i class='fa fa-question-circle fa-stack-3x text-primary'></i>";
    }
    if (type.toLowerCase() === "error") {
        if (header === "") { vheader = "Error"; }
        icon = "<i class='fa fa-exclamation fa-stack-3x text-danger'></i>";
    }

    $('#ModalHeader').html(icon + '&nbsp;&nbsp;' + vheader);
    $('#ModalBody').html($.parseHTML(msg));
    $('#ModalFooter').html(btn);

    $(document).ready(function () {
        $('#MyModel').modal('show');
    });
    return false;
}
