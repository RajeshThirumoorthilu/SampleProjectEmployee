using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CRUD_Operations.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Role { get; set; }
        public decimal Salary { get; set; }
    }
}
