using ASAPTask.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Domain.Entities
{
    public class Invoice : BaseAuditEntity<long>
    {
        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetails>();
        }
        public string InvoiceNumber { get; set; }
        public double TotalAmount { get; set; }
        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }
    }
}
