using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;

using UtilityLib.Models;

namespace WorkerService.Services
{
    public class Photographer
    {
        public Photographer(List<CameraSettings> cameras)
        {
            _cameras = cameras;
        }

        public async Task<List<Stream>> GetPhotos()
        {
            List<Stream> output = new List<Stream>();
            List<Image> photos = new List<Image>();

            if ((_cameras != null) && (_cameras.Count > 0))
            {
                using (var client = new HttpClient())
                {
                    foreach (var camera in _cameras)
                    {
                        var credentials = Encoding.ASCII.GetBytes($"{camera.User}:{camera.Password}");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));

                        var response = await client.GetAsync(camera.Uri);
                        if (response.IsSuccessStatusCode)
                        {

                            var img = Image.Load(await response.Content.ReadAsStreamAsync());
                            img.Mutate(o => o.Resize(320, 200));

                            photos.Add(img);
                        }
                    }
                }

                int height = photos.Count * 200;
                var collage = new Image<Rgba32>(320, height);

                for (int i = 0; i<photos.Count; i++)
                {
                    collage.Mutate(o => o.DrawImage(photos[i], new Point(0, i * 200), 1f));
                    photos[i].Dispose();
                }

                MemoryStream ms = new MemoryStream();
                collage.SaveAsJpeg(ms);
                ms.Position = 0;

                output.Add(ms);
            }

            return output;
        }

        private List<CameraSettings> _cameras;
    }
}
