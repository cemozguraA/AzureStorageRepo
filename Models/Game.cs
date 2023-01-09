using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;
public class Game : ITableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long Price { get; set; }
    public string PartitionKey { get ; set ; }
    public string RowKey { get ; set ; }
    public DateTimeOffset? Timestamp { get ; set ; }
    public ETag ETag { get ; set ; }
}
