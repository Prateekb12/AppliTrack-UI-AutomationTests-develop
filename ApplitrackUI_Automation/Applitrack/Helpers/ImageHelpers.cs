using System;
using System.Drawing;
using System.IO;

namespace ApplitrackUITests.Helpers
{
    public static class ImageHelpers
    {
        //TODO use this somewhere
        /// <summary>
        /// Compare an actual and expected image and see if they are the same
        /// </summary>
        /// <param name="expectedImage">The expected image</param>
        /// <param name="actualImage">The actual image</param>
        /// <returns>True if the images are the same</returns>
        public static bool ImagesAreSame(Image expectedImage, Image actualImage)
        {
            var expected = GetImageString(expectedImage);
            var actual = GetImageString(actualImage);

            return expected.Equals(actual);
        }

        /// <summary>
        /// Convert an image into a string array
        /// </summary>
        /// <param name="image">The image to convert</param>
        /// <returns>A string representing the bytes of an image</returns>
        private static string GetImageString(Image image)
        {
            var ms = new MemoryStream();

            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            var imageString = Convert.ToBase64String(ms.ToArray());

            ms.Close();

            return imageString;
        }
    }
}
