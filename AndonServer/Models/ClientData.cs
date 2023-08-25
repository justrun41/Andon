using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndonServer.Models
{
     internal class ClientData : IEquatable<ClientData>
    {
        public required string ComputerName { get; set; }
        public string? ColorCode { get; set; }
        public bool IsChanged { get; set; }
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
