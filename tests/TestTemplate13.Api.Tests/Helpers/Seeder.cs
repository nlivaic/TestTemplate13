using System.Collections.Generic;
using TestTemplate13.Core.Entities;
using TestTemplate13.Data;

namespace TestTemplate13.Api.Tests.Helpers
{
    public static class Seeder
    {
        public static void Seed(this TestTemplate13DbContext ctx)
        {
            ctx.Foos.AddRange(
                new List<Foo>
                {
                    new ("Text 1"),
                    new ("Text 2"),
                    new ("Text 3")
                });
            ctx.SaveChanges();
        }
    }
}
