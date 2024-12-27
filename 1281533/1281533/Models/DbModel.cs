using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _1281533.Models
{
    public enum DeviceType { Mobile = 1, Tab }
    public class Device
    {
        public int DeviceId { get; set; }
        [Required, StringLength(40)]
        public string DeviceName { get; set; } = default!;
        [Required, EnumDataType(typeof(DeviceType))]
        public DeviceType DeviceType { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime? ReleaseDate { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required, StringLength(40)]
        public string Picture { get; set; } = default!;
        public bool InStock { get; set; }
        public virtual ICollection<Spec> Specs { get; set; } = new List<Spec>();
    }
    public class Spec
    {
        public int SpecId { get; set; }
        [Required, StringLength(40)]
        public string SpecName { get; set; } = default!;
        [Required, StringLength(40)]
        public string Value { get; set; } = default!;
        [Required, ForeignKey("Device")]
        public int DeviceId { get; set; }
        public virtual Device? Device { get; set; } = default!;
    }

    public class DeviceDbContext : DbContext
    {
        public DeviceDbContext(DbContextOptions<DeviceDbContext> options) : base(options) { }
        public DbSet<Device> Devices { get; set; } = default!;
        public DbSet<Spec> Specs { get; set; } = default!;

    }
}
