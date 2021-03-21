using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProgrammersBlog.Entities.Dtos
{
    public class CategoryUpdateDto
    {
        [Required]
        public int CategoryId { get; set; }

        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} boş geçilemez")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden büyük olamaz")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olamaz")]
        public string Name { get; set; }

        [DisplayName("Kategori AÇıklaması")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden büyük olamaz")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olamaz")]
        public string Description { get; set; }

        [DisplayName("Kategori Notu")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden büyük olamaz")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olamaz")]
        public string Note { get; set; }

        [DisplayName("Aktif mi?")]
        [Required]
        public bool ısActive { get; set; }

        [DisplayName("Silindi mi?")]
        [Required]
        public bool ısDeleted { get; set; }
    }
}
