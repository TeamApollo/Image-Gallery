namespace ImageGallery.Common
{
    using System;

    public class ImageGalleryException : Exception
    {
        public ImageGalleryException(string message)
            : base(message)
        { 
        }
    }
}
