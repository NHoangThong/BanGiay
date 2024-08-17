var form = document.getElementById("form1");

var nameInput = document.getElementById("name");
var emailInput = document.getElementById("email");
var passwordInput = document.getElementById("password");
var confirmInput = document.getElementById("confirm");
var phoneInput = document.getElementById("phone");

var nameError = document.getElementById("name-error");
var emailError = document.getElementById("email-error");
var passwordError = document.getElementById("password-error");
var confirmError = document.getElementById("confirm-error");
var phoneError = document.getElementById("phone-error");

function validateForm() {
  var valid = true;

  // Kiểm tra và xử lý lỗi cho trường Tên
  var nameValue = nameInput.value.trim();
  if (nameValue === "") {
    nameError.textContent = "Tên bắt buộc nhập";
    valid = false;
  } else if (nameValue.length < 10) {
    nameError.textContent = "Tên tối thiểu 10 ký tự";
    valid = false;
  } else if (!/^[a-zA-Z0-9 ]+$/.test(nameValue)) {
    nameError.textContent = "Tên không chứa ký tự đặc biệt";
    valid = false;
  } else {
    nameError.textContent = "";
  }
    // Kiểm tra và xử lý lỗi cho trường Số điện thoại
    var phoneValue = phoneInput.value.trim();
    if (phoneValue === "") {
        phoneError.textContent = "Số điện thoại bắt buộc nhập";
        valid = false;
    } else if (!/^[0-9]+$/.test(phoneValue)) {
        phoneError.textContent = "Số điện thoại chỉ chứa chữ số";
        valid = false;
    } else if (phoneValue.length < 10 || phoneValue.length >= 11) {
        phoneError.textContent = "Số điện thoại không hợp lệ";
        valid = false;
    } else {
        phoneError.textContent = "";
    }

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

  // Kiểm tra và xử lý lỗi cho trường Xác nhận mật khẩu
  var confirmValue = confirmInput.value.trim();
  if (confirmValue === "") {
    confirmError.textContent = "Xác nhận mật khẩu bắt buộc nhập";
    valid = false;
  } else if (confirmValue !== passwordValue) {
    confirmError.textContent = "Xác nhận mật khẩu không khớp";
    valid = false;
  } else {
    confirmError.textContent = "";
  }
    //$("#form1").submit();
  return valid;
}

form.addEventListener("submit", function(event) {
  event.preventDefault();

  var result = validateForm();

  if (result) {
        alert("Đăng ký thành công");
        nameInput.value = "";
        emailInput.value = "";
        passwordInput.value = "";
        confirmInput.value = "";
        phoneInput.value = "";
  }
});