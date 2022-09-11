const request = require('request');

request.post('http://whatspanel.ir/Api/MessageStatus.php', {
    form: {
        phoneNumber: "09121111111",     // phonenumber of panel
        passWord: "12345678",           // password of panel
        messageId: "287455796"          // id of message
    }
}, function (error, response, body) {
    if (!error && response.statusCode === 200) {
        console.log(JSON.parse(response.body));
    } else {
        console.log("error");

    }
});
