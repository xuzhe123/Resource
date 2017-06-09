using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Builders;

namespace Tc.Gl.Framework.MongoHelper
{
    /// <summary>
    /// 待更新的元素
    /// </summary>
    /// <typeparam name="TDoc">目标对象的类型</typeparam>
    public class ModifyElement<TDoc>
    {
        private UpdateBuilder<TDoc> _update = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public ModifyElement()
        {
        }

        /// <summary>
        /// 获取待更新的字段
        /// </summary>
        public UpdateBuilder<TDoc> UpdateFields
        {
            get
            {
                return _update;
            }
            private set
            {
                _update = value;
            }
        }

        /// <summary>
        /// 向元素集中增加待更新的字段及其字段值
        /// </summary>
        /// <typeparam name="TMember">待更新字段的类型</typeparam>
        /// <param name="lambda">用于指定待更新字段的lambda表达式</param>
        /// <param name="val">待更新的值</param>
        public void Push<TMember>(Expression<Func<TDoc, TMember>> lambda, TMember val)
        {
            if (_update == null)
            {
                _update = Update<TDoc>.Set(lambda, val);
            }
            else
            {
                _update.Combine(Update<TDoc>.Set(lambda, val));
            }
        }
    }
}
