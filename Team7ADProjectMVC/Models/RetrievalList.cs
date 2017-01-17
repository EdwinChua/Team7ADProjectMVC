using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models
{
    public class RetrievalList
    {
        public int retrievalListId { get; set; }
        public List<Inventory> itemsToRetrieve { get; set; }

        public RetrievalList()
        {
            
        }
        public RetrievalList(int retrievalListId)
        {
            this.retrievalListId = retrievalListId;
        }


    }
}