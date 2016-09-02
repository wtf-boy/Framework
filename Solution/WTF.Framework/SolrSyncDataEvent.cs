namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;

    public abstract class SolrSyncDataEvent
    {
        protected SolrSyncDataEvent()
        {
        }

        public abstract void OnDelete<T>(List<T> deleteIDList);
        public abstract void OnUpdate<T>(List<T> updateIDList);
    }
}

