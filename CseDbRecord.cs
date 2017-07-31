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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    public class CseDbContext : DbContext
    {
        public DbSet<CseDbRecord> CseDbRecords { get; set; }

        public CseDbContext(string connectionString)
            : base(connectionString)
        {
        }
    }

    public class CseDbRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string QueryId { get; set; }

        public DateTimeOffset CreatedTime { get; set; }

        public string QueryText { get; set; }

        public string Market { get; set; }

        public string CseResponse { get; set; }
    }
}
