using PersonalEconomist.Entities.Models.Item;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonalEconomist.Services.Stores.ItemStore
{
    public interface IItemStore
    {
        Task<ItemDTO> AddItem(ItemDTO modelDto);
        Task<ItemDTO> UpdateItem(Guid id, ItemDTO modelDto);
        Task<ItemDTO> GetItem(Guid Id);
        Task<IEnumerable<ItemDTO>> AllItems();
        Task<ItemDTO> DeleteItem(Guid Id);
    }
}
