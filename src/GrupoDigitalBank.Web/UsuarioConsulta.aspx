<%@ Page Title="Usuario consulta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UsuarioConsulta.aspx.cs" Inherits="GrupoDigitalBank.Web.UsuarioConsulta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="page-title">
        <p class="eyebrow">Consulta y mantenimiento</p>
        <h1>Usuario consulta</h1>
        <p>Listado de usuarios registrados con opciones de modificar y eliminar.</p>
    </section>

    <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert">
        <asp:Literal ID="litMensaje" runat="server" />
    </asp:Panel>

    <section class="table-panel" aria-labelledby="tituloConsulta">
        <div class="table-heading">
            <h2 id="tituloConsulta">Usuarios registrados</h2>
            <a class="btn btn-primary" href="Usuario.aspx">Nuevo usuario</a>
        </div>

        <asp:GridView ID="gvUsuarios" runat="server"
            AutoGenerateColumns="false"
            DataKeyNames="Id"
            CssClass="data-grid"
            GridLines="None"
            EmptyDataText="No hay usuarios registrados."
            OnRowCommand="gvUsuarios_RowCommand">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha de nacimiento" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" />
                <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <div class="row-actions">
                            <asp:HyperLink ID="lnkModificar" runat="server" CssClass="text-link" Text="Modificar" NavigateUrl='<%# Eval("Id", "~/Usuario.aspx?id={0}") %>' />
                            <asp:LinkButton ID="lnkEliminar" runat="server"
                                CssClass="danger-link"
                                Text="Eliminar"
                                CommandName="EliminarUsuario"
                                CommandArgument='<%# Eval("Id") %>'
                                OnClientClick="return confirm('Desea eliminar este usuario?');" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </section>
</asp:Content>
