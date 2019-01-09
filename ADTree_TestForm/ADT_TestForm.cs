using System;
using System.Windows.Forms;
//using WeifenLuo.WinFormsUI.Docking;

namespace ADTree_TestForm
{
    public partial class ADT_TestForm
    {
        private string _currentDomain;
        //private DockContent DockPnl;

        public ADT_TestForm()
        {
            InitializeComponent();
            //FontOverrider.FontOverride(this);
            //DockPnl = new DockContent();
            //_currentDomain = Environment.UserDomainName;
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

        private void btnImport_Click(object sender, EventArgs e)
        {
        }

        /*
	    private static void txtDomain_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				e.IsInputKey = true;
		}
        */

        private void txtDomain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            ChangeDomain();
            e.SuppressKeyPress = true;
        }

        private void btnChangeDomain_Click(object sender, EventArgs e)
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
            btnImport.Enabled = !string.IsNullOrEmpty(ActiveDirectoryTree.ADPath);
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
