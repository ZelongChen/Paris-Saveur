using Paris_Saveur.Model;
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

        public static async void DownloadImageIntoImage(Restaurant restaurant)
        {


            if (restaurant.thumbnail != null)
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
                restaurant.ThumbnailBitmap = b;
            }
        }

        public static async void DownloadImageIntoImage(User user)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            if (user.avatar_url.Contains("http://www.gravatar.com"))
            {
                response = await client.GetAsync(user.avatar_url);

            }
            else
            {
                response = await client.GetAsync("http://www.vivelevendredi.com" + user.avatar_url);
            }
            byte[] img = await response.Content.ReadAsByteArrayAsync();
            InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
            DataWriter writer = new DataWriter(randomAccessStream.GetOutputStreamAt(0));
            writer.WriteBytes(img);
            await writer.StoreAsync();
            BitmapImage b = new BitmapImage();
            b.SetSource(randomAccessStream);
            user.AvatarThumbnailBitmap = b;
        }
    }
}
