using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record AddressDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string UserId { get; set; }
    }

    public record CreateAddressDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public record UpdateAddressDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}
