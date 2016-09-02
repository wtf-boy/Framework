using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WTF.Framework
{
public class HashNodeHelper<T>
{
    // Fields
    private SortedList<long, T> ketamaNodes;

    // Methods
    public HashNodeHelper(IEnumerable<T> nodes, int nodeCopies)
    {
        this.ketamaNodes = new SortedList<long, T>();
        this.ketamaNodes = new SortedList<long, T>();
        foreach (T local in nodes)
        {
            for (int i = 0; i < (nodeCopies / 4); i++)
            {
                byte[] digest = this.ComputeMd5(local.ToString() + "_" + i);
                for (int j = 0; j < 4; j++)
                {
                    long num3 = this.HashValue(digest, j);
                    this.ketamaNodes[num3] = local;
                }
            }
        }
    }

    private byte[] ComputeMd5(string k)
    {
        MD5 md = new MD5CryptoServiceProvider();
        byte[] buffer = md.ComputeHash(Encoding.UTF8.GetBytes(k));
        md.Clear();
        return buffer;
    }

    private T GetNodeForKey(long hash)
    {
        Func<KeyValuePair<long, T>, bool> predicate = null;
        long key = hash;
        if (!this.ketamaNodes.ContainsKey(key))
        {
            if (predicate == null)
            {
                predicate = coll => coll.Key > hash;
            }
            var source = from coll in this.ketamaNodes.Where<KeyValuePair<long, T>>(predicate) select new { Key = coll.Key };
            if ((source == null) || (source.Count() == 0))
            {
                key = this.ketamaNodes.FirstOrDefault<KeyValuePair<long, T>>().Key;
            }
            else
            {
                key = source.FirstOrDefault().Key;
            }
        }
        return this.ketamaNodes[key];
    }

    public T GetNodeValue(string key)
    {
        byte[] digest = this.ComputeMd5(key);
        return this.GetNodeForKey(this.HashValue(digest, 0));
    }

    private long HashValue(byte[] digest, int nTime)
    {
        long num = ((((digest[3 + (nTime * 4)] & 0xff) << 0x18) | ((digest[2 + (nTime * 4)] & 0xff) << 0x10)) | ((digest[1 + (nTime * 4)] & 0xff) << 8)) | (digest[nTime * 4] & 0xffL);
        return (num & ((long) 0xffffffffL));
    }
}

}
