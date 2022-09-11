<?php

$data=array(
    'phoneNumber'=>"09121111111",     // phonenumber of panel
    'passWord'=>"12345678",           // password of panel
    'messageId'=>"287455796",         // id of message
);

$url = "http://whatspanel.ir/Api/MessageStatus.php";

$handler = curl_init($url);

curl_setopt($handler, CURLOPT_CUSTOMREQUEST, "POST");
curl_setopt($handler, CURLOPT_POSTFIELDS, $data);
curl_setopt($handler, CURLOPT_RETURNTRANSFER, true);

$response = curl_exec($handler);
$response=json_decode($response);

var_dump($response);