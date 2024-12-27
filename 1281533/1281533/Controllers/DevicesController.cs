using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _1281533.Models;
using _1281533.ViewModels;

namespace _1281533.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly DeviceDbContext _context;
        private readonly IWebHostEnvironment _env;
        public DevicesController(DeviceDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            return await _context.Devices.ToListAsync();
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
            var device = await _context.Devices.Include(x => x.Specs).FirstOrDefaultAsync(x => x.DeviceId == id);

            if (device == null)
            {
                return NotFound();
            }

            return device;
        }
        [HttpGet("Specs/Of/{id}")]
        public async Task<ActionResult<IEnumerable<Spec>>> GetSpectOfDevice(int id)
        {
            return await _context.Specs.Where(x => x.DeviceId == id).ToListAsync();
        }
        // PUT: api/Devices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(int id, Device device)
        {
            if (id != device.DeviceId)
            {
                return BadRequest();
            }
            var existing = await _context.Devices.FirstOrDefaultAsync(x => x.DeviceId == id);
            if (existing == null) { return NotFound(); }
            existing.DeviceName = device.DeviceName;
            existing.DeviceId = device.DeviceId;
            existing.DeviceType = device.DeviceType;
            existing.ReleaseDate = device.ReleaseDate;
            existing.Price = device.Price;
            existing.InStock = device.InStock;
            existing.Picture = device.Picture;
            await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Specs WHERE DeviceId={id}");
            foreach (var spec in device.Specs)
            {
                _context.Specs.Add(new Spec { SpecName=spec.SpecName, DeviceId=id, Value=spec.Value });
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Devices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevice", new { id = device.DeviceId }, device);
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("Image/Upload")]
        public async Task<ActionResult<ImageUploadResponse>> Upload(IFormFile pic)
        {
            string ext = Path.GetExtension(pic.FileName);
            string f = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())+ext;
            string savePath = Path.Combine(_env.WebRootPath, "Pictures", f);
            FileStream fs = new FileStream(savePath, FileMode.Create);
            await pic.CopyToAsync(fs);
            fs.Close();
            return new ImageUploadResponse { NewFileName= f };
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.DeviceId == id);
        }
    }
}