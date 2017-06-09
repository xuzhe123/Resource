using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoModel
{
    /// <summary>
    /// 活动线路最大最小价格
    /// </summary>
    public class ActivityLineTopPrice
    {
        /// <summary>
        /// MongoDB唯一ID
        /// </summary>
        [BsonId]
        public ObjectId ObjectId { get; set; }

        /// <summary>
        /// 获取或设置模板ID
        /// </summary>
        [BsonElement("t")]
        public int TemplateId { get; set; }

        /// <summary>
        /// 获取或设置活动期数
        /// </summary>
        [BsonElement("p")]
        public int Periods { get; set; }

        /// <summary>
        /// 获取或设置线路ID
        /// </summary>
        [BsonElement("l")]
        public int LineId { get; set; }

        /// <summary>
        /// 获取或设置活动最小价，单位（元）
        /// </summary>
        [BsonElement("min")]
        public int MinPrice { get; set; }

        /// <summary>
        /// 获取或设置活动最大价，单位（元）
        /// </summary>
        [BsonElement("max")]
        public int MaxPrice { get; set; }
    }
}
