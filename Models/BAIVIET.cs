namespace CafeOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BAIVIET")]
    public partial class BAIVIET
    {
        public int BaiVietID { get; set; }

        public string MoTaNgan { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string ChiTiet { get; set; }

        public int? ChuDe { get; set; }

        [StringLength(100)]
        public string HinhAnh { get; set; }

        public DateTime? NgayViet { get; set; }

        public DateTime? NgayDang { get; set; }

        [StringLength(250)]
        public string GhiChu { get; set; }

        public bool? TrangThai { get; set; }

        public virtual CHUDEBAIVIET CHUDEBAIVIET { get; set; }
    }
}
