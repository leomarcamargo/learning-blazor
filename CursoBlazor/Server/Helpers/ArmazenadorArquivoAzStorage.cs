using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace CursoBlazor.Server.Helpers
{
    public class ArmazenadorArquivoAzStorage : IArmazenadorArquivo
    {
        private readonly string _connectionString;

        public ArmazenadorArquivoAzStorage(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AzureStorage");
        }

        public async Task<string> EditarArquivo(byte[] conteudo, string extensao, string nomeContainer, string diretorioAtual)
        {
            if (!string.IsNullOrEmpty(diretorioAtual))
            {
                await RemoverArquivo(diretorioAtual, nomeContainer);
            }

            return await SalvarArquivo(conteudo, extensao, nomeContainer);
        }

        public async Task RemoverArquivo(string diretorio, string nomeContainer)
        {
            var conta = CloudStorageAccount.Parse(_connectionString);
            var servicoCliente = conta.CreateCloudBlobClient();
            var container = servicoCliente.GetContainerReference(nomeContainer);

            var blobName = Path.GetFileName(diretorio);
            var blob = container.GetBlobReference(blobName);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> SalvarArquivo(byte[] conteudo, string extensao, string nomeContainer)
        {
            var conta = CloudStorageAccount.Parse(_connectionString);
            var servicoCliente = conta.CreateCloudBlobClient();
            var container = servicoCliente.GetContainerReference(nomeContainer);

            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            var nomeArquivo = $"{Guid.NewGuid()}.{extensao}";
            var blob = container.GetBlockBlobReference(nomeArquivo);
            await blob.UploadFromByteArrayAsync(conteudo, 0, conteudo.Length);
            blob.Properties.ContentType = "image/jpg";
            await blob.SetPropertiesAsync();
            return blob.Uri.ToString();
        }
    }
}
