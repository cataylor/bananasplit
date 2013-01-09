<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="BananaSplit._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BananaSplit Management Console: Login</title>
    <style type="text/css">
        #top_logo
        {
            background-image:url("Images/top_back.png");
            background-repeat:repeat-x;
        }
        body        
        {
            background-image:url("Images/background.png");
            background-repeat:repeat-y;
            margin:0px;
        }
        
        #main_body
        {
            margin:0px auto;
            width:620px;
            height:115px;
            padding-top:80px;
        }
        #title_text
        {
            font-family:Arial, Tahoma;
            font-weight:bold;
            font-size:20px;
            font-weight:bold;
            padding-bottom:8px;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="top_logo">
            <img src="Images/btext.png" alt="Banana Split" />
        </div>
        <div id="main_body">
            <div id="title_text">Banana Split Management Console</div>
            <a href=""><img src="Images/fb_login.png" alt="Login with Facebook" /></a>
        </div>
    </div>
    </form>
</body>
</html>
