const toggler = document.querySelector(".toggler-btn");
toggler.addEventListener("click", function () {
    document.querySelector("#sidebar").classList.toggle("collapsed");
});




$("#logout").click(function (e) {
    e.preventDefault();

        $.ajax({
            url: '/Cuenta/Login?handler=Logout',
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            method: 'POST',
     
            success: function (response) {
                if (response.status == "Ok") {
                   /* alert(response.status);*/
                    window.location.href = '/';
                } else {
                    alert('Logout failed.');
                }
            },
            error: function (error) {
                alert('An error occurred during logout.');
                console.log(error);
            }
        });
});
