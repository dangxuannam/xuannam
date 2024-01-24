namespace CafeOnline.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOATDONG")]
    public partial class HOATDONG
    {
        public int HoatDongID { get; set; }

        [StringLength(250)]
        public string NoiDung { get; set; }

        public DateTime? ThoiGian { get; set; }

        public int? NguoiThucHien { get; set; }

        public int LoaiHoatDong { get; set; }

        [StringLength(250)]
        public string GhiChu { get; set; }

        public bool? TrangThai { get; set; }

        public virtual NGUOIDUNG NGUOIDUNG { get; set; }
    }
}
