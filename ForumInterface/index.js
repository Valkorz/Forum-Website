let login_btn = document.getElementById("Login");
let join_btn = document.getElementById("Join");

if(localStorage.getItem("logged") == false){
    login_btn.style.visibility = "hidden";
    join_btn.style.visibility = "hidden";
}
else{
    login_btn.style.visibility = "visible";
    join_btn.style.visibility = "visible";
}

login_btn.addEventListener("click", function() {
    window.location.href = '/page/login/login.html';
});

join_btn.addEventListener("click", function() {
    window.location.href = '/page/register/register.html';
});