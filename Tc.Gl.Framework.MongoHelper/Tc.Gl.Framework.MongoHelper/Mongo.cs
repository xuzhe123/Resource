using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Tc.Gl.Framework.MongoHelper
{
    /// <summary>
    /// MongoDB数据库对象
    /// </summary>
    /// <remarks>
    /// 当前为简易模式，仅支持MongoDB副本集模式
    /// </remarks>
    public class Mongo
    {
        private static readonly object objLock = new object();
        private static Mongo instance = null;

        private Dictionary<string, string> settings = null;
        private Dictionary<string, MongoClient> clients = null;

        private Mongo()
        {
            settings = new Dictionary<string, string>();
            clients = new Dictionary<string, MongoClient>();
            Initialize();
        }

        /// <summary>
        /// 获取MongoDB数据库对象实例
        /// </summary>
        /// <returns></returns>
        public static Mongo GetInstance()
        {
            if (instance == null)
            {
                lock (objLock)
                {
                    if (instance == null)
                    {
                        instance = new Mongo();
                    }
                }
            }

            return instance;
        }

        private void Initialize()
        {
           //settings.Add("tczzymarketing", "mongodb://[username:password@]host1[:port1][,host2[:port2],…[,hostN[:portN]]][/[database][?options]]");
            settings.Add("tczzymarketing", ConfigurationManager.AppSettings["tczzymarketing"].ToString());
            settings.Add("tczzyprice", ConfigurationManager.AppSettings["tczzyprice"].ToString());
        }

        /// <summary>
        /// 获取MongoDB服务器
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <returns></returns>
        public MongoServer GetMongoServer(string dbName)
        {
            return GetMongoClient(dbName).GetServer();
        }

        /// <summary>
        /// 获取MongoDB客户端
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <returns></returns>
        public MongoClient GetMongoClient(string dbName)
        {
            MongoClient _client = null;
            if (string.IsNullOrWhiteSpace(dbName))
            {
                throw new ArgumentNullException("dbName");
            }
            if (!clients.TryGetValue(dbName, out _client))
            {
                lock (objLock)
                {
                    if (!clients.TryGetValue(dbName, out _client))
                    {
                        _client = new MongoClient(settings[dbName]);
                    }
                }
            }
            return _client;
        }

        /// <summary>
        /// 获取MongoDB数据库，若不指定数据库名称，则获取默认配置数据库
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <returns></returns>
        public MongoDatabase GetDatabase(string dbName)
        {
            return GetMongoServer(dbName).GetDatabase(dbName);
        }

        /// <summary>
        /// 获取指定名称的文档集
        /// </summary>
        /// <typeparam name="T">文档对象的类型</typeparam>
        /// <param name="collectionName">文档集名称</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="usingWriteServer">是否指定为写库，默认为false，不指定为写库</param>
        /// <returns></returns>
        public MongoCollection<T> GetCollection<T>(string collectionName, string dbName, bool usingWriteServer = false)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentNullException("collectionName");
            }
            if (string.IsNullOrWhiteSpace(dbName))
            {
                throw new ArgumentNullException("dbName");
            }
            if (usingWriteServer)
            {
                MongoCollectionSettings settings = new MongoCollectionSettings();
                settings.ReadPreference = new ReadPreference(ReadPreferenceMode.PrimaryPreferred);
                return GetDatabase(dbName).GetCollection<T>(collectionName, settings);
            }
            return GetDatabase(dbName).GetCollection<T>(collectionName);
        }

        /// <summary>
        /// 获取指定名称的数据集 
        /// </summary>
        /// <param name="collectionName">数据集名称</param>
        /// <param name="dbName">数据库名称</param>
        /// <param name="usingWriteServer">是否指定为写库，默认为false，不指定为写库</param>
        /// <returns></returns>
        public MongoCollection GetCollection(string collectionName, string dbName, bool usingWriteServer = false)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentNullException("collectionName");
            }
            if (string.IsNullOrWhiteSpace(dbName))
            {
                throw new ArgumentNullException("dbName");
            }
            if (usingWriteServer)
            {
                MongoCollectionSettings settings = new MongoCollectionSettings();
                settings.ReadPreference = new ReadPreference(ReadPreferenceMode.PrimaryPreferred);
                return GetDatabase(dbName).GetCollection(collectionName, settings);
            }
            return GetDatabase(dbName).GetCollection(collectionName);
        }
    }
}
