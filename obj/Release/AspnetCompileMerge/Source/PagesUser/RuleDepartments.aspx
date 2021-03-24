<%@ Page Title="Управление Подразделениями" Language="C#" AutoEventWireup="true" MasterPageFile="~/PagesUser/User.Master" CodeBehind="RuleDepartments.aspx.cs" Inherits="WEB_SMBIE.PagesUser.RuleDeparment" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%:Title %></h2>
    <asp:Label ID="Labelcorrent" runat="server" Text=""></asp:Label>
    <hr />
    <div class="Tree">
        <h3>Дерево подразделений: </h3>
        <asp:TreeView ID="TreeView1" runat="server" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
            <Nodes>
            </Nodes>
        </asp:TreeView>
        <hr />
    </div>

    <asp:Panel ID="PanelShowWorker" runat="server">
        <table>
            <tr>
                <td>
                    <h4 style="width: 219px; height: 18px">
                        <asp:Label Text="Глава подразделения" runat="server" />
                    </h4>
               </td>
                <td>
                    <asp:Button ID="btnEditHead" runat="server" Text="Изменить" OnClick="btnEditHead_Click"/>        
               </td>
           </tr>
        </table>
        

        <asp:GridView CssClass="table" ID="GridView2" runat="server" DataSourceID="LinqDataSource2" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True">
            <Columns>
                <asp:BoundField DataField="Id" Visible="false" HeaderText="Id" ReadOnly="True" SortExpression="Id"></asp:BoundField>
                <asp:BoundField DataField="FIO" HeaderText="FIO" ReadOnly="True" SortExpression="FIO"></asp:BoundField>
                <asp:BoundField DataField="Phone" HeaderText="Phone" ReadOnly="True" SortExpression="Phone"></asp:BoundField>
                <asp:BoundField DataField="Character" HeaderText="Character" ReadOnly="True" SortExpression="Character"></asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:button ID="btnNextProfile" runat="server" OnClick="btnNextProfile_Click" Text="Перейти"></asp:button>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:LinqDataSource runat="server" EntityTypeName="" ID="LinqDataSource2" ContextTypeName="BDSystemSMBIEContext.BDSystemSMBIEDataContext" Select="new (Id, FIO, Phone, Character, Department)" TableName="Users" Where="IdDep == @IdDep">
            <WhereParameters>
                <asp:ControlParameter ControlID="TreeView1" DefaultValue="1" PropertyName="SelectedValue" Name="IdDep" Type="Int32"></asp:ControlParameter>
            </WhereParameters>
        </asp:LinqDataSource>

    </asp:Panel>

    <asp:Panel ID="PanelTableUnit" runat="server">
            <div class="btn">
                <asp:Button ID="ButtonAdd" runat="server" Text="Добавить" OnClick="ButtonAdd_Click" />
            </div>
            <h4>
                <asp:Label Text="Подконтрольные подразделения" runat="server" />
            </h4>
            <asp:GridView ID="GridView1" CssClass="table" runat="server" DataSourceID="LinqDataSource1" AutoGenerateColumns="False" DataKeyNames="Id" AllowPaging="True" AllowSorting="True" OnRowDeleted="GridView1_RowDeleted" OnRowDeleting="GridView1_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="Id" Visible="false" HeaderText="Id" ReadOnly="True" InsertVisible="False" SortExpression="Id"></asp:BoundField>
                    <asp:BoundField DataField="TypeDepartment.Name" ReadOnly="True" HeaderText="IdType" SortExpression="TyprDepartment.Name"></asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="Name" ApplyFormatInEditMode="true" SortExpression="Name"></asp:BoundField>
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description"></asp:BoundField>
                    <asp:BoundField DataField="IdUserHead" ReadOnly="True" HeaderText="IdUserHead" SortExpression="IdUserHead"></asp:BoundField>
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True"></asp:CommandField>
                </Columns>
            </asp:GridView>

            <asp:LinqDataSource runat="server" EntityTypeName="" ID="LinqDataSource1" ContextTypeName="BDSystemSMBIEContext.BDSystemSMBIEDataContext" TableName="Departments" EnableDelete="True" EnableInsert="True" EnableUpdate="True" Where="IdParentDep == @IdParentDep">
                <WhereParameters>
                    <asp:ControlParameter ControlID="TreeView1" DefaultValue="1" PropertyName="SelectedValue" Name="IdParentDep" Type="Int32"></asp:ControlParameter>
                </WhereParameters>
            </asp:LinqDataSource>

        </asp:Panel>

    <asp:Panel ID="PanelTableWorker" Visible="false" runat="server">
        <h3>Незанятые работники</h3>
        <br />
        <asp:Button ID="ButtonCreatePerson" runat="server" Text="Создать профиль работника" OnClick="CreatePerson_Click" />
        <br />
        <br />
        <asp:Button ID="ButtonCancelSelectWork" runat="server" Text="Отмена" OnClick="ButtonCancelSelectWork_Click"/>
        <br />
        <asp:GridView CssClass="table" ID="GridView3" runat="server" DataSourceID="LinqDataSource3" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" OnSelectedIndexChanged="GridView3_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                <asp:BoundField DataField="Id" Visible="false" HeaderText="Id" ReadOnly="True" SortExpression="Id"></asp:BoundField>
                <asp:BoundField DataField="FIO" HeaderText="FIO" ReadOnly="True" SortExpression="FIO"></asp:BoundField>
                <asp:BoundField DataField="Character" HeaderText="Character" ReadOnly="True" SortExpression="Character"></asp:BoundField>
                <asp:BoundField DataField="idDep" HeaderText="idDep" ReadOnly="True" SortExpression="idDep"></asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:LinqDataSource runat="server" EntityTypeName="" ID="LinqDataSource3" ContextTypeName="BDSystemSMBIEContext.BDSystemSMBIEDataContext" Select="new (Id, FIO, Character, idDep)" TableName="Users" Where="IdDep == @IdDep">
            <WhereParameters>
                <asp:Parameter DefaultValue="0" Name="IdDep" Type="Int32"></asp:Parameter>
            </WhereParameters>
        </asp:LinqDataSource>
    </asp:Panel>

    <asp:Panel ID="PanelCreateEdit" Visible="false" runat="server">
            <h3>
                <asp:Label ID="LabelOperation" runat="server" Text="Создание подразделения"></asp:Label>
            </h3>
            <asp:Table CssClass="table" ID="Table1" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:label text="Название" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </asp:TableCell></asp:TableRow><asp:TableRow>
                    <asp:TableCell>
                        <asp:label text="Описание" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="100"></asp:TextBox>
                    </asp:TableCell></asp:TableRow></asp:Table><asp:Button ID="ButtonInsert" runat="server" Text="Создать" OnClick="ButtonInsert_Click" />
            <asp:Button ID="ButtonCancel" runat="server" Text="Отменить" OnClick="ButtonCancel_Click" />

        </asp:Panel>
    
    <asp:Panel ID="PanelCreatePerson" Visible="false" runat="server">
            <asp:Table ID="Table2" CssClass="table" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:label text="Логин" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
                    </asp:TableCell></asp:TableRow><asp:TableRow>

                    <asp:TableCell>
                        <asp:label text="Пароль" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    </asp:TableCell></asp:TableRow><asp:TableRow>

                    <asp:TableCell>
                        <asp:label text="ФИО" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="txtFIO" runat="server"></asp:TextBox>
                    </asp:TableCell></asp:TableRow><asp:TableRow>

                    <asp:TableCell>
                        <asp:label text="Телефон" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                    </asp:TableCell></asp:TableRow><asp:TableRow>

                    <asp:TableCell>
                        <asp:label text="Краткая характиристика" runat="server" />
                    </asp:TableCell><asp:TableCell>
                        <asp:TextBox ID="txtCharacter" TextMode="MultiLine" Rows="5" Columns="100" runat="server"></asp:TextBox>
                    </asp:TableCell></asp:TableRow></asp:Table><div class="Button">
                <asp:Button ID="btnCreatePerson" CssClass="btn" runat="server" Text="Завершить регистрацию" OnClick="btnCreatePerson_Click" />
                <asp:Button ID="btnCancelPerson" CssClass="btn" runat="server" Text="Отменить" OnClick="btnCancelPerson_Click" />
            </div>
        </asp:Panel>
   
</asp:Content>
