using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tc.Gl.Framework.MongoHelper
{
    /// <summary>
    /// Mongo数据库名称管理
    /// </summary>
    public class MongoDbManager
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public struct DbName
        {
            /// <summary>
            /// 营销库名称
            /// </summary>
            public const string Db_TCZZYMarketing = "tczzymarketing";

            /// <summary>
            /// 价格库名称
            /// </summary>
            public const string Db_TCZZYPrice = "tczzyprice";
        }

        /// <summary>
        /// Mongo数据库数据集名称
        /// </summary>
        public struct MongoCollectionName
        {
            /// <summary>
            /// 线路套餐价格数据集名称
            /// </summary>
            public const string Coll_LinePackagePrice = "ZZYLinePackagePrice";

            /// <summary>
            /// 资源产品价格数据集名称
            /// </summary>
            public const string Coll_ResourceProductPrice = "ZZYResourceProductPrice";

            /// <summary>
            /// 资源和套餐的关系数据集名称
            /// </summary>
            public const string Coll_ResourcePackageRelationship = "ZZYResourcePackageRelationship";

            /// <summary>
            /// 线路平台最小价数据集名称
            /// </summary>
            public const string Coll_LineTopPrice = "ZZYLineTopPrice";

            /// <summary>
            /// 模板实例数据集
            /// </summary>
            public const string Coll_ActivityInstance = "ZZYActivityInstance";

            /// <summary>
            /// 活动套餐数据集
            /// </summary>
            public const string Coll_ActivityPackage = "ZZYActivityPackage";

            /// <summary>
            /// 10元度周末场次信息数据集
            /// </summary>
            public const string Coll_TenYuanScreenInfo = "ZZYTenYuanScreenInfo";

            /// <summary>
            /// 10元度周末活动线路信息数据集
            /// </summary>
            public const string Coll_TenYuanActivityLineInfo = "ZZYTenYuanActivityLineInfo";

            /// <summary>
            /// 活动线路描述信息数据集
            /// </summary>
            public const string Coll_ActivityLineDescription = "ZZYActivityLineDescription";

            /// <summary>
            /// 活动线路最大最小价数据集
            /// </summary>
            public const string Coll_ActivityLineTopPrice = "ZZYActivityLineTopPrice";

            /// <summary>
            /// 活动套餐最大最小价数据集
            /// </summary>
            public const string Coll_ActivityPackageTopPrice = "ZZYActivityPackageTopPrice";

            /// <summary>
            /// 活动套餐价格库存数据集
            /// </summary>
            public const string Coll_ActivityPackagePriceInventory = "ZZYActivityPackagePriceInventory";

            /// <summary>
            /// 预付酒店活动模板实例数据集
            /// </summary>
            public const string Coll_HotelActivityResourceProductPolicy = "ZZYHotelActivityResourceProductPolicy";
        }
    }
}
