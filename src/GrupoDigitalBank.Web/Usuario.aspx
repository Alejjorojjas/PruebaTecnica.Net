<%@ Page Title="Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="GrupoDigitalBank.Web.Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="page-title">
        <p class="eyebrow">Capa de presentacion</p>
        <h1>Usuario</h1>
        <p>Registra usuarios nuevos o modifica la informacion existente desde el servicio WCF.</p>
    </section>

    <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert">
        <asp:Literal ID="litMensaje" runat="server" />
    </asp:Panel>

    <section class="form-panel" aria-labelledby="tituloFormulario">
        <h2 id="tituloFormulario">Datos del usuario</h2>
        <asp:ValidationSummary ID="vsResumen" runat="server" CssClass="validation-summary" HeaderText="Revisa los siguientes campos:" />

        <div class="form-grid">
            <div class="form-field">
                <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" Text="Nombre" />
                <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" CssClass="input-control" />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre es obligatorio." Display="Dynamic" CssClass="field-error" />
            </div>

            <div class="form-field">
                <asp:Label ID="lblFechaNacimiento" runat="server" AssociatedControlID="txtFechaNacimiento" Text="Fecha de nacimiento" />
                <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date" CssClass="input-control" />
                <asp:RequiredFieldValidator ID="rfvFechaNacimiento" runat="server" ControlToValidate="txtFechaNacimiento" ErrorMessage="La fecha de nacimiento es obligatoria." Display="Dynamic" CssClass="field-error" />
            </div>

            <div class="form-field">
                <asp:Label ID="lblSexo" runat="server" AssociatedControlID="ddlSexo" Text="Sexo" />
                <asp:DropDownList ID="ddlSexo" runat="server" CssClass="input-control" />
                <asp:RequiredFieldValidator ID="rfvSexo" runat="server" ControlToValidate="ddlSexo" InitialValue="" ErrorMessage="El sexo es obligatorio." Display="Dynamic" CssClass="field-error" />
            </div>
        </div>

        <div class="actions">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" CausesValidation="false" OnClick="btnCancelar_Click" />
            <a class="text-link" href="UsuarioConsulta.aspx">Ver usuarios registrados</a>
        </div>
    </section>
</asp:Content>
