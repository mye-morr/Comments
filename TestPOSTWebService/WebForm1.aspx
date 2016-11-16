<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TestPOSTWebService.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
        <hr />

        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
            EnableModelValidation="True" ForeColor="#333333" ShowFooter="True" DataKeyNames="numRow"
            OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelling"
            OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <emptydatarowstyle backcolor="LightBlue" forecolor="Red"/>
            <emptydatatemplate>No Data Found.</emptydatatemplate> 
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
                <asp:TemplateField HeaderText="vcClient" SortExpression="vcClient" >
                    <ItemStyle horizontalalign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblVcClient" runat="server" Text='<%# Eval("vcClient") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcClient" runat="server" onkeydown = "return (event.keyCode!=13);" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="vcRefNo" SortExpression="vcRefNo" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtVcRefNo" runat="server" Text='<%# Bind ("vcRefNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblVcRefNo" runat="server" Text='<%# Eval("vcRefNo") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="footerVcRefNo" runat="server" onkeydown = "return (event.keyCode!=13);" Text="(Optional)" />
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
        </asp:GridView>
    </form>
</body>
</html>