namespace LoWaiLo.Data
{
    using System;

    using LoWaiLo.Data.Common;

    using Microsoft.EntityFrameworkCore;

    public class DbQueryRunner : IDbQueryRunner
    {
        public DbQueryRunner(LoWaiLoDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public LoWaiLoDbContext Context { get; set; }

        public void RunQuery(string query, params object[] parameters)
        {
            this.Context.Database.ExecuteSqlCommand(query, parameters);
        }

        public void Dispose()
        {
            this.Context?.Dispose();
        }
    }
}
