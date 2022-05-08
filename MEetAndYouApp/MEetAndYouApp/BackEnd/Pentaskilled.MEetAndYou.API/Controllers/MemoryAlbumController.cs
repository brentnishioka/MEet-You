using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pentaskilled.MEetAndYou.Entities.DBModels;
using System.Web.Http.Cors;
using System.Data;
using System.Data.SqlClient;
using Pentaskilled.MEetAndYou.DataAccess;
using Pentaskilled.MEetAndYou.Managers.Contracts;
using Pentaskilled.MEetAndYou.DataAccess.Contracts;
using Pentaskilled.MEetAndYou.Entities.Models;

namespace Pentaskilled.MEetAndYou.API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MemoryAlbumController : ControllerBase
    {
        private readonly MEetAndYouDBContext _dbcontext;
        private readonly IMemoryAlbumManager _memoryAlbumManager;
        private readonly IMemoryAlbumDAO _memoryAlbumDAO;

        public MemoryAlbumController(MEetAndYouDBContext context, IMemoryAlbumManager memoryAlbumManager, IMemoryAlbumDAO memoryAlbumDAO)
        {
            _dbcontext = context;
            this._memoryAlbumManager = memoryAlbumManager;
            this._memoryAlbumDAO = memoryAlbumDAO;
        }

        // GET: api/Images/5
        [HttpGet]
        [Route("GetImage/{id}")]

        public async Task<ActionResult<MemoryAlbumResponse>> GetImage(int id)
        {
            try
            {
                MemoryAlbumResponse getImages = await _memoryAlbumDAO.GetImageRecordAsync(id);
                return getImages;
            }
               catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }


        // POST: api/Images
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("/PostImages")]

        public async Task<ActionResult<MemoryAlbumResponse>> PostImages( string ImageName, string ImageExtension, string ImagePath, int itineraryID)
        {
            try
            {
                MemoryAlbumResponse getImages = await _memoryAlbumDAO.AddImageToItineraryAsync(ImageName, ImageExtension, ImagePath, itineraryID);
                return getImages;
            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        // DELETE: api/Imagem/5
        [HttpDelete]
        [Route("DeleteImage/{id}")]

        public async Task<ActionResult<MemoryAlbumResponse>> DeleteImage(string imageName, int id)
        {
            try
            {
                MemoryAlbumResponse getImages = await _memoryAlbumDAO.RemoveImageFromItineraryAsync(imageName, id);
                return getImages;
            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
            

        



    }
}

/*[HttpGet]
public JsonResult Get()
{
    string query = @"
                            select ImageId, ImageName,ImageExtension,imagePath,itineraryID
                            from
                            dbo.Images
                            ";

    DataTable table = new DataTable();
    _connectionString = GetConnectionString();
    SqlDataReader myReader;
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Open();
        using (SqlCommand command = new SqlCommand("[MEetAndYou].[Images]", connection))
        {
            myReader = command.ExecuteReader();
            table.Load(myReader);
            myReader.Close();
            connection.Close();
        }
    }

    return new JsonResult(table);
}*/



