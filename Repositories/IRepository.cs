using Quillser.Entities;
using System;
using System.Collections.Generic;

namespace Quillser.Repositories
{
    public interface IRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Guid id);
    }
}