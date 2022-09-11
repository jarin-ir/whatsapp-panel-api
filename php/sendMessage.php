<?php

$data=array(
    'phoneNumber'=>"09121111111",        // phonenumber of panel
    'passWord'=>"12345678",              // password of panel
    'destPhoneNumbers'=>"09122222222",   // destination phonenumber - for two or more phonenumber use ; - examle 09121111111;09122222222
    'text'=>"hi",                        // text message - for messagePack use # + packCode - example : #2vbo94
    'lines'=>"09133333333",              // message lines - for two or more line use - examle 09121111111;09122222222
);

$url = "http://whatspanel.ir/Api/SendMessage.php";

$handler = curl_init($url);

curl_setopt($handler, CURLOPT_CUSTOMREQUEST, "POST");
curl_setopt($handler, CURLOPT_POSTFIELDS, $data);
curl_setopt($handler, CURLOPT_RETURNTRANSFER, true);

$response = curl_exec($handler);
$response=json_decode($response);

if($response->status==100)
{
    echo  "message number : ".$response->code;
}
else
{
    echo "error : ".$response->status;
}