using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.DTOs
{
    public class DatabaseDTO
    {
        public long IdDb { get; set; }
        public string DbSchema { get; set; } = null!;
        public string DbName { get; set; } = null!;
        public string ServerName { get; set; } = null!;
        public string ServerRoute { get; set; } = null!;

    }
}
