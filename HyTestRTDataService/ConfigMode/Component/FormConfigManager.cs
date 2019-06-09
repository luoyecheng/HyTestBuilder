﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyTestRTDataService.ConfigMode.Component
{
    public partial class FormConfigManager : Form
    {
        private string xmlPath = "";    //文件存放路径

        private ConfigManager confman;
        private Config config;

        private ConfigAdapter configAdapter;
        private ConfigDevice configDevice;
        private ConfigIOmap configIOmap;

        public FormConfigManager()
        {
            InitializeComponent();
            
            ReadXmlConfigInfoIfExist();

            this.configAdapter = new ConfigAdapter();
            this.configDevice = new ConfigDevice();
        }
        //从xml文件读入config信息，写到config里面去，显示出来
        private void ReadXmlConfigInfoIfExist()
        {
            if (IsExist(""))
            {
                //将xml信息读入config
                //显示config
            }
        }

        

        public FormConfigManager(ConfigManager confman) : this()
        {
            this.confman = confman;
            this.config = Config.getConfig();
        }

        #region 方法

        private void saveAllConfig()
        {

        }

        private void saveDeviceConfig()
        {

        }

        private void saveIOmapConfig()
        {

        }
        //判断配置文件是否存在
        private bool IsExist(string filePath)
        {
            return false;
        }

        #endregion

        #region 事件

        private void btn_ScanAdapter_Click(object sender, EventArgs e)
        {
            DataTable adapterTable = configAdapter.getAdapterTable();
            this.dataGridView1.DataSource = adapterTable;
        }

        private void btn_SelectAdapter_Click(object sender, EventArgs e)
        {
            int selectedAdapter = this.dataGridView1.SelectedRows[0].Index;
            try
            {
                configAdapter.selectAdapter(selectedAdapter);
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void btn_ScanDevices_Click(object sender, EventArgs e)
        {
            TreeNode rootDeviceTree = configDevice.getDeviceTree();
            treeView1.Nodes.Add(rootDeviceTree);
        }

        private void btn_SaveDeviceConfig_Click(object sender, EventArgs e)
        {
            saveDeviceConfig();
        }

        private void btn_SaveConfig_Click(object sender, EventArgs e)
        {
            saveAllConfig();
        }

        private void btn_ReloadConfig_Click(object sender, EventArgs e)
        {

        }

        //导入变量表
        private void btn_ImportExcel_Click(object sender, EventArgs e)
        {

        }
        //导出变量表
        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {

        }
        //保存映射文件的更改
        private void btn_SaveIOmapChange_Click(object sender, EventArgs e)
        {

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
