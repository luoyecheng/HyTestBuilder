﻿using HyTestIEEntity;
using System;
using System.Data;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    [Serializable]
    public class ConfigAdapterInfo
    {
        public DataTable adapterTable;
        public Adapter currentAdapter;
        public int adapterNum;
    }
}