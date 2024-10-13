using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model
{
    public class Details
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="nvarchar(100)")]
        public string Name { get; set; } = "";

        [Column(TypeName = "nvarchar(10)")]
        public int Age { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Address { get; set; } = "";


    }
}
