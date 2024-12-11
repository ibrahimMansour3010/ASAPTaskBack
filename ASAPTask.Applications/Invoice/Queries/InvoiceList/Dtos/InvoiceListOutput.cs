using ASAPTask.Applications.Item.Queries.ItemList.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Invoice.Queries.InvoiceList.Dtos
{
    public class InvoiceListOutput
    {
        public int AllItemCount { get; set; }
        public IList<InvoiceListDto> Result { get; set; }
    }
    public class InvoiceListDto
    {
        public string InvoiceNumber { get; set; }
        public string CreationDate { get; set; }
        public double TotalPrice { get; set; }
    }
}
