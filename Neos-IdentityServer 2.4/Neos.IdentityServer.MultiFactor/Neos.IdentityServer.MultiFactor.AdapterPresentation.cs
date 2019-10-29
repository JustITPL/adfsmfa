﻿//******************************************************************************************************************************************************************************************//
// Copyright (c) 2019 Neos-Sdi (http://www.neos-sdi.com)                                                                                                                                    //                        
//                                                                                                                                                                                          //
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),                                       //
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,   //
// and to permit persons to whom the Software is furnished to do so, subject to the following conditions:                                                                                   //
//                                                                                                                                                                                          //
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.                                                           //
//                                                                                                                                                                                          //
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,                                      //
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,                            //
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.                               //
//                                                                                                                                                                                          //
// https://adfsmfa.codeplex.com                                                                                                                                                             //
// https://github.com/neos-sdi/adfsmfa                                                                                                                                                      //
//                                                                                                                                                                                          //
//******************************************************************************************************************************************************************************************//
using System;
using Microsoft.IdentityServer.Web.Authentication.External;
using Neos.IdentityServer.MultiFactor.Resources;
using Neos.IdentityServer.MultiFactor;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Neos.IdentityServer.MultiFactor.Common;

namespace Neos.IdentityServer.MultiFactor
{
    public class AdapterPresentationDefault : BasePresentation
    {
        #region Constructors
        /// <summary>
        /// Constructor implementation
        /// </summary>
        public AdapterPresentationDefault(AuthenticationProvider provider, IAuthenticationContext context):base(provider, context)
        {
        }

        /// <summary>
        /// Constructor overload implementation
        /// </summary>
        public AdapterPresentationDefault(AuthenticationProvider provider, IAuthenticationContext context, string message): base(provider, context, message)
        {
        }

        /// <summary>
        /// Constructor overload implementation
        /// </summary>
        public AdapterPresentationDefault(AuthenticationProvider provider, IAuthenticationContext context, string message, bool ismessage): base(provider, context, message, ismessage)
        {
        }

        /// <summary>
        /// Constructor overload implementation
        /// </summary>
        public AdapterPresentationDefault(AuthenticationProvider provider, IAuthenticationContext context, ProviderPageMode suite): base(provider, context, suite)
        {
        }

        /// <summary>
        /// Constructor overload implementation
        /// </summary>
        public AdapterPresentationDefault(AuthenticationProvider provider, IAuthenticationContext context, string message, ProviderPageMode suite, bool disableoptions = false)
            : base(provider, context, message, suite, disableoptions)
        {
        }
        #endregion

        #region CSS
        /// <summary>
        /// GetFormPreRenderHtmlCSS implementation
        /// </summary>
        protected override string GetFormPreRenderHtmlCSS(AuthenticationContext usercontext, bool removetag = false)
        {
            string result = string.Empty;

            result += "#buttonquit, #gotoinscription {";
            result += "border:none;";
            result += "background-color:rgb(232, 17, 35);";
            result += "min-width:120px;";
            result += "width:auto;";
            result += "height:30px;";
            result += "padding:4px 20px 6px 20px;";
            result += "border-style:solid;";
            result += "border-width:1px;";
            result += "transition:background 0s;";
            result += "color:rgb(255, 255, 255);";
            result += "cursor:pointer;";
            result += "margin-bottom:8px;";
            result += "-ms-user-select:none;";
            result += "-moz-transition:background 0s;";
            result += "-webkit-transition:background 0s;";
            result += "-o-transition:background 0s;";
            result += "-webkit-touch-callout:none;";
            result += "-webkit-user-select:none;";
            result += "-khtml-user-select:none;";
            result += "-moz-user-select: none;";
            result += "-o-user-select: none;";
            result += "user-select:none;";
            result += "}" + "\r\n";

            result += "#buttonquit:hover {";
            result += "background-color:rgb(170, 0, 0);";
            result += "}" + "\r\n";

            string baseresult = base.GetFormPreRenderHtmlCSS(usercontext, true);
            if (!removetag)
                return "<style>" + baseresult + result + "</style>";
            return result + "\r\n";
        }
        #endregion

        #region Identification
        /// <summary>
        /// GetFormPreRenderHtmlIdentification implementation
        /// </summary>
        public override string GetFormPreRenderHtmlIdentification(AuthenticationContext usercontext)
        {
            string result = "<script type='text/javascript'>" + "\r\n";

            result += "function SetLinkData(frm, data)";
            result += "{";
            result += "var lnk = document.getElementById('lnk');";
            result += "if (lnk)";
            result += "{";
            result += "lnk.value = data;";
            result += "frm.submit();";
            result += "return true;";
            result += "}";
            result += "return false;";
            result += "}" + "\r\n";

            result += "</script>" + "\r\n";
            return result;
        }

        /// <summary>
        /// GetFormHtmlIdentification implementation
        /// </summary>
        public override string GetFormHtmlIdentification(AuthenticationContext usercontext)
        {
            string result = "<form method=\"post\" id=\"IdentificationForm\" autocomplete=\"off\">";
            IExternalProvider prov = RuntimeAuthProvider.GetProvider(usercontext.PreferredMethod);
            if ((prov != null) && (prov.IsUIElementRequired(usercontext, RequiredMethodElements.CodeInputRequired)))
            {
                result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + prov.GetUILabel(usercontext) + "</div>";
                result += "<input id=\"totp\" name=\"totp\" type=\"password\" placeholder=\"Code\" class=\"text fullWidth\" autofocus=\"autofocus\" /></br>";
                result += "<div class=\"fieldMargin smallText\">" + prov.GetUIMessage(usercontext) + "</div></br>";
                if (!string.IsNullOrEmpty(prov.GetUIWarningThirdPartyLabel(usercontext)) && (usercontext.IsSendBack))
                    result += "<div class=\"error smallText\">" + prov.GetUIWarningThirdPartyLabel(usercontext) + "</div></br>";
            }
            if ((prov != null) && (prov.IsUIElementRequired(usercontext, RequiredMethodElements.PinParameterRequired)))
            {
                result += "<div id=\"wizardMessage2\" class=\"groupMargin\">" + BaseExternalProvider.GetPINLabel(usercontext) + "</div>";
                result += "<input id=\"pincode\" name=\"pincode\" type=\"password\" placeholder=\"PIN Number\" class=\"text fullWidth\" /></br>";
                result += "<div class=\"fieldMargin smallText\">" + BaseExternalProvider.GetPINMessage(usercontext) + "</div></br>";
            }

            if (Provider.HasAccessToOptions(prov))
            {
                if (usercontext.ShowOptions)
                    result += "<input id=\"options\" type=\"checkbox\" name=\"Options\" checked=\"true\" /> " + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMAccessOptions") + "</br></br></br>";
                else
                    result += "<input id=\"options\" type=\"checkbox\" name=\"Options\" /> " + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMAccessOptions") + "</br></br></br>";
            }
            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";
            result += "<input id=\"lnk\" type=\"hidden\" name=\"lnk\" value=\"0\"/>";
            result += "<input id=\"continueButton\" type=\"submit\" class=\"submit\" name=\"Continue\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMConnexion") + "\" /></br></br>";
            if (RuntimeAuthProvider.GetActiveProvidersCount()>1)
                result += "<a class=\"actionLink\" href=\"#\" id=\"nocode\" name=\"nocode\" onclick=\"return SetLinkData(IdentificationForm, '3')\"; style=\"cursor: pointer;\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMNoCode") + "</a>";
            result += GetFormHtmlMessageZone(usercontext);
            result += "</form>";
            return result;
        }
        #endregion

        #region ManageOptions
        /// <summary>
        /// GetFormPreRenderHtmlManageOptions implementation
        /// </summary>
        public override string GetFormPreRenderHtmlManageOptions(AuthenticationContext usercontext)
        {
            string reg = @"/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,})+$/";
            string pho = @"/^\+(?:[0-9] ?){6,14}[0-9]$/";
            string pho10 = @"/^\d{10}$/";
            string phous = @"/^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/";

            string result = "<script type='text/javascript'>" + "\r\n";

            result += "function ValidateOptions(frm)";
            result += "{";
            result += "var mailformat = " + reg + " ;";
            result += "var phoneformat = " + pho + " ;";
            result += "var phoneformat10 = " + pho10 + " ;";
            result += "var phoneformatus = " + phous + " ;";
            result += "var email = document.getElementById('email');";
            result += "var phone = document.getElementById('phone');";
            result += " var err = document.getElementById('errorText');";

            result += "var lnk = document.getElementById('btnclicked');";
            result += "if (lnk.value=='1')";
            result += "{";

            if (RuntimeAuthProvider.IsUIElementRequired(usercontext, RequiredMethodElements.EmailParameterRequired))
            {
                result += "if ((email) && (email.value=='') && (email.placeholder==''))";
                result += "{";
                result += "   err.innerHTML = \"" + Resources.GetString(ResourcesLocaleKind.Validation, "ValidIncorrectEmail") + "\";";
                result += "   return false;";
                result += "}";
                result += "if ((email) && (email.value!==''))";
                result += "{";
                result += "   if (!email.value.match(mailformat))";
                result += "   {";
                result += "      err.innerHTML = \"" + Resources.GetString(ResourcesLocaleKind.Validation, "ValidIncorrectEmail") + "\";";
                result += "      return false;";
                result += "   }";
                result += "}";
            }
            if (RuntimeAuthProvider.IsUIElementRequired(usercontext, RequiredMethodElements.PhoneParameterRequired))
            {
                result += "if ((phone) && (phone.value=='') && (phone.placeholder==''))";
                result += "{";
                result += "   err.innerHTML = \"" + Resources.GetString(ResourcesLocaleKind.Validation, "ValidIncorrectPhoneNumber") + "\";";
                result += "   return false;";
                result += "}";
                result += "if ((phone) && (phone.value!==''))";
                result += "{";
                result += "   if (!phone.value.match(phoneformat) && !phone.value.match(phoneformat10) && !phone.value.match(phoneformatus) )";
                result += "   {";
                result += "      err.innerHTML = \"" + Resources.GetString(ResourcesLocaleKind.Validation, "ValidIncorrectPhoneNumber") + "\";";
                result += "      return false;";
                result += "   }";
                result += "}";
            }
            result += "}";

            result += "err.innerHTML = \"\";";
            result += "return true;";
            result += "}";
            result += "\r\n";

            result += "function fnlinkclicked(frm, data)";
            result += "{";
            result += "var lnk = document.getElementById('btnclicked');";
            result += "lnk.value = data;";
            result += "frm.submit();";
            result += "return true;";
            result += "}" + "\r\n";

            result += "function fnbtnclicked(id)";
            result += "{";
            result += "var lnk = document.getElementById('btnclicked');";
            result += "lnk.value = id;";
            result += "return true;";
            result += "}" + "\r\n";
            result += "</script>" + "\r\n";
            return result;
        }

        /// <summary>
        /// GetFormHtmlManageOptions implementation
        /// </summary>
        public override string GetFormHtmlManageOptions(AuthenticationContext usercontext)
        {
            string result = string.Empty;
            result += "<form method=\"post\" id=\"OptionsForm\" autocomplete=\"off\" onsubmit=\"return ValidateOptions(this)\" >";
            result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + Resources.GetString(ResourcesLocaleKind.Titles, "ManageOptionsPageTitle") + "</div>";

            IExternalProvider prov1 = RuntimeAuthProvider.GetProvider(PreferredMethod.Code);
            if ((prov1 != null) && (prov1.Enabled) && prov1.IsUIElementRequired(usercontext, RequiredMethodElements.OTPLinkRequired))
            {
                result += "<a class=\"actionLink\" href=\"#\" id=\"enrollopt\" name=\"enrollopt\" onclick=\"fnlinkclicked(OptionsForm, 3)\" style=\"cursor: pointer;\">" + prov1.GetWizardLinkLabel(usercontext) + "</a>";
            }
            IExternalProvider prov2 = RuntimeAuthProvider.GetProvider(PreferredMethod.Email);
            if ((prov2 != null) && (prov2.Enabled) && prov2.IsUIElementRequired(usercontext, RequiredMethodElements.EmailLinkRequired))
            {
                result += "<a class=\"actionLink\" href=\"#\" id=\"enrollemail\" name=\"enrollemail\" onclick=\"fnlinkclicked(OptionsForm, 4)\" style=\"cursor: pointer;\">" + prov2.GetWizardLinkLabel(usercontext) + "</a>";
            }
            IExternalProvider prov3 = RuntimeAuthProvider.GetProvider(PreferredMethod.External);
            if ((prov3 != null) && (prov3.Enabled) && prov3.IsUIElementRequired(usercontext, RequiredMethodElements.PhoneLinkRequired))
            {
                result += "<a class=\"actionLink\" href=\"#\" id=\"enrollphone\" name=\"enrollphone\" onclick=\"fnlinkclicked(OptionsForm, 5)\" style=\"cursor: pointer;\">" + prov3.GetWizardLinkLabel(usercontext) + "</a>";
            }
            IExternalProvider prov4 = RuntimeAuthProvider.GetProvider(PreferredMethod.Biometrics);
            if ((prov4 != null) && (prov4.Enabled) && prov4.IsUIElementRequired(usercontext, RequiredMethodElements.BiometricParameterRequired))
            {
                result += "<a class=\"actionLink\" href=\"#\" id=\"enrollbio\" name=\"enrollbio\" onclick=\"fnlinkclicked(OptionsForm, 6)\" style=\"cursor: pointer;\">" + prov4.GetWizardLinkLabel(usercontext) + "</a>";
            }
            if (RuntimeAuthProvider.IsPinCodeRequired(usercontext))
            {
                result += "<a class=\"actionLink\" href=\"#\" id=\"enrollpin\" name=\"enrollpin\" onclick=\"fnlinkclicked(OptionsForm, 7)\" style=\"cursor: pointer;\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlEnrollPinCode") + "</a>";
            }
            result += "</br>";

            if (Provider.Config.KeepMySelectedOptionOn)
            {
                result += GetPartHtmlSelectMethod(usercontext);
                result += "</br>";
            }
            if (!Provider.Config.UserFeatures.IsMFARequired() && !Provider.Config.UserFeatures.IsMFAMixed())
            {
                result += "<input id=\"disablemfa\" type=\"checkbox\" name=\"disablemfa\" /> " + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMDisableMFA");
                result += "</br>";
            }
            result += "</br>";
            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";

            result += "<input id=\"btnclicked\" type=\"hidden\" name=\"btnclicked\" value=\"0\" />";

            result += "<table><tr>";
            result += "<td>";
            result += "<input id=\"saveButton\" type=\"submit\" class=\"submit\" name=\"continue\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDSave") + "\" onclick=\"fnbtnclicked(1)\" />";
            result += "</td>";
            if (!Provider.Config.UserFeatures.IsMFAMixed() || (usercontext.Enabled && usercontext.IsRegistered))
            {
                result += "<td style=\"width: 15px\" />";
                result += "<td>";
                result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onclick=\"fnbtnclicked(2)\" />";
                result += "</td>";
            }
            result += "</tr></table>";
            result += GetFormHtmlMessageZone(usercontext);
            result += "</form>";
            return result;
        }
        #endregion

        #region Registration
        /// <summary>
        /// GetFormPreRenderHtmlRegistration implementation
        /// </summary>
        public override string GetFormPreRenderHtmlRegistration(AuthenticationContext usercontext)
        {
            string result = "<script type='text/javascript'>" + "\r\n";

            result += "function OnRefreshPost(frm)";
            result += "{";
            result += "   document.getElementById('registrationForm').submit();";
            result += "   return true;";
            result += "}";

            result += "function fnbtnclicked(id)";
            result += "{";
            result += "   var lnk = document.getElementById('btnclicked');";
            result += "   lnk.value = id;";
            result += "}" + "\r\n";

            result += "</script>" + "\r\n";
            return result;
        }

        /// <summary>
        /// GetFormHtmlRegistration implementation
        /// </summary>
        public override string GetFormHtmlRegistration(AuthenticationContext usercontext)
        {
            string result = string.Empty;
            bool IsRequired = true;
            PreferredMethod m = PreferredMethod.Choose;
            if (usercontext.EnrollPageStatus == EnrollPageStatus.Run)
            {
                m = Utilities.FindNextWizardToPlay(usercontext, ref IsRequired);
                if (m != PreferredMethod.None)
                    usercontext.EnrollPageStatus = EnrollPageStatus.NewStep;
                else
                {
                    result = "<script type='text/javascript'>" + "\r\n";
                    result += "if (window.addEventListener)";
                    result += "{";
                    result += "   window.addEventListener('load', OnRefreshPost, false);";
                    result += "}";
                    result += "else if (window.attachEvent)";
                    result += "{";
                    result += "   window.attachEvent('onload', OnRefreshPost);";
                    result += "}";
                    result += "\r\n";
                    result += "</script>" + "\r\n";
                }
            }
            result += "<form method=\"post\" id=\"registrationForm\" autocomplete=\"off\" >";

            switch (usercontext.EnrollPageStatus)
            {
                case EnrollPageStatus.Start:
                    result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + Resources.GetString(ResourcesLocaleKind.Titles, "RegistrationPageTitle") + "</div>";
                    result += "<div id=\"pwdMessage\" class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Errors, "ErrorAccountAuthorized") + "</div></br>";
                    List<IExternalProvider> lst = RuntimeAuthProvider.GeActiveProvidersList();
                    if (lst.Count > 0)
                    {
                        result += "<div id=\"xMessage\" class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMustPrepareLabel") + "</div>";
                        foreach (IExternalProvider itm in lst)
                        {
                            if (itm.IsRequired)
                                result += "<div id=\"reqvalue\" class=\"fieldMargin smallText\">- " + itm.GetUIEnrollmentTaskLabel(usercontext) + "</div>";
                            else
                                result += "<div id=\"reqvalue\" class=\"fieldMargin smallText\">- " + itm.GetUIEnrollmentTaskLabel(usercontext) + " (*)" + "</div>";
                        }
                        if (RuntimeAuthProvider.IsPinCodeRequired(usercontext))
                            result += "<div id=\"reqvalue\" class=\"fieldMargin smallText\">- " + Resources.GetString(ResourcesLocaleKind.Html, "HtmlEnrollPinTaskLabel") + "</div>";
                        result += "</br>";
                    }

                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"gotoregistration\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMGoToRegistration") + "\" onclick=\"fnbtnclicked(2)\"/>";
                    result += "</td>";
                    result += "<td style=\"width: 15px\" />";
                    if (!Provider.Config.UserFeatures.IsRegistrationMixed())
                    {
                        result += "<td>";
                        result += "<input id=\"continueButton\" type=\"submit\" class=\"submit\" name=\"Continue\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMConnexion") + "\" onclick=\"fnbtnclicked(1)\" />";
                        result += "</td>";
                    }
                    result += "</tr></table>";
                    break;
                case EnrollPageStatus.NewStep:
                    if (m != PreferredMethod.Pin)
                    {
                        IExternalProvider prov = RuntimeAuthProvider.GetProviderInstance(m);
                        result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + prov.GetWizardUILabel(usercontext) + "</div>";
                        result += "<div id=\"pwdMessage\" class=\"fieldMargin smallText\">" + prov.GetUIEnrollmentTaskLabel(usercontext) + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIEnrollContinue") + "</div></br>";
                        result += "<table><tr>";
                        result += "<td>";
                        result += "<input id=\"continueButton\" type=\"submit\" class=\"submit\" name=\"Continue\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelWVERIFYOTP") + "\" onclick=\"fnbtnclicked(2)\" />";
                        result += "</td>";
                        result += "<td style=\"width: 15px\" />";
                        if ((prov.Enabled) && (!prov.IsRequired))
                        {
                            result += "<td>";
                            result += "<input id=\"skipoption\" type=\"submit\" class=\"submit\" name=\"skipoption\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUINext") + "\" onclick=\"fnbtnclicked(6)\"/>";
                            result += "</td>";
                        }
                        result += "</tr></table>";
                    }
                    else
                    {   // Pin Code not provider
                        result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + BaseExternalProvider.GetPINWizardUILabel(usercontext) + "</div>";
                        result += "<div id=\"pwdMessage\" class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlEnrollPinTaskLabel") + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIEnrollContinue") + "</div></br>";
                        result += "<table><tr>";
                        result += "<td>";
                        result += "<input id=\"continueButton\" type=\"submit\" class=\"submit\" name=\"Continue\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelWVERIFYOTP") + "\" onclick=\"fnbtnclicked(2)\" />";
                        result += "</td>";
                        result += "</tr></table>";
                    }
                    break;
                case EnrollPageStatus.Stop:
                    result += "</br>";
                    result += "<p style=\"text-align:center\"><img id=\"msgreen\" src=\"data:image/png;base64," + Convert.ToBase64String(images.cvert.ToByteArray(ImageFormat.Jpeg)) + "\"/></p></br></br>";
                    result += "<div id=\"lbl\" class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelVERIFYOTPSuccess") + "</div></br>";
                    List<IExternalProvider> lst2 = RuntimeAuthProvider.GeActiveProvidersList();
                    if (lst2.Count > 0)
                    {
                        result += "<div id=\"xMessage\" class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIResultLabel") + "</div>";
                        foreach (IExternalProvider itm in lst2)
                        {
                            string value = string.Empty;
                            switch (itm.Kind)
                            {
                                default:
                                case PreferredMethod.Code:
                                    if (usercontext.KeyStatus == SecretKeyStatus.Success)
                                        value = "OK";
                                    else
                                        value = "<empty>";
                                    break;
                                case PreferredMethod.Email:
                                    value = Utilities.StripEmailAddress(usercontext.MailAddress);
                                    break;
                                case PreferredMethod.External:
                                    value = Utilities.StripPhoneNumber(usercontext.PhoneNumber);
                                    break;
                            }

                            if (itm.IsRequired)
                                result += "<div id=\"reqvalue\" class=\"fieldMargin smallText\">- " + itm.GetUIListOptionLabel(usercontext) + " : " + value + "</div>";
                            else
                                result += "<div id=\"reqvalue\" class=\"fieldMargin smallText\">- " + itm.GetUIListOptionLabel(usercontext) + " (*) : " + value + "</div>";
                        }
                        if (RuntimeAuthProvider.IsPinCodeRequired(usercontext))
                            result += "<div id=\"reqvalue\" class=\"fieldMargin smallText\">- " + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIPinOptionLabel") + " : " + Utilities.StripPinCode(usercontext.PinCode) + "</div>";
                        result += "</br>";
                    }
                    result += "<input id=\"finishButton\" type=\"submit\" class=\"submit\" name=\"finishButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelVERIFYOTPOK") + "\" onclick=\"fnbtnclicked(5)\" />";
                    result += "</br>";
                    break;
            }

            result += "<input id=\"isprovider\" type=\"hidden\" name=\"isprovider\" value=\"" + (int)m + "\" />";
            result += "<input id=\"btnclicked\" type=\"hidden\" name=\"btnclicked\" value=\"0\" />";
            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";

            result += "</form>";
            return result;
        }
        #endregion

        #region Invitation
        /// <summary>
        /// GetFormPreRenderHtmlInvitation implementation
        /// </summary>
        public override string GetFormPreRenderHtmlInvitation(AuthenticationContext usercontext)
        {
            string result = "<script type='text/javascript'>" + "\r\n";

            result += "function OnRefreshPost(frm)";
            result += "{";
            result += "   document.getElementById('invitationForm').submit();";
            result += "   return true;";
            result += "}";

            result += "function fnbtnclicked(id)";
            result += "{";
            result += "   var lnk = document.getElementById('btnclicked');";
            result += "   lnk.value = id;";
            result += "}" + "\r\n";

            result += "</script>" + "\r\n";
            return result;
        }

        /// <summary>
        /// GetFormHtmlInvitation implementation
        /// </summary>
        public override string GetFormHtmlInvitation(AuthenticationContext usercontext)
        {
            string result = string.Empty;
            bool IsRequired = true;
            PreferredMethod m = PreferredMethod.Choose;
            if (usercontext.EnrollPageStatus == EnrollPageStatus.Run)
            {
                m = Utilities.FindNextWizardToPlay(usercontext, ref IsRequired);
                if (m != PreferredMethod.None)
                    usercontext.EnrollPageStatus = EnrollPageStatus.NewStep;
                else
                {
                    result = "<script type='text/javascript'>" + "\r\n";
                    result += "if (window.addEventListener)";
                    result += "{";
                    result += "   window.addEventListener('load', OnRefreshPost, false);";
                    result += "}";
                    result += "else if (window.attachEvent)";
                    result += "{";
                    result += "   window.attachEvent('onload', OnRefreshPost);";
                    result += "}";
                    result += "\r\n";
                    result += "</script>" + "\r\n";
                }
            }
            result += "<form method=\"post\" id=\"invitationForm\" autocomplete=\"off\" >";

            switch (usercontext.EnrollPageStatus)
            {
                case EnrollPageStatus.Start:
                    result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + Resources.GetString(ResourcesLocaleKind.Titles, "InvitationPageTitle") + "</div>";
                    result += "<div id=\"pwdMessage\" class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Errors, "ErrorAccountNotEnabled") + "</div></br>";
                    List<IExternalProvider> lst = RuntimeAuthProvider.GeActiveProvidersList();
                    if (lst.Count > 0)
                    {
                        result += "<div id=\"xMessage\" class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMustPrepareLabel") + "</div>";
                        foreach (IExternalProvider itm in lst)
                        {
                            if (itm.IsRequired)
                                result += "<div id=\"reqvalue\" class=\"fieldMargin smallText\">- " + itm.GetUIEnrollmentTaskLabel(usercontext) + "</div>";
                            else
                                result += "<div id=\"reqvalue\" class=\"fieldMargin smallText\">- " + itm.GetUIEnrollmentTaskLabel(usercontext) + " (*)" + "</div>";
                        }
                        if (RuntimeAuthProvider.IsPinCodeRequired(usercontext))
                            result += "<div id=\"reqvalue\" class=\"fieldMargin smallText\">- " + Resources.GetString(ResourcesLocaleKind.Html, "HtmlEnrollPinTaskLabel") + "</div>";
                        result += "</br>";
                    }
                    result += "<table><tr>";
                    if (Provider.Config.UserFeatures.IsManaged())
                    {
                        result += "<td>";
                        result += "<input id=\"gotoinscription2\" type=\"submit\" class=\"submit\" name=\"gotoinscription2\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMGotoInscription") + "\" onclick=\"fnbtnclicked(2)\"/>";
                        result += "</td>";
                        result += "<td style=\"width: 15px\" />";
                        result += "<td>";
                        result += "<input id=\"continueButton\" type=\"submit\" class=\"submit\" name=\"continueButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMConnexion") + "\" onclick=\"fnbtnclicked(1)\" />";
                        result += "</td>";
                    }
                    else
                    {
                        result += "<td>";
                        result += "<input id=\"gotoinscription\" type=\"submit\" class=\"submit\" name=\"gotoinscription\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMGotoInscription") + "\" onclick=\"fnbtnclicked(2)\"/>";
                        result += "</td>";
                    }
                    result += "</tr></table>";
                    break;
                case EnrollPageStatus.NewStep:
                    if (m != PreferredMethod.Pin)
                    {
                        IExternalProvider prov = RuntimeAuthProvider.GetProviderInstance(m);
                        result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + prov.GetWizardUILabel(usercontext) + "</div>";
                        result += "<div id=\"pwdMessage\" class=\"fieldMargin smallText\">" + prov.GetUIEnrollmentTaskLabel(usercontext) + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIEnrollContinue") + "</div></br>";
                        result += "<table><tr>";
                        result += "<td>";
                        result += "<input id=\"continueButton\" type=\"submit\" class=\"submit\" name=\"Continue\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelWVERIFYOTP") + "\" onclick=\"fnbtnclicked(2)\" />";
                        result += "</td>";
                        result += "<td style=\"width: 15px\" />";
                        if ((prov.Enabled) && (!prov.IsRequired))
                        {
                            result += "<td>";
                            result += "<input id=\"skipoption\" type=\"submit\" class=\"submit\" name=\"skipoption\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUINext") + "\" onclick=\"fnbtnclicked(6)\"/>";
                            result += "</td>";
                        }
                        result += "</tr></table>";
                    }
                    else
                    {   // Pin Code not provider
                        result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + BaseExternalProvider.GetPINWizardUILabel(usercontext) + "</div>";
                        result += "<div id=\"pwdMessage\" class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlEnrollPinTaskLabel") + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIEnrollContinue") + "</div></br>";
                        result += "<table><tr>";
                        result += "<td>";
                        result += "<input id=\"continueButton\" type=\"submit\" class=\"submit\" name=\"Continue\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelWVERIFYOTP") + "\" onclick=\"fnbtnclicked(2)\" />";
                        result += "</td>";
                        result += "</tr></table>";
                    }
                    break;
                case EnrollPageStatus.Stop:
                    result += "</br>";
                    result += "<p style=\"text-align:center\"><img id=\"msgreen\" src=\"data:image/png;base64," + Convert.ToBase64String(images.cvert.ToByteArray(ImageFormat.Jpeg)) + "\"/></p></br></br>";
                    result += "<div id=\"lbl\" class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelVERIFYOTPSuccess") + "</div></br>";
                    List<IExternalProvider> lst2 = RuntimeAuthProvider.GeActiveProvidersList();
                    if (lst2.Count > 0)
                    {
                        result += "<div id=\"xMessage\" class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIResultLabel") + "</div>";
                        foreach (IExternalProvider itm in lst2)
                        {
                            string value = string.Empty;
                            switch (itm.Kind)
                            {
                                default:
                                case PreferredMethod.Code:
                                    if (usercontext.KeyStatus == SecretKeyStatus.Success)
                                        value = "OK";
                                    else
                                        value = "<empty>";
                                    break;
                                case PreferredMethod.Email:
                                    value = Utilities.StripEmailAddress(usercontext.MailAddress);
                                    break;
                                case PreferredMethod.External:
                                    value = Utilities.StripPhoneNumber(usercontext.PhoneNumber);
                                    break;
                            }

                            if (itm.IsRequired)
                                result += "<div id=\"reqvalue\" class=\"fieldMargin smallText\">- " + itm.GetUIListOptionLabel(usercontext) + " : " + value + "</div>";
                            else
                                result += "<div id=\"reqvalue\" class=\"fieldMargin smallText\">- " + itm.GetUIListOptionLabel(usercontext) + " (*) : " + value + "</div>";
                        }
                        if (RuntimeAuthProvider.IsPinCodeRequired(usercontext))
                            result += "<div id=\"reqvalue\" class=\"fieldMargin smallText\">- " + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIPinOptionLabel") + " : " + Utilities.StripPinCode(usercontext.PinCode) + "</div>";
                        result += "</br>";
                    }
                    result += "<input id=\"finishButton\" type=\"submit\" class=\"submit\" name=\"finishButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelVERIFYOTPOK") + "\" onclick=\"fnbtnclicked(5)\" />";
                    result += "</br>";
                    break;
            }

            result += "<input id=\"isprovider\" type=\"hidden\" name=\"isprovider\" value=\"" + (int)m + "\" />";
            result += "<input id=\"btnclicked\" type=\"hidden\" name=\"btnclicked\" value=\"0\" />";
            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";

            result += "</form>";
            return result;
        }
        #endregion

        #region Activation
        /// <summary>
        /// GetFormPreRenderHtmlActivation implementation
        /// </summary>
        public override string GetFormPreRenderHtmlActivation(AuthenticationContext usercontext)
        {
            string result = "<script type='text/javascript'>" + "\r\n";

            result += "function OnRefreshPost(frm)";
            result += "{";
            result += "   document.getElementById('activationForm').submit();";
            result += "   return true;";
            result += "}";

            result += "function fnbtnclicked(id)";
            result += "{";
            result += "   var lnk = document.getElementById('btnclicked');";
            result += "   lnk.value = id;";
            result += "}" + "\r\n";

            result += "</script>" + "\r\n";
            return result;

        }

        /// <summary>
        /// GetFormHtmlActivation implementation
        /// </summary>
        public override string GetFormHtmlActivation(AuthenticationContext usercontext)
        {
            string result = string.Empty;
            result += "<form method=\"post\" id=\"activationForm\" autocomplete=\"off\" >";

            result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + Resources.GetString(ResourcesLocaleKind.Titles, "ActivationPageTitle") + "</div>";
            result += "<div id=\"pwdMessage\" class=\"groupMargin\">" + Resources.GetString(ResourcesLocaleKind.Errors, "ErrorAccountActivate") + "</div></br>";

            result += "<table><tr>";
            result += "<td>";
            result += "<input id=\"activateButton\" type=\"submit\" class=\"submit\" name=\"activateButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIActivate") + "\" onclick=\"fnbtnclicked(2)\"/>";
            result += "</td>";
            result += "<td style=\"width: 15px\" />";
            result += "<td>";
            result += "<input id=\"continueButton\" type=\"submit\" class=\"submit\" name=\"continueButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelWVERIFYOTP") + "\" onclick=\"fnbtnclicked(1)\" />";
            result += "</td>";
            result += "</tr></table>";

            result += "<input id=\"btnclicked\" type=\"hidden\" name=\"btnclicked\" value=\"0\" />";
            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";

            result += "</form>";
            return result;

        }
        #endregion

        #region SelectOptions
        /// <summary>
        /// GetFormPreRenderHtmlSelectOptions implementation
        /// </summary>
        public override string GetFormPreRenderHtmlSelectOptions(AuthenticationContext usercontext)
        {
            string result = "<script type='text/javascript'>" + "\r\n";

            result += "function SetLinkTitle(frm, data)";
            result += "{";
            result += "var lnk = document.getElementById('lnk');";
            result += "if (lnk)";
            result += "{";
            result += "lnk.value = data;";
            result += "}";
            result += "var donut = document.getElementById('cookiePullWaitingWheel');";
            result += "if (donut)";
            result += "{";
            result += "donut.style.visibility = 'visible';";
            result += "}";
            result += "frm.submit();";
            result += "return true;";
            result += "}" + "\r\n";

            result += "function fnbtnclicked()";
            result += "{";
            result += "var lnk = document.getElementById('btnclicked');";
            result += "lnk.value = 1;";
            result += "}" + "\r\n";

            result += "</script>" + "\r\n";
            return result;
        }

        /// <summary>
        /// GetFormHtmlSelectOptions implementation
        /// </summary>
        public override string GetFormHtmlSelectOptions(AuthenticationContext usercontext)
        {
            string result = string.Empty;
            bool showdonut = false;
            result += "<form method=\"post\" id=\"selectoptionsForm\" autocomplete=\"off\" >";

            if (Provider.Config.UserFeatures.CanManageOptions())
            {
                result += "<a class=\"actionLink\" href=\"#\" id=\"chgopt\" name=\"chgopt\" onclick=\"return SetLinkTitle(selectoptionsForm, '1')\"; style=\"cursor: pointer;\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlChangeConfiguration") + "</a>";
                showdonut = RuntimeAuthProvider.IsProviderAvailable(usercontext, PreferredMethod.Azure);
            }
            if (Provider.Config.UserFeatures.CanManagePassword() && RuntimeRepository.CanChangePassword(usercontext.UPN))
            {
                if (!Provider.Config.CustomUpdatePassword)
                    result += "<a class=\"actionLink\" href=\"/adfs/portal/updatepassword?username=" + usercontext.UPN + "\" id=\"chgpwd\" name=\"chgpwd\"; style=\"cursor: pointer;\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlChangePassword") + "</a>";
                else
                    result += "<a class=\"actionLink\" href=\"#\" id=\"chgpwd\" name=\"chgpwd\" onclick=\"return SetLinkTitle(selectoptionsForm, '2')\"; style=\"cursor: pointer;\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlChangePassword") + "</a>";
            }
            result += "</br>";
            if (Provider.Config.UserFeatures.CanEnrollDevices())
            {
                bool WantPin = false;
                IExternalProvider prov = null;
                if (RuntimeAuthProvider.IsUIElementRequired(usercontext, RequiredMethodElements.OTPLinkRequired))
                {
                    prov = RuntimeAuthProvider.GetProvider(PreferredMethod.Code);
                    if (prov.PinRequired)
                        WantPin = true;
                    if (Provider.HasStrictAccessToOptions(prov))
                        result += "<a class=\"actionLink\" href=\"#\" id=\"enrollopt\" name=\"enrollopt\" onclick=\"return SetLinkTitle(selectoptionsForm, '3')\"; style=\"cursor: pointer;\">" + prov.GetWizardLinkLabel(usercontext) + "</a>";
                }
                if (RuntimeAuthProvider.IsUIElementRequired(usercontext, RequiredMethodElements.BiometricLinkRequired))
                {
                    prov = RuntimeAuthProvider.GetProvider(PreferredMethod.Biometrics);
                    if (prov.PinRequired)
                        WantPin = true;
                    if (Provider.HasStrictAccessToOptions(prov))
                        result += "<a class=\"actionLink\" href=\"#\" id=\"enrollbio\" name=\"enrollbio\" onclick=\"return SetLinkTitle(selectoptionsForm, '4')\"; style=\"cursor: pointer;\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlEnrollBiometric") + "</a>";
                }
                if (RuntimeAuthProvider.IsUIElementRequired(usercontext, RequiredMethodElements.EmailLinkRequired))
                {
                    prov = RuntimeAuthProvider.GetProvider(PreferredMethod.Email);
                    if (prov.PinRequired)
                        WantPin = true;
                    if (Provider.HasStrictAccessToOptions(prov))
                        result += "<a class=\"actionLink\" href=\"#\" id=\"enrollemail\" name=\"enrollemail\" onclick=\"return SetLinkTitle(selectoptionsForm, '5')\"; style=\"cursor: pointer;\">" + prov.GetWizardLinkLabel(usercontext) + "</a>";
                }
                if (RuntimeAuthProvider.IsUIElementRequired(usercontext, RequiredMethodElements.PhoneLinkRequired))
                {
                    prov = RuntimeAuthProvider.GetProvider(PreferredMethod.External);
                    if (prov.PinRequired)
                        WantPin = true;
                    if (Provider.HasStrictAccessToOptions(prov))
                        result += "<a class=\"actionLink\" href=\"#\" id=\"enrollphone\" name=\"enrollphone\" onclick=\"return SetLinkTitle(selectoptionsForm, '6')\"; style=\"cursor: pointer;\">" + prov.GetWizardLinkLabel(usercontext) + "</a>";
                }
                if (RuntimeAuthProvider.IsUIElementRequired(usercontext, RequiredMethodElements.PinLinkRequired))
                {
                    if (WantPin)
                        result += "<a class=\"actionLink\" href=\"#\" id=\"enrollpin\" name=\"enrollpin\"  onclick=\"return SetLinkTitle(selectoptionsForm, '7')\"; style=\"cursor: pointer;\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlEnrollPinCode") + "</a>";
                }
            }
            result += "<script>";
            result += "   document.cookie = 'showoptions=;expires=Thu, 01 Jan 1970 00:00:01 GMT;path=/adfs/'";
            result += "</script>";
            result += "</br>";
            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";
            result += "<input id=\"btnclicked\" type=\"hidden\" name=\"btnclicked\" value=\"0\" />";
            result += "<input id=\"lnk\" type=\"hidden\" name=\"lnk\" value=\"0\"/>";
            result += "<input id=\"saveButton\" type=\"submit\" class=\"submit\" name=\"saveButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMConnexion") + "\" onclick=\"fnbtnclicked()\"/>";
            if (showdonut)
            {
                result += "</br></br></br>";
                result += GetPartHtmlDonut(false);
            }
            result += "</br>";
            result += GetFormHtmlMessageZone(usercontext);
            result += "</form>";
            return result;
        }
        #endregion

        #region ChooseMethod
        /// <summary>
        /// GetFormPreRenderHtmlChooseMethod implementation
        /// </summary>
        public override string GetFormPreRenderHtmlChooseMethod(AuthenticationContext usercontext)
        {
            string result = "<script type='text/javascript'>" + "\r\n";

            result += "function ChooseMethodChanged()";
            result += "{";
            result += "var data = document.getElementById('opt3');";
            result += "var edit = document.getElementById('stmail');";
            result += "if (data.checked)";
            result += "{";
            result += "edit.disabled = false;";
            result += "}";
            result += "else";
            result += "{";
            result += "edit.disabled = true;";
            result += "}";
            result += "return true;";
            result += "}";
            result += "\r\n";

            result += "function fnbtnclicked(id)";
            result += "{";
            result += "   var lnk = document.getElementById('btnclicked');";
            result += "   lnk.value = id;";
            result += "}";
            result += "\r\n";

            result += "</script>" + "\r\n";
            return result;
        }

        /// <summary>
        /// GetFormHtmlChooseMethod implementation
        /// </summary>
        public override string GetFormHtmlChooseMethod(AuthenticationContext usercontext)
        {
            string result = string.Empty;
            result += "<form method=\"post\" id=\"ChooseMethodForm\" autocomplete=\"off\" \" >";
            result += "<b><div id=\"pwdTitle\" class=\"groupMargin\">" + Resources.GetString(ResourcesLocaleKind.Titles, "MustUseCodePageTitle") + "</div></b>";
            PreferredMethod method = GetMethod4FBUsers(usercontext);
            if (RuntimeAuthProvider.IsProviderAvailableForUser(usercontext, PreferredMethod.Code))
            {
                result += "<input id=\"opt1\" name=\"opt\" type=\"radio\" value=\"0\" onchange=\"ChooseMethodChanged()\" "+ (((method == PreferredMethod.Code) || (method == PreferredMethod.Choose)) ? "checked=\"checked\"> " : "> ") + RuntimeAuthProvider.GetProvider(PreferredMethod.Code).GetUIChoiceLabel(usercontext)  + "<br /><br/>";
            }

            if (RuntimeAuthProvider.IsProviderAvailableForUser(usercontext, PreferredMethod.Email))
            {
                result += "<input id=\"opt3\" name=\"opt\" type=\"radio\" value=\"2\" onchange=\"ChooseMethodChanged()\" " + ((method == PreferredMethod.Email) ? "checked=\"checked\"> " : "> ") + RuntimeAuthProvider.GetProvider(PreferredMethod.Email).GetUIChoiceLabel(usercontext) + "<br/>";
                if (RuntimeAuthProvider.GetProvider(PreferredMethod.Email).IsBuiltIn)
                    result += "<div style=\"text-indent: 20px;\"><i>" + Utilities.StripEmailAddress(usercontext.MailAddress) + "</i></div><br/>";
                else
                    result += "<br/>";
            }

            if (RuntimeAuthProvider.IsProviderAvailableForUser(usercontext, PreferredMethod.External))
            {
               result += "<input id=\"opt2\" name=\"opt\" type=\"radio\" value=\"1\" onchange=\"ChooseMethodChanged()\" " + ((method == PreferredMethod.External) ? "checked=\"checked\"> " : "> ") + RuntimeAuthProvider.GetProvider(PreferredMethod.External).GetUIChoiceLabel(usercontext) + "<br/>";
               if (RuntimeAuthProvider.GetProvider(PreferredMethod.External).IsBuiltIn)
                  result += "<div style=\"text-indent: 20px;\"><i>" + Utilities.StripPhoneNumber(usercontext.PhoneNumber) + "</i></div><br/>";
                else
                    result += "<br/>";
            }

            if (RuntimeAuthProvider.IsProviderAvailableForUser(usercontext, PreferredMethod.Azure))
            {
                result += "<input id=\"opt4\" name=\"opt\" type=\"radio\" value=\"3\" onchange=\"ChooseMethodChanged()\" " + ((method == PreferredMethod.Azure) ? "checked=\"checked\"> " : "> ") + RuntimeAuthProvider.GetProvider(PreferredMethod.Azure).GetUIChoiceLabel(usercontext) + "<br/>";
            }

            result += "<br/>";
            if (Provider.KeepMySelectedOptionOn())
                result += "<input id=\"remember\" type=\"checkbox\" name=\"Remember\" checked=\"true\" > " + Resources.GetString(ResourcesLocaleKind.Html, "HtmlCHOOSEOptionRemember") + "<br/>";
            result += "<br/>";
            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\">";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\">";
            result += "<input id=\"btnclicked\" type=\"hidden\" name=\"btnclicked\" value=\"0\" />";

            result += "<table><tr>";
            result += "<td>";
            result += "<input id=\"saveButton\" type=\"submit\" class=\"submit\" name=\"Continue\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlCHOOSEOptionSendCode") + "\" onclick=\"fnbtnclicked(0)\" />";
            result += "</td>";
            result += "<td style=\"width: 15px\" />";
            result += "<td>";
            result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onclick=\"fnbtnclicked(1)\" />";
            result += "</td>";
            result += "</tr></table>";
            result += GetFormHtmlMessageZone(usercontext);
            result += "</form>";
            return result;
        }

        /// <summary>
        /// GetMethod4FBUsers method implementation
        /// </summary>
        private PreferredMethod GetMethod4FBUsers(AuthenticationContext usercontext)
        {
            PreferredMethod method = usercontext.PreferredMethod;
            PreferredMethod idMethod = usercontext.PreferredMethod;
            do
            {
                idMethod++;
                if (idMethod > PreferredMethod.Azure)
                    idMethod = PreferredMethod.Code;
                else if (idMethod == method)
                    return method;
            } while (!RuntimeAuthProvider.IsProviderAvailableForUser(usercontext, idMethod));
            return idMethod;

        }
        #endregion

        #region ChangePassword
        /// <summary>
        /// GetFormPreRenderHtmlChangePassword implementation
        /// </summary>
        public override string GetFormPreRenderHtmlChangePassword(AuthenticationContext usercontext)
        {
            string result = string.Empty;
            if (Provider.Config.CustomUpdatePassword)
            {
                result += "<script type='text/javascript'>" + "\r\n";

                result += "function ValidChangePwd(frm)";
                result += "{";
                result += "var lnk = document.getElementById('btnclicked');";
                result += "if (lnk.value=='1')";
                result += "{";
                result += "var err = document.getElementById('errorText');";
                result += "var oldpwd = document.getElementById('oldpwdedit');";
                result += "var newpwd = document.getElementById('newpwdedit');";
                result += "var cnfpwd = document.getElementById('cnfpwdedit');";
                result += "if (oldpwd.value == \"\")";
                result += "{";
                result += "err.innerHTML = \"" + Resources.GetString(ResourcesLocaleKind.Validation, "ValidPassActualError") + "\";";
                result += "oldpwd.focus();";
                result += "return false;";
                result += "}";
                result += "if (newpwd.value == \"\")";
                result += "{";
                result += "err.innerHTML = \"" + Resources.GetString(ResourcesLocaleKind.Validation, "ValidPassNewError") + "\";";
                result += "newpwd.focus();";
                result += "return false;";
                result += "}";
                result += "if (cnfpwd.value == \"\")";
                result += "{";
                result += "err.innerHTML = \"" + Resources.GetString(ResourcesLocaleKind.Validation, "ValidConfirmNewPassError") + "\";";
                result += "cnfpwd.focus();";
                result += "return false;";
                result += "}";
                result += "if (cnfpwd.value != newpwd.value)";
                result += "{";
                result += "err.innerHTML = \"" + Resources.GetString(ResourcesLocaleKind.Validation, "ValidNewPassError") + "\";";
                result += "cnfpwd.focus();";
                result += "return false;";
                result += "}";
                result += "err.innerHTML = \"\";";
                result += "}";
                result += "return true;";
                result += "}";
                result += "\r\n";

                result += "function fnbtnclicked(id)";
                result += "{";
                result += "   var lnk = document.getElementById('btnclicked');";
                result += "   lnk.value = id;";
                result += "}";
                result += "\r\n";

                result += "</script>" + "\r\n";
            }
            return result;
        }

        /// <summary>
        /// GetFormHtmlChangePassword implementation
        /// </summary>
        public override string GetFormHtmlChangePassword(AuthenticationContext usercontext)
        {
            string result = string.Empty;
            if (Provider.Config.CustomUpdatePassword)
            {
                result += "<form method=\"post\" id=\"passwordForm\" autocomplete=\"off\" onsubmit=\"return ValidChangePwd(this)\" >";
                result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + Resources.GetString(ResourcesLocaleKind.Titles, "PasswordPageTitle") + "</div>";

                result += "<div id=\"oldpwd\"  class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDLabelActual") + "</div>";
                result += "<input id=\"oldpwdedit\" name=\"oldpwdedit\" type=\"password\" class=\"text fullWidth\"/></br></br>";

                result += "<div id=\"newpwd\" class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDLabelNew") + "</div>";
                result += "<input id=\"newpwdedit\" name=\"newpwdedit\" type=\"password\" class=\"text fullWidth\"/></br></br>";

                result += "<div id=\"cnfpwd\" class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDLabelNewConfirmation") + "</div>";
                result += "<input id=\"cnfpwdedit\" name=\"cnfpwdedit\" type=\"password\" class=\"text fullWidth\"/></br></br>";

                result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
                result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";
                result += "<input id=\"btnclicked\" type=\"hidden\" name=\"btnclicked\" value=\"1\" />";
                result += "<table><tr>";
                result += "<td>";
                result += "<input id=\"saveButton\" type=\"submit\" class=\"submit\" name=\"continue\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDSave") + "\" onClick=\"fnbtnclicked(1)\" />";
                result += "</td>";
                result += "<td style=\"width: 15px\" />";
                result += "<td>";
                result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onClick=\"fnbtnclicked(2)\"/>";
                result += "</td>";
                result += "</tr></table>";
                result += GetFormHtmlMessageZone(usercontext);
                result += "</form>";
            }
            return result;
        }
        #endregion

        #region Bypass
        /// <summary>
        /// GetFormPreRenderHtmlBypass implementation
        /// </summary>
        public override string GetFormPreRenderHtmlBypass(AuthenticationContext usercontext)
        {
            string result = "<script type='text/javascript'>" + "\r\n";

            result += "function OnAutoPost(frm)";
            result += "{";
            result += "var title = document.getElementById('mfaGreetingDescription');";
            result += "if (title)";
            result += "{";
            result += "title.innerHTML = \"<b>" + Resources.GetString(ResourcesLocaleKind.Titles, "TitleRedirecting") + "</b>\";";
            result += "}";
            result += "document.getElementById('bypassForm').submit();";
            result += "return true;";
            result += "}";
            result += "\r\n";
            if (!string.IsNullOrEmpty(usercontext.AccountManagementUrl))
            {
                result += "function OnUnload(frm)";
                result += "{";
                result += "window.open('" + usercontext.AccountManagementUrl + "', '_blank');";
                result += "return true;";
                result += "}";
                result += "\r\n";
            }

            result += "</script>" + "\r\n";
            return result;
        }
        /// <summary>
        /// GetFormHtmlBypass implementation
        /// </summary>
        public override string GetFormHtmlBypass(AuthenticationContext usercontext)
        {
            IExternalProvider prov = RuntimeAuthProvider.GetProvider(usercontext.PreferredMethod);
            bool needinput = ((usercontext.IsTwoWay) && (prov != null) && (prov.IsUIElementRequired(usercontext, RequiredMethodElements.PinParameterRequired)));
            if ((usercontext.WizContext == WizardContextMode.Registration) || (usercontext.WizContext == WizardContextMode.Invitation)) // do not ask after registration/Invitation Enrollment process
                needinput = false;

            string result = string.Empty;
            result += "<script type='text/javascript'>" + "\r\n";
            result += "if (window.addEventListener)";
            result += "{";
            if (!needinput)
                result += "window.addEventListener('load', OnAutoPost, false);";
            if (!string.IsNullOrEmpty(usercontext.AccountManagementUrl))
                result += "window.addEventListener('unload', OnUnload, false);";
            result += "}";
            result += "else if (window.attachEvent)";
            result += "{";
            if (!needinput)
                result += "window.attachEvent('onload', OnAutoPost);";
            if (!string.IsNullOrEmpty(usercontext.AccountManagementUrl))
                result += "window.attachEvent('unload', OnUnload);";
            result += "}";
            result += "\r\n";
            result += "</script>" + "\r\n";

            if (needinput)
            {
                result += "<form method=\"post\" id=\"bypassForm\" autocomplete=\"off\" title=\"PIN Confirmation\" >";
                result += "<div class=\"fieldMargin smallText\">" + BaseExternalProvider.GetPINLabel(usercontext) + " : </div>";
                result += "<input id=\"pincode\" name=\"pincode\" type=\"password\" placeholder=\"PIN\" class=\"text fullWidth\" /></br>";
                result += "<div class=\"fieldMargin smallText\">" + BaseExternalProvider.GetPINMessage(usercontext) + "</div></br>";
                result += "<input id=\"continueButton\" type=\"submit\" class=\"submit\" name=\"continueButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMConnexion") + "\" /></br></br>";

                result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
                result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";

            }
            else
            {
                result += "<form method=\"post\" id=\"bypassForm\" autocomplete=\"off\" title=\"Redirecting\" >";
                result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
                result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";
            }
            result += "</form>";
            return result;
        }
        #endregion

        #region Locking
        /// <summary>
        /// GetFormPreRenderHtmlLocking implementation
        /// </summary>
        public override string GetFormPreRenderHtmlLocking(AuthenticationContext usercontext)
        {
            string result = string.Empty;
            result += "<script type=\"text/javascript\">" + "\r\n";

            result += "function fnbtnclicked(id)";
            result += "{";
            result += "var lnk = document.getElementById('btnclicked');";
            result += "lnk.value = id;";
            result += "}";
            result += "\r\n";

            result += "</script>" + "\r\n";
            return result;
        }

        /// <summary>
        /// GetFormHtmlLocking implementation
        /// </summary>
        public override string GetFormHtmlLocking(AuthenticationContext usercontext)
        {
            string result = "<form method=\"post\" id=\"lockingForm\" autocomplete=\"off\">";

            if (!usercontext.IsRegistered)
            {
                if (Provider.Config.UserFeatures.IsRegistrationRequired())
                {
                    result += "<div id=\"pwdMessage\" class=\"groupMargin\">" + Resources.GetString(ResourcesLocaleKind.Errors, "ErrorAccountAdminNotEnabled") + "</div></br>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"buttonquit\" type=\"submit\"  value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMQuit") + "\" onClick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                }
                else
                {
                    result += GetFormHtmlMessageZone(usercontext);
                    result += "<br/>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"buttonquit\" type=\"submit\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMQuit") + "\" onClick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                }
            }
            else if (!usercontext.Enabled)
            {
                if (Provider.Config.UserFeatures.IsMFARequired())
                {
                    result += "<div id=\"pwdMessage\" class=\"groupMargin\">" + Resources.GetString(ResourcesLocaleKind.Errors, "ErrorAccountAdminNotEnabled") + "</div></br>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"buttonquit\" type=\"submit\"  value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMQuit") + "\" onClick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                }
                else
                {
                    result += GetFormHtmlMessageZone(usercontext);
                    result += "<br/>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"buttonquit\" type=\"submit\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMQuit") + "\" onClick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                }
            }
            else
            {
                result += GetFormHtmlMessageZone(usercontext);
                result += "<br/>";
                result += "<table><tr>";
                result += "<td>";
                result += "<input id=\"buttonquit\" type=\"submit\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMQuit") + "\" onClick=\"fnbtnclicked(1)\" />";
                result += "</td>";
                result += "</tr></table>";
            }

            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";
            result += "<input id=\"btnclicked\" type=\"hidden\" name=\"btnclicked\" value=\"1\" />";
            result += "</form>";
            return result;
        }
        #endregion

        #region CodeRequest
        /// <summary>
        /// GetFormPreRenderHtmlSendCodeRequest implementation
        /// </summary>
        public override string GetFormPreRenderHtmlSendCodeRequest(AuthenticationContext usercontext)
        {
            string dt = DateTime.Now.AddMilliseconds(Provider.Config.DeliveryWindow).ToString("R");
            string result = "<script type='text/javascript'>" + "\r\n";

            result += "function OnRefreshPost(frm)";
            result += "{";
            result += "document.getElementById('refreshForm').submit();";
            result += "return true;";
            result += "}";
            result += "\r\n";

            result += "function SetLinkTitle(frm, data)" + "\r\n";
            result += "{";
            result += "var lnk = document.getElementById('lnk');";
            result += "if (lnk)";
            result += "{";
            result += "lnk.value = data;";
            result += "}";
            result += "frm.submit();";
            result += "return true;";
            result += "}";
            result += "\r\n";

            result += "function SetOptions(frm)" + "\r\n";
            result += "{";
            result += "var opt = document.getElementById('options');";
            result += "if (opt)";
            result += "{";
            result += "if (opt.checked)";
            result += "{";
            result += "document.cookie = 'showoptions=1;expires=" + dt + ";path=/adfs/';";
            result += "}";
            result += "else";
            result += "{";
            result += "document.cookie = 'showoptions=;expires=Thu, 01 Jan 1970 00:00:01 GMT';";
            result += "}";
            result += "}";
            result += "return true;";
            result += "}";
            result += "\r\n";

            result += "</script>" + "\r\n";
            return result;
        }

        /// <summary>
        /// GetFormHtmlCodeRequest implementation
        /// </summary>
        public override string GetFormHtmlSendCodeRequest(AuthenticationContext usercontext)
        {
            string result = "<script type='text/javascript'>" + "\r\n";
            result += "if (window.addEventListener)";
            result += "{";
            result += "window.addEventListener('load', OnRefreshPost, false);";
            result += "}";
            result += "else if (window.attachEvent)";
            result += "{";
            result += "window.attachEvent('onload', OnRefreshPost);";
            result += "}";
            result += "\r\n";
            result += "</script>" + "\r\n";

            result += "<form method=\"post\" id=\"refreshForm\" autocomplete=\"off\" \">";

            if (usercontext.IsRemote)
            {
                IExternalProvider prov = RuntimeAuthProvider.GetProvider(usercontext.PreferredMethod);
                result += GetPartHtmlDonut();
                result += "<div class=\"fieldMargin smallText\">" + prov.GetUIMessage(usercontext) + "</div></br>";
                if (!string.IsNullOrEmpty(prov.GetUIWarningInternetLabel(usercontext)) && (usercontext.IsRemote))
                    result += "<div class=\"error smallText\">" + prov.GetUIWarningInternetLabel(usercontext) + "</div>";
                if (!string.IsNullOrEmpty(prov.GetUIWarningThirdPartyLabel(usercontext)) && (usercontext.IsTwoWay))
                    result += "<div class=\"error smallText\">" + prov.GetUIWarningThirdPartyLabel(usercontext) + "</div>";
                result += "<br />";
                if (usercontext.IsTwoWay && (Provider.Config.UserFeatures.CanAccessOptions()))
                {
                    result += "<input id=\"options\" type=\"checkbox\" name=\"options\" onclick=\"SetOptions(refreshForm)\"; /> " + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMAccessOptions");
                    result += "<script>";
                    result += "   document.cookie = 'showoptions=;expires=Thu, 01 Jan 1970 00:00:01 GMT;path=/adfs/'";
                    result += "</script>";
                    result += "</br></br></br>";
                }
                result += "<a class=\"actionLink\" href=\"#\" id=\"nocode\" name=\"nocode\" onclick=\"return SetLinkTitle(refreshForm, '3')\"; style=\"cursor: pointer;\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMNoCode") + "</a>";
                result += "<input id=\"lnk\" type=\"hidden\" name=\"lnk\" value=\"0\"/>";
            }
            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";
            result += GetFormHtmlMessageZone(usercontext);

            result += "</form>";
            return result;
        }
        #endregion

        #region InvitationRequest
        /// <summary>
        /// GetFormPreRenderHtmlSendAdministrativeRequest implementation
        /// </summary>
        public override string GetFormPreRenderHtmlSendAdministrativeRequest(AuthenticationContext usercontext)
        {
            string result = "<script type='text/javascript'>" + "\r\n";

            result += "function OnRefreshPost(frm)";
            result += "{";
            result += "document.getElementById('invitationReqForm').submit();";
            result += "return true;";
            result += "}";
            result += "\r\n";

            result += "</script>" + "\r\n";
            return result;
        }

        /// <summary>
        /// GetFormHtmlSendAdministrativeRequest implementation
        /// </summary>
        public override string GetFormHtmlSendAdministrativeRequest(AuthenticationContext usercontext)
        {
            string result = "<script type='text/javascript'>" + "\r\n";

            result += "if (window.addEventListener)";
            result += "{";
            result += "window.addEventListener('load', OnRefreshPost, false);";
            result += "}";
            result += "else if (window.attachEvent)";
            result += "{";
            result += "window.attachEvent('onload', OnRefreshPost);";
            result += "}";
            result += "\r\n";
            result += "</script>" + "\r\n";

            result += "<form method=\"post\" id=\"invitationReqForm\" autocomplete=\"off\" \">";
            result += "<div class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlINSWaitRequest") + "</div>";

            IExternalAdminProvider admprov = RuntimeAuthProvider.GetAdministrativeProvider(Provider.Config);
            result += GetPartHtmlDonut();
            if (admprov != null)
            {
                result += "<div class=\"fieldMargin smallText\">" + admprov.GetUIInscriptionMessageLabel(usercontext) + "</div></br>";
                if (!string.IsNullOrEmpty(admprov.GetUIWarningInternetLabel(usercontext)) && (usercontext.IsRemote))
                    result += "<div class=\"error smallText\">" + admprov.GetUIWarningInternetLabel(usercontext) + "</div>";
            }
            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";
            result += "</form>";
            return result;
        }
        #endregion

        #region SendKeyRequest
        /// <summary>
        /// GetFormPreRenderHtmlSendKeyRequest method implementation
        /// </summary>
        public override string GetFormPreRenderHtmlSendKeyRequest(AuthenticationContext usercontext)
        {
            string result = "<script type='text/javascript'>" + "\r\n";

            result += "function OnAutoRefreshPost(frm)";
            result += "{";
            result += "document.getElementById('sendkeyReqForm').submit();";
            result += "return true;";
            result += "}";
            result += "\r\n";

            result += "</script>" + "\r\n";
            return result;
        }

        /// <summary>
        /// GetFormHtmlSendKeyRequest method implementation
        /// </summary>
        public override string GetFormHtmlSendKeyRequest(AuthenticationContext usercontext)
        {
            string result = string.Empty;
            result += "<script type=\"text/javascript\">" + "\r\n";
            result += "if (window.addEventListener)";
            result += "{";
            result += "window.addEventListener('load', OnAutoRefreshPost, false);";
            result += "}";
            result += "else if (window.attachEvent)";
            result += "{";
            result += "window.attachEvent('onload', OnAutoRefreshPost);";
            result += "}";
            result += "</script>" + "\r\n";

            result += "<form method=\"post\" id=\"sendkeyReqForm\" autocomplete=\"off\" \">";
            result += "<div class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlINSWaitRequest") + "</div>";

            IExternalAdminProvider admprov = RuntimeAuthProvider.GetAdministrativeProvider(Provider.Config);

            result += GetPartHtmlDonut();
            if (admprov != null)
            {
                result += "<div class=\"fieldMargin smallText\">" + admprov.GetUISecretKeyMessageLabel(usercontext) + "</div></br>";
                if (!string.IsNullOrEmpty(admprov.GetUIWarningInternetLabel(usercontext)) && (usercontext.IsRemote))
                    result += "<div class=\"error smallText\">" + admprov.GetUIWarningInternetLabel(usercontext) + "</div>";
            }
            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";
            result += "</form>";
            return result;
        }
        #endregion

        #region EnrollOTP
        /// <summary>
        /// GetFormPreRenderHtmlEnrollOTP method implementation
        /// </summary>
        public override string GetFormPreRenderHtmlEnrollOTP(AuthenticationContext usercontext)
        {
            string result = string.Empty;
            result += "<script type=\"text/javascript\">" + "\r\n";

            result += "function fnbtnclicked(id)";
            result += "{";
            result += "var lnk = document.getElementById('btnclicked');";
            result += "lnk.value = id;";
            result += "return true;";
            result += "}";
            result += "\r\n";

            result += "</script>" + "\r\n";
            return result;
        }

        /// <summary>
        /// GetFormHtmlEnrollOTP method implmentation
        /// </summary>
        public override string GetFormHtmlEnrollOTP(AuthenticationContext usercontext)
        {
            string result = "<form method=\"post\" id=\"enrollotpForm\" autocomplete=\"off\" \">";
            IExternalProvider prov = RuntimeAuthProvider.GetProvider(PreferredMethod.Code);
            switch (usercontext.WizPageID)
            {
                case 0:
                    int pos = 0;
                    result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + prov.GetWizardUILabel(usercontext) + "</div>";
                    result += "<div class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelWREGOTP") + "</div></br>";
                    result += "<table>";
                    result += "  <tr>";
                    result += "    <th width=\"60px\" />";
                    result += "    <th width=\"34px\" />";
                    result += "    <th width=\"82px\" />";
                    result += "    <th width=\"60px\" />";
                    result += "    <th width=\"34px\" />";
                    result += "    <th width=\"82px\" />";
                    result += "  </tr>";
                    if (!Provider.Config.OTPProvider.WizardOptions.HasFlag(OTPWizardOptions.NoMicrosoftAuthenticator))
                    {
                        pos++;
                        if (pos % 2 == 1)
                            result += " <tr>";
                        result += "  <td>";
                        result += "   <img id=\"ms\" src=\"data:image/png;base64," + Convert.ToBase64String(images.microsoft.ToByteArray(ImageFormat.Png)) + "\"/>";
                        result += "  </td>";
                        result += "  <td colspan=\"2\"> ";
                        result += "    <table>";
                        result += "      <tr>";
                        result += "        <td>&nbsp</td>";
                        result += "        <td> ";
                        result += "          <a href=\"https://www.microsoft.com/store/p/microsoft-authenticator/9nblgggzmcj6\" target=\"blank\">Microsoft Store</a>";
                        result += "        </td>";
                        result += "      </tr>";
                        result += "      <tr>";
                        result += "        <td>&nbsp</td>";
                        result += "        <td>";
                        result += "          <a href=\"https://play.google.com/store/apps/details?id=com.azure.authenticator\" target=\"blank\">Google Play</a>";
                        result += "        </td>";
                        result += "      </tr>";
                        result += "      <tr>";
                        result += "        <td>&nbsp</td>";
                        result += "        <td>";
                        result += "          <a href=\"https://itunes.apple.com/app/microsoft-authenticator/id983156458\" target=\"blank\">iTunes</a>";
                        result += "        </td>";
                        result += "      </tr>";
                        result += "    </table";
                        result += "  </td>";
                        if (pos % 2 == 0)
                            result += " </tr>";
                    }
                    if (!Provider.Config.OTPProvider.WizardOptions.HasFlag(OTPWizardOptions.NoGoogleAuthenticator))
                    {
                        pos++;
                        if (pos % 2 == 1)
                            result += " <tr>";
                        result += "  <td>";
                        result += "    <img id=\"gl\" src=\"data:image/png;base64," + Convert.ToBase64String(images.google.ToByteArray(ImageFormat.Png)) + "\"/>";
                        result += "  </td>";
                        result += "  <td colspan=\"2\"> ";
                        result += "    <table>";
                        result += "      <tr>";
                        result += "        <td>&nbsp</td>";
                        result += "        <td>&nbsp</td>";
                        result += "      </tr>";
                        result += "      <tr>";
                        result += "        <td>&nbsp</td>";
                        result += "        <td >";
                        result += "          <a href=\"https://play.google.com/store/apps/details?id=com.google.android.apps.authenticator2\" target=\"blank\">Google Play</a>";
                        result += "        </td>";
                        result += "      </tr>";
                        result += "      <tr>";
                        result += "        <td>&nbsp</td>";
                        result += "        <td>";
                        result += "          <a href=\"https://itunes.apple.com/app/google-authenticator/id388497605\" target=\"blank\">iTunes</a>";
                        result += "        </td>";
                        result += "      </tr>";
                        result += "    </table";
                        result += "  </td>";
                        if (pos % 2 == 0)
                            result += " </tr>";
                    }
                    if (!Provider.Config.OTPProvider.WizardOptions.HasFlag(OTPWizardOptions.NoAuthyAuthenticator))
                    {
                        pos++;
                        if (pos % 2 == 1)
                            result += " <tr>";
                        result += "  <td>";
                        result += "    <img id=\"at\" src=\"data:image/png;base64," + Convert.ToBase64String(images.authy2.ToByteArray(ImageFormat.Png)) + "\"/>";
                        result += "  </td>";
                        result += "  <td colspan=\"2\">";
                        result += "    <table";
                        result += "      <tr>";
                        result += "        <td>&nbsp</td>";
                        result += "        <td>&nbsp</td>";
                        result += "      </tr>";
                        result += "      <tr>";
                        result += "        <td>&nbsp</td>";
                        result += "        <td>";
                        result += "          <a href=\"https://play.google.com/store/apps/details?id=com.authy.authy\" target=\"blank\">Google Play</a>";
                        result += "        </td>";
                        result += "      </tr>";
                        result += "      <tr>";
                        result += "        <td>&nbsp</td>";
                        result += "        <td>";
                        result += "          <a href=\"https://itunes.apple.com/app/authy/id494168017\" target=\"blank\">iTunes</a>";
                        result += "        </td>";
                        result += "      </tr>";
                        result += "    </table";
                        result += "  </td>";
                        if (pos % 2 == 0)
                            result += " </tr>";
                    }
                    if (!Provider.Config.OTPProvider.WizardOptions.HasFlag(OTPWizardOptions.NoGooglSearch))
                    {
                        pos++;
                        if (pos % 2 == 1)
                            result += " <tr>";
                        result += "  <td colspan=\"2\" >";
                        result += "    <img id=\"at\" src=\"data:image/png;base64," + Convert.ToBase64String(images.googlelogo.ToByteArray(ImageFormat.Png)) + "\"/>";
                        result += "  </td>";
                        result += "  <td> ";
                        result += "    <table";
                        result += "      <tr>";
                        result += "        <td>&nbsp</td>";
                        result += "        <td>&nbsp</td>";
                        result += "      </tr>";
                        result += "      <tr>";
                        result += "        <td>&nbsp</td>";
                        result += "        <td colspan=\"2\">";
                        result += "          <a href=\"https://www.google.fr/search?q=Authenticator+apps&oq=Authenticator+apps\" target=\"blank\">Search...</a>";
                        result += "        </td>";
                        result += "      </tr>";
                        result += "      <tr>";
                        result += "        <td>&nbsp</td>";
                        result += "        <td>";
                        result += "        </td>";
                        result += "      </tr>";
                        result += "    </table";
                        result += "  </td>";
                        if (pos % 2 == 0)
                            result += " </tr>";
                    }
                    result += "</table>";

                    if (Provider.KeepMySelectedOptionOn())
                    {
                        Registration reg = RuntimeRepository.GetUserRegistration(Provider.Config, usercontext.UPN);
                        result += "<br/>";
                        if (reg!=null)
                            result += "<input id=\"remember\" type=\"checkbox\" name=\"Remember\" " + ((reg.PreferredMethod == PreferredMethod.Code) ? "checked=\"checked\"> " : "> ") + Resources.GetString(ResourcesLocaleKind.Html, "HtmlCHOOSEOptionRemember2") + "<br/>";
                        else
                            result += "<input id=\"remember\" type=\"checkbox\" name=\"Remember\" checked=\"checked\"> " + Resources.GetString(ResourcesLocaleKind.Html, "HtmlCHOOSEOptionRemember2") + "<br/>";
                    }
                    if (!string.IsNullOrEmpty(prov.GetAccountManagementUrl(usercontext)))
                    {
                        result += "</br>";
                        result += "<input type=\"checkbox\" id=\"manageaccount\" name=\"manageaccount\" /> " + prov.GetUIAccountManagementLabel(usercontext) + "</br>";
                    }
                    result += "<br/>";

                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"nextButton\" type=\"submit\" class=\"submit\" name=\"nextButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMRecordNewKey") + "\" onclick=\"fnbtnclicked(2)\" />";
                    result += "</td>";
                    result += "<td style=\"width: 15px\" />";
                    result += "<td>";
                    if (Utilities.CanCancelWizard(usercontext, prov, ProviderPageMode.EnrollOTP))
                        result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                    break;
                case 1: // Always Reset the Key
                    KeysManager.NewKey(usercontext.UPN);
                    string displaykey = KeysManager.EncodedKey(usercontext.UPN);

                    result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + prov.GetWizardUILabel(usercontext) + "</div>";
                    result += "<div class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelWRQRCode") + "</div></br>";
                    if (prov.IsUIElementRequired(usercontext, RequiredMethodElements.KeyParameterRequired))
                        result += "<input id=\"secretkey\" name=\"secretkey\" type=\"text\" readonly=\"true\" placeholder=\"DisplayKey\" class=\"text fullWidth\" style=\"background-color: #F0F0F0\" value=\"" + Utilities.StripDisplayKey(displaykey) + "\"/></br></br>";
                    result += "<p style=\"text-align:center\"><img id=\"qr\" src=\"data:image/png;base64," + Provider.GetQRCodeString(usercontext) + "\"/></p></br>";
                    result += "<input id=\"nextButton\" type=\"submit\" class=\"submit\" name=\"nextButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelWVERIFYOTP") + "\" onclick=\"fnbtnclicked(3)\" />";                    
                    break;
                case 2: // Code verification
                    result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + prov.GetUILabel(usercontext) + "</div>";
                    result += "<input id=\"totp\" name=\"totp\" type=\"password\" placeholder=\"Verification Code\" class=\"text fullWidth\" autofocus=\"autofocus\" /></br>";
                    result += "<div class=\"fieldMargin smallText\">" + prov.GetUIMessage(usercontext) + "</div></br>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"checkButton\" type=\"submit\" class=\"submit\" name=\"checkButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMCheck") + "\" onclick=\"fnbtnclicked(4)\" />";
                    result += "</td>";
                    result += "<td style=\"width: 15px\" />";
                    result += "<td>";
                    if (Utilities.CanCancelWizard(usercontext, prov, ProviderPageMode.EnrollOTP))
                        result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                    break;
                case 3: // Successfull test
                    result += "</br>";
                    result += GetFormHtmlMessageZone(usercontext);
                    result += "</br>";
                    result += "<input id=\"finishButton\" type=\"submit\" class=\"submit\" name=\"finishButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelVERIFYOTPOK") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</br>";
                    result += GetFormHtmlMessageZone(usercontext);
                    break;
                case 4: // Wrong result test
                    result += "</br>";
                    result += GetFormHtmlMessageZone(usercontext);
                    result += "</br>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"priorButton\" type=\"submit\" class=\"submit\" name=\"priorButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelVERIFYOTPPRIOR") + "\" onclick=\"fnbtnclicked(2)\" />";
                    result += "</td>";
                    result += "<td style=\"width: 15px\" />";
                    result += "<td>";
                    if (Utilities.CanCancelWizard(usercontext, prov, ProviderPageMode.EnrollOTP))
                        result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                    result += "</br>";

                    break;
            }

            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";
            result += "<input id=\"btnclicked\" type=\"hidden\" name=\"btnclicked\" value=\"1\" />";

            result += "</form>";
            return result;
        }
        #endregion

        #region EnrollEmail
        /// <summary>
        /// GetFormPreRenderHtmlEnrollEmail method implementation
        /// </summary>
        public override string GetFormPreRenderHtmlEnrollEmail(AuthenticationContext usercontext)
        {
            string result = string.Empty;

            string reg = @"/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,})+$/";

            result += "<script type=\"text/javascript\">" + "\r\n";

            result += "function ValidateRegistration(frm)";
            result += "{";

            if (usercontext.WizPageID == 0)
            {
                result += "var mailformat = " + reg + " ;";
                result += "var email = document.getElementById('email');";
                result += "var err = document.getElementById('errorText');";
                result += "var canceled = document.getElementById('btnclicked');";

                result += "if ((canceled) && (canceled.value==1))";
                result += "{";
                result += "   return true;";
                result += "}";
                result += "if ((email) && (email.value=='') && (email.placeholder==''))";
                result += "{";
                result += "   err.innerHTML = \"" + Resources.GetString(ResourcesLocaleKind.Validation, "ValidIncorrectEmail") + "\";";
                result += "   return false;";
                result += "}";
                result += "if ((email) && (email.value!==''))";
                result += "{";
                result += "   if (!email.value.match(mailformat))";
                result += "   {";
                result += "      err.innerHTML = \"" + Resources.GetString(ResourcesLocaleKind.Validation, "ValidIncorrectEmail") + "\";";
                result += "      return false;";
                result += "   }";
                result += "}";
                result += "err.innerHTML = \"\";";
            }
            result += "return true;";
            result += "}";
            result += "\r\n";

            result += "function OnRefreshPost(frm)";
            result += "{";
            result += "fnbtnclicked(3);";
            result += "document.getElementById('enrollemailForm').submit();";
            result += "return true;";
            result += "}";
            result += "\r\n";

            result += "function fnbtnclicked(id)";
            result += "{";
            result += "var lnk = document.getElementById('btnclicked');";
            result += "lnk.value = id;";
            result += "return true;";
            result += "}";
            result += "\r\n";

            result += "</script>" + "\r\n";
            return result;
        }

        /// <summary>
        /// GetFormHtmlEnrollEmail method implmentation
        /// </summary>
        public override string GetFormHtmlEnrollEmail(AuthenticationContext usercontext)
        {
            string result = string.Empty;
            if (usercontext.WizPageID == 1)
            {
                result += "<script type=\"text/javascript\">" + "\r\n";
                result += "if (window.addEventListener)";
                result += "{";
                result += "window.addEventListener('load', OnRefreshPost, false);";
                result += "}";
                result += "else if (window.attachEvent)";
                result += "{";
                result += "window.attachEvent('onload', OnRefreshPost);";
                result += "}";
                result += "</script>" + "\r\n";
            }
            result += "<form method=\"post\" id=\"enrollemailForm\" autocomplete=\"off\" onsubmit=\"return ValidateRegistration(this)\" >";

            IExternalProvider prov = RuntimeAuthProvider.GetProvider(PreferredMethod.Email);
            prov.GetAuthenticationContext(usercontext);
            switch (usercontext.WizPageID)
            {
                case 0: // Get User email
                    result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + prov.GetWizardUILabel(usercontext) + "</div>";
                    if (prov.IsUIElementRequired(usercontext, RequiredMethodElements.EmailParameterRequired))
                    {
                        result += "<div class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelWREmail") + "</div></br>";
                        result += "<input id=\"email\" name=\"email\" type=\"text\" placeholder=\"" + Utilities.StripEmailAddress(usercontext.MailAddress) + "\" class=\"text fullWidth\" /></br>";
                    }
                    List<AvailableAuthenticationMethod> lst = prov.GetAuthenticationMethods(usercontext);
                    if (lst.Count > 1)
                    {
                        AuthenticationResponseKind ov = AuthenticationResponseKind.Error;
                        if (prov.AllowOverride)
                        {
                            ov = prov.GetOverrideMethod(usercontext);
                            result += "<input id=\"optiongroup\" name=\"optionitem\" type=\"radio\" value=\"Default\" checked=\"checked\" /> " + prov.GetUIDefaultChoiceLabel(usercontext) + "</br>";
                        }
                        int i = 1;
                        foreach (AvailableAuthenticationMethod met in lst)
                        {
                            if (ov != AuthenticationResponseKind.Error)
                            {
                                if (met.Method == ov)
                                    result += "<input id=\"optiongroup" + i.ToString() + "\" name=\"optionitem\" type=\"radio\" value=\"" + met.Method.ToString() + "\" checked=\"checked\" /> " + prov.GetUIChoiceLabel(usercontext, met) + "</br>";
                                else
                                    result += "<input id=\"optiongroup" + i.ToString() + "\" name=\"optionitem\" type=\"radio\" value=\"" + met.Method.ToString() + "\" /> " + prov.GetUIChoiceLabel(usercontext, met) + "</br>";
                            }
                            else
                                result += "<input id=\"optiongroup" + i.ToString() + "\" name=\"optionitem\" type=\"radio\" value=\"" + met.Method.ToString() + "\" /> " + prov.GetUIChoiceLabel(usercontext, met) + "</br>";
                            i++;
                        }
                    }
                    if (Provider.KeepMySelectedOptionOn())
                    {
                        Registration reg = RuntimeRepository.GetUserRegistration(Provider.Config, usercontext.UPN);
                        result += "</br>";
                        if (reg!=null)
                            result += "<input id=\"remember\" type=\"checkbox\" name=\"Remember\"" + (reg.PreferredMethod == PreferredMethod.Email ? " checked=\"checked\"" : "\"\"") + "\" > " + Resources.GetString(ResourcesLocaleKind.Html, "HtmlCHOOSEOptionRemember2") + "<br/>";
                        else
                            result += "<input id=\"remember\" type=\"checkbox\" name=\"Remember\" > " + Resources.GetString(ResourcesLocaleKind.Html, "HtmlCHOOSEOptionRemember2") + "<br/>";
                    }
                    if (!string.IsNullOrEmpty(prov.GetAccountManagementUrl(usercontext)))
                    {
                        result += "</br>";
                        result += "<input type=\"checkbox\" id=\"manageaccount\" name=\"manageaccount\" /> " + prov.GetUIAccountManagementLabel(usercontext) + "</br>";
                    }
                    result += "</br>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"nextButton\" type=\"submit\" class=\"submit\" name=\"nextButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelWVERIFYOTP") + "\" onclick=\"fnbtnclicked(2)\" />";
                    result += "</td>";
                    result += "<td style=\"width: 15px\" />";
                    result += "<td>";
                    if (Utilities.CanCancelWizard(usercontext, prov, ProviderPageMode.EnrollEmail))
                        result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                    result += GetFormHtmlMessageZone(usercontext);
                    break;
                case 1:
                    result += GetPartHtmlDonut();
                    result += "<div class=\"fieldMargin smallText\">" + prov.GetUIMessage(usercontext) + "</div></br>";
                    if (!string.IsNullOrEmpty(prov.GetUIWarningInternetLabel(usercontext)) && (usercontext.IsRemote))
                       result += "<div class=\"error smallText\">" + prov.GetUIWarningInternetLabel(usercontext) + "</div>";
                    if (!string.IsNullOrEmpty(prov.GetUIWarningThirdPartyLabel(usercontext)) && (usercontext.IsTwoWay))
                       result += "<div class=\"error smallText\">" + prov.GetUIWarningThirdPartyLabel(usercontext) + "</div>";
                    result += "<br />";
                    break;
                case 2: // Code verification
                    result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + prov.GetUILabel(usercontext) + "</div>";
                    result += "<input id=\"totp\" name=\"totp\" type=\"password\" placeholder=\"Verification Code\" class=\"text fullWidth\" autofocus=\"autofocus\" /></br>";
                    result += "<div class=\"fieldMargin smallText\">" + prov.GetUIMessage(usercontext) + "</div></br>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"checkButton\" type=\"submit\" class=\"submit\" name=\"checkButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMCheck") + "\" onclick=\"fnbtnclicked(4)\" />";
                    result += "</td>";
                    result += "<td style=\"width: 15px\" />";
                    result += "<td>";
                    if (Utilities.CanCancelWizard(usercontext, prov, ProviderPageMode.EnrollEmail))
                        result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                   // result += "<br />";
                    break;
                case 3: // Successfull test
                    result += "</br>";
                    result += GetFormHtmlMessageZone(usercontext);
                    result += "</br>";
                    result += "<input id=\"finishButton\" type=\"submit\" class=\"submit\" name=\"finishButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelVERIFYOTPOK") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</br>";
                    break;
                case 4: // Wrong result test
                    result += "</br>";
                    result += GetFormHtmlMessageZone(usercontext);
                    result += "</br>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"priorButton\" type=\"submit\" class=\"submit\" name=\"priorButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelVERIFYOTPPRIOR") + "\" onclick=\"fnbtnclicked(0)\" />";
                    result += "</td>";
                    result += "<td style=\"width: 15px\" />";
                    result += "<td>";
                    if (Utilities.CanCancelWizard(usercontext, prov, ProviderPageMode.EnrollEmail))
                        result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                    result += "</br>";
                    break;
            }

            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";
            result += "<input id=\"btnclicked\" type=\"hidden\" name=\"btnclicked\" value=\"1\" />";

            result += "</form>";
            return result;
        }
        #endregion

        #region EnrollPhone
        /// <summary>
        /// GetFormPreRenderHtmlEnrollPhone method implementation
        /// </summary>
        public override string GetFormPreRenderHtmlEnrollPhone(AuthenticationContext usercontext)
        {
            string result = string.Empty;
            string pho = @"/^\+(?:[0-9] ?){6,14}[0-9]$/";
            string pho10 = @"/^\d{10}$/";
            string phous = @"/^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/";

            result += "<script type=\"text/javascript\">" + "\r\n";

            result += "function ValidateRegistration(frm)";
            result += "{";
            if (usercontext.WizPageID == 0)
            {
                result += "var phoneformat = " + pho + " ;";
                result += "var phoneformat10 = " + pho10 + " ;";
                result += "var phoneformatus = " + phous + " ;";
                result += "var phone = document.getElementById('phone');";
                result += "var err = document.getElementById('errorText');";
                result += "var canceled = document.getElementById('btnclicked');";

                result += "if ((canceled) && (canceled.value==1))";
                result += "{";
                result += "   return true;";
                result += "}";

                result += "if ((phone) && (phone.value=='') && (phone.placeholder==''))";
                result += "{";
                result += "   err.innerHTML = \"" + Resources.GetString(ResourcesLocaleKind.Validation, "ValidIncorrectPhoneNumber") + "\";";
                result += "   return false;";
                result += "}";

                result += "if ((phone) && (phone.value!==''))";
                result += "{";
                result += "   if (!phone.value.match(phoneformat) && !phone.value.match(phoneformat10) && !phone.value.match(phoneformatus) )";
                result += "   {";
                result += "      err.innerHTML = \"" + Resources.GetString(ResourcesLocaleKind.Validation, "ValidIncorrectPhoneNumber") + "\";";
                result += "      return false;";
                result += "   }";
                result += "}";
                result += "err.innerHTML = \"\";";
            }
            result += "return true;";
            result += "}";
            result += "\r\n";

            result += "function OnRefreshPost(frm)";
            result += "{";
            result += "   ChangeTitle();";
            result += "   fnbtnclicked(3);";
            result += "   document.getElementById('enrollphoneForm').submit();";
            result += "   return true;";
            result += "}";
            result += "\r\n";

            result += "function fnbtnclicked(id)";
            result += "{";
            result += "   var lnk = document.getElementById('btnclicked');";
            result += "   lnk.value = id;";
            result += "   return true;";
            result += "}";
            result += "\r\n";

            result += "function ChangeTitle()";
            result += "{";
            result += "   var title = document.getElementById('mfaGreetingDescription');";
            result += "   if (title)";
            result += "   {";
            if (usercontext.TargetUIMode == ProviderPageMode.Registration)
                result += "      title.innerHTML = \"<b>" + Resources.GetString(ResourcesLocaleKind.Titles, "RegistrationPageTitle") + "</b>\";";
            else if (usercontext.TargetUIMode == ProviderPageMode.Invitation)
                result += "      title.innerHTML = \"<b>" + Resources.GetString(ResourcesLocaleKind.Titles, "InvitationPageTitle") + "</b>\";";
            result += "   }";
            result += "   return true;";
            result += "}";
            result += "\r\n";

            result += "</script>" + "\r\n";
            return result;
        }

        /// <summary>
        /// GetFormHtmlEnrollPhone method implmentation
        /// </summary>
        public override string GetFormHtmlEnrollPhone(AuthenticationContext usercontext)
        {
            string result = string.Empty;
            if (usercontext.WizPageID == 1)
            {
                result += "<script type=\"text/javascript\">" + "\r\n";
                result += "if (window.addEventListener)";
                result += "{";
                result += "window.addEventListener('load', OnRefreshPost, false);";
                result += "}";
                result += "else if (window.attachEvent)";
                result += "{";
                result += "window.attachEvent('onload', OnRefreshPost);";
                result += "}";
                result += "</script>" + "\r\n";
            }
            result += "<form method=\"post\" id=\"enrollphoneForm\" autocomplete=\"off\" onsubmit=\"return ValidateRegistration(this)\" >";

            IExternalProvider prov = RuntimeAuthProvider.GetProvider(PreferredMethod.External);
            prov.GetAuthenticationContext(usercontext);
            switch (usercontext.WizPageID)
            {
                case 0: // Get User Phone number
                    result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + prov.GetWizardUILabel(usercontext) + "</div>";
                    if (prov.IsUIElementRequired(usercontext, RequiredMethodElements.PhoneParameterRequired))
                    {
                        result += "<div class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelWRPhone") + "</div></br>";
                        result += "<input id=\"phone\" name=\"phone\" type=\"text\" placeholder=\"" + Utilities.StripPhoneNumber(usercontext.PhoneNumber) + "\" class=\"text fullWidth\" /></br>";
                    }
                    List<AvailableAuthenticationMethod> lst = prov.GetAuthenticationMethods(usercontext);
                    if (lst.Count > 1)
                    {
                        AuthenticationResponseKind ov = AuthenticationResponseKind.Error;
                        if (prov.AllowOverride)
                        {
                            ov = prov.GetOverrideMethod(usercontext);
                            result += "<input id=\"optiongroup\" name=\"optionitem\" type=\"radio\" value=\"Default\" checked=\"checked\" /> " + prov.GetUIDefaultChoiceLabel(usercontext) + "</br>";
                        }
                        int i = 1;
                        foreach (AvailableAuthenticationMethod met in lst)
                        {
                            if (ov != AuthenticationResponseKind.Error)
                            {
                                if (met.Method == ov)
                                    result += "<input id=\"optiongroup" + i.ToString() + "\" name=\"optionitem\" type=\"radio\" value=\"" + met.Method.ToString() + "\" checked=\"checked\" /> " + prov.GetUIChoiceLabel(usercontext, met) + "</br>";
                                else
                                    result += "<input id=\"optiongroup" + i.ToString() + "\" name=\"optionitem\" type=\"radio\" value=\"" + met.Method.ToString() + "\" /> " + prov.GetUIChoiceLabel(usercontext, met) + "</br>";
                            }
                            else
                                result += "<input id=\"optiongroup" + i.ToString() + "\" name=\"optionitem\" type=\"radio\" value=\"" + met.Method.ToString() + "\" /> " + prov.GetUIChoiceLabel(usercontext, met) + "</br>";
                            i++;
                        }
                    }

                    if (Provider.KeepMySelectedOptionOn())
                    {
                        Registration reg = RuntimeRepository.GetUserRegistration(Provider.Config, usercontext.UPN);
                        result += "</br>";
                        if (reg!=null)
                            result += "<input id=\"remember\" type=\"checkbox\" name=\"Remember\"" + (reg.PreferredMethod == PreferredMethod.External ? " checked=\"checked\"" : "\"\"") + "\" > " + Resources.GetString(ResourcesLocaleKind.Html, "HtmlCHOOSEOptionRemember2") + "<br/>";
                        else
                            result += "<input id=\"remember\" type=\"checkbox\" name=\"Remember\" > " + Resources.GetString(ResourcesLocaleKind.Html, "HtmlCHOOSEOptionRemember2") + "<br/>";
                    }
                    if (!string.IsNullOrEmpty(prov.GetAccountManagementUrl(usercontext)))
                    {
                        result += "</br>";
                        result += "<input type=\"checkbox\" id=\"manageaccount\" name=\"manageaccount\" /> " + prov.GetUIAccountManagementLabel(usercontext) + "</br>";
                    }

                    result += "</br>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"nextButton\" type=\"submit\" class=\"submit\" name=\"nextButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelWVERIFYOTP") + "\" onclick=\"fnbtnclicked(2)\" />";
                    result += "</td>";
                    result += "<td style=\"width: 15px\" />";
                    result += "<td>";
                    if (Utilities.CanCancelWizard(usercontext, prov, ProviderPageMode.EnrollPhone))
                        result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                    result += GetFormHtmlMessageZone(usercontext);
                    break;
                case 1:
                    result += GetPartHtmlDonut();
                    result += "<div class=\"fieldMargin smallText\">" + prov.GetUIMessage(usercontext) + "</div></br>";
                    if (!string.IsNullOrEmpty(prov.GetUIWarningInternetLabel(usercontext)) && (usercontext.IsRemote))
                        result += "<div class=\"error smallText\">" + prov.GetUIWarningInternetLabel(usercontext) + "</div>";
                    if (!string.IsNullOrEmpty(prov.GetUIWarningThirdPartyLabel(usercontext)) && (usercontext.IsTwoWay))
                        result += "<div class=\"error smallText\">" + prov.GetUIWarningThirdPartyLabel(usercontext) + "</div>";
                    result += "<br />";
                    break;
                case 2: // Code verification If One-Way
                    result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + prov.GetUILabel(usercontext) + "</div>";
                    result += "<input id=\"totp\" name=\"totp\" type=\"password\" placeholder=\"Verification Code\" class=\"text fullWidth\" autofocus=\"autofocus\" /></br>";
                    result += "<div class=\"fieldMargin smallText\">" + prov.GetUIMessage(usercontext) + "</div></br>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"checkButton\" type=\"submit\" class=\"submit\" name=\"checkButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMCheck") + "\" onclick=\"fnbtnclicked(4)\" />";
                    result += "</td>";
                    result += "<td style=\"width: 15px\" />";
                    result += "<td>";
                    if (Utilities.CanCancelWizard(usercontext, prov, ProviderPageMode.EnrollPhone))
                        result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                    break;
                case 3: // Successfull test
                    result += "</br>";
                    result += GetFormHtmlMessageZone(usercontext);
                    result += "</br>";
                    result += "<input id=\"finishButton\" type=\"submit\" class=\"submit\" name=\"finishButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelVERIFYOTPOK") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</br>";
                    break;
                case 4: // Wrong result test
                    result += "</br>";
                    result += GetFormHtmlMessageZone(usercontext);
                    result += "</br>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"priorButton\" type=\"submit\" class=\"submit\" name=\"priorButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelVERIFYOTPPRIOR") + "\" onclick=\"fnbtnclicked(0)\" />";
                    result += "</td>";
                    result += "<td style=\"width: 15px\" />";
                    result += "<td>";
                    if (Utilities.CanCancelWizard(usercontext, prov, ProviderPageMode.EnrollPhone))
                        result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                    result += "</br>";
                    break;
            }
            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";
            result += "<input id=\"btnclicked\" type=\"hidden\" name=\"btnclicked\" value=\"1\" />";

            result += "</form>";
            return result;
        }
        #endregion

        #region EnrollBiometrics
        /// <summary>
        /// GetFormPreRenderHtmlEnrollBio method implementation
        /// </summary>
        public override string GetFormPreRenderHtmlEnrollBio(AuthenticationContext Context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetFormHtmlEnrollBio method implementation
        /// </summary>
        public override string GetFormHtmlEnrollBio(AuthenticationContext Context)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region EnrollPINCode
        /// <summary>
        /// GetFormPreRenderHtmlEnrollPinCode method implementation
        /// </summary>
        public override string GetFormPreRenderHtmlEnrollPinCode(AuthenticationContext usercontext)
        {
            string result = string.Empty;

            string pl = Provider.Config.PinLength.ToString();
            string reg = @"/([0-9]{"+pl+","+pl+"})/";  

            result += "<script type=\"text/javascript\">" + "\r\n";

            result += "function ValidateRegistration(frm)";
            result += "{";

            if (usercontext.WizPageID == 0)
            {
                result += "var pinformat = " + reg + " ;";
                result += "var pincode = document.getElementById('pincode');";
                result += "var err = document.getElementById('errorText');";

                result += "if ((pincode) && (pincode.value=='') && (pincode.placeholder==''))";
                result += "{";
                result += "err.innerHTML = \"" + Resources.GetString(ResourcesLocaleKind.Validation, "ValidIncorrectPinCode") + "\";";
                result += "return false;";
                result += "}";
                result += "if ((pincode) && (pincode.value!==''))";
                result += "{";
                result += "if (!pincode.value.match(pinformat))";
                result += "{";
                result += "err.innerHTML = \"" + Resources.GetString(ResourcesLocaleKind.Validation, "ValidIncorrectPinCode") + "\";";
                result += "return false;";
                result += "}";
                result += "}";
                result += "err.innerHTML = \"\";";
            }
            result += "return true;";
            result += "}";
            result += "\r\n";

            result += "function fnbtnclicked(id)";
            result += "{";
            result += "var lnk = document.getElementById('btnclicked');";
            result += "lnk.value = id;";
            result += "return true;";
            result += "}";
            result += "\r\n";

            result += "</script>" + "\r\n";
            return result;
        }

        /// <summary>
        /// GetFormHtmlEnrollEmail method implmentation
        /// </summary>
        public override string GetFormHtmlEnrollPinCode(AuthenticationContext usercontext)
        {
            string result = "<form method=\"post\" id=\"enrollPinForm\" autocomplete=\"off\" onsubmit=\"return ValidateRegistration(this)\" >";
            switch (usercontext.WizPageID)
            {
                case 0: // Get User Pin
                    result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + BaseExternalProvider.GetPINWizardUILabel(usercontext) + "</div>";
                    result += "<div class=\"fieldMargin smallText\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelWRPinCode") + "</div></br>";
                    if (usercontext.PinCode <= 0)
                        result += "<input id=\"pincode\" name=\"pincode\" type=\"text\" placeholder=\"" + Utilities.StripPinCode(Provider.Config.DefaultPin) + "\" class=\"text fullWidth\" /></br>";
                    else
                        result += "<input id=\"pincode\" name=\"pincode\" type=\"text\" placeholder=\"" + Utilities.StripPinCode(usercontext.PinCode) + "\" class=\"text fullWidth\" /></br>";
                    result += "</br>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"nextButton\" type=\"submit\" class=\"submit\" name=\"nextButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelWVERIFYOTP") + "\" onclick=\"fnbtnclicked(2)\" />";
                    result += "</td>";
                    result += "<td style=\"width: 15px\" />";
                    result += "<td>";
                    if (Utilities.CanCancelWizard(usercontext, null, ProviderPageMode.EnrollPin))
                        result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                    result += GetFormHtmlMessageZone(usercontext);
                    break;
                case 2: // Code verification
                    result += "<div id=\"wizardMessage\" class=\"groupMargin\">" + BaseExternalProvider.GetPINLabel(usercontext) + "</div>";
                    result += "<input id=\"pincode\" name=\"pincode\" type=\"password\" placeholder=\"PIN\" class=\"text fullWidth\" autofocus=\"autofocus\" /></br>";
                    result += "<div class=\"fieldMargin smallText\">" + BaseExternalProvider.GetPINMessage(usercontext) + "</div></br>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"checkButton\" type=\"submit\" class=\"submit\" name=\"checkButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlUIMCheck") + "\" onclick=\"fnbtnclicked(4)\" />";
                    result += "</td>";
                    result += "<td style=\"width: 15px\" />";
                    result += "<td>";
                    if (Utilities.CanCancelWizard(usercontext, null, ProviderPageMode.EnrollPin))
                        result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                    break;
                case 3: // Successfull test
                    result += "</br>";
                    result += GetFormHtmlMessageZone(usercontext);
                    result += "</br>";
                    result += "<input id=\"finishButton\" type=\"submit\" class=\"submit\" name=\"finishButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelVERIFYOTPOK") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</br>";
                    break;
                case 4: // Wrong result test
                    result += "</br>";
                    result += GetFormHtmlMessageZone(usercontext);
                    result += "</br>";
                    result += "<table><tr>";
                    result += "<td>";
                    result += "<input id=\"priorButton\" type=\"submit\" class=\"submit\" name=\"priorButton\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlLabelVERIFYOTPPRIOR") + "\" onclick=\"fnbtnclicked(2)\" />";
                    result += "</td>";
                    result += "<td style=\"width: 15px\" />";
                    result += "<td>";
                    if (Utilities.CanCancelWizard(usercontext, null, ProviderPageMode.EnrollPin))
                        result += "<input id=\"mfa-cancelButton\" type=\"submit\" class=\"submit\" name=\"cancel\" value=\"" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlPWDCancel") + "\" onclick=\"fnbtnclicked(1)\" />";
                    result += "</td>";
                    result += "</tr></table>";
                    result += "</br>";                   
                    break;
            }

            result += "<input id=\"context\" type=\"hidden\" name=\"Context\" value=\"%Context%\"/>";
            result += "<input id=\"authMethod\" type=\"hidden\" name=\"AuthMethod\" value=\"%AuthMethod%\"/>";
            result += "<input id=\"btnclicked\" type=\"hidden\" name=\"btnclicked\" value=\"1\" />";

            result += "</form>";
            return result;
        }
        #endregion

        /// <summary>
        /// GetPartHtmlSelectMethod method ipmplmentation
        /// </summary>
        private string GetPartHtmlSelectMethod(AuthenticationContext usercontext)
        {
            Registration reg = RuntimeRepository.GetUserRegistration(Provider.Config, usercontext.UPN);
            PreferredMethod method = PreferredMethod.None;
            if (reg == null)
                method = usercontext.PreferredMethod;
            else
                method = reg.PreferredMethod;
            string result = string.Empty;
            result += "<b><div class=\"fieldMargin\">" + Resources.GetString(ResourcesLocaleKind.Html, "HtmlREGAccessMethod") + "</div></b>";
            result += "<select id=\"selectopt\" name=\"selectopt\" role=\"combobox\"  required=\"required\" contenteditable=\"false\" class=\"text fullWidth\" >";

            result += "<option value=\"0\" " + ((method == PreferredMethod.Choose) ? "selected=\"true\"> " : "> ") + Resources.GetString(ResourcesLocaleKind.Html, "HtmlREGOptionChooseBest") + "</option>";

            if (RuntimeAuthProvider.IsProviderAvailableForUser(usercontext, PreferredMethod.Code))
                result += "<option value=\"1\" " + ((method == PreferredMethod.Code) ? "selected=\"true\"> " : "> ") + RuntimeAuthProvider.GetProvider(PreferredMethod.Code).GetUIListChoiceLabel(usercontext) + "</option>";

            if (RuntimeAuthProvider.IsProviderAvailableForUser(usercontext, PreferredMethod.Email))
                result += "<option value=\"2\" " + ((method == PreferredMethod.Email) ? "selected=\"true\"> " : "> ") + RuntimeAuthProvider.GetProvider(PreferredMethod.Email).GetUIListChoiceLabel(usercontext) + "</option>";

            if (RuntimeAuthProvider.IsProviderAvailableForUser(usercontext, PreferredMethod.External))
                result += "<option value=\"3\" " + ((method == PreferredMethod.External) ? "selected=\"true\"> " : "> ") + RuntimeAuthProvider.GetProvider(PreferredMethod.External).GetUIListChoiceLabel(usercontext) + "</option>";

            if (RuntimeAuthProvider.IsProviderAvailableForUser(usercontext, PreferredMethod.Azure))
                result += "<option value=\"4\" " + ((method == PreferredMethod.Azure) ? "selected=\"true\">" : ">") + RuntimeAuthProvider.GetProvider(PreferredMethod.Azure).GetUIListChoiceLabel(usercontext) + "</option>";

            if (RuntimeAuthProvider.IsProviderAvailableForUser(usercontext, PreferredMethod.Biometrics))
                result += "<option value=\"5\" " + ((method == PreferredMethod.Biometrics) ? "selected=\"true\">" : ">") + RuntimeAuthProvider.GetProvider(PreferredMethod.Biometrics).GetUIListChoiceLabel(usercontext) + "</option>";

            result += "</select></br>";
            return result;
        }

    }
}