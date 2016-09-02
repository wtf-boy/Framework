using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// PlanConfigInfo 的摘要说明
/// </summary>
public class PlanConfigInfo
{
    public PlanConfigInfo()
    {
        StartDate = DateTime.MinValue;
        EndDate = DateTime.MaxValue;
        LastRunDate = DateTime.MinValue;
        Remark = "";
    }

    public DateTime StartDate
    {
        get;
        set;
    }
    public DateTime EndDate
    {
        get;
        set;
    }
    public DateTime LastRunDate
    {
        get;
        set;
    }

    public string Remark
    {
        get;
        set;
    }
}