using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PetShop.Data.Base;

namespace PetShop.Data.Model
{
    public partial class Comment : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Content: ")]
        [Required(ErrorMessage = "if a comment does not have content, is it really a comment?")]
        public string Content { get; set; } = null!;
        public int? AnimalId { get; set; }

        [ForeignKey("AnimalId")]
        [InverseProperty("Comments")]
        public virtual Animal? Animal { get; set; }
    }
}
