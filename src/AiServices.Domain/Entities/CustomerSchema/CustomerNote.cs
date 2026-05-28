using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiServices.Domain.Entities.CompanySchema;
using AiServices.Domain.Entities.HrSchema;
using AiServices.Domain.Enums.CustomerEnum;

namespace AiServices.Domain.Entities.CustomerSchema
{
    public class CustomerNote
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        public Guid AuthorEmployeeId { get; set; }  // ai ghi
        public Guid AuthorGroupId { get; set; }      // group của người ghi (lấy từ CustomerAssignment hiện tại của Author)
        public string Content { get; set; } = string.Empty;

        public NoteVisibility Visibility { get; set; } = NoteVisibility.Group; // mặc định riêng tư
        public bool IsApprovedShare { get; set; } = false; // trưởng nhóm duyệt → true

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid CompanyId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Group AuthorGroup { get; set; } = null!;
        public virtual Employee AuthorEmployee { get; set; } = null!;
        public virtual Company Company { get; set; } = null!;
    }
}
