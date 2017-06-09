using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Tc.Gl.Framework.MongoHelper
{
    /// <summary>
    /// Mongo查询条件
    /// </summary>
    public class Condition
    {
        private IMongoQuery _query = null;

        private Condition()
        {
        }

        /// <summary>
        /// 获取查询条件对象
        /// </summary>
        public IMongoQuery Where
        {
            get
            {
                return _query;
            }
            private set
            {
                _query = value;
            }
        }

        /// <summary>
        /// 创建Mongo查询条件
        /// </summary>
        /// <typeparam name="TDoc">待查询的目标对象类型</typeparam>
        /// <typeparam name="TMember">用于相等比较的字段类型</typeparam>
        /// <param name="lambda">用于指定比较字段的lambda表达式</param>
        /// <param name="val">用于比较的值</param>
        /// <returns></returns>
        public static Condition Create<TDoc, TMember>(Expression<Func<TDoc, TMember>> lambda, TMember val)
        {
            var query = Query<TDoc>.EQ(lambda, val);
            Condition ret = new Condition();
            ret.Where = query;
            return ret;
        }

        /// <summary>
        /// 创建Mongo查询条件
        /// </summary>
        /// <typeparam name="TDoc">待查询的目标对象类型</typeparam>
        /// <param name="lambda">查询条件lambda表达式</param>
        /// <returns></returns>
        public static Condition Create<TDoc>(Expression<Func<TDoc, bool>> lambda)
        {
            var query = Query<TDoc>.Where(lambda);
            Condition ret = new Condition();
            ret.Where = query;
            return ret;
        }
    }
}
