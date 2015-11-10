namespace ImageGallery.Common
{
    using System;

    public static class Validator
    {
        public static void ValidateObjectIsNotNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("The " + nameof(obj) + "cannot be null!");
            }
        }
    }
}
