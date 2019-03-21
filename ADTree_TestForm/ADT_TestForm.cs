using System;
using System.Windows.Forms;

namespace ADTree_TestForm
{
    public partial class AdtTestForm
    {
        private string _currentDomain;

        public AdtTestForm()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Private Methods

        #region Event Handlers

        private void ADImport_Load(object sender, EventArgs e)
        {
            ApplyLanguage();
            _currentDomain = Environment.UserDomainName;
            txtDomain.Text = _currentDomain;
            ActiveDirectoryTree.Domain = _currentDomain;
            EnableDisableImportButton();

            // Domain doesn't refresh on load, so it defaults to DOMAIN without this...
            ChangeDomain();
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
        }

        /*
	    private static void txtDomain_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				e.IsInputKey = true;
		}
        */

        private void TxtDomain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            ChangeDomain();
            e.SuppressKeyPress = true;
        }

        private void BtnChangeDomain_Click(object sender, EventArgs e)
        {
            ChangeDomain();
        }

        private void ActiveDirectoryTree_ADPathChanged(object sender)
        {
            EnableDisableImportButton();
        }

        #endregion

        private void ApplyLanguage()
        {
            btnImport.Text = "Import";
            lblDomain.Text = "Domain";
            chkSubOU.Text = "Import Sub OUs";
            btnChangeDomain.Text = "Change";
            btnClose.Text = "Close";
        }

        private void ChangeDomain()
        {
            _currentDomain = txtDomain.Text;
            ActiveDirectoryTree.Domain = _currentDomain;
            ActiveDirectoryTree.Refresh();
        }

        private void EnableDisableImportButton()
        {
            btnImport.Enabled = !string.IsNullOrEmpty(ActiveDirectoryTree.AdPath);
        }

        #endregion
    }
}