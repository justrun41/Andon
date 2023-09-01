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
     internal class ClientData : IEquatable<ClientData>
    {
        [BsonId]

        [BsonRepresentation(BsonType.ObjectId)]
        public required string ComputerName { get; set; }
        public string? ColorCode { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public bool CoordinatesChanged { get; set; }
        public bool Equals(ClientData other)
        {
            return other != null && ClientData.Equals(other.ComputerName, StringComparison.Ordinal);
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as ClientData);
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
