using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entities
{
    public class Person
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("stateId")]
        public Guid StateId { get; set; }

        [JsonPropertyName("state")]
        public State? State { get; set; }

        [JsonPropertyName("countryId")]
        public Guid CountryId { get; set; }

        [JsonPropertyName("country")]
        public Country? Country { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "Email é Obrigatório")]
        [EmailAddress(ErrorMessage = "Email não está em um formato correto")]
        public string Email { get; set; }

        [JsonPropertyName("phoneNumber")]
        [Required(ErrorMessage = "Número é obrigatório!")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("photoUrl")]
        public string? PhotoUrl { get; set; }

        [JsonPropertyName("birthday")]
        [Required(ErrorMessage = "Data de nascimento é obrigatório!")]
        public DateTime Birthday { get; set; }

        [InverseProperty("APerson")]
        public ICollection<Friendship>? FriendshipsA { get; set; }

        [InverseProperty("BPerson")]
        public ICollection<Friendship>? FriendshipsB { get; set; }
    }
}