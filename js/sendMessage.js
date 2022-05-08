const request = require('request');

request.post('http://wp.jarin.ir/Api/SendMessage.php', {
    form: {
        phoneNumber: "09121111111",        // phonenumber of panel
        passWord: "12345678",              // password of panel
        destPhoneNumbers: "09122222222",   // destination phonenumber - for two or more phonenumber use ; - examle 09121111111;09122222222
        text: "hi",                        // text message - for messagePack use # + packCode - example : #2vbo94
        lines: "09133333333",              // message lines - for two or more line use - examle 09121111111;09122222222
    }
}, function (error, response, body) {
    if (!error && response.statusCode === 200) {
        console.log(JSON.parse(response.body));
    } else {
        console.log("error");

    }
});
