<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TestPOSTWebService.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body
        {
        position: fixed; 
        overflow-y: scroll;
        width: 100%;
        }

        .FixedHeader {
            position:absolute;
        }
    </style>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>
    <script type="text/javascript" src="gridviewScroll.min.js"></script>
    <link href="GridviewScroll.css" rel="stylesheet" />

    <script type="text/javascript">
	  function triggerFileUpload()
	  {
		document.getElementById("File1").click();
	  }

	  function setHiddenValue()
	  {
	      document.getElementById("Hidden1").value = document.getElementById("File1").value;
	      this.form1.submit();
	  }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="height:50%">
        <asp:Table ID="Table2" runat="server" Style="margin-left:10px" GridLines="Both" BorderStyle="Solid" Width="98%">
            <asp:TableRow Height="80px">
                <asp:TableCell ColumnSpan="2">
                    <div style="height:80px; overflow:auto">
                    <asp:DataList ID="DataList2" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" 
                        BorderWidth="1px" CellPadding="3" DataKeyField="numRow" GridLines="Horizontal" RepeatDirection="Horizontal">
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <HeaderTemplate>
                            <table>
                        </HeaderTemplate>
                          <ItemTemplate>
                                    <tr>
                                        <td>
                                        <%# DataBinder.Eval(Container.DataItem, "decVariance")  %>
                                        </td>
                                        <td>&emsp;</td>
                                        <td>
                                        <%# DataBinder.Eval(Container.DataItem, "vcComment")  %>
                                        </td>
                                        <td></td>
                                    </tr>
                          </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:DataList>
                    </div>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Width="50%" HorizontalAlign="Right">
                    <div id="DataListFollowUpComments" style="height:250px;overflow:auto">
                    <asp:DataList ID="DataList4" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" 
                        BorderWidth="1px" CellPadding="3" DataKeyField="numRowFu" GridLines="Horizontal" RepeatDirection="Vertical"
                        OnEditCommand="DataList4_EditCommand" OnCancelCommand="DataList4_CancelCommand" 
                        OnDeleteCommand="DataList4_DeleteCommand" OnUpdateCommand="DataList4_UpdateCommand" >
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <HeaderTemplate>
                                <table style="border-collapse:collapse">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "datFuComment").ToString().Split()[0] %>' />,  
                                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "vcFuCommentBy") %>' /> 
                                    </td>
                                    <td>                                        
                                    </td>
                                    <td rowspan="2" style="border-left:1px solid grey; max-width: 100px">                                        
                                    </td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "vcCallRefNo") %>' /><br /> 
                                    </td>
                                </tr>
                                <tr style="border-bottom:2px solid grey">
                                    <td>
                                        <asp:LinkButton id="LinkButton1" runat="server" Text="Edit" CommandName="edit" />
                                        <asp:LinkButton id="LinkButton2" runat="server" Text="Delete" CommandName="delete" />
                                    </td>
                                    <td style="text-align:right; padding:10px">
                                        <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "vcFuComment") %>' /><br /> 
                                    </td>
                                    <td>                                        
                                    </td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "vcContactName") %>' /><br />
                                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "vcContactPhone") %>' /><br /> 
                                        <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "vcContactEmail").ToString().Replace("@","@<br />&nbsp;") %>' />
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "datFollowUp").ToString().Split()[0] %>' /><br /> 
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "datFuComment").ToString().Split()[0] %>' />,  
                                        <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "vcFuCommentBy") %>' /> 
                                    </td>
                                    <td>                                        
                                    </td>
                                    <td rowspan="2" style="border-left:1px solid grey; max-width: 100px">                                        
                                    </td>
                                    <td>
                                        <asp:TextBox id="txtVcCallRefNo" runat="server" PlaceHolder="Call Ref #" Text='<%# DataBinder.Eval(Container.DataItem, "vcCallRefNo") %>' /><br /> 
                                    </td>
                                </tr>
                                <tr style="border-bottom:2px solid grey">
                                    <td>
                                        <asp:LinkButton id="LinkButton1" runat="server" Text="Update" CommandName="update" />
                                        <asp:LinkButton id="LinkButton2" runat="server" Text="Cancel" CommandName="cancel" />
                                    </td>
                                    <td style="text-align:right; padding:10px">
                                        <asp:TextBox id="txtVcFuComment" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "vcFuComment") %>' /><br /> 
                                    </td>
                                    <td>                                        
                                    </td>
                                    <td>
                                        <asp:TextBox id="txtVcContactName" runat="server" PlaceHolder="Name" Text='<%# DataBinder.Eval(Container.DataItem, "vcContactName") %>' /><br />
                                        <asp:TextBox id="txtVcContactPhone" runat="server" PlaceHolder="Phone" Text='<%# DataBinder.Eval(Container.DataItem, "vcContactPhone") %>' /><br />
                                        <asp:TextBox id="txtVcContactEmail" runat="server" PlaceHolder="Email" Text='<%# DataBinder.Eval(Container.DataItem, "vcContactEmail") %>' /><br />
                                        <asp:TextBox id="txtDatFollowUp" runat="server" PlaceHolder="Follow-Up Date" Text='<%# DataBinder.Eval(Container.DataItem, "datFollowUp").ToString().Split()[0] %>' />
                                    </td>
                                </tr>
                            </EditItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:DataList>
                    </div>
                </asp:TableCell>

                <asp:TableCell Width="50%">
                    <br />
                    <table style="margin-left:10px; margin-right:10px">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtVcCallRefNo" runat="server" Width="95px" PlaceHolder="Call Ref #" onkeydown = "return (event.keyCode!=13);" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtVcContactName" runat="server" Width="95px" PlaceHolder="Name" onkeydown = "return (event.keyCode!=13);" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtVcContactPhone" runat="server" Width="95px" PlaceHolder="Phone" onkeydown = "return (event.keyCode!=13);" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtVcContactEmail" runat="server" Width="95px" PlaceHolder="Email" onkeydown = "return (event.keyCode!=13);" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4"><br /></td>                            
                        </tr>
                        <tr>                            
                            <td ><asp:TextBox ID="txtVcFuCommentBy" runat="server" Width="95px" PlaceHolder="Author" onkeydown = "return (event.keyCode!=13);" /></td>
                            <td></td>
                            <td ><asp:TextBox ID="txtDatFollowUp" runat="server" Width="95px" PlaceHolder="Follow-Up Date" onkeydown = "return (event.keyCode!=13);" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="4"><asp:TextBox ID="txtVcFuComment" runat="server" Rows="7" Width="425px" TextMode="MultiLine" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnAppendFollowUp" runat="server" Text="Append <=" OnClick="btnAppendFollowUp_Click" /><br />
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumInitialRow" runat="server" onkeydown = "return (event.keyCode!=13);" visible="false" />
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" Height="80px">
                    <asp:GridView ID="GridView2" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                        EnableModelValidation="True" ForeColor="#333333" DataKeyNames="numRow"
                        OnRowEditing="GridView2_RowEditing" OnRowCancelingEdit="GridView2_RowCancelling"
                        OnRowDeleting="GridView2_RowDeleting" OnRowUpdating="GridView2_RowUpdating">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <div style="width:40px">
                                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div style="width:40px">
                                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Update" Text="Update"></asp:LinkButton>
                                    <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                    </div>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="vcAcctNo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtVcAcctNo" runat="server" Text='<%# Bind ("vcAcctNo") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle horizontalalign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblVcAcctNo" runat="server" Text='<%# Eval("vcAcctNo") %>' OnClick="btnPreview_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="vcClient">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtVcClient" runat="server" Text='<%# Bind ("vcClient") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblVcClient" runat="server" Text='<%# Eval("vcClient") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="vcPatName">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtVcPatName" runat="server" Text='<%# Bind ("vcPatName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblVcPatName" runat="server" Text='<%# Eval("vcPatName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="vcPatSSN">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtVcPatSSN" runat="server" Text='<%# Bind ("vcPatSSN") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblVcPatSSN" runat="server" Text='<%# Eval("vcPatSSN") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="vcPatIns">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtVcPatIns" runat="server" Text='<%# Bind ("vcPatIns") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblVcPatIns" runat="server" Text='<%# Eval("vcPatIns") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="vcPatInsNo">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtVcPatInsIdNo" runat="server" Text='<%# Bind ("vcPatInsIdNo") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblVcPatInsIdNo" runat="server" Text='<%# Eval("vcPatInsIdNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="decTotalChgs">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDecTotalChgs" runat="server" Text='<%# Bind ("decTotalChgs") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDecTotalChgs" runat="server" Text='<%# Eval("decTotalChgs") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="decExpected">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDecExpected" runat="server" Text='<%# Bind ("decExpected") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDecExpected" runat="server" Text='<%# Eval("decExpected") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="vcUpCategory">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtVcUpCategory" runat="server" Text='<%# Bind ("vcUpCategory") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle horizontalalign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblVcUpCategory" runat="server" Text='<%# Eval("vcUpCategory") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </div>
        <br />
        <div style="float:left; height:5%">
            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
            &emsp;

	  <input runat="server" id="Hidden1" type="hidden" />
	  <input runat="server" id="File1" type="file" onchange="setHiddenValue()" style="display:none" />
	  <input id="Button2" type="button" onclick="triggerFileUpload()" value="Upload" />

        </div>
        <div style="float:right; max-height:5%; margin-right:20px">
<a href="http://192.168.161.126/tips/" target="popup" onclick="window.open('http://192.168.161.126/tips/','popup','width=800,height=800'); return false;"><img src="Img/help_icon.jpg" style="height:15px" /></a>
            <asp:Label ID="lblCustomSQL" runat="server" Text="Sort / Filter:"/>
            <asp:TextBox ID="txtCustomSQL" runat="server" Width="350px"></asp:TextBox>
            <asp:Button ID="btnQuery" runat="server" Text="Run" OnClick="btnCustomSQL_Click" />
        </div>
        <br /><br />
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
            EnableModelValidation="True" ForeColor="#333333" ShowFooter="True" DataKeyNames="numRow"
            OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelling"
            OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" OnSorting="GridView1_Sorting">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <div style="width:50px">
                        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                        <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div style="width:50px">
                        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Update" Text="Update"></asp:LinkButton>
                        <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                        </div>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FollowUpComments" SortExpression="FollowUpComments">
                    <ItemTemplate>
                        <div style="width:250px">
                        <asp:Label ID="lblFollowUpComments" runat="server" Text='
                            <%# Convert.ToString(Eval("FollowUpComments")).Replace("||","<br /><br />").Replace("|","<br />&emsp;") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                    <FooterStyle backcolor="White" horizontalalign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="datComment" SortExpression="datComment" >
                    <ItemStyle horizontalalign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblDatComment" runat="server" Text='<%# Eval("datComment").ToString().Split()[0] %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle backcolor="White" horizontalalign="Center" />
                    <FooterTemplate>
                        <asp:LinkButton ID="InsertButton" runat="server" Text="Add New Account <=" OnClick="InsertButton_Click"></asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcCommentBy" SortExpression="vcCommentBy" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcCommentBy" runat="server" Text='<%# Bind ("vcCommentBy") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle horizontalalign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblVcCommentBy" runat="server" Text='<%# Eval("vcCommentBy") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcCommentBy" runat="server" onkeydown = "return (event.keyCode!=13);" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcComment" SortExpression="vcComment" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcComment" runat="server" Rows="3" TextMode="MultiLine" Text='<%# Bind ("vcComment") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVcComment" runat="server" Text='<%# Eval("vcComment").ToString().Replace(Environment.NewLine, "<br />") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcComment" runat="server" Rows="3" TextMode="MultiLine" onkeydown = "return (event.keyCode!=13);" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcAcctNo" SortExpression="vcAcctNo" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcAcctNo" runat="server" Text='<%# Bind ("vcAcctNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle horizontalalign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lblVcAcctNo" runat="server" Text='<%# Eval("vcAcctNo") %>' OnClick="btnPreview_Click"></asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcAcctNo" runat="server" onkeydown = "return (event.keyCode!=13);" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcClient" SortExpression="vcClient" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcClient" runat="server" Text='<%# Bind ("vcClient") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVcClient" runat="server" Text='<%# Eval("vcClient") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcClient" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcPatName" SortExpression="vcPatName" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcPatName" runat="server" Text='<%# Bind ("vcPatName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVcPatName" runat="server" Text='<%# Eval("vcPatName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcPatName" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcPatSSN" SortExpression="vcPatSSN" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcPatSSN" runat="server" Text='<%# Bind ("vcPatSSN") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVcPatSSN" runat="server" Text='<%# Eval("vcPatSSN") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcPatSSN" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcPatIns" SortExpression="vcPatIns" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcPatIns" runat="server" Text='<%# Bind ("vcPatIns") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVcPatIns" runat="server" Text='<%# Eval("vcPatIns") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcPatIns" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcPatInsNo" SortExpression="vcPatInsNo" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcPatInsIdNo" runat="server" Text='<%# Bind ("vcPatInsIdNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVcPatInsIdNo" runat="server" Text='<%# Eval("vcPatInsIdNo") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcPatInsIdNo" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="decTotalChgs" SortExpression="decTotalChgs" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDecTotalChgs" runat="server" Text='<%# Bind ("decTotalChgs") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDecTotalChgs" runat="server" Text='<%# Eval("decTotalChgs") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerDecTotalChgs" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="decExpected" SortExpression="decExpected" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDecExpected" runat="server" Text='<%# Bind ("decExpected") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDecExpected" runat="server" Text='<%# Eval("decExpected") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerDecExpected" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcUpCategory" SortExpression="vcUpCategory" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcUpCategory" runat="server" Text='<%# Bind ("vcUpCategory") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle horizontalalign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblVcUpCategory" runat="server" Text='<%# Eval("vcUpCategory") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcUpCategory" runat="server" onkeydown = "return (event.keyCode!=13);" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <EmptyDataTemplate>
                <tr style="background-color: #5D7B9D; color:white; font-weight:bold; text-decoration:underline">
                    <th scope="col">
                        
                    </th>
                    <th scope="col">
                        vcCommentBy
                    </th>
                    <th scope="col">
                       vcComment
                    </th>
                    <th scope="col">
                        vcAcctNo
                    </th>
                     <th scope="col">
                        vcClient
                    </th>
                     <th scope="col">
                        vcPatName
                    </th>
                     <th scope="col">
                        vcPatSSN
                    </th>
                     <th scope="col">
                        vcPatIns
                    </th>
                     <th scope="col">
                        vcPatInsIdNo
                    </th>
                     <th scope="col">
                        decTotalChgs
                    </th>
                    <th scope="col">
                        decExpected
                    </th>
                    <th scope="col">
                        vcUpCategory
                    </th>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="AddNewButton" runat="server" Text="Add New" OnClick="InsertButton_Click"  />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcCommentBy" runat="server" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcComment" runat="server" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                     <td>
                        <asp:TextBox ID="footerVcAcctNo" runat="server" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcClient" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcPatName" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcPatSSN" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcPatIns" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcPatInsIdNo" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                     <td>
                        <asp:TextBox ID="footerDecTotalChgs" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerDecExpected" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcUpCategory" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                </tr>
            </EmptyDataTemplate>
        </asp:GridView>
        <br /><br />
        <asp:Table ID="Table1" runat="server" Style="margin-left:30px" GridLines="Horizontal" BorderStyle="Solid">
        </asp:Table>

        <br /><br />
    </form>

    <script type="text/javascript">

        $(document).ready(function () {
            gridviewScroll('#GridView1', $(window).height() / 2, 2);
            gridviewScroll('#GridView2', 80, 1);
        });

        $(window).on('resize', function () {
            gridviewScroll('#GridView1', $(window).height() / 2, 2);
            gridviewScroll('#GridView2', 80, 1);
        });

	    function gridviewScroll(sId, iHeight, iFreezeSize) {
	        gridView1 = $(sId).gridviewScroll({
                width: "100%",
                height: iHeight,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: iFreezeSize,
                arrowsize: 30,
                varrowtopimg: "Img/arrowvt.png",
                varrowbottomimg: "Img/arrowvb.png",
                harrowleftimg: "Img/arrowhl.png",
                harrowrightimg: "Img/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize: 8
            });
	    }
	</script>
</body>
</html>