<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TestPOSTWebService._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test POST Web Service</title>
        <style type="text/css">
        p, td
        {
            font-family: Arial;
            font-size: 0.8em;
        }
        h1
        {
            font-family: Arial;
            font-size: 2.0em;
            background-color: #4890CA;
            color: White;
            border: 1px none gray;
            text-align: center;
            padding: 7px 0px 7px 0px;
            margin: 0px 3px 13px 3px;
        }
        .cssBorder
        {
            background-color: #8ADBFC;
            border: 1px solid blue;
            width: 804px;
            margin: 5px;
            padding: 10px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div class="cssBorder">
        <h1><strong>JSON "POST" Web Service tester</strong></h1>
        <table>
            <tr>
                <td width="200"><asp:Label ID="Label1" runat="server" Text="URL of POST web service:"></asp:Label></td>
                <td><asp:TextBox ID="tbWebServiceURL" runat="server" Width="550px"></asp:TextBox></td>
             </tr>
            <tr valign="top">
                <td><asp:Label ID="Label2" runat="server" Text="JSON to send to Web service:"></asp:Label></td>
                <td><asp:TextBox ID="tbJSONdata" runat="server" Height="154px" Width="500px" TextMode="MultiLine" />
                </td>
            </tr>
            <tr>
                <td />
                <td>
                    <asp:Button ID="btnCallPOSTwebService" runat="server" Text="Call POST web service" Width="150" onclick="btnCallPOSTwebService_Click" />
                </td>
            </tr>
            <tr>
                <td valign="top">Results:</td>
                <td><asp:TextBox ID="txtResult" runat="server" Height="70px" 
                        TextMode="MultiLine" Width="500px"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <p>www.MikesKnowledgeBase.com</p>
    </div>
    </form>
</body>
</html>
