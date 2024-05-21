// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var objCredentials = [
    {
        username: "Daniel",
        password: "1234"
    },
    {
        username: "Rainier",
        password: "4321"
    }
]

//start login
function login() {
    //get data from Form
    var username = document.getElementById("InputUser").value;
    var password = document.getElementById("InputPassword").value;

    //loop thru users and verify 
    for (i = 0; i < objCredentials.length; i++) {
        if (username == objCredentials[i].username && password == objCredentials[i].password) {
            console.log(username + "Is logged in!");
            return;
        }
    }
}

//error wrong user/pass
console.log("incorrect username or password")