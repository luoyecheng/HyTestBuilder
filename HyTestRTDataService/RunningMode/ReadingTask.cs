﻿using HyTestRTDataService.Entities;
using HyTestRTDataService.Interfaces;
using System;

namespace HyTestRTDataService.RunningMode
{
    /// <summary>
    /// 此类作用在于保存高频读取任务
    /// </summary>
    public class ReadingTask
    {
        private int taskId;
        private string varName;
        private Port port;
        private int frequency;
        private int[] data;
        private TaskState state = TaskState.Undefine;

        private IReader reader;

        private HighCallback callback;
        
        public ReadingTask(int taskId, 
                           string varName, 
                           int frequency, 
                           IReader reader, 
                           HighCallback callback, 
                           Port port)
        {
            this.taskId = taskId;
            this.varName = varName;
            this.frequency = frequency;
            this.reader = reader;
            this.callback = callback;
            this.port = port;
            state = TaskState.Free;
        }

        private ReadingTask() { }

        public void StartSampling()
        {
            if (state == TaskState.Running || state == TaskState.Finished)
            {
                throw new TaskRunningStateError();
            }

            reader.HighFreqRead(this.port.deviceId, this.port.channelId,callback);
            state = TaskState.Running;
        }

        public void StopSampling()
        {
            if(state != TaskState.Running)
            {
                throw new TaskRunningStateError();
            }

            reader.HighFreqReadStop(port.deviceId, port.channelId);
            state = TaskState.Finished;
        }

    }

    public enum TaskState
    {
        Undefine,   //未定义的任务
        Free,       //空闲状态
        Running,    //运行状态
        Finished,   //已结束
    }

    public class TaskRunningStateError : Exception
    {

    }
}
