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
    /// 套餐价格对象
    /// </summary>
    [Serializable]
    public class LinePackagePrice
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public LinePackagePrice()
        {
            Price = new List<Price>();
        }

        /// <summary>
        /// 获取指定日期的价格
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        [BsonIgnore]
        public Price this[DateTime date]
        {
            get
            {
                return Price.FirstOrDefault(m => m.Date == date);
            }
        }

        /// <summary>
        /// Mongo唯一ID
        /// </summary>
        [BsonId]
        public ObjectId ObjectId { get; set; }

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

        /// <summary>
        /// 获取或设置价格的集合
        /// </summary>
        [BsonElement("p", Order = 3)]
        public List<Price> Price { get; private set; }
    }


    /// <summary>
    /// 价格对象
    /// </summary>
    [Serializable]
    public class Price : IEqualityComparer<Price>
    {
        /// <summary>
        /// 获取或设置价格的日期
        /// </summary>
        [BsonElement("d", Order = 1)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }

        /// <summary>
        /// 获取或设置结算价，单位（分）
        /// </summary>
        [BsonElement("ac", Order = 2)]
        public int AmountContract { get; set; }

        /// <summary>
        /// 获取或设置同程价（卖价），单位（分）
        /// </summary>
        [BsonElement("ad", Order = 3)]
        public int AmountDirect { get; set; }

        /// <summary>
        /// 获取或设置门市价，单位（分）
        /// </summary>
        [BsonElement("aa", Order = 4)]
        public int AdmountAdvice { get; set; }

        /// <summary>
        /// 获取价格对象的hash
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        #region IEqualityComparer<Price> 成员

        public bool Equals(Price x, Price y)
        {
            bool ret = false;
            if (x != null && y != null)
            {
                ret = x.Date.Year == y.Date.Year && x.Date.Month == y.Date.Month && x.Date.Day == y.Date.Day;
                ret = ret && x.AdmountAdvice == y.AdmountAdvice;
                ret = ret && x.AmountContract == y.AmountContract;
                ret = ret && x.AmountDirect == y.AmountDirect;
            }

            return ret;
        }

        public int GetHashCode(Price obj)
        {
            string temp = obj.Date.ToString("yyyyMMdd");
            int hash = Int32.Parse(temp);

            return hash ^ obj.AdmountAdvice ^ obj.AmountContract ^ obj.AmountDirect;
        }

        #endregion
    }

    /// <summary>
    /// 基于日期的价格比较器
    /// </summary>
    public class PriceDateComparer : IEqualityComparer<Price>
    {

        #region IEqualityComparer<Price> 成员

        public bool Equals(Price x, Price y)
        {
            bool ret = false;
            if (x != null && y != null)
            {
                ret = x.Date.Year == y.Date.Year && x.Date.Month == y.Date.Month && x.Date.Day == y.Date.Day;
            }

            return ret;
        }

        public int GetHashCode(Price obj)
        {
            string temp = obj.Date.ToString("yyyyMMdd");
            return Int32.Parse(temp);
        }

        #endregion
    }
}
