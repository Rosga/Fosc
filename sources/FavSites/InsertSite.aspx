<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InsertSite.aspx.cs" Inherits="FavSites.InsertSite" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
            <asp:Label ID="lblSiteName" runat="server" Text="Enter name of the site"></asp:Label>
            <br />
            <asp:TextBox ID="txtSiteName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSiteName" runat="server" 
                ErrorMessage="Please fill this field" ControlToValidate="txtSiteName"></asp:RequiredFieldValidator>
            <br />

            <asp:Label ID="lblSiteLink" runat="server" Text="Enter link to the site"></asp:Label>
            <br />
            <asp:TextBox ID="txtSiteLink" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSiteLink" runat="server" ErrorMessage="Please fill this field" ControlToValidate="txtSiteLink"></asp:RequiredFieldValidator>
            <br />

            <asp:Button ID="btnAddSite" runat="server" Font-Size="Medium" Height="33px" Text="Add" Width="61px" OnClick="btnAddSite_Click" />
</asp:Content>
