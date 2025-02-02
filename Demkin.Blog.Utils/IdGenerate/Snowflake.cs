﻿using Demkin.Blog.Utils.Help;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demkin.Blog.Utils.IdGenerate
{
    public class Snowflake
    {
        private const long _twEpoch = 1546272000000L;//2019-01-01 00:00:00

        private const int _workerIdBits = 5;
        private const int _datacenterIdBits = 5;
        private const int _sequenceBits = 12;
        private const long _maxWorkerId = -1L ^ (-1L << _workerIdBits);
        private const long _maxDatacenterId = -1L ^ (-1L << _datacenterIdBits);

        private const int _workerIdShift = _sequenceBits;
        private const int _datacenterIdShift = _sequenceBits + _workerIdBits;
        private const int _timestampLeftShift = _sequenceBits + _workerIdBits + _datacenterIdBits;
        private const long _sequenceMask = -1L ^ (-1L << _sequenceBits);

        private long _sequence = 0L;
        private long _lastTimestamp = -1L;

        /// <summary>
        ///10位的数据机器位中的高位
        /// </summary>
        public long WorkerId { get; protected set; }

        /// <summary>
        /// 10位的数据机器位中的低位
        /// </summary>
        public long DatacenterId { get; protected set; }

        private readonly object _lock = new object();

        /// <summary>
        /// 基于Twitter的snowflake算法
        /// </summary>
        /// <param name="workerId">10位的数据机器位中的高位，默认不应该超过5位(5byte)</param>
        /// <param name="datacenterId">10位的数据机器位中的低位，默认不应该超过5位(5byte)</param>
        /// <param name="sequence">初始序列</param>
        public Snowflake(long workerId, long datacenterId, long sequence = 0L)
        {
            WorkerId = workerId;
            DatacenterId = datacenterId;
            _sequence = sequence;

            if (workerId > _maxWorkerId || workerId < 0)
            {
                throw new ArgumentException($"worker Id can't be greater than {_maxWorkerId} or less than 0");
            }

            if (datacenterId > _maxDatacenterId || datacenterId < 0)
            {
                throw new ArgumentException($"datacenter Id can't be greater than {_maxDatacenterId} or less than 0");
            }
        }

        public long CurrentId { get; private set; }

        /// <summary>
        /// 获取下一个Id，该方法线程安全
        /// </summary>
        /// <returns></returns>
        public long NextId()
        {
            lock (_lock)
            {
                var timestamp = DateTimeHelper.GetUnixTimeStamp(DateTime.Now);
                if (timestamp < _lastTimestamp)
                {
                    //TODO 是否可以考虑直接等待？
                    throw new Exception(
                        $"Clock moved backwards or wrapped around. Refusing to generate id for {_lastTimestamp - timestamp} ticks");
                }

                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & _sequenceMask;
                    if (_sequence == 0)
                    {
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    _sequence = 0;
                }
                _lastTimestamp = timestamp;
                CurrentId = ((timestamp - _twEpoch) << _timestampLeftShift) |
                         (DatacenterId << _datacenterIdShift) |
                         (WorkerId << _workerIdShift) | _sequence;

                return CurrentId;
            }
        }

        private long TilNextMillis(long lastTimestamp)
        {
            var timestamp = DateTimeHelper.GetUnixTimeStamp(DateTime.Now);
            while (timestamp <= lastTimestamp)
            {
                timestamp = DateTimeHelper.GetUnixTimeStamp(DateTime.Now);
            }
            return timestamp;
        }
    }
}