using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace ClassLibrary1.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public int? UnitInStock { get; set; }
        public double? UnitPrice { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}
