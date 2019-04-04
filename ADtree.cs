using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ADTree
{
    public sealed class ADtree : UserControl
    {
        private IContainer components;
        [AccessedThroughProperty("tvAD")] private TreeView _tvAd;
        [AccessedThroughProperty("imglTree")] private ImageList _imglTree;

        private string _domain;

        public ADtree()
        {
            Load += ADtree_Load;
            InitializeComponent();
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!disposing || components == null)
                    return;
                components.Dispose();
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            components = new Container();
            var componentResourceManager = new ComponentResourceManager(typeof(ADtree));
            TvAd = new TreeView();
            ImglTree = new ImageList(components);
            SuspendLayout();
            TvAd.BorderStyle = BorderStyle.None;
            TvAd.Dock = DockStyle.Fill;
            TvAd.HideSelection = false;
            TvAd.ImageIndex = 0;
            TvAd.ImageList = ImglTree;
            TvAd.Location = new Point(0, 0);
            // ReSharper disable once NotResolvedInText
            TvAd.Name = "tvAD";
            TvAd.SelectedImageIndex = 0;
            TvAd.Size = new Size(254, 254);
            TvAd.TabIndex = 0;
            ImglTree.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imglTree.ImageStream");
            ImglTree.TransparentColor = Color.Transparent;
            ImglTree.Images.SetKeyName(0, "Root.png");
            ImglTree.Images.SetKeyName(1, "OU.png");
            ImglTree.Images.SetKeyName(2, "Folder.png");
            ImglTree.Images.SetKeyName(3, "Question.png");
            AutoScaleDimensions = new SizeF(96f, 96f);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(TvAd);
            Name = nameof(ADtree);
            ResumeLayout(false);
        }

        private TreeView TvAd
        {
            get => _tvAd;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_tvAd != null)
                {
                    _tvAd.AfterExpand -= TvAD_AfterExpand;
                    _tvAd.AfterSelect -= TvAD_AfterSelect;
                }

                _tvAd = value;
                if (_tvAd == null)
                    return;
                _tvAd.AfterExpand += TvAD_AfterExpand;
                _tvAd.AfterSelect += TvAD_AfterSelect;
            }
        }

        internal ImageList ImglTree
        {
            get => _imglTree;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set => _imglTree = value;
        }

        public string AdPath { get; set; }

        public string Domain
        {
            private get => string.IsNullOrEmpty(_domain) == false ? _domain : Environment.UserDomainName;
            set => _domain = value;
        }

        public override ContextMenu ContextMenu
        {
            get => TvAd.ContextMenu;
            set => TvAd.ContextMenu = value;
        }

        public TreeNode SelectedNode
        {
            get => TvAd.SelectedNode;
            set => TvAd.SelectedNode = value;
        }

        public override void Refresh()
        {
            base.Refresh();
            LoadAd();
        }

#if false
        public void SelectNodeAt(Point Pt)
        {
            tvAD.SelectedNode = tvAD.GetNodeAt(Pt);
        }
#endif

        private void LoadAd()
        {
            TvAd.Nodes.Clear();
            var treeNode = new TreeNode(Domain) {Tag = ""};
            TvAd.Nodes.Add(treeNode);
            AddTreeNodes(treeNode);
            TvAd.Nodes[0].Expand();
        }

        private void AddTreeNodes(TreeNode tNode)
        {
            var adhelper = new ADhelper(Domain);
            adhelper.GetChildEntries(tNode.Tag.ToString());
            var enumerator = adhelper.Children.GetEnumerator();
            TvAd.BeginUpdate();
            while (enumerator.MoveNext())
            {
                var flag1 = false;
                if (enumerator.Key == null) continue;
                var node1 = new TreeNode(enumerator.Key.ToString().Substring(3))
                {
                    Tag = RuntimeHelpers.GetObjectValue(enumerator.Value)
                };
                if (!enumerator.Key.ToString().Substring(0, 2).Equals("CN") ||
                    enumerator.Key.ToString().Equals("CN=Computers") ||
                    enumerator.Key.ToString().Equals("CN=Users"))
                    flag1 = true;

                if (flag1)
                {
                    var flag2 = false;
                    try
                    {
                        foreach (TreeNode node2 in tNode.Nodes)
                        {
                            if (!node2.Text.Equals(node1.Text)) continue;
                            flag2 = true;
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    if (!flag2)
                        tNode.Nodes.Add(node1);
                }

                var imageIndex = GetImageIndex(enumerator.Key.ToString().Substring(0, 2));
                node1.ImageIndex = imageIndex;
                node1.SelectedImageIndex = imageIndex;
            }

            TvAd.EndUpdate();
        }

        private static int GetImageIndex(string objType)
        {
            if (objType.Equals("CN"))
                return 2;
            return objType.Equals("OU") ? 1 : 3;
        }

        private void TvAD_AfterExpand(object sender, TreeViewEventArgs e)
        {
            try
            {
                foreach (TreeNode node in e.Node.Nodes)
                    AddTreeNodes(node);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void TvAD_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AdPath = e.Node.Tag.ToString();
            var pathChangedEvent = AdPathChanged;
            pathChangedEvent?.Invoke(this);
        }

        private void ADtree_Load(object sender, EventArgs e)
        {
            LoadAd();
        }

        public event AdPathChangedEventHandler AdPathChanged;

        public delegate void AdPathChangedEventHandler(object sender);
    }
}