using Azure.Data.Tables;
using DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL;
public class AzureTableClient : IAzureTableClient
{
    private readonly AzureTableClientOptions _azureTableClientOptions;

    public AzureTableClient(AzureTableClientOptions azureTableClientOptions)
    {
        _azureTableClientOptions = azureTableClientOptions;
        _tableServiceClient = new TableServiceClient(_azureTableClientOptions.AzureTableConnectionString, _azureTableClientOptions);

    }
    TableServiceClient _tableServiceClient;

    TableServiceClient IAzureTableClient.TableServiceClient { get { return _tableServiceClient; } }
}
