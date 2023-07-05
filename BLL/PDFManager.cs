using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using DAL;
using TourplannerModel;
using iText.IO.Image;
using System.Windows.Media.Imaging;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.StyledXmlParser.Jsoup.Nodes;
using System.Collections.ObjectModel;
using MiNET.Blocks;
using iText.Kernel.Pdf.Canvas.Draw;
using MiNET.Entities.Hostile;
using System.Xml;
using Microsoft.Win32;

namespace BLL
{
    public class PDFManager
    {
        private ITourRepository tourRepository;
        private ITourLogRepository tourLogRepository;
        public PDFManager(TourplannerContext tourplannerContext)
        {
            tourRepository = new TourRepository(tourplannerContext);
            tourLogRepository = new TourLogRepository(tourplannerContext);
        }

        public bool createPDF(int tourid)
        {
            TourModel tour = tourRepository.GetTourById(tourid);
            IEnumerable<TourLogModel> tourlogs = tourLogRepository.GetTourLogsById(tourid);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "pdf file (*.pdf)|*.pdf";
            saveFileDialog.FileName = "Tour_" + tourid.ToString() + "_" + tour.Name + ".pdf";
            string path;

            if (saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;

                using (PdfWriter writer = new PdfWriter(path))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (iText.Layout.Document document = new iText.Layout.Document(pdf))
                {
                    // Add Header
                    Paragraph heading = new Paragraph("Tour: " + tour.Name)
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFontSize(20)
                        .SetUnderline()
                        .SetBold();
                    document.Add(heading);

                    // Add Subheader
                    Paragraph subheader = new Paragraph("Tour details:")
                        .SetTextAlignment(TextAlignment.LEFT)
                        .SetFontSize(15);
                    document.Add(subheader);

                    // Add tourdetails
                    List list = new List();
                    list.Add("Description: " + tour.Description);
                    list.Add("From: " + tour.From);
                    list.Add("To: " + tour.To);
                    list.Add("Transport Type: " + tour.TransportType);
                    list.Add("Distance: " + tour.TourDistance + " km");
                    list.Add("Estimated Time (hh:mm:ss): " + tour.EstimatedTime);
                    document.Add(list);

                    // Add image of route
                    ImageData imageData = ImageDataFactory.Create(tour.Image);
                    document.Add(new Image(imageData));

                    // add a blank line
                    document.Add(new Paragraph(""));

                    // Line separator
                    LineSeparator ls = new LineSeparator(new SolidLine());
                    document.Add(ls);

                    // add a blank line
                    document.Add(new Paragraph(""));

                    // Add Tourlog Table
                    Table table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
                    table.AddHeaderCell("DateTime");
                    table.AddHeaderCell("Difficulty");
                    table.AddHeaderCell("Total Time");
                    table.AddHeaderCell("Rating");
                    table.SetFontSize(14).SetBackgroundColor(ColorConstants.WHITE);

                    if (tourlogs != null)
                    {
                        foreach (TourLogModel log in tourlogs)
                        {
                            table.AddCell(log.DateTime.ToString());
                            table.AddCell(log.Difficulty.ToString());
                            table.AddCell(log.TotalTime.ToString());
                            table.AddCell(log.Rating.ToString());
                        }
                    }
                    else
                    {
                        table.AddCell("-");
                        table.AddCell("-");
                        table.AddCell("-");
                        table.AddCell("-");
                    }
                    document.Add(table);
                }
                return true;
            }
            return false;
        }
    }
}
