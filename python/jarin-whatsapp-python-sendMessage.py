import requests
import json

url = 'http://wp.jarin.ir/Api/SendMessage.php'
myobj = {
         "phoneNumber": "09121111111" ,   #phonenumber of panel
         "passWord": "12345678" , #password of panel
         "destPhoneNumbers": "09122222222" , #destination phonenumber - for two or more phonenumber use ; - examle 09121111111;09122222222
         "text": "Hello World!!!!" ,    #text message - for messagePack use # + packCode - example : #2vbo94
         "lines": "09133333333" } #message lines - for two or more line use - examle 09121111111;09122222222

x = requests.post(url, data = myobj)

y = json.loads(x.text)

print(y)
