<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TestPOSTWebService.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_Click" />
        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
        <hr />
        <asp:Label ID="Label1" runat="server">To see sample dialog, <br /> please input an Id and click Preview</asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
            EnableModelValidation="True" ForeColor="#333333" ShowFooter="True" DataKeyNames="numRow"
            OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelling"
            OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" OnSorting="GridView1_Sorting">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                        <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Update" Text="Update"></asp:LinkButton>
                        <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="datComment" SortExpression="datComment" >
                    <ItemTemplate>
                        <asp:Label ID="lblDatComment" runat="server" Text='<%# Eval("datComment") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle backcolor="White" horizontalalign="Center" />
                    <FooterTemplate>
                        <asp:LinkButton ID="InsertButton" runat="server" Text="Append This Comment <=" OnClick="InsertButton_Click"></asp:LinkButton>
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
                <asp:TemplateField HeaderText="idClaim" SortExpression="idClaim" >
                    <ItemStyle horizontalalign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblIdClaim" runat="server" Text='<%# Eval("idClaim") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerIdClaim" runat="server" onkeydown = "return (event.keyCode!=13);" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcCategory" SortExpression="vcCategory" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcCategory" runat="server" Text='<%# Bind ("vcCategory") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVcCategory" runat="server" Text='<%# Eval("vcCategory") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcCategory" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
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
                <asp:TemplateField HeaderText="decVariance" SortExpression="decVariance" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDecVariance" runat="server" Text='<%# Bind ("decVariance") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDecVariance" runat="server" Text='<%# Eval("decVariance") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerDecVariance" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcClient" SortExpression="vcClient" >
                    <ItemStyle horizontalalign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblVcClient" runat="server" Text='<%# Eval("vcClient") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcClient" runat="server" onkeydown = "return (event.keyCode!=13);" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcInsurance" SortExpression="vcInsurance" >
                    <ItemStyle horizontalalign="Center" />
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcInsurance" runat="server" Text='<%# Bind ("vcInsurance") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVcInsurance" runat="server" Text='<%# Eval("vcInsurance") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcInsurance" runat="server" onkeydown = "return (event.keyCode!=13);"  Text="(Optional)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcContactName" SortExpression="vcContactName" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcContactName" runat="server" Text='<%# Bind ("vcContactName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVcContactName" runat="server" Text='<%# Eval("vcContactName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcContactName" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcContactPhone" SortExpression="vcContactPhone" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcContactPhone" runat="server" Text='<%# Bind ("vcContactPhone") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVcContactPhone" runat="server" Text='<%# Eval("vcContactPhone") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcContactPhone" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcContactEmail" SortExpression="vcContactEmail" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcContactEmail" runat="server" Text='<%# Bind ("vcContactEmail") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVcContactEmail" runat="server" Text='<%# Eval("vcContactEmail") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcContactEmail" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcCallRefNo" SortExpression="vcCallRefNo" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcCallRefNo" runat="server" Text='<%# Bind ("vcCallRefNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVcCallRefNo" runat="server" Text='<%# Eval("vcCallRefNo") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcCallRefNo" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="datFollowUp" SortExpression="datFollowUp" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDatFollowUp" runat="server" Text='<%# Bind ("datFollowUp") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDatFollowUp" runat="server" Text='<%# Eval("datFollowUp") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerDatFollowUp" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcNotes" SortExpression="vcNotes" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcNotes" runat="server" Rows="3" TextMode="MultiLine" Text='<%# Bind("vcNotes") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVcNotes" runat="server" Text='<%# Eval("vcNotes").ToString().Replace(Environment.NewLine, "<br />") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcNotes" runat="server" Rows="3" TextMode="MultiLine" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
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
                        idClaim
                    </th>
                     <th scope="col">
                        vcCategory
                    </th>
                     <th scope="col">
                        vcClient
                    </th>
                     <th scope="col">
                        vcInsurance
                    </th>
                     <th scope="col">
                        vcContactName
                    </th>
                     <th scope="col">
                        vcContactPhone
                    </th>
                     <th scope="col">
                        vcContactEmail
                    </th>
                    <th scope="col">
                        vcCallRefNo
                    </th>
                    <th scope="col">
                       datFollowUp
                    </th>
                    <th scope="col">
                        vcNotes
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
                        <asp:TextBox ID="footerIdClaim" runat="server" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcCategory" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcClient" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcInsurance" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcContactName" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcContactPhone" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcContactEmail" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                     <td>
                        <asp:TextBox ID="footerVcCallRefNo" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerDatFollowUp" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                    <td>
                        <asp:TextBox ID="footerVcNotes" runat="server" Text="(Optional)" onkeydown = "return (event.keyCode!=13);" />
                    </td>
                </tr>
            </EmptyDataTemplate>
        </asp:GridView>
        <br /><br />
        <asp:Table ID="Table1" runat="server" Style="margin-left:30px" GridLines="Horizontal" BorderStyle="Solid">
        </asp:Table>

    </form>
</body>
</html>