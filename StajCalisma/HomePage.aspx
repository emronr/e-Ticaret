<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="design.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }

        .auto-style3 {
            width: 147px;
        }

        .auto-style7 {
            width: 169px;
            margin-left: 0px;
        }

        .auto-style9 {
            width: 277px;
        }

        .auto-style10 {
            width: 295px;
        }

        .auto-style12 {
            width: 184px;
        }

        </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-2">
            <br />
            <br />
            <br />
        </div>
        <div class="col-6">
            <asp:TextBox ID="TextBox1" runat="server" BorderStyle="Dotted" BorderColor="Olive"> </asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:Button ID="btnFilter" runat="server" Text="Ara" OnClick="btnFilter_Click" />
        </div>
        <div class="col-4">
        </div>
    </div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="col-12">
         <center>
        <table class="auto-style1" border="1" id="l">
            <thead>
                <tr class="warning">
                    <th class="auto-style10">Görsel</th>
                    <th class="auto-style12">İsim</th>
                    <th class="auto-style7">Marka</th>
                    <th class="auto-style9">Özellikleri</th>
                    <th class="auto-style3">Fiyat</th>
                    <th class="auto-style3">Adet</th>
                    <th class="auto-style3">Sepete Ekle</th>
            </thead>
            <tbody>
                <asp:Repeater ID="rptProduct" runat="server" OnItemCommand="rptProduct_ItemCommand">


                    <ItemTemplate>
                        <td>
                            <img src='ProductImage/<%#Eval("pimage") %>' width="200" /></td>
                        <td><%# Eval("pname") %></td>
                        <td><%# Eval("brand") %> </td>
                        <td><%# Eval("comment") %></td>
                        <td><b><%# Eval("price") %> TL</b></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtAdet" Text="1" CommandName="Adet"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button runat="server" CommandName="AddHamper" CommandArgument='<%# Eval("pid") %>' ID="button2" Text="Sepete Ekle" />
                        </td>
                        <tr class="active">
                        </tr>
                    </ItemTemplate>

                </asp:Repeater>
            </tbody>
        </table>
             </center>
    </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <p>
        &nbsp;
    </p>
    
</asp:Content>

