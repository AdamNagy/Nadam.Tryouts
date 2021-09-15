using System;

namespace MongoDbPoc
{
    public class Ingestion
    {
        public int Attempts { get; set; }
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }
        public string DataPartition { get; set; }
        public int RevisionId { get; set; }
        public string ObjectType { get; set; }
        public Guid ObjectId { get; set; }
        public string Status { get; set; }

        public override string ToString()
            => $"{Id} {Created} {Changed} {DataPartition} {Status}";
    }
}
