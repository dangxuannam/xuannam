namespace CafeOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHUDEBAIVIET")]
    public partial class CHUDEBAIVIET
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHUDEBAIVIET()
        {
            BAIVIETs = new HashSet<BAIVIET>();
        }

        [Key]
        public int ChuDeID { get; set; }

        [StringLength(250)]
        public string NoiDung { get; set; }

        public DateTime? NgayTao { get; set; }

        [StringLength(250)]
        public string GhiChu { get; set; }

        public bool? TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAIVIET> BAIVIETs { get; set; }
    }
}
