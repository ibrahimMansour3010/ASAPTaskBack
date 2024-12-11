using ASAPTask.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Domain.Entities
{
    public class InvoiceDetails:BaseAuditEntity<long>
    {
        public double UnitPrice { get; set; }
        public double TotalItems { get; set; }
        public long Quantity { get; set; }


        [ForeignKey("Invoice")]
        public long InvoiceId { get; set; }
        [ForeignKey("Item")]
        public long ItemId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Item Item { get; set; }
    }
}
