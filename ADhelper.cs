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
    private Hashtable _Children;
    private string _Domain;
    private DirectoryEntry dEntry;

    public Hashtable Children
    {
      get
      {
        return this._Children;
      }
      set
      {
        this._Children = value;
      }
    }

    public string Domain
    {
      get
      {
        return this._Domain;
      }
      set
      {
        this._Domain = value;
      }
    }

    public ADhelper(string domain)
    {
      this._Children = new Hashtable();
      this._Domain = domain;
    }

    public void GetChildEntries()
    {
      this.GetChildEntries("");
    }

    public void GetChildEntries(string adPath)
    {
      this.dEntry = adPath.Length <= 0 ? (this._Domain.Length <= 0 ? new DirectoryEntry() : new DirectoryEntry("LDAP://" + this._Domain)) : new DirectoryEntry(adPath);
      try
      {
        try
        {
          foreach (DirectoryEntry child in this.dEntry.Children)
            this._Children.Add((object) child.Name, (object) child.Path);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace);
        }
      }
      catch (COMException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        COMException comException = ex;
        if (Operators.CompareString(comException.Message.ToLower(), "the server is not operational", false) == 0)
          throw new Exception("Could not find AD Server", (Exception) comException);
        ProjectData.ClearProjectError();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        throw ex;
      }
    }
  }
}
