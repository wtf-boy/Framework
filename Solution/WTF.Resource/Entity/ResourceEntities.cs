namespace WTF.Resource.Entity
{
    using System;
    using System.Data.EntityClient;
    using System.Data.Objects;

    public class ResourceEntities : ObjectContext
    {
        private ObjectSet<Sys_Resource> _sys_resource;
        private ObjectSet<Sys_ResourceData> _sys_resourcedata;
        private ObjectSet<Sys_ResourceFileInfo> _sys_resourcefileinfo;
        private ObjectSet<Sys_ResourcePath> _sys_resourcepath;
        private ObjectSet<Sys_ResourceRestrict> _sys_resourcerestrict;
        private ObjectSet<Sys_ResourceRestrictPic> _sys_resourcerestrictpic;
        private ObjectSet<Sys_ResourceType> _sys_resourcetype;
        private ObjectSet<Sys_ResourceVer> _sys_resourcever;
        private ObjectSet<Sys_WaterImage> _sys_waterimage;

        public ResourceEntities() : base("name=ResourceEntities", "ResourceEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }

        public ResourceEntities(EntityConnection connection) : base(connection, "ResourceEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }

        public ResourceEntities(string connectionString) : base(connectionString, "ResourceEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }

        public void AddTosys_resource(Sys_Resource sys_Resource)
        {
            base.AddObject("sys_resource", sys_Resource);
        }

        public void AddTosys_resourcedata(Sys_ResourceData sys_ResourceData)
        {
            base.AddObject("sys_resourcedata", sys_ResourceData);
        }

        public void AddTosys_resourcefileinfo(Sys_ResourceFileInfo sys_ResourceFileInfo)
        {
            base.AddObject("sys_resourcefileinfo", sys_ResourceFileInfo);
        }

        public void AddTosys_resourcepath(Sys_ResourcePath sys_ResourcePath)
        {
            base.AddObject("sys_resourcepath", sys_ResourcePath);
        }

        public void AddTosys_resourcerestrict(Sys_ResourceRestrict sys_ResourceRestrict)
        {
            base.AddObject("sys_resourcerestrict", sys_ResourceRestrict);
        }

        public void AddTosys_resourcerestrictpic(Sys_ResourceRestrictPic sys_ResourceRestrictPic)
        {
            base.AddObject("sys_resourcerestrictpic", sys_ResourceRestrictPic);
        }

        public void AddTosys_resourcetype(Sys_ResourceType sys_ResourceType)
        {
            base.AddObject("sys_resourcetype", sys_ResourceType);
        }

        public void AddTosys_resourcever(Sys_ResourceVer sys_ResourceVer)
        {
            base.AddObject("sys_resourcever", sys_ResourceVer);
        }

        public void AddTosys_waterimage(Sys_WaterImage sys_WaterImage)
        {
            base.AddObject("sys_waterimage", sys_WaterImage);
        }

        public ObjectResult<Sys_ResourceFileInfo> GetResourceFilePathByResourceTypeID(int? p_ResourceTypeID)
        {
            ObjectParameter parameter;
            if (p_ResourceTypeID.HasValue)
            {
                parameter = new ObjectParameter("p_ResourceTypeID", p_ResourceTypeID);
            }
            else
            {
                parameter = new ObjectParameter("p_ResourceTypeID", typeof(int));
            }
            return base.ExecuteFunction<Sys_ResourceFileInfo>("GetResourceFilePathByResourceTypeID", new ObjectParameter[] { parameter });
        }

        public ObjectResult<Sys_ResourceFileInfo> GetResourceFilePathByResourceTypeID(int? p_ResourceTypeID, MergeOption mergeOption)
        {
            ObjectParameter parameter;
            if (p_ResourceTypeID.HasValue)
            {
                parameter = new ObjectParameter("p_ResourceTypeID", p_ResourceTypeID);
            }
            else
            {
                parameter = new ObjectParameter("p_ResourceTypeID", typeof(int));
            }
            return base.ExecuteFunction<Sys_ResourceFileInfo>("GetResourceFilePathByResourceTypeID", mergeOption, new ObjectParameter[] { parameter });
        }

        public ObjectResult<Sys_ResourceVer> GetResourceVerByResourceTypeID(int? p_ResourceTypeID)
        {
            ObjectParameter parameter;
            if (p_ResourceTypeID.HasValue)
            {
                parameter = new ObjectParameter("p_ResourceTypeID", p_ResourceTypeID);
            }
            else
            {
                parameter = new ObjectParameter("p_ResourceTypeID", typeof(int));
            }
            return base.ExecuteFunction<Sys_ResourceVer>("GetResourceVerByResourceTypeID", new ObjectParameter[] { parameter });
        }

        public ObjectResult<Sys_ResourceVer> GetResourceVerByResourceTypeID(int? p_ResourceTypeID, MergeOption mergeOption)
        {
            ObjectParameter parameter;
            if (p_ResourceTypeID.HasValue)
            {
                parameter = new ObjectParameter("p_ResourceTypeID", p_ResourceTypeID);
            }
            else
            {
                parameter = new ObjectParameter("p_ResourceTypeID", typeof(int));
            }
            return base.ExecuteFunction<Sys_ResourceVer>("GetResourceVerByResourceTypeID", mergeOption, new ObjectParameter[] { parameter });
        }

        public ObjectSet<Sys_Resource> sys_resource
        {
            get
            {
                if (this._sys_resource == null)
                {
                    this._sys_resource = base.CreateObjectSet<Sys_Resource>("sys_resource");
                }
                return this._sys_resource;
            }
        }

        public ObjectSet<Sys_ResourceData> sys_resourcedata
        {
            get
            {
                if (this._sys_resourcedata == null)
                {
                    this._sys_resourcedata = base.CreateObjectSet<Sys_ResourceData>("sys_resourcedata");
                }
                return this._sys_resourcedata;
            }
        }

        public ObjectSet<Sys_ResourceFileInfo> sys_resourcefileinfo
        {
            get
            {
                if (this._sys_resourcefileinfo == null)
                {
                    this._sys_resourcefileinfo = base.CreateObjectSet<Sys_ResourceFileInfo>("sys_resourcefileinfo");
                }
                return this._sys_resourcefileinfo;
            }
        }

        public ObjectSet<Sys_ResourcePath> sys_resourcepath
        {
            get
            {
                if (this._sys_resourcepath == null)
                {
                    this._sys_resourcepath = base.CreateObjectSet<Sys_ResourcePath>("sys_resourcepath");
                }
                return this._sys_resourcepath;
            }
        }

        public ObjectSet<Sys_ResourceRestrict> sys_resourcerestrict
        {
            get
            {
                if (this._sys_resourcerestrict == null)
                {
                    this._sys_resourcerestrict = base.CreateObjectSet<Sys_ResourceRestrict>("sys_resourcerestrict");
                }
                return this._sys_resourcerestrict;
            }
        }

        public ObjectSet<Sys_ResourceRestrictPic> sys_resourcerestrictpic
        {
            get
            {
                if (this._sys_resourcerestrictpic == null)
                {
                    this._sys_resourcerestrictpic = base.CreateObjectSet<Sys_ResourceRestrictPic>("sys_resourcerestrictpic");
                }
                return this._sys_resourcerestrictpic;
            }
        }

        public ObjectSet<Sys_ResourceType> sys_resourcetype
        {
            get
            {
                if (this._sys_resourcetype == null)
                {
                    this._sys_resourcetype = base.CreateObjectSet<Sys_ResourceType>("sys_resourcetype");
                }
                return this._sys_resourcetype;
            }
        }

        public ObjectSet<Sys_ResourceVer> sys_resourcever
        {
            get
            {
                if (this._sys_resourcever == null)
                {
                    this._sys_resourcever = base.CreateObjectSet<Sys_ResourceVer>("sys_resourcever");
                }
                return this._sys_resourcever;
            }
        }

        public ObjectSet<Sys_WaterImage> sys_waterimage
        {
            get
            {
                if (this._sys_waterimage == null)
                {
                    this._sys_waterimage = base.CreateObjectSet<Sys_WaterImage>("sys_waterimage");
                }
                return this._sys_waterimage;
            }
        }
    }
}

