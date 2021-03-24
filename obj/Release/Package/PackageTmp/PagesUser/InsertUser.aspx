<%@ Page Title="Регистрация пользователя" Language="C#" MasterPageFile="~/PagesUser/User.Master" AutoEventWireup="true" CodeBehind="InsertUser.aspx.cs" Inherits="WEB_SMBIE.PagesUser.RuleBS" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>

    <div>
        <asp:Table ID="Table1" runat="server" CellPadding="10">
            <asp:TableRow runat="server">
                 <asp:TableHeaderCell>
                     ФИО
                 </asp:TableHeaderCell>
                 <asp:TableCell>
                     <asp:TextBox ID="TextBoxFIO" runat="server" Columns="100"></asp:TextBox>
                 </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableHeaderCell>
                     Логин
                 </asp:TableHeaderCell>
                 <asp:TableCell>
                     <asp:TextBox ID="TextBoxLogin" runat="server" Columns="100"></asp:TextBox>
                 </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableHeaderCell>
                    Пароль
                 </asp:TableHeaderCell>
                 <asp:TableCell>
                     <asp:TextBox ID="TextBoxPass" runat="server" Columns="100"></asp:TextBox>
                 </asp:TableCell>
            </asp:TableRow>
             <asp:TableRow runat="server">
                <asp:TableHeaderCell>
                    Пароль
                 </asp:TableHeaderCell>
                 <asp:TableCell>
                     <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="LinqDataSource1" DataTextField="Name" DataValueField="Name"></asp:DropDownList>
                     <asp:LinqDataSource runat="server" EntityTypeName="" ID="LinqDataSource1" ContextTypeName="BDSystemSMBIEContext.BDSystemSMBIEDataContext" GroupBy="Name" Select="new (key as Name, it as Positions)" TableName="Positions"></asp:LinqDataSource>
                 </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableHeaderCell>
                    Краткая характиристика
                 </asp:TableHeaderCell>
                 <asp:TableCell>
                     <asp:TextBox ID="TextBox1" runat="server" Columns="200" Rows="5" TextMode="MultiLine"></asp:TextBox>
                 </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="ButtonInsert" runat="server" Text="Зарегестрировать" />
    </div>

</asp:Content>
