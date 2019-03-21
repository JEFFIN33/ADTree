// Decompiled with JetBrains decompiler
// Type: ADTree.ADhelper
// Assembly: ADTree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AD3D2372-2CAC-4221-B25D-8335DA876BAA
// Assembly location: \Source\mRemoteNG\mRemoteV1\References\ADTree.dll

using System;
using System.Collections;
using System.DirectoryServices;
using System.Runtime.InteropServices;

namespace ADTree
{
    public class ADhelper
    {
        private DirectoryEntry _dEntry;

        public ADhelper(string domain)
        {
            Children = new Hashtable();
            Domain = domain;
        }

        public Hashtable Children { get; }

        private string Domain { get; }

        public void GetChildEntries(string adPath = "")
        {
            _dEntry = adPath.Length <= 0
                ? Domain.Length <= 0 ? new DirectoryEntry() : new DirectoryEntry("LDAP://" + Domain)
                : new DirectoryEntry(adPath);
            try
            {
                foreach (DirectoryEntry child in _dEntry.Children)
                    Children.Add(child.Name, child.Path);
            }
            catch (COMException ex)
            {
                if (ex.Message.ToLower().Equals("the server is not operational"))
                    throw new Exception("Could not find AD Server", ex);
            }
        }
    }
}