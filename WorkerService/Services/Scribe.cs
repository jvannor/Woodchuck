using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Services
{
    public class Scribe
    {
        public Scribe(string destination)
        {
            _uri = destination;
        }

        public async Task<List<string>> SaveStreams(List<Stream> streams)
        {
            List<string> results = new List<string>();

            if (!string.IsNullOrEmpty(_uri))
            {
                if ((streams != null) && (streams.Count > 0))
                {
                    var uri = new Uri(_uri);
                    var container = new BlobContainerClient(uri);
                    
                    foreach (var stream in streams)
                    {
                        var file = string.Format("{0}.jpg", Guid.NewGuid());
                        var blob = container.GetBlobClient(file);
                        await blob.UploadAsync(stream);
                        results.Add(blob.Uri.AbsoluteUri);
                    }
                }
            }

            return results;
        }

        private string _uri;
    }
}
