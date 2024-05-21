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
