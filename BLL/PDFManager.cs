using iText.Kernel.Pdf;
using iText.Layout.Element;
using DAL;
using TourplannerModel;
using iText.IO.Image;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using System.Collections.ObjectModel;
using iText.Kernel.Pdf.Canvas.Draw;
using Microsoft.Win32;
using BLL.Exceptions;
using TextAlignment = iText.Layout.Properties.TextAlignment;

namespace BLL
{
    public class PDFManager
    {
        private ITourRepository _tourRepository;
        private TourCalculation _calculator;
        public PDFManager(TourplannerContext tourplannerContext)
        {
            _tourRepository = new TourRepository(tourplannerContext);
            _calculator = new TourCalculation();
        }

        public bool createTourReportPDF(/*int tourid*/TourModel tour)
        {
            /*TourModel tour = tourRepository.GetTourById(tourid);
            IEnumerable<TourLogModel> tourlogs = tourLogRepository.GetTourLogsById(tourid);*/
            ObservableCollection<TourLogModel> tourlogs = tour.TourLogs;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "pdf file (*.pdf)|*.pdf";
            saveFileDialog.FileName = "Tour_" + tour.Id.ToString() + "_" + tour.Name + ".pdf";
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
                    list.Add("Popularity: " + tour.Popularity);
                    list.Add("Childfriendliness " + tour.ChildFriendliness + "/12 The lower the value the more child friendly");
                    document.Add(list);

                    // Add image of route
                    if (tour.Image != null)
                    {
                        try
                        {
                            ImageData imageData = ImageDataFactory.Create(tour.Image);
                            document.Add(new Image(imageData));
                        }
                        catch{/*do nothing*/}
                    }
                            

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

        public bool createSummaryPDF()
        {
            try
            {
                IEnumerable<TourModel> tours = _tourRepository.GetTours();
                if(!tours.Any())
                {
                    throw new NoToursException();
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "pdf file (*.pdf)|*.pdf";
                saveFileDialog.FileName = "Summary.pdf";
                string path;

                if (saveFileDialog.ShowDialog() == true)
                {
                    path = saveFileDialog.FileName;

                    using (PdfWriter writer = new PdfWriter(path))
                    using (PdfDocument pdf = new PdfDocument(writer))
                    using (iText.Layout.Document document = new iText.Layout.Document(pdf))
                    {
                        // Add Header
                        Paragraph heading = new Paragraph($"Summary of: {tours.Count()} Tours")
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFontSize(20)
                            .SetUnderline()
                            .SetBold();
                        document.Add(heading);

                        foreach (TourModel tour in tours)
                        {
                            // Add Subheader
                            Paragraph subheader = new Paragraph($"Tour with Id: {tour.Id}")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(15);
                            document.Add(subheader);

                            // Add values
                            List list = new List();
                            list.Add("Average time (d:hh:mm:ss): " + _calculator.GetTotalTimeAverage(tour));
                            list.Add("Distance: " + tour.TourDistance + " km");
                            list.Add("Average Rating " + _calculator.GetAverageRating(tour));
                            document.Add(list);
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
            
    }
}
