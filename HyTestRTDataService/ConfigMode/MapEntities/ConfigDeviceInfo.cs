﻿using HyTestIEEntity;
using System;
using System.Data;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    [Serializable]
    public class ConfigDeviceInfo
    {
        public DataTable deviceTable;
        public int deviceNum;
        public IOdevice[] deviceArr;
    }
}