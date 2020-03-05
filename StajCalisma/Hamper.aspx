<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Hamper.aspx.cs" Inherits="Hamper"  %>

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

        .auto-style3 {
            width: 148px;
            height: 27px;
        }

        .auto-style4 {
            width: 195px;
            height: 27px;
        }

        .auto-style5 {
            width: 134px;
            height: 27px;
        }

        .auto-style6 {
            width: 123px;
            height: 27px;
        }

        .auto-style8 {
            height: 27px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
        <center>
             <h2 class="style2">
           &nbsp;&nbsp;&nbsp;&nbsp;Sepetim</h2>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <tr class="warning">
               <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
                  </tr>
               <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>
               <p>
        

    <table class="auto-style1" border="1" id="tblHamperContract">
        <thead>
	   <tr class="warning">
          <th class="auto-style8">HamperID</th>
          <th class="auto-style8">Görsel</th>
	      <th class="auto-style2">İsim</th>
	      <th class="auto-style1">Marka</th>
          <th class="auto-style4">Özelikleri</th>
          <th class="auto-style5">Adet Fiyatı</th>
	      <th class="auto-style6">Adet</th>
          <th class="auto-style5">Toplam Fiyat</th>
          <th class="auto-style3">Tarih</th>
          <th class="auto-style3">Güncelle</th>
          <th class="auto-style3">Sil</th>
	   </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptHampers" runat="server" OnItemCommand="rptHampers_ItemCommand" >
            
           
             <ItemTemplate>  
             <tr>
        <td><%# Eval("hamperid") %></td>
        <td><img src='ProductImage/<%#Eval("pimage") %>'  width="150" /></td>
        <td><%# Eval("pname") %></td>
        <td><%# Eval("brand") %> </td>
        <td><%# Eval("comment") %></td>
        <td><b><%# Eval("price") %>  TL</b></td>
        <td>  <asp:TextBox runat="server" ID="txtAdet" Text='<%# Eval("quantity") %>' CommandName="Adet" Width="100" ></asp:TextBox>
            <asp:TextBox runat="server" ID="txtAdetOrj" Text='<%# Eval("quantity") %>' CommandName="Adet" visible="false"></asp:TextBox>
          <%--  <input type="button" onclick="alert('Hello World!')" value="Click Me!">
             <div class="quantity-wrapper">
                            <div class="input-group">
                              

                            </div>

                            <div class="update-item-quantity">
                                <a href="javascript://" onclick="typeof utag !== 'undefined' ? utag.link({event_category: 'Cart', event_action: 'Click', event_label: 'UpdateQuantity'}) : " data-bind="click: $parent.update">Güncelle</a>
                                 <a href="javascript://" data-bind="click: $parent.remove, attr: { 'data-hbus': davinciDeleteProduct }">Ürünü Sil</a>
                            </div>

                        </div> 
          --%>   </td>
        <td><b><%# Eval("tprice") %>  TL</b></td>
        <td><%# Eval("date") %></td>
        
        <td> <asp:Button AutoPostBack="true" runat="server" CommandName="UpdateQuantityHamper"  CommandArgument='<%# Eval("hamperid") %>' ID="button1" Text="Güncelle" Text-Align="center" Width="100" /></td>
        <td><asp:Button AutoPostBack="true" runat="server" CommandName="DeleteHamper"  CommandArgument='<%# Eval("hamperid") %>' ID="button2" Text="Ürünü Sil"  Width="100"/></td>
             <tr>
                  

        </ItemTemplate>

</asp:Repeater>
            </tbody>
        </table>
            </p>
                   

                   <h2 class="style2">
                 <p>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Label ID="Label3" runat="server" Text="Toplam Fiyat :"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                         

                     <asp:Label ID="lblTotalPrice" runat="server" BorderStyle="None" Font-Bold="True"></asp:Label>
                     &nbsp;TL<h2></h2>
                     <h2></h2>
                     <h2></h2>
                     <h2></h2>
                     <h2></h2>
                     </h2>
               </ContentTemplate>
                </asp:UpdatePanel>
            </center>
              <asp:Button ID="btnConfirm" runat="server" Text="SEPETİ ONAYLA" align="right" OnClick="btnConfirm_Click"></asp:Button>

        <p align="right">&nbsp;</p>
        </asp:Content>

