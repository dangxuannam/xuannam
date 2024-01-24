namespace CafeOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MATHANG")]
    public partial class MATHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MATHANG()
        {
            CTHDs = new HashSet<CTHD>();
        }
        [Key]
        public int MatHangID { get; set; }

        [StringLength(250)]
        [Required]
        public string TenMatHang { get; set; }

        [Required]
        public string MoTa { get; set; }

        public int? LoaiHang { get; set; }

        [StringLength(100)]
        public string HinhAnh { get; set; }

        [Required]
        public decimal? DonGia { get; set; }

        [StringLength(10)]
        public string DVT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTao { get; set; }

        public int? LuotXem { get; set; }

        [StringLength(250)]
        public string GhiChu { get; set; }

        public bool? TrangThai { get; set; }

        [StringLength(10)]
        [Required]
        public string MaMatHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHD> CTHDs { get; set; }

        public virtual LOAIMATHANG LOAIMATHANG { get; set; }
    }
}
