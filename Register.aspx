<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" MasterPageFile="~/MasterPage.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" CreateUserButtonText="Odeslat" OnCreatedUser="CreateUserWizard1_CreatedUser" OnCreateUserError="CreateUserWizard1_CreateUserError"
        DuplicateUserNameErrorMessage="Va�e u�ivatelsk� jm�no ji� n�kdo pou��v�.<br/>Zkuste zadat jin�."
        RequireEmail="true"
        DuplicateEmailErrorMessage="Tento e-mail je ji� pou��v�n."
        InvalidPasswordErrorMessage="Heslo mus� m�t minim�ln� {0} znak�."
        LoginCreatedUser="false">
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server">
                <ContentTemplate>
                    <table border="0">
                        <tr>
                            <td align="center" colspan="2">
                                <strong>Registrace nov�ho z�kazn�ka</strong></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">P�ihla�ovac� jm�no:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                    ErrorMessage="Nen� vypln�no p�ihla�ovac� jm�no." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Heslo:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                    ErrorMessage="Nen� vypln�no heslo." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Heslo znovu:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                    ErrorMessage="Zadejte heslo znovu pro ov��en�."
                                    ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                    ErrorMessage="Nen� vypln�n e-mail." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">Doru�ovac� adresa</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="FirstName">Jm�no:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="FirstName"
                                    ErrorMessage="Nen� vypln�no jm�no." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="LastNameLabel" runat="server" AssociatedControlID="LastName">P��jmen�:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="LastName"
                                    ErrorMessage="Nen� vypln�no p��jmen�." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="StreetLabel" runat="server" AssociatedControlID="Street">Ulice:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="Street" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="StreetRequired" runat="server" ControlToValidate="Street"
                                    ErrorMessage="Nen� vypln�na ulice." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="CityLabel" runat="server" AssociatedControlID="City">Obec:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="City" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CityRequired" runat="server" ControlToValidate="City"
                                    ErrorMessage="Nen� vypln�na obec." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ZipCodeLabel" runat="server" AssociatedControlID="ZipCode">PS�:</asp:Label></td>
                            <td align="left">
                                <asp:TextBox ID="ZipCode" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ZipCodeRequired" runat="server" ControlToValidate="ZipCode"
                                    ErrorMessage="Nen� vypln�no PS�." ValidationGroup="CreateUserWizard1" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                    ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="Zadan� hesla se neshoduj�."
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
                                <strong>Registrace nov�ho z�kazn�ka</strong></td>
                        </tr>
                        <tr>
                            <td>Blahop�ejeme, usp�n� jste se zaregistroval. Nyn� se m��ete p�ihl�sit.</td>
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
