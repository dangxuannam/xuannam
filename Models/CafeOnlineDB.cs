namespace CafeOnline.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CafeOnlineDB : DbContext
    {
       
        private static CafeOnlineDB db;
        protected CafeOnlineDB()
            : base("name=CafeOnlineDB")
        {
            Database.SetInitializer<CafeOnlineDB>(null);
        }

        public static CafeOnlineDB ConnectDatabase()
        {
            
            if (db == null)
                db = new CafeOnlineDB();
            return db;
        }


        public virtual DbSet<BAIVIET> BAIVIETs { get; set; }
        public virtual DbSet<BAN> BANs { get; set; }
        public virtual DbSet<CALAMVIEC> CALAMVIECs { get; set; }
        public virtual DbSet<CHAMCONG> CHAMCONGs { get; set; }
        public virtual DbSet<CHUDEBAIVIET> CHUDEBAIVIETs { get; set; }
        public virtual DbSet<CTHD> CTHDs { get; set; }
        public virtual DbSet<HOADON> HOADONs { get; set; }
        public virtual DbSet<HOATDONG> HOATDONGs { get; set; }
        public virtual DbSet<LOAIMATHANG> LOAIMATHANGs { get; set; }
        public virtual DbSet<MATHANG> MATHANGs { get; set; }
        public virtual DbSet<NGUOIDUNG> NGUOIDUNGs { get; set; }
        public virtual DbSet<NHOMNGUOIDUNG> NHOMNGUOIDUNGs { get; set; }
        public virtual DbSet<QUYDINHDIMUON> QUYDINHDIMUONs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BAN>()
                .Property(e => e.PhuThu)
                .HasPrecision(18, 0);

            modelBuilder.Entity<CALAMVIEC>()
                .HasMany(e => e.CHAMCONGs)
                .WithOptional(e => e.CALAMVIEC)
                .HasForeignKey(e => e.CaLam);

            modelBuilder.Entity<CALAMVIEC>()
                .HasMany(e => e.HOADONs)
                .WithOptional(e => e.CALAMVIEC1)
                .HasForeignKey(e => e.CaLamViec);

            modelBuilder.Entity<CHUDEBAIVIET>()
                .HasMany(e => e.BAIVIETs)
                .WithOptional(e => e.CHUDEBAIVIET)
                .HasForeignKey(e => e.ChuDe);

            modelBuilder.Entity<HOADON>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HOADON>()
                .Property(e => e.GiamGia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HOADON>()
                .HasMany(e => e.CTHDs)
                .WithRequired(e => e.HOADON)
                .HasForeignKey(e => e.SoHD);

            modelBuilder.Entity<LOAIMATHANG>()
                .HasMany(e => e.MATHANGs)
                .WithOptional(e => e.LOAIMATHANG)
                .HasForeignKey(e => e.LoaiHang)
                .WillCascadeOnDelete();

            modelBuilder.Entity<MATHANG>()
                .Property(e => e.DonGia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<MATHANG>()
                .HasMany(e => e.CTHDs)
                .WithRequired(e => e.MATHANG1)
                .HasForeignKey(e => e.MatHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NGUOIDUNG>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<NGUOIDUNG>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<NGUOIDUNG>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NGUOIDUNG>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<NGUOIDUNG>()
                .Property(e => e.LuongTheoGio)
                .HasPrecision(18, 0);

            modelBuilder.Entity<NGUOIDUNG>()
                .HasMany(e => e.CHAMCONGs)
                .WithOptional(e => e.NGUOIDUNG)
                .HasForeignKey(e => e.NhanVienID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<NGUOIDUNG>()
                .HasMany(e => e.CTHDs)
                .WithRequired(e => e.NGUOIDUNG)
                .HasForeignKey(e => e.NhanVienPhucVu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NGUOIDUNG>()
                .HasMany(e => e.HOADONs)
                .WithOptional(e => e.NGUOIDUNG)
                .HasForeignKey(e => e.KhachHang);

            modelBuilder.Entity<NGUOIDUNG>()
                .HasMany(e => e.HOATDONGs)
                .WithOptional(e => e.NGUOIDUNG)
                .HasForeignKey(e => e.NguoiThucHien);

            modelBuilder.Entity<NHOMNGUOIDUNG>()
                .HasMany(e => e.NGUOIDUNGs)
                .WithOptional(e => e.NHOMNGUOIDUNG)
                .HasForeignKey(e => e.NhomSD);

            modelBuilder.Entity<QUYDINHDIMUON>()
                .Property(e => e.PhatTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<QUYDINHDIMUON>()
                .HasMany(e => e.CHAMCONGs)
                .WithOptional(e => e.QUYDINHDIMUON)
                .HasForeignKey(e => e.DiMuon);
        }
    }
}
