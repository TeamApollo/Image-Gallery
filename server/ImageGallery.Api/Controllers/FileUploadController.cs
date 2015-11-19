namespace DateFirst.Api.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;

    using Spring.IO;
    using Spring.Social.Dropbox.Api;
    using Spring.Social.Dropbox.Connect;
    using System.Net.Http;
    using Microsoft.AspNet.Identity.Owin;
    using ImageGallery.Data.Contracts;
    using ImageGallery.Models;

    public class FileUploadController : ApiController
    {
        private const string DropboxAppKey = "1vr5ezudby56rmx";
        private const string DropboxAppSecret = "5a0va0gmb54a4vg";

        private const string OAuthTokenFileName = "OAuthTokenFileName.txt";
        private string name;

        private readonly IImageGalleryData data;

        public FileUploadController(IImageGalleryData data)
        {
            this.data = data;
        }

        [Authorize]
        [HttpPost]
        public void UploadFile()
        {
            var filePath = string.Empty;
            var albumId = Request.Headers.FirstOrDefault(h => h.Key == "X-Album-Id").Value.First();
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                
                if (httpPostedFile != null)
                {
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/"), httpPostedFile.FileName);
                    filePath = fileSavePath;
                    name = httpPostedFile.FileName;
                    httpPostedFile.SaveAs(fileSavePath);
                }
            }

            DropboxServiceProvider dropboxServiceProvider = new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.Full);

            IDropbox dropbox = dropboxServiceProvider.GetApi("0ptqwblxvjk42u2d", "k4rlrdx430xld72");

            Entry uploadFileEntry = dropbox.UploadFileAsync(new FileResource(string.Format(filePath)), string.Format("/DateFirstImagesDb/{0}", name)).Result;

            var link = dropbox.GetMediaLinkAsync(string.Format("/DateFirstImagesDb/{0}", name));
            var UrlForDb = link.Result.Url;

            //var currentUserName = this.User.Identity.Name;

            //var userId = this.data.Users.All().FirstOrDefault(x => x.UserName == currentUserName).Id;

            int albumIdReal = int.Parse(albumId);
            var image = new Image()
            {
                AlbumId = albumIdReal,
                OriginalFileName = name,
                UrlPath = UrlForDb
            };
                                 
            this.data.Images.Add(image);
            data.SaveChanges();
        }
    }
}