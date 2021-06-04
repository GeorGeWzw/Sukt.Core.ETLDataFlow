using Microsoft.EntityFrameworkCore;
using Sukt.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.EtlCore.EntityFrameworkCore
{
    public class SuktContext : SuktDbContextBase
    {
        public SuktContext(DbContextOptions<SuktContext> options, IServiceProvider serviceProvider)
          : base(options, serviceProvider)
        {
        }
    }
}
