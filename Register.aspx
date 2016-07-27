<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" CreateUserButtonText="Odeslat" OnCreatedUser="CreateUserWizard1_CreatedUser" OnCreateUserError="CreateUserWizard1_CreateUserError"
        DuplicateUserNameErrorMessage="Vaše uživatelské jméno již někdo používá.<br/>Zkuste zadat jiné."
        RequireEmail="true"
        DuplicateEmailErrorMessage="Tento e-mail je již používán."
        InvalidPasswordErrorMessage="Heslo musí mít minimálně {0} znaků."
        LoginCreatedUser="false">
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server">
                <ContentTemplate>
                    <table border="0">
                        <tr>
                            <td align="center" colspan="2">
                                <strong>Registrace nového zákazníka</strong></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Přihlašovací jméno:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                    ErrorMessage="Není vyplněno přihlašovací jméno." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Heslo:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                    ErrorMessage="Není vyplněno heslo." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Heslo znovu:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                    ErrorMessage="Zadejte heslo znovu pro ověření."
                                    ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                    ErrorMessage="Není vyplněn e-mail." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">Doručovací adresa</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="FirstName">Jméno:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="FirstName"
                                    ErrorMessage="Není vyplněno jméno." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="LastNameLabel" runat="server" AssociatedControlID="LastName">Příjmení:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="LastName"
                                    ErrorMessage="Není vyplněno příjmení." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="StreetLabel" runat="server" AssociatedControlID="Street">Ulice:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="Street" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="StreetRequired" runat="server" ControlToValidate="Street"
                                    ErrorMessage="Není vyplněna ulice." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="CityLabel" runat="server" AssociatedControlID="City">Obec:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="City" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CityRequired" runat="server" ControlToValidate="City"
                                    ErrorMessage="Není vyplněna obec." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ZipCodeLabel" runat="server" AssociatedControlID="ZipCode">PSČ:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="ZipCode" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ZipCodeRequired" runat="server" ControlToValidate="ZipCode"
                                    ErrorMessage="Není vyplněno PSČ." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                    ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="Zadaná hesla se neshodují."
                                    ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color: red">
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep runat="server">
                <ContentTemplate>
                    <table border="0">
                        <tr>
                            <td align="center" colspan="2">
                                <strong>Registrace nového zákazníka</strong></td>
                        </tr>
                        <tr>
                            <td>Blahopřejeme, uspěšně jste se zaregistroval. Nyní se můžete přihlásit.</td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">&nbsp;</td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
        <TextBoxStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
    </asp:CreateUserWizard>
</asp:Content>
