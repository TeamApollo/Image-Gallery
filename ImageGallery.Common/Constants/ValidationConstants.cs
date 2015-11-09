namespace ImageGallery.Common.Constants
{
    public class ValidationConstants
    {
        // User
        public const int NameMaxLength = 30;

        // Album
        public const int AlbumNameMaxLength = 100;
        public const int AlbumDescriptionMaxLength = 1000;

        // FileInfo
        public const int MaxFileExtensionLength = 4;
        public const int MaxOriginalFileNameLength = 255;
        public const int MaxUrlPathLength = 2000;

        // Comment
        public const int MaxCommentBodyLength = 4000;

        // Tag
        public const int MaxTagDescriptionLength = 50;
    }
}