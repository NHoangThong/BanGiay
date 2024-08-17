var form = document.getElementById("form1");


var emailInput = document.getElementById("email");
var passwordInput = document.getElementById("password");



var emailError = document.getElementById("email-error");
var passwordError = document.getElementById("password-error");
function validateForm() {
    var valid = true;


    // Kiểm tra và xử lý lỗi cho trường Email
    var emailValue = emailInput.value.trim();
    if (emailValue === "") {
        emailError.textContent = "Email bắt buộc nhập";
        valid = false;
    } else if (!/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(emailValue)) {
        emailError.textContent = "Email sai định dạng";
        valid = false;
    } else {
        emailError.textContent = "";
    }

    // Kiểm tra và xử lý lỗi cho trường Mật khẩu
    var passwordValue = passwordInput.value.trim();
    if (passwordValue === "") {
        passwordError.textContent = "Nhập mật khẩu";
        valid = false;
    } else {
        passwordError.textContent = "";
    }


    return valid;
}

form.addEventListener("submit", function (event) {
    event.preventDefault();

    var result = validateForm();

    if (result) {
        alert("Đăng nhập thành công");

        emailInput.value = "";
        passwordInput.value = "";

    }
});