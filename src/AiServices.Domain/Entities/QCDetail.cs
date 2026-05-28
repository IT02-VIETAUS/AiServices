using System;
using System.Collections.Generic;
using AiServices.Domain.Entities.DevandqaSchema;

namespace AiServices.Domain.Entities;

public partial class Qcdetail
{
    public Guid Id { get; set; }

    public string? BatchExternalId { get; set; }

    public Guid? BatchId { get; set; }

    public string MachineExternalId { get; set; } = null!;

    public virtual ProductInspection? Batch { get; set; }
}
