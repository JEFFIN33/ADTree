// Decompiled with JetBrains decompiler
// Type: ADTree.ADtree
// Assembly: ADTree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AD3D2372-2CAC-4221-B25D-8335DA876BAA
// Assembly location: \Source\mRemoteNG\mRemoteV1\References\ADTree.dll

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ADTree
{
  [DesignerGenerated]
  public class ADtree : UserControl
  {
    private IContainer components;
    [AccessedThroughProperty("tvAD")]
    private TreeView _tvAD;
    [AccessedThroughProperty("imglTree")]
    private ImageList _imglTree;
    private string _ADPath;
    private string _Domain;

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
      var componentResourceManager = new ComponentResourceManager(typeof (ADtree));
      tvAD = new TreeView();
      imglTree = new ImageList(components);
      SuspendLayout();
      tvAD.BorderStyle = BorderStyle.None;
      tvAD.Dock = DockStyle.Fill;
      tvAD.HideSelection = false;
      tvAD.ImageIndex = 0;
      tvAD.ImageList = imglTree;
      tvAD.Location = new Point(0, 0);
      tvAD.Name = $"tvAD";
      tvAD.SelectedImageIndex = 0;
      tvAD.Size = new Size(254, 254);
      tvAD.TabIndex = 0;
      imglTree.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imglTree.ImageStream");
      imglTree.TransparentColor = Color.Transparent;
      imglTree.Images.SetKeyName(0, "Root.png");
      imglTree.Images.SetKeyName(1, "OU.png");
      imglTree.Images.SetKeyName(2, "Folder.png");
      imglTree.Images.SetKeyName(3, "Question.png");
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      Controls.Add(tvAD);
      Name = nameof (ADtree);
      ResumeLayout(false);
    }

    internal virtual TreeView tvAD
    {
      get
      {
        return _tvAD;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        TreeViewEventHandler viewEventHandler1 = tvAD_AfterExpand;
        MouseEventHandler mouseEventHandler = tvAD_MouseDown;
        TreeViewEventHandler viewEventHandler2 = tvAD_AfterSelect;
        if (_tvAD != null)
        {
          _tvAD.AfterExpand -= viewEventHandler1;
          _tvAD.MouseDown -= mouseEventHandler;
          _tvAD.AfterSelect -= viewEventHandler2;
        }
        _tvAD = value;
        if (_tvAD == null)
          return;
        _tvAD.AfterExpand += viewEventHandler1;
        _tvAD.MouseDown += mouseEventHandler;
        _tvAD.AfterSelect += viewEventHandler2;
      }
    }

    internal virtual ImageList imglTree
    {
      get
      {
        return _imglTree;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        _imglTree = value;
      }
    }

    public string ADPath
    {
      get
      {
        return _ADPath;
      }
      set
      {
        _ADPath = value;
      }
    }

    public string Domain
    {
      get
      {
        if (Operators.CompareString(_Domain, "", false) != 0)
          return _Domain;
        return Environment.UserDomainName.ToLower();
      }
      set
      {
        _Domain = value;
      }
    }

    public override ContextMenu ContextMenu
    {
      get
      {
        return tvAD.ContextMenu;
      }
      set
      {
        tvAD.ContextMenu = value;
      }
    }

    public TreeNode SelectedNode
    {
      get
      {
        return tvAD.SelectedNode;
      }
      set
      {
        tvAD.SelectedNode = value;
      }
    }

    public override void Refresh()
    {
      base.Refresh();
      LoadAD();
    }

    public void SelectNodeAt(Point Pt)
    {
      tvAD.SelectedNode = tvAD.GetNodeAt(Pt);
    }

    public void LoadAD()
    {
      tvAD.Nodes.Clear();
      var treeNode = new TreeNode(Domain);
      treeNode.Tag = "";
      tvAD.Nodes.Add(treeNode);
      AddTreeNodes(treeNode);
      tvAD.Nodes[0].Expand();
    }

    public void AddTreeNodes(TreeNode tNode)
    {
      var adhelper = new ADhelper(Domain);
      adhelper.GetChildEntries(Conversions.ToString(tNode.Tag));
      var enumerator1 = adhelper.Children.GetEnumerator();
      tvAD.BeginUpdate();
      while (enumerator1.MoveNext())
      {
        var flag1 = false;
        if (enumerator1.Key == null) continue;
        var node1 = new TreeNode(enumerator1.Key.ToString().Substring(3))
        {
            Tag = RuntimeHelpers.GetObjectValue(enumerator1.Value)
        };
        if (Operators.CompareString(enumerator1.Key.ToString().Substring(0, 2), "CN", false) != 0)
            flag1 = true;
        if (Operators.CompareString(enumerator1.Key.ToString().Substring(0), "CN=Users", false) == 0)
            flag1 = true;
        if (flag1)
        {
            var flag2 = false;
            try
            {
                foreach (TreeNode node2 in tNode.Nodes)
                {
                    if (Operators.CompareString(node2.Text, node1.Text, false) != 0) continue;
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
        var imageIndex = GetImageIndex(enumerator1.Key.ToString().Substring(0, 2));
        node1.ImageIndex = imageIndex;
        node1.SelectedImageIndex = imageIndex;
      }
      tvAD.EndUpdate();
    }

    public int GetImageIndex(string ObjType)
    {
      var sLeft = ObjType;
      if (Operators.CompareString(sLeft, "CN", false) == 0)
        return 2;
      return Operators.CompareString(sLeft, "OU", false) == 0 ? 1 : 3;
    }

    private void tvAD_AfterExpand(object sender, TreeViewEventArgs e)
    {
      try
      {
        foreach (TreeNode node in e.Node.Nodes)
          AddTreeNodes(node);
      }
      catch(Exception ex)
      {
        Console.WriteLine(ex.StackTrace);
      }
    }

    private void tvAD_AfterSelect(object sender, TreeViewEventArgs e)
    {
      _ADPath = Conversions.ToString(e.Node.Tag);
      var pathChangedEvent = ADPathChanged;
      pathChangedEvent?.Invoke(this);
    }

    private void tvAD_MouseDown(object sender, MouseEventArgs e)
    {
      var mouseDownEvent = MouseDown;
      mouseDownEvent?.Invoke(this, e);
    }

    private void ADtree_Load(object sender, EventArgs e)
    {
      LoadAD();
    }

    public event ADPathChangedEventHandler ADPathChanged;

    public event MouseDownEventHandler MouseDown;

    public delegate void ADPathChangedEventHandler(object sender);

    public delegate void MouseDownEventHandler(object sender, MouseEventArgs e);
  }
}
