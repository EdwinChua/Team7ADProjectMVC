using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7ADProjectMVC.Models
{
    interface IStoreService
    {
        
        List<Inventory> ReturnAllInventory();

        List<Category> ReturnAllCategories();
    }
}
