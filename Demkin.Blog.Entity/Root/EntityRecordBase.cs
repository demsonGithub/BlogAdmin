using Demkin.Blog.Enum;
using SqlSugar;
using System;

namespace Demkin.Blog.Entity.Root
{
    public class EntityRecordBase
    {
        public EntityRecordBase()
        {
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public long Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public DateTime CreateTime { get; protected set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public long Modifier { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? ModifyTime { get; set; } = DateTime.Now;

        /// <summary>
        ///获取或设置状态
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public Status Status { get; set; } = Status.Enable;
    }
}