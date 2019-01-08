using System;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace ApplitrackUITests.Helpers
{
    //TODO This will be used when the Applitrack PDF generation functionality is refactored
    //Currently all PDFs generated for applicants will fail to open.
    //Once the refactor happens, this class should be expanded
    class Pdf 
    {
        /// <summary>
        /// Read a page of a PDF and return its text
        /// </summary>
        /// <param name="path">The path to the PDF</param>
        /// <param name="pageNumber">The page number to read</param>
        /// <returns>The text on the specified page</returns>
        public string GetPageText(string path, int pageNumber)
        {
            try
            {
                var reader = new PdfReader(path);
                var parser = new PdfReaderContentParser(reader);
                var strategy = parser.ProcessContent(pageNumber, new SimpleTextExtractionStrategy());
                reader.Close();
                return strategy.GetResultantText();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // TODO see if this actually works...
        /// <summary>
        /// Get the number of degrees the pdf is rotated
        /// </summary>
        /// <param name="path">The path to the PDF</param>
        /// <returns>The degrees of rotation</returns>
        public int GetRotationDegrees(string path)
        {
            var reader = new PdfReader(path);
            var rotation = reader.GetPageRotation(0);
            reader.Close();
            return rotation;
        }

    }
}
