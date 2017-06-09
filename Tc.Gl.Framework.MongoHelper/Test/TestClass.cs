using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoModel;
using Tc.Gl.Framework.MongoHelper;

namespace Test
{
    public class TestClass
    {
        public void GetrprList()
        {
            List<ResourcePackageRelationship> rprList = MongoDBHelper.FindAll<ResourcePackageRelationship>(MongoDbManager.MongoCollectionName.Coll_ResourcePackageRelationship,
          Condition.Create<ResourcePackageRelationship>(m => m.ResourceId >= 0 && m.ResourceId <= 10000), MongoDbManager.DbName.Db_TCZZYPrice, true).ToList();
        }
    }
}
