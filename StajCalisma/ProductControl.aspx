<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProductControl.aspx.cs" Inherits="ProductControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="design.css" rel="stylesheet" />
    <style type="text/css">
       
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
        .auto-style13 {
            width: 116px;
        }
        .auto-style14 {
            width: 183px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <center>
        <h2 class="style2">
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           Ürün Kayıt</h2><table class="style1">
           <tr><td class="style2">
                     Ürün Adı</td>
                <td>
                    <asp:TextBox ID="TextBox1" 

                    runat="server"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Marka</td>
                <td>
                    <asp:TextBox ID="TextBox2" 

                    runat="server"></asp:TextBox>
                    <br />
                </td>
            </tr>
           <tr>
               <td class="style2">
                   Açıklama</td>
               <td>
                   <asp:TextBox ID="TextBox3"
                     
                    runat="server" TextMode="MultiLine"></asp:TextBox>
                   <br />
               </td>
           </tr>
            <tr>
               <td class="style2">
                   Fiyat</td>
               <td>
                   <asp:TextBox ID="TextBox4"
                     
                    runat="server"></asp:TextBox>
                   <br />
               </td>
           </tr>
            <tr>
               <td class="style2">
                   Adet</td>
               <td>
                   <asp:TextBox ID="TextBox6"
                     
                    runat="server"></asp:TextBox>
                   <br />
               </td>
           </tr>
            <tr>
               <td class="style2">
                   Görsel</td>
               <td>
                   <br />
                   <asp:FileUpload ID="UploadTest" runat="server" Height="21px" style="margin-bottom: 16px" Width="235px" />
               </td>
           </tr>
           <tr>
               <td>
                   <asp:Button ID="btnUrunEkle" runat="server" OnClick="btnUrunEkle_Click" Text="Kayıt Ol" />

               </td>
           </tr>
        </table>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
         <p>
             &nbsp;</p>
       
        <p>
            <asp:TextBox ID="TextBox5" runat="server" Height="16px" Width="246px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <asp:Button ID="btnFilter" runat="server" Text="Ara" OnClick="btnFilter_Click" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>
               <p>
     <table class="auto-style1" border="1">
        <thead>
	   <tr class="warning">
           <th style="width: 5px;">ID</th>
          <th class="auto-style10">Görsel</th> 
	      <th class="auto-style12">İsim</th>
	      <th class="auto-style7">Marka</th>
          <th class="auto-style9">Özellik</th>
	      <th class="auto-style14">Fiyat</th>
	      <th class="auto-style13">Adet</th>

         
        </thead>
        <tbody>
             <asp:Repeater ID="rptView" runat="server" OnItemCommand="rptView_ItemCommand"  >
           
           
      <ItemTemplate>  
        <td><%# Eval("pid") %></td>
        <td><img src='ProductImage/<%#Eval("pimage") %>'  width="200" /></td>
        <td><%# Eval("pname") %></td> 
        <td><%# Eval("brand") %> </td>
        <td><%# Eval("comment") %></td>
        <td><b><%# Eval("price") %> TL</b></td>
          <td><%# Eval("stoch") %></td>
         <td><asp:Button runat="server" CommandName="Delete" CommandArgument='<%# Eval("pid") %>' ID="button2" Text="Sil" /> </td>
        <tr class="active">
        
    </tr>
        </ItemTemplate>

</asp:Repeater>
            </tbody>
         </table>
            </p></ContentTemplate>
                </asp:UpdatePanel>
                 <p>
                     &nbsp;</p>
    </center>
     <p align="right"  >  &nbsp;</p>
</asp:Content>

