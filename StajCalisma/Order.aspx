<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <title></title>
    <link href="design.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 130px;
            height: 27px;
        }
        .auto-style2 {
            width: 160px;
            height: 27px;
        }
        .auto-style4 {
            width: 195px;
            height: 27px;
        }
        .auto-style8 {
            height: 27px;
        }
        .auto-style10 {
            height: 27px;
            width: 69px;
        }
        .auto-style14 {
            height: 27px;
            width: 10px;
        }
        .auto-style15 {
            height: 27px;
            width: 47px;
        }
        .auto-style19 {
            height: 27px;
            width: 80px;
        }
        .auto-style25 {
            width: 146px;
            height: 27px;
        }
        </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <center>
             <h2 class="style2">
           &nbsp;&nbsp;&nbsp;&nbsp;SİPARİŞ GEÇMİŞİM</h2>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <tr class="warning">
               <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
                  </tr>
               <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>
               <p>
        

    <table class="table table-bordered table-striped csstable" border="1">
        <thead>
	   <tr class="warning">
          <th class="auto-style8">OrderID</th>
	      <th class="auto-style2">Toplam Fatura Tutarı</th>
	      <th class="auto-style1">Ürün Çeşidi</th>
          <th class="auto-style4">Sipariş Tarihi</th>
	   </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptOrder" runat="server"   OnItemCommand="rptOrder_ItemCommand" >
            
           
             <ItemTemplate>  
             <tr>
        <td><%# Eval("orderid") %></td>
        <td><b><%# Eval("price") %>  TL</b></td>
        <td><%# Eval("item") %></td>
        <td><%# Eval("date") %></td>
         <td><asp:Button AutoPostBack="true" runat="server" CommandName="Detay"  CommandArgument='<%# Eval("orderid") %>' ID="button2" Text="Detaylar" /> </td>
             </tr>
        
      
        </ItemTemplate>

</asp:Repeater>
            </tbody>
        </table>
            </p>

                        <p>
        

    <table class="table table-bordered table-striped csstable" border="1">
        <thead>
	   <tr class="warning">
          <th class="auto-style25">Görsel</th>
          <th class="auto-style14">Ürün Adı</th>
          <th class="auto-style15">Marka</th>
          <th class="auto-style10">Özellikler</th>
	      <th class="auto-style19">Ürün Tutarı</th>
	      <th class="auto-style1">Ürün Adedi</th>
          <th class="auto-style4">Sipariş Tarihi</th>
	   </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptOrderDetail" runat="server" Visible="true"  >
            
           
             <ItemTemplate>  
               
             <tr>
       <td><img src='ProductImage/<%#Eval("pimage") %>'  width="100" /></td>
        <td><%# Eval("pname") %></td>
        <td><%# Eval("brand") %></td>
        <td><%# Eval("comment") %></td>
        <td><b><%# Eval("price") %>  TL</b></td>
        <td><%# Eval("quantity") %></td>
        <td><%# Eval("date") %></td>
            </tr>
                   
        </ItemTemplate>

</asp:Repeater>
            </tbody>
        </table>
            </p>
               </ContentTemplate>
                </asp:UpdatePanel>
            </center>
</asp:Content>

