﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyTestIEInterface;
using HyTestIEEntity;
using System.Data;

namespace HyTestRTDataService.ConfigMode
{
    public class ConfigAdapter
    {
        IAdapterLoader loader;

        private IList<Adapter> adapterList;
        private IList<Adapter> ignoreList;  //忽略列表
        private DataTable adapterTable;

        private Adapter currentAdapter;
        private string currentName;
        private string currentDesc;
        public int adapterNum;
        
        public ConfigAdapter(bool autoLoad)
        {
            if (autoLoad)
            {
                //从xml初始化adapterList
            }
            else
            {
                adapterList = new List<Adapter>();
                getAdapterList();
            }
            
        }
        
        public IList<Adapter> getAdapterList()
        {
            if (this.adapterList != null)
            {
                //debug adapterList不为空
            }
            this.adapterList = loader.getAdapter().ToList();
            this.adapterNum = adapterList.Count;
            return this.adapterList;
        } 

        public DataTable getAdapterTable()
        {
            if (this.adapterTable == null)
            {
                getAdapterList();

                adapterTable = new DataTable();
                adapterTable.Columns.Add("ID", typeof(int));
                adapterTable.Columns.Add("NAME", typeof(string));
                adapterTable.Columns.Add("DESCRIPTION", typeof(string));
                adapterTable.Columns.Add("STATE", typeof(string));
                for (int i = 0; i < this.adapterNum; i++)
                {
                    DataRow row = adapterTable.NewRow();
                    row[0] = i + 1;
                    row[1] = adapterList[i].name;
                    row[2] = adapterList[i].desc;
                    row[3] = "OK";
                    adapterTable.Rows.Add(row);
                }
            }

            return this.adapterTable;
        }

        public void refreshAdapterList()
        {

        }

        public void selectAdapter(int adapterId)//id是角标+1
        {
            //选取网卡，处理错误

            ErrorCode errCode = loader.setAdapter(adapterId);
            currentAdapter = adapterList[adapterId - 1];
        }
    }
}
