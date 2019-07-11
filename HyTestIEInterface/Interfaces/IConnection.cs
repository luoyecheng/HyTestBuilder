﻿using System;

namespace HyTestIEInterface
{
    public interface IConnection
    {
        event EventHandler datachanged;
        //ConnectionContext context { get; set; }

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns>成功返回正值</returns>
        int connect();
        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns>关闭返回正值</returns>
        int close();
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        int disconnect();
    }
}