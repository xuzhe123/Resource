using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Tc.Gl.Framework.MongoHelper
{
    /// <summary>
    /// MongoDB操作帮助对象
    /// </summary>
    public class MongoDBHelper
    {
        private static readonly Mongo mongo = Mongo.GetInstance();

        #region 增

        /// <summary>
        /// 向Mongo中添加单条数据
        /// </summary>
        /// <typeparam name="T">数据对象的类型</typeparam>
        /// <param name="obj">待添加的数据</param>
        /// <param name="collName">目标数据集名称</param>
        /// <param name="dbName">数据库名称</param>
        /// <returns>若添加成功则返回true，否则false</returns>
        public static bool Add<T>(T obj, string collName, string dbName)
        {
            var coll = mongo.GetCollection<T>(collName, dbName);
            var result = coll.Insert<T>(obj);
            return result.Ok;
        }

        /// <summary>
        /// 向Mongo中批量添加数据
        /// </summary>
        /// <typeparam name="T">数据对象的类型</typeparam>
        /// <param name="objs">待添加的数据</param>
        /// <param name="collName">目标数据集名称</param>
        /// <param name="dbName">数据库名称</param>
        /// <returns>返回实际添加的数量</returns>
        public static int Add<T>(IEnumerable<T> objs, string collName, string dbName)
        {
            var coll = mongo.GetCollection<T>(collName, dbName);
            var result = coll.InsertBatch<T>(objs);
            return result.Count();
        }

        #endregion

        #region 删

        /// <summary>
        /// 物理删除符合查询条件的记录
        /// </summary>
        /// <typeparam name="TDoc">待删除的对象的类型</typeparam>
        /// <param name="collName">数据集名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="dbName">数据库名称</param>
        /// <returns>返回实际删除的数量</returns>
        public static long Remove<TDoc>(string collName, Condition condition, string dbName)
        {
            if (string.IsNullOrWhiteSpace(collName))
            {
                throw new ArgumentNullException("collName");
            }
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }
            var coll = mongo.GetCollection<TDoc>(collName, dbName);
            var result = coll.Remove(condition.Where);
            return result.DocumentsAffected;
        }

        #endregion

        #region 改

        /// <summary>
        /// 修改所有符合查询条件的记录的字段值
        /// </summary>
        /// <typeparam name="TDoc">目标对象的类型</typeparam>
        /// <param name="collName">数据集名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="modify">待更新的元素</param>
        /// <param name="dbName">数据库名称</param>
        /// <returns></returns>
        public static long Modify<TDoc>(string collName, Condition condition, ModifyElement<TDoc> modify, string dbName)
        {
            if (string.IsNullOrWhiteSpace(collName))
            {
                throw new ArgumentNullException("collName");
            }
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }
            if (modify == null)
            {
                throw new ArgumentNullException("modify");
            }
            var coll = mongo.GetCollection<TDoc>(collName, dbName);
            var result = coll.Update(condition.Where, modify.UpdateFields, UpdateFlags.Multi);
            return result.DocumentsAffected;
        }

        #endregion

        #region 查

        /// <summary>
        /// 通过指定查询条件获取单条记录
        /// </summary>
        /// <typeparam name="TDoc">待返回的对象的类型</typeparam>
        /// <param name="collName">数据集名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="usingWriteServer">是否指定为写库，默认为false，不指定为写库</param>
        /// <returns></returns>
        public static TDoc FindOne<TDoc>(string collName, Condition condition, string dbName, bool usingWriteServer = false)
        {
            if (string.IsNullOrWhiteSpace(collName))
            {
                throw new ArgumentNullException("collName");
            }
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }
            var coll = mongo.GetCollection<TDoc>(collName, dbName, usingWriteServer);
            return coll.FindOne(condition.Where);
        }

        /// <summary>
        /// 通过指定查询条件获取记录集合
        /// </summary>
        /// <typeparam name="TDoc">待返回的对象的类型</typeparam>
        /// <param name="collName">数据集名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="usingWriteServer">是否指定为写库，默认为false，不指定为写库</param>
        /// <returns></returns>
        public static IEnumerable<TDoc> FindAll<TDoc>(string collName, Condition condition, string dbName, bool usingWriteServer = false)
        {
            if (string.IsNullOrWhiteSpace(collName))
            {
                throw new ArgumentNullException("collName");
            }
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }
            var coll = mongo.GetCollection<TDoc>(collName, dbName, usingWriteServer);
            return coll.Find(condition.Where);
        }

       
        #endregion
    }
}
