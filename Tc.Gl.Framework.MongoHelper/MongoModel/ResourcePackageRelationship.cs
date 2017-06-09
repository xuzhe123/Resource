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
    /// 资源和套餐的关系数据对象
    /// </summary>
    [Serializable]
    public class ResourcePackageRelationship
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ResourcePackageRelationship()
        {
            Relationship = new List<LineWithPackageId>();
        }

        /// <summary>
        /// Mongo唯一ID
        /// </summary>
        [BsonId]
        public ObjectId ObjectId { get; set; }

        /// <summary>
        /// 获取或设置资源ID
        /// </summary>
        [BsonElement("rid", Order = 1)]
        public long ResourceId { get; set; }

        /// <summary>
        /// 获取或设置资源产品ID
        /// </summary>
        [BsonElement("rp", Order = 2)]
        public long ResourceProductId { get; set; }

        /// <summary>
        /// 获取或设置供应商ID
        /// </summary>
        [BsonElement("sb", Order = 3)]
        public int SupplierBasicId { get; set; }

        /// <summary>
        /// 获取或设置供应商价格策略ID
        /// </summary>
        [BsonElement("spp", Order = 4)]
        public int SupplierPricePolicyId { get; set; }

        /// <summary>
        /// 获取或设置线路及套餐的ID对集合
        /// </summary>
        [BsonElement("rs", Order = 5)]
        public List<LineWithPackageId> Relationship { get; set; }

        /// <summary>
        /// 获取或设置销售策略Id([TCZiZhuYou].dbo.ZZY_ResourceProductSupplierPriceExtensions.Id)
        /// </summary>
        [BsonElement("pui", Order = 6)]
        public long ProductUniqueId { get; set; }
    }

    /// <summary>
    /// 线路和套餐的ID对
    /// </summary>
    [Serializable]
    public class LineWithPackageId
    {
        /// <summary>
        /// 获取或设置线路ID
        /// </summary>
        [BsonElement("l", Order = 1)]
        public int LineId { get; set; }

        /// <summary>
        /// 获取或设置套餐ID
        /// </summary>
        [BsonElement("lp", Order = 2)]
        public int LinePackageId { get; set; }
    }
}
