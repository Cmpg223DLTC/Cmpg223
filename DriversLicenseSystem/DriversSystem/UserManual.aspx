<%@ Page Title="User Manual" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserManual.aspx.cs" Inherits="DriversSystem.UserManual" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .manual-panel {
            background-color: white;
            border: 2px solid black;
            border-radius: 10px;
            padding: 30px;
            margin-top: 50px;
            max-width: 800px;
            margin-left: auto;
            margin-right: auto;
        }

        .panel-heading {
            font-size: 26px;
            color: #007bff;
            margin-bottom: 20px;
            text-align: center;
        }

        .pdf-container {
            text-align: center;
            margin-top: 30px;
        }

        .pdf-frame {
            width: 100%;
            height: 600px;
            border: none;
        }
    </style>

    <div class="manual-panel">
        <div class="pdf-container">
            <iframe src="<%= ResolveUrl("~/Manuals/UserManual.pdf") %>" class="pdf-frame"></iframe>
        </div>
    </div>

</asp:Content>
