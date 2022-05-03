using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using System.Web.Http.Cors;


namespace Pentaskilled.MEetAndYou.API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MemoryAlbumController : ControllerBase
    {
        private readonly MEetAndYouDBContext _dbcontext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public MemoryAlbumController(MEetAndYouDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dbcontext = context;
            this.webHostEnvironment = webHostEnvironment;
        }


        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            return await _dbcontext.Images.Select
                    (x => new Image() {
                        ImageId = x.ImageId,
                        ImageName = x.ImageName,
                        ImageExtension = x.ImageExtension,
                        ImagePath = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
                    })
                .ToListAsync();
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(int id)
        {
            var @image = await _dbcontext.Images.FindAsync(id);

            if (@image == null)
            {
                return NotFound();
            }

            return @image;
        }

        // PUT: api/Event/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImages(int id, Image @image)
        {
            if (id != @image.ImageId)
            {
                return BadRequest();
            }

            _dbcontext.Entry(@image).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItineraryExists(id))
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

        // POST: api/Images
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Image>> PostEvent(Image @imageModel)
        {
            @imageModel.ImageName = await SaveImage(@imageModel.ImageFile);
            _dbcontext.Images.Add(@imageModel);
            await _dbcontext.SaveChangesAsync();

            return StatusCode(201);
        }


        // DELETE: api/Imagem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @image = await _dbcontext.Images.FindAsync(id);
            if (@image == null)
            {
                return NotFound();
            }

            _dbcontext.Images.Remove(@image);
            await _dbcontext.SaveChangesAsync();

            return NoContent();
        }

/*        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }*/


        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmss") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }



        private bool ItineraryExists(int id)
        {
            return _dbcontext.Images.Any(e => e.ItineraryId == id);
        }

    }



}