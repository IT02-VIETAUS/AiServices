using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiServices.Domain.Entities.ShiftReportSchema;

namespace AiServices.Infrastructure.DatabaseContext.ApplicationDbs
{
    public partial class ApplicationDbContext
    {
        public virtual DbSet<EndOfShiftReportForAll> EndOfShiftReportForAlls { get; set; } = default!;
        public virtual DbSet<EndOfShiftReportDetailForAll> EndOfShiftReportDetailForAlls { get; set; } = default!;
        public virtual DbSet<TempEndOfShiftReportForAll> TempEndOfShiftReportForAlls { get; set; } = default!;
        public virtual DbSet<OperationHistoryMd> OperationHistoryMds { get; set; } = default!;

    }
}
