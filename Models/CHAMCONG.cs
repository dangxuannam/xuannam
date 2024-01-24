namespace CafeOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHAMCONG")]
    public partial class CHAMCONG
    {
        public int ChamCongID { get; set; }

        public int? NhanVienID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ngay { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCham { get; set; }

        public int? CaLam { get; set; }

        [StringLength(250)]
        public string GhiChu { get; set; }

        public bool? TrangThai { get; set; }

        public int? DiMuon { get; set; }

        public virtual CALAMVIEC CALAMVIEC { get; set; }

        public virtual NGUOIDUNG NGUOIDUNG { get; set; }

        public virtual QUYDINHDIMUON QUYDINHDIMUON { get; set; }
    }
}
