using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers;
public class AzureTableClientOptions : TableClientOptions
{
    public AzureTableClientOptions()
    {

    }
    public AzureTableClientOptions(string azureTableConnectionString)
    {
        AzureTableConnectionString = azureTableConnectionString;
    }
    public string AzureTableConnectionString { get; set; }
}