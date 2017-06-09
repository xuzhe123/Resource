using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoModel;
using Tc.Gl.Framework.MongoHelper;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //获取集合
            List<ResourcePackageRelationship> rprList = MongoDBHelper.FindAll<ResourcePackageRelationship>(MongoDbManager.MongoCollectionName.Coll_ResourcePackageRelationship,
                                  Condition.Create<ResourcePackageRelationship>(m => m.ResourceId >= 0 && m.ResourceId <= 10000), MongoDbManager.DbName.Db_TCZZYPrice, true).ToList();

            //获取单个实体
            LinePackagePrice linePackagePrice = MongoDBHelper.FindOne<LinePackagePrice>(MongoDbManager.MongoCollectionName.Coll_LinePackagePrice,
                                  Condition.Create<LinePackagePrice>(m => m.LineId >=10885), MongoDbManager.DbName.Db_TCZZYPrice);
            //增
            ActivityLineTopPrice price = new ActivityLineTopPrice();
            price.LineId = 123456;
            price.MaxPrice = 11111;
            price.MinPrice = 1;
            price.Periods = 1;
            bool addResult = MongoDBHelper.Add<ActivityLineTopPrice>(price, MongoDbManager.MongoCollectionName.Coll_ActivityLineTopPrice, MongoDbManager.DbName.Db_TCZZYMarketing);


            //改
            ModifyElement<ActivityLineTopPrice> modifyElement = new ModifyElement<ActivityLineTopPrice>();
            modifyElement.Push(m => m.MaxPrice, 200);
            modifyElement.Push(m => m.MinPrice, 10);
            bool modifyResult = MongoDBHelper.Modify<ActivityLineTopPrice>(MongoDbManager.MongoCollectionName.Coll_ActivityLineTopPrice,
                  Condition.Create<ActivityLineTopPrice>(m => m.ObjectId == new MongoDB.Bson.ObjectId("55b98965426f2f5b7f3980fb")), modifyElement, MongoDbManager.DbName.Db_TCZZYMarketing) > 0;

            //删
            long removeCount = MongoDBHelper.Remove<ActivityLineTopPrice>(MongoDbManager.MongoCollectionName.Coll_ActivityLineTopPrice,
                       Condition.Create<ActivityLineTopPrice>(m => m.ObjectId == new MongoDB.Bson.ObjectId("55b98965426f2f5b7f3980fb")), MongoDbManager.DbName.Db_TCZZYMarketing);//移除Mongo中数据

            //改
            Modify(linePackagePrice);

           

        }

        public static bool Modify(LinePackagePrice linePackagePrice)
        {
            Mongo mongo = Mongo.GetInstance();
            //最底层的方法（获取指定名称的文档集）
            var coll = mongo.GetCollection<LinePackagePrice>(MongoDbManager.MongoCollectionName.Coll_LinePackagePrice, MongoDbManager.DbName.Db_TCZZYPrice);
            var query = new QueryDocument
            {
                {"l", linePackagePrice.LineId},
                {"lp", linePackagePrice.LinePackageId},
                {"p.d", new DateTime(2016, 3, 9)}
                
            };
            var updateElement = new BsonDocument
                                    {
                                        {"p.$.aa", "123"},
                                        {"p.$.ac","123"},
                                        {"p.$.ad", "123"}
                                    };
            var update = new UpdateDocument
            {
                {"$set", updateElement}
            };
            var result = coll.Update(query, update);
            return result.Ok;
        }
    }
}
