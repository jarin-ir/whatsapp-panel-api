import requests
import json

url = 'http://wp.jarin.ir/Api/MessageStatus.php'
myobj = {
         "phoneNumber": "09121111111" ,   #phonenumber of panel
         "passWord": "12345678" , #password of panel
         "messageId": "286455987" , #id of message
         }

x = requests.post(url, data = myobj)

y = json.loads(x.text)


print(y)
