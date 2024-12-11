using ASAPTask.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Domain.Entities
{
    public class Item:BaseAuditEntity<long>
    {
        public Item()
        {
            InvoiceDetails = new HashSet<InvoiceDetails>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public long AvailableQuantity { get; set; }

        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }

    }
}
