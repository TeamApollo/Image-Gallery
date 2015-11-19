namespace DateFirst.Api.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    using ImageGallery.Data.Contracts;
    using ImageGallery.Models;
    using Spring.IO;
    using Spring.Social.Dropbox.Api;
    using Spring.Social.Dropbox.Connect;        
     
    public class FileUploadController : ApiController
    {   
        private const string DropboxAppKey = "1vr5ezudby56rmx";
        private const string DropboxAppSecret = "5a0va0gmb54a4vg";  
        private const string DropboxReturnedAppKey = "0ptqwblxvjk42u2d";
        private const string DropboxReturnedAppSecret = "k4rlrdx430xld72";
        private const string DataFirstPath = "/DateFirstImagesDb/{0}";  
        private const string OAuthTokenFileName = "OAuthTokenFileName.txt";
        private readonly IImageGalleryData data;
        private string name;            

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
                    this.name = httpPostedFile.FileName;
                    httpPostedFile.SaveAs(fileSavePath);
                }
            }

            DropboxServiceProvider dropboxServiceProvider = new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.Full);

            IDropbox dropbox = dropboxServiceProvider.GetApi(DropboxReturnedAppKey, DropboxReturnedAppSecret);

            Entry uploadFileEntry = dropbox.UploadFileAsync(new FileResource(string.Format(filePath)), string.Format(DataFirstPath, this.name)).Result;

            var link = dropbox.GetMediaLinkAsync(string.Format(DataFirstPath, this.name));
            var urlForDb = link.Result.Url;            
            int albumIdReal = int.Parse(albumId);
            var image = new Image()
            {
                AlbumId = albumIdReal,
                OriginalFileName = this.name,
                UrlPath = urlForDb
            };
                                 
            this.data.Images.Add(image);
            this.data.SaveChanges();
        }
    }
}