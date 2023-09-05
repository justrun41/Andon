using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AndonServer.Models
{
    public class ClientData
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? categoryId { get; set; }

        public string ComputerName { get; set; }
        public string? ColorCode { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public bool CoordinatesChanged { get; set; }

    }
}
