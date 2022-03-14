using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace MongoDbPoc
{
    [BsonIgnoreExtraElements]
    public class Ingestion
    {
        public virtual string Key { get; set; }
        public string ProjectId { get; set; }
        public string RevisionId { get; set; }
        public string ObjectType { get; set; }
        public string ObjectId { get; set; }
        public long Attempts { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }
        public string DataPartition { get; set; }

        public override string ToString()
            => $"{Key} {Created} {Changed} {DataPartition} {Status}";
    }
}
