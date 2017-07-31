// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CseDbRecord.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the CseDbRecord type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CsePlayer
{
    using System;
    using System.Data.Entity;

    public class OmUhrsDbContext : DbContext
    {
        public DbSet<CseDbRecord> CseDbRecords { get; set; }
    }

    public class CseDbRecord
    {
        public long Id { get; set; }

        public DateTimeOffset CreatedTime { get; set; }

        public string QueryText { get; set; }

        public string Market { get; set; }

        public string CseResponse { get; set; }
    }
}
