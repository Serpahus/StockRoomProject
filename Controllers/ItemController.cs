using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using StockRoomProject.Interface;
using static StockRoomProject.Models.ItemModel;


namespace StockRoomProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItem _item;

        public ItemController(ILogger<ItemController> logger, IItem item)
        {
            _logger = logger;
            _item = item;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var result = await _item.GetItems();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetItem(int Id)
        {
            var result = await _item.GetItem(Id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemRequestModel items)
        {
            var result = await _item.AddItem(items);

            if (result ) return Ok(result);
            else return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> ItemUpdate(ItemRequestModel items)
        {
            var result = await _item.UpdateItem(items);

            if (result) return Ok(result);
            else return BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> ItemDelete(int Id)
        {
          var result=  await _item.DeleteItem(Id);

            if (result) return Ok();
            else return BadRequest();
        }
    }
}
