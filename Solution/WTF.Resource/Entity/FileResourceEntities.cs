namespace WTF.Resource.Entity
{
    using System;
    using System.Data.EntityClient;
    using System.Data.Objects;

    public class FileResourceEntities : ObjectContext
    {
        private ObjectSet<WTF.Resource.Entity.resource_filehistory> _resource_filehistory;
        private ObjectSet<WTF.Resource.Entity.resource_fileresource> _resource_fileresource;
        private ObjectSet<WTF.Resource.Entity.resource_filerestrict> _resource_filerestrict;
        private ObjectSet<WTF.Resource.Entity.resource_filerestrictpic> _resource_filerestrictpic;
        private ObjectSet<WTF.Resource.Entity.resource_filestoragepath> _resource_filestoragepath;

        public FileResourceEntities() : base("name=FileResourceEntities", "FileResourceEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }

        public FileResourceEntities(EntityConnection connection) : base(connection, "FileResourceEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }

        public FileResourceEntities(string connectionString) : base(connectionString, "FileResourceEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }

        public void AddToresource_filehistory(WTF.Resource.Entity.resource_filehistory resource_filehistory)
        {
            base.AddObject("resource_filehistory", resource_filehistory);
        }

        public void AddToresource_fileresource(WTF.Resource.Entity.resource_fileresource resource_fileresource)
        {
            base.AddObject("resource_fileresource", resource_fileresource);
        }

        public void AddToresource_filerestrict(WTF.Resource.Entity.resource_filerestrict resource_filerestrict)
        {
            base.AddObject("resource_filerestrict", resource_filerestrict);
        }

        public void AddToresource_filerestrictpic(WTF.Resource.Entity.resource_filerestrictpic resource_filerestrictpic)
        {
            base.AddObject("resource_filerestrictpic", resource_filerestrictpic);
        }

        public void AddToresource_filestoragepath(WTF.Resource.Entity.resource_filestoragepath resource_filestoragepath)
        {
            base.AddObject("resource_filestoragepath", resource_filestoragepath);
        }

        public ObjectSet<WTF.Resource.Entity.resource_filehistory> resource_filehistory
        {
            get
            {
                if (this._resource_filehistory == null)
                {
                    this._resource_filehistory = base.CreateObjectSet<WTF.Resource.Entity.resource_filehistory>("resource_filehistory");
                }
                return this._resource_filehistory;
            }
        }

        public ObjectSet<WTF.Resource.Entity.resource_fileresource> resource_fileresource
        {
            get
            {
                if (this._resource_fileresource == null)
                {
                    this._resource_fileresource = base.CreateObjectSet<WTF.Resource.Entity.resource_fileresource>("resource_fileresource");
                }
                return this._resource_fileresource;
            }
        }

        public ObjectSet<WTF.Resource.Entity.resource_filerestrict> resource_filerestrict
        {
            get
            {
                if (this._resource_filerestrict == null)
                {
                    this._resource_filerestrict = base.CreateObjectSet<WTF.Resource.Entity.resource_filerestrict>("resource_filerestrict");
                }
                return this._resource_filerestrict;
            }
        }

        public ObjectSet<WTF.Resource.Entity.resource_filerestrictpic> resource_filerestrictpic
        {
            get
            {
                if (this._resource_filerestrictpic == null)
                {
                    this._resource_filerestrictpic = base.CreateObjectSet<WTF.Resource.Entity.resource_filerestrictpic>("resource_filerestrictpic");
                }
                return this._resource_filerestrictpic;
            }
        }

        public ObjectSet<WTF.Resource.Entity.resource_filestoragepath> resource_filestoragepath
        {
            get
            {
                if (this._resource_filestoragepath == null)
                {
                    this._resource_filestoragepath = base.CreateObjectSet<WTF.Resource.Entity.resource_filestoragepath>("resource_filestoragepath");
                }
                return this._resource_filestoragepath;
            }
        }
    }
}

