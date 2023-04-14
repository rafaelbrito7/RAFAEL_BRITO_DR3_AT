using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    public class Friendship
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("aPersonId")]
        public Guid APersonId { get; set; }

        [JsonPropertyName("aPerson")]
        public Person? APerson { get; set; }

        [JsonPropertyName("bPersonId")]
        public Guid BPersonId{ get; set; }

        [JsonPropertyName("bPerson")]
        public Person? BPerson { get; set; }
    }
}
