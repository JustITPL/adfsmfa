﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neos.IdentityServer.MultiFactor.Administration;
using Neos.IdentityServer.MultiFactor;

namespace Neos.IdentityServer.Console
{
    public partial class UserAttestationsControl : UserControl, IUserPropertiesDataObject
    {
        private UserPropertyPage userPropertyPage;
        private string _upn = string.Empty;

        /// <summary>
        /// UserPropertiesKeysControl constructor
        /// </summary>
        public UserAttestationsControl(UserPropertyPage parentPropertyPage)
        {
            InitializeComponent();
            userPropertyPage = parentPropertyPage;
        }

        #region IUserPropertiesDataObject
        /// <summary>
        /// SyncDisabled property implementation
        /// </summary>
        public bool SyncDisabled { get; set; } = false;


        /// <summary>
        /// GetUserControlData method implementation
        /// </summary>
        public MFAUserList GetUserControlData(MFAUserList lst)
        {
            MFAUser obj = lst[0];
            return lst;
        }

        /// <summary>
        /// SetUserControlData method implementation
        /// </summary>
        public void SetUserControlData(MFAUserList lst, bool disablesync)
        {
            SyncDisabled = disablesync;
            try
            {
                MFAUser obj = ((MFAUserList)lst)[0];
                _upn = ((MFAUser)obj).UPN;
                BuildKeysControl();
              //  userPropertyPage.Dirty = true;
                UpdateControlsEnabled();
            }
            finally
            {
                SyncDisabled = false;
            }
        }
        #endregion

        /// <summary>
        /// UpdateControlsEnabled method implementation
        /// </summary>
        private void UpdateControlsEnabled()
        {
            foreach (Control ctrl in this.WebAuthN.Controls)
            {
                if (ctrl is CheckBox)
                {
                    CheckBox box = ctrl as CheckBox;
                    if (box.Checked)
                    { 
                        btnDel.Enabled = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// DeleteKeys method implementation
        /// </summary>
        private void DeleteKeys()
        {
            foreach (Control ctrl in this.WebAuthN.Controls)
            {
                if (ctrl is CheckBox)
                {
                    CheckBox box = ctrl as CheckBox;
                    if (box.Checked)
                    {
                        MMCService.RemoveUserStoredCredentials(_upn, box.Tag.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// BuildKeysControl method implementation
        /// </summary>
        private void BuildKeysControl()
        {
            var credlist = MMCService.GetUserStoredCredentials(_upn);
            this.WebAuthN.Controls.Clear();
            int i = 1;
            foreach (WebAuthNCredentialInformation cred in credlist)
            {
                CheckBox rdio = new CheckBox();
                rdio.Tag = cred.CredentialID;
                rdio.Top = (i * 25);
                rdio.Left = 25;
                rdio.Width = 125;
                Font fnt = rdio.Font;
                rdio.Font = new Font(fnt.FontFamily, fnt.Size, FontStyle.Bold, fnt.Unit, fnt.GdiCharSet);
                rdio.Text = cred.CredType;
                this.WebAuthN.Controls.Add(rdio);
                rdio.CheckedChanged += Rdio_CheckedChanged;

                Label lbl = new Label();
                lbl.Top = (i * 25) + 6;
                lbl.Left = 175;
                lbl.Width = 150;
                lbl.Text = cred.RegDate.ToString();
                this.WebAuthN.Controls.Add(lbl);

                Label cnt = new Label();
                Font fntcnt = cnt.Font;
                cnt.Font = new Font(fntcnt.FontFamily, fntcnt.Size, FontStyle.Bold, fntcnt.Unit, fntcnt.GdiCharSet);
                cnt.Top = (i * 25) + 6;
                cnt.Left = 350;
                cnt.Width = 45;
                cnt.Text = "(" + cred.SignatureCounter.ToString() + ")";
                this.WebAuthN.Controls.Add(cnt);
                i++;
            }
        }

        private void Rdio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControlsEnabled();
        }

        /// <summary>
        /// btnDel_Click method implementation
        /// </summary>
        private void btnDel_Click(object sender, EventArgs e)
        {
            DeleteKeys();
            BuildKeysControl();
        }
    }
}
