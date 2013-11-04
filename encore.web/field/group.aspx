﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master/twocolumn.master" AutoEventWireup="true" Inherits="field_group" Codebehind="group.aspx.cs" %>
<%@ Register src="~/controls/TextBoxControl.ascx" tagPrefix="encore" tagName="TextBox" %>
<%@ Register src="~/controls/SelectControl.ascx" tagPrefix="encore" tagName="Select" %>
<%@ Register src="~/controls/FormButtons.ascx" tagPrefix="encore" tagName="Buttons" %>

<%@ Register Assembly="com.Encore.jControls" TagPrefix="jquery" Namespace="com.Encore.jControls"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <fieldset class="panel dataentry">
        <div class="dataentryrow">
            <encore:TextBox ID="txtName" FieldSize="Medium" Required="true" runat="server" Label="Group Name:"  />
        </div>
    </fieldset>
    <fieldset class="panel dataentry">
        <label>Add Fields to Group</label>
        <p>Search and drag fields from the left hand box to the right hand box to add them to the group</p>
        <jquery:jSortable runat="server" ID="jsGroupFields" CssClass="connected" DataURL="fielddata.aspx"
        Title="Add Fields to Group" HelpText="Search for fields to add to this group"/>
    </fieldset>
    <encore:Buttons ID="btnFormButtons" runat="server" OnSave="btnSubmit_Click" OnDelete="btnDelete_Click" />              
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SupportingContent" runat="server">
    <h2>Help</h2>
    <div class="panel">
    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc gravida imperdiet dui. Donec posuere, purus vel ultrices consequat, dui massa iaculis leo, vitae facilisis libero dolor eget diam. Vestibulum bibendum venenatis leo in varius. Maecenas et pellentesque est. In auctor mi ac tortor lobortis porttitor. Phasellus sit amet sagittis orci. Vestibulum id felis id odio feugiat eleifend. Sed sed lectus mi. Sed vitae massa felis. Maecenas id tellus in lorem facilisis eleifend eget quis odio. Sed turpis diam, varius eget dapibus non, tincidunt congue quam. Mauris vel elit lacus.</p>
    </div>
</asp:Content>