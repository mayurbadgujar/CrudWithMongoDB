using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrudWithMongoDB.Models
{
    public class Status
    {
        public string Result { set; get; }
        public string Message { set; get; }
    }
}