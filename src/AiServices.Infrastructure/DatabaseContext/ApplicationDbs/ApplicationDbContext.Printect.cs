using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiServices.Domain.Entities.PrintectSchema;

namespace AiServices.Infrastructure.DatabaseContext.ApplicationDbs
{
    public partial class ApplicationDbContext
    {
        // ======= DbSet cho PrintcctSchema =======
        public virtual DbSet<HistoryPrintLabelForAll> HistoryPrintLabelForAlls { get; set; } = default!;
        public virtual DbSet<LabelElement> LabelElements { get; set; } = default!;
        public virtual DbSet<LabelTemplate> LabelTemplates { get; set; } = default!;

    }
}
