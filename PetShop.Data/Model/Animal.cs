using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetShop.Data.Base;

namespace PetShop.Data.Model
{
    public partial class Animal : IEntityBase
    {
        public Animal()
        {
            Comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [Display(Name = "Name: ")]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        [Display(Name = "Description: ")]
        public string? Description { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Date of Birth: ")]
        public DateTime? BirthDate { get; set; }

        public string? PhotoUrl { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [Display(Name = "Category: ")]
        [InverseProperty("Animals")]
        public virtual Category? Category { get; set; }

        [InverseProperty("Animal")]
        [Display(Name = "Comments: ")]
        public virtual ICollection<Comment> Comments { get; set; }
   
    }
}
