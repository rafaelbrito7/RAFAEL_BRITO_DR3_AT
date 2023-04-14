using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    public class State
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Name { get; set; }

        [JsonPropertyName("photoUrl")]
        public string? PhotoUrl { get; set; }

        [JsonPropertyName("countryId")]
        public Guid CountryId { get; set; }

        [JsonPropertyName("country")]
        public Country? Country { get; set; }

        [JsonPropertyName("people")]
        public ICollection<Person>? People { get; set; }
    }
}
