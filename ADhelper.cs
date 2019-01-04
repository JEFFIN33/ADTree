// Decompiled with JetBrains decompiler
// Type: ADTree.ADhelper
// Assembly: ADTree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AD3D2372-2CAC-4221-B25D-8335DA876BAA
// Assembly location: \Source\mRemoteNG\mRemoteV1\References\ADTree.dll

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.DirectoryServices;
using System.Runtime.InteropServices;

namespace ADTree
{
  public class ADhelper
  {
      private DirectoryEntry dEntry;

    public Hashtable Children { get; set; }

    public string Domain { get; set; }

    public ADhelper(string domain)
    {
      Children = new Hashtable();
      Domain = domain;
    }

    public void GetChildEntries(string adPath = "")
    {
      dEntry = adPath.Length <= 0 ? Domain.Length <= 0 ? new DirectoryEntry() : new DirectoryEntry("LDAP://" + Domain) : new DirectoryEntry(adPath);
      try
      {
        try
        {
          foreach (DirectoryEntry child in dEntry.Children)
            Children.Add(child.Name, child.Path);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace);
        }
      }
      catch (COMException ex)
      {
        ProjectData.SetProjectError(ex);
        var comException = ex;
        if (Operators.CompareString(comException.Message.ToLower(), "the server is not operational", false) == 0)
          throw new Exception("Could not find AD Server", comException);
        ProjectData.ClearProjectError();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        throw;
      }
    }
  }
}
