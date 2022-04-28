using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;


namespace Pentaskilled.MEetAndYou.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MemoryAlbumController : ControllerBase
    {
        private readonly MEetAndYouDBContext _dbcontext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public MemoryAlbumController(MEetAndYouDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dbcontext = context;
            this.webHostEnvironment = webHostEnvironment;
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

        // PUT: api/Images/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImages(int id, Image image)
        {
            if (id != image.ImageId)
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


        // POST: api/Employee
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        /*        public async Task<ActionResult<IActionResult>> PostImageModel([FromForm] Image imageModel)
                {
                    imageModel.ImageName = await SaveImage(imageModel.ImageFile);
                    _context.Employees.Add(employeeModel);
                    await _context.SaveChangesAsync();

                    return StatusCode(201);
                }



                private string UploadedFile(Image model)
                {
                    string uniqueFileName = null;

                    if (model.ProfileImage != null)
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            model.ProfileImage.CopyTo(fileStream);
                        }
                    }
                    return uniqueFileName;
                }*/

        private bool ItineraryExists(int id)
        {
            return _dbcontext.Images.Any(e => e.ItineraryId == id);
        }
    }



}