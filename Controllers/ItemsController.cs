using Microsoft.AspNetCore.Mvc;
using Quillser.DTOs;
using Quillser.Entities;
using Quillser.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quillser.Controller
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository _repository;

        public ItemsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<ItemDTO> GetItems()
        {
            var items = _repository.GetItems().Select(item => item.AsDTO());

            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDTO> GetItem(Guid id)
        {
            var item = _repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDTO());
        }

        [HttpPost]
        public ActionResult<ItemDTO> CreateItem(CreateItemDTO itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _repository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDTO());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDTO itemDto)
        {
            var existingItem = _repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            _repository.UpdateItem(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = _repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            _repository.DeleteItem(id);

            return NoContent();
        }
    }
}