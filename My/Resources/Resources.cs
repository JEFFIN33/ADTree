// Decompiled with JetBrains decompiler
// Type: ADTree.My.Resources.Resources
// Assembly: ADTree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AD3D2372-2CAC-4221-B25D-8335DA876BAA
// Assembly location: \Source\mRemoteNG\mRemoteV1\References\ADTree.dll

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ADTree.My.Resources
{
  [DebuggerNonUserCode]
  [CompilerGenerated]
  [StandardModule]
  [HideModuleName]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
  internal sealed class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) ADTree.My.Resources.Resources.resourceMan, (object) null))
          ADTree.My.Resources.Resources.resourceMan = new ResourceManager("ADTree.Resources", typeof (ADTree.My.Resources.Resources).Assembly);
        return ADTree.My.Resources.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return ADTree.My.Resources.Resources.resourceCulture;
      }
      set
      {
        ADTree.My.Resources.Resources.resourceCulture = value;
      }
    }

    internal static Bitmap Folder
    {
      get
      {
        return (Bitmap) RuntimeHelpers.GetObjectValue(ADTree.My.Resources.Resources.ResourceManager.GetObject(nameof (Folder), ADTree.My.Resources.Resources.resourceCulture));
      }
    }

    internal static Bitmap OU
    {
      get
      {
        return (Bitmap) RuntimeHelpers.GetObjectValue(ADTree.My.Resources.Resources.ResourceManager.GetObject(nameof (OU), ADTree.My.Resources.Resources.resourceCulture));
      }
    }

    internal static Bitmap Question
    {
      get
      {
        return (Bitmap) RuntimeHelpers.GetObjectValue(ADTree.My.Resources.Resources.ResourceManager.GetObject(nameof (Question), ADTree.My.Resources.Resources.resourceCulture));
      }
    }

    internal static Bitmap Root
    {
      get
      {
        return (Bitmap) RuntimeHelpers.GetObjectValue(ADTree.My.Resources.Resources.ResourceManager.GetObject(nameof (Root), ADTree.My.Resources.Resources.resourceCulture));
      }
    }
  }
}
