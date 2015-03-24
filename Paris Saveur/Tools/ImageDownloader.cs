using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Paris_Saveur.Tools
{
    class ImageDownloader
    {
        public static async void DownloadImageIntoImage(Image image, String url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            byte[] img = await response.Content.ReadAsByteArrayAsync();
            InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
            DataWriter writer = new DataWriter(randomAccessStream.GetOutputStreamAt(0));
            writer.WriteBytes(img);
            await writer.StoreAsync();
            BitmapImage b = new BitmapImage();
            b.SetSource(randomAccessStream);
            image.Source = b;
        }

        public static async Task DownloadImageIntoImage(Restaurant restaurant)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://www.vivelevendredi.com" + restaurant.thumbnail);
            byte[] img = await response.Content.ReadAsByteArrayAsync();
            InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
            DataWriter writer = new DataWriter(randomAccessStream.GetOutputStreamAt(0));
            writer.WriteBytes(img);
            await writer.StoreAsync();
            BitmapImage b = new BitmapImage();
            b.SetSource(randomAccessStream);
            restaurant.thumbnailBitmap = b;
        }
    }
}
