using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PetShop.Data.Base;

namespace PetShop.Data.Model
{
    public partial class Category : IEntityBase
    {
        public Category()
        {
            Animals = new HashSet<Animal>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        [Display(Name = "Category: ")]
        [Required(ErrorMessage = "Name For The Category Is Required")]
        public string Name { get; set; } = null!;

        [InverseProperty("Category")]
        [Display(Name = "Animals: ")]
        public virtual ICollection<Animal> Animals { get; set; }

        public override string ToString() => Name;
    }
}
