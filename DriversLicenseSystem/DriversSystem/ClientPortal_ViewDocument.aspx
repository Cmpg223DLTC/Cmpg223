﻿<%@ Page Title="Admin Portal - Reports" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ClientPortal_ViewDocument.aspx.cs" Inherits="DriversSystem.ClientPortal_ViewDocument" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-container {
            max-width: 800px;
            margin: 50px auto;
            background-color: #f8f9fa;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
        }

        .form-container h2 {
            text-align: center;
            margin-bottom: 20px;
            font-weight: bold;
            color: #28a745;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-label {
            font-weight: bold;
            margin-bottom: 5px;
            display: block;
            color: #333;
        }

        .form-control-static {
            padding: 10px;
            background-color: #e9ecef;
            border-radius: 5px;
            border: 1px solid #ddd;
            font-size: 16px;
            display: block;
            color: #495057;
        }

        .btn-container {
            text-align: center;
            margin-top: 20px;
        }

        .custom-btn {
            padding: 10px 20px;
            font-size: 16px;
            font-weight: bold;
            background-color: #28a745;
            color: white;
            border: 2px solid #28a745;
            border-radius: 5px;
            transition: all 0.3s ease;
            cursor: pointer;
        }

        .custom-btn:hover {
            background-color: white;
            color: #28a745;
            border-color: #28a745;
        }

        /* Print styles */
        @media print {
            body {
                -webkit-print-color-adjust: exact !important;
            }
            .form-container {
                box-shadow: none;
                border: none;
            }
            .btn-container {
                display: none; /* Hide buttons during print */
            }
        }
    </style>

    <div class="form-container">
        <h2>Application Details</h2>

        <!-- Name -->
        <div class="form-group">
            <label for="NameLabel" class="form-label">Name</label>
            <asp:Label ID="NameLabel" runat="server" CssClass="form-control-static"></asp:Label>
        </div>

        <!-- Surname -->
        <div class="form-group">
            <label for="SurnameLabel" class="form-label">Surname</label>
            <asp:Label ID="SurnameLabel" runat="server" CssClass="form-control-static"></asp:Label>
        </div>

        <!-- ID Number -->
        <div class="form-group">
            <label for="IDNumberLabel" class="form-label">ID Number</label>
            <asp:Label ID="IDNumberLabel" runat="server" CssClass="form-control-static"></asp:Label>
        </div>

        <!-- Address -->
        <div class="form-group">
            <label for="AddressLabel" class="form-label">Address</label>
            <asp:Label ID="AddressLabel" runat="server" CssClass="form-control-static"></asp:Label>
        </div>

        <!-- Address Number -->
        <div class="form-group">
            <label for="AddressNumberLabel" class="form-label">Address Number</label>
            <asp:Label ID="AddressNumberLabel" runat="server" CssClass="form-control-static"></asp:Label>
        </div>

        <!-- Timeslot -->
        <div class="form-group">
            <label for="TimeslotLabel" class="form-label">Timeslot</label>
            <asp:Label ID="TimeslotLabel" runat="server" CssClass="form-control-static"></asp:Label>
        </div>

        <!-- Date -->
        <div class="form-group">
            <label for="DateLabel" class="form-label">Date</label>
            <asp:Label ID="DateLabel" runat="server" CssClass="form-control-static"></asp:Label>
        </div>

        <!-- Service -->
        <div class="form-group">
            <label for="ServiceLabel" class="form-label">Service</label>
            <asp:Label ID="ServiceLabel" runat="server" CssClass="form-control-static"></asp:Label>
        </div>

        <!-- Button Container -->
        <div class="btn-container">
            <asp:Button ID="PrintButton" runat="server" Text="Print" CssClass="custom-btn" OnClientClick="window.print();return false;" />
        </div>
    </div>
</asp:Content>