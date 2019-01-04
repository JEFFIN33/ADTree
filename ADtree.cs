// Decompiled with JetBrains decompiler
// Type: ADTree.ADtree
// Assembly: ADTree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AD3D2372-2CAC-4221-B25D-8335DA876BAA
// Assembly location: \Source\mRemoteNG\mRemoteV1\References\ADTree.dll

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
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
      this.Load += new EventHandler(this.ADtree_Load);
      this.InitializeComponent();
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing || this.components == null)
          return;
        this.components.Dispose();
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ADtree));
      this.tvAD = new TreeView();
      this.imglTree = new ImageList(this.components);
      this.SuspendLayout();
      this.tvAD.BorderStyle = BorderStyle.None;
      this.tvAD.Dock = DockStyle.Fill;
      this.tvAD.HideSelection = false;
      this.tvAD.ImageIndex = 0;
      this.tvAD.ImageList = this.imglTree;
      this.tvAD.Location = new Point(0, 0);
      this.tvAD.Name = "tvAD";
      this.tvAD.SelectedImageIndex = 0;
      TreeView tvAd = this.tvAD;
      Size size1 = new Size(254, 254);
      Size size2 = size1;
      tvAd.Size = size2;
      this.tvAD.TabIndex = 0;
      this.imglTree.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imglTree.ImageStream");
      this.imglTree.TransparentColor = Color.Transparent;
      this.imglTree.Images.SetKeyName(0, "Root.png");
      this.imglTree.Images.SetKeyName(1, "OU.png");
      this.imglTree.Images.SetKeyName(2, "Folder.png");
      this.imglTree.Images.SetKeyName(3, "Question.png");
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.tvAD);
      this.Name = nameof (ADtree);
      size1 = new Size(254, 254);
      this.Size = size1;
      this.ResumeLayout(false);
    }

    internal virtual TreeView tvAD
    {
      get
      {
        return this._tvAD;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        TreeViewEventHandler viewEventHandler1 = new TreeViewEventHandler(this.tvAD_AfterExpand);
        MouseEventHandler mouseEventHandler = new MouseEventHandler(this.tvAD_MouseDown);
        TreeViewEventHandler viewEventHandler2 = new TreeViewEventHandler(this.tvAD_AfterSelect);
        if (this._tvAD != null)
        {
          this._tvAD.AfterExpand -= viewEventHandler1;
          this._tvAD.MouseDown -= mouseEventHandler;
          this._tvAD.AfterSelect -= viewEventHandler2;
        }
        this._tvAD = value;
        if (this._tvAD == null)
          return;
        this._tvAD.AfterExpand += viewEventHandler1;
        this._tvAD.MouseDown += mouseEventHandler;
        this._tvAD.AfterSelect += viewEventHandler2;
      }
    }

    internal virtual ImageList imglTree
    {
      get
      {
        return this._imglTree;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        this._imglTree = value;
      }
    }

    public string ADPath
    {
      get
      {
        return this._ADPath;
      }
      set
      {
        this._ADPath = value;
      }
    }

    public string Domain
    {
      get
      {
        if (Operators.CompareString(this._Domain, "", false) != 0)
          return this._Domain;
        return Environment.UserDomainName.ToLower();
      }
      set
      {
        this._Domain = value;
      }
    }

    public override ContextMenu ContextMenu
    {
      get
      {
        return this.tvAD.ContextMenu;
      }
      set
      {
        this.tvAD.ContextMenu = value;
      }
    }

    public TreeNode SelectedNode
    {
      get
      {
        return this.tvAD.SelectedNode;
      }
      set
      {
        this.tvAD.SelectedNode = value;
      }
    }

    public override void Refresh()
    {
      base.Refresh();
      this.LoadAD();
    }

    public void SelectNodeAt(Point Pt)
    {
      this.tvAD.SelectedNode = this.tvAD.GetNodeAt(Pt);
    }

    public void LoadAD()
    {
      this.tvAD.Nodes.Clear();
      TreeNode treeNode = new TreeNode(this.Domain);
      treeNode.Tag = (object) "";
      this.tvAD.Nodes.Add(treeNode);
      this.AddTreeNodes(treeNode);
      this.tvAD.Nodes[0].Expand();
    }

    public void AddTreeNodes(TreeNode tNode)
    {
      ADhelper adhelper = new ADhelper(this.Domain);
      adhelper.GetChildEntries(Conversions.ToString(tNode.Tag));
      IDictionaryEnumerator enumerator1 = adhelper.Children.GetEnumerator();
      this.tvAD.BeginUpdate();
      while (enumerator1.MoveNext())
      {
        bool flag1 = false;
        TreeNode node1 = new TreeNode(enumerator1.Key.ToString().Substring(3));
        node1.Tag = RuntimeHelpers.GetObjectValue(enumerator1.Value);
        if (Operators.CompareString(enumerator1.Key.ToString().Substring(0, 2), "CN", false) != 0)
          flag1 = true;
        if (Operators.CompareString(enumerator1.Key.ToString().Substring(0), "CN=Users", false) == 0)
          flag1 = true;
        if (flag1)
        {
          bool flag2 = false;
          try
          {
              foreach (TreeNode node2 in tNode.Nodes)
              {
                  if (Operators.CompareString(node2.Text, node1.Text, false) == 0)
                  {
                      flag2 = true;
                      break;
                  }
              }
          }
          catch (Exception ex)
          {
              Console.WriteLine(ex.StackTrace);
          }

          if (!flag2)
            tNode.Nodes.Add(node1);
        }
        int imageIndex = this.GetImageIndex(enumerator1.Key.ToString().Substring(0, 2));
        node1.ImageIndex = imageIndex;
        node1.SelectedImageIndex = imageIndex;
      }
      this.tvAD.EndUpdate();
    }

    public int GetImageIndex(string ObjType)
    {
      string Left = ObjType;
      if (Operators.CompareString(Left, "CN", false) == 0)
        return 2;
      return Operators.CompareString(Left, "OU", false) == 0 ? 1 : 3;
    }

    private void tvAD_AfterExpand(object sender, TreeViewEventArgs e)
    {
      try
      {
        foreach (TreeNode node in e.Node.Nodes)
          this.AddTreeNodes(node);
      }
      catch(Exception ex)
      {
        Console.WriteLine(ex.StackTrace);
      }
    }

    private void tvAD_AfterSelect(object sender, TreeViewEventArgs e)
    {
      this._ADPath = Conversions.ToString(e.Node.Tag);
      ADtree.ADPathChangedEventHandler pathChangedEvent = this.ADPathChangedEvt;
      if (pathChangedEvent == null)
        return;
      pathChangedEvent((object) this);
    }

    private void tvAD_MouseDown(object sender, MouseEventArgs e)
    {
      ADtree.MouseDownEventHandler mouseDownEvent = this.MouseDownEvt;
      if (mouseDownEvent == null)
        return;
      mouseDownEvent((object) this, e);
    }

    private void ADtree_Load(object sender, EventArgs e)
    {
      this.LoadAD();
    }

    public event ADtree.ADPathChangedEventHandler ADPathChangedEvt;

    public event ADtree.MouseDownEventHandler MouseDownEvt;

    public delegate void ADPathChangedEventHandler(object sender);

    public delegate void MouseDownEventHandler(object sender, MouseEventArgs e);
  }
}
