﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyTestRTDataService.RunningMode
{
    /// <summary>
    /// 数值转换工具。
    /// 模拟量输入4-20ma  对应的数字量是5530-27648
    /// 模拟量输出0-10V 对应的数字量是0-27648
    /// </summary>
    public class DataTransformer
    {
        public Type datatype;
        
        //模拟量数字最小值
        private const int ANALOG_MIN = 0;
        //模拟量数字最大值
        private const int ANALOG_MAX = 27648;
        //物理输入最大值
        private const double INPUT_MAX = 20.0;
        //物理输入最小值
        private const double INPUT_MIN = 0.0;
        //物理输出最大值
        private const double OUTPUT_MAX = 10.0;
        //物理输出最小值
        private const double OUTPUT_MIN = 0.0;

        private const double TRUE = 1.0;
        private const double FALSE = 0.0;

        public DataTransformer() { }

        /// <summary>
        /// 将数据池double转为bool
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool DoubleToBool(double value)
        {
            if (value == TRUE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 将数据池double转为int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int DoubleToInt(double value)
        {
            return (int)value;
        }

        /// <summary>
        /// 将数据池double转为double
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double DoubleToDouble(double value)
        {
            return value;
        }

        /// <summary>
        /// 将模拟量转为物理输入量
        /// </summary>
        /// <param name="analog"></param>
        /// <returns></returns>
        public static double InputAnalogToPhysical(int analog)
        {
            double rate = (INPUT_MIN - INPUT_MAX) / (ANALOG_MIN - ANALOG_MAX);    //rate<1
            double result = rate * (analog - ANALOG_MIN) + INPUT_MIN;
            return result;
        }

        /// <summary>
        /// 将物理输入量转为模拟量
        /// </summary>
        /// <param name="physical"></param>
        /// <returns></returns>
        public static int InputPhysicalToAnalog(double physical)
        {
            double rate = (ANALOG_MAX - ANALOG_MAX) / (INPUT_MAX - INPUT_MIN);  //rate>1
            int result = (int)((physical - INPUT_MIN) * rate) + ANALOG_MIN;
            return result;
        }

        /// <summary>
        /// 将模拟量转为物理输出量
        /// </summary>
        /// <param name="analog"></param>
        /// <returns></returns>
        public static double OutputAnalogToPhysical(int analog)
        {
            double rate = (OUTPUT_MAX - OUTPUT_MIN) / (ANALOG_MAX - ANALOG_MIN);    //rate<1
            double result = rate * (analog - ANALOG_MIN) + INPUT_MIN;
            return result;
        }

        /// <summary>
        /// 将物理输出量转换为模拟量
        /// </summary>
        /// <param name="analog"></param>
        /// <returns></returns>
        public static int OutputPhysicalToAnalog(double physical)
        {
            double rate = (ANALOG_MAX - ANALOG_MIN) / (OUTPUT_MAX - OUTPUT_MIN);    //rate>1
            int result = (int)(rate * (physical - OUTPUT_MIN)) + ANALOG_MIN;
            return result;
        }

        public double TransDigitalToBoolDouble(byte digital) 
        {
            if (digital==1)
            {
                return TRUE;
            }
            else
            {
                return FALSE;
            }
        }

    }
}
