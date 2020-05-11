using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walking_Dinner__asp.net_.Documents
{
    public static class PDF
    {
        public static void Email(DateTime? dinnerDate = null, string dinnerType = "Ontbijt", int numberOfPeople = 8, string duoName = "Familie Barendson", string dieetWensen = "", string time = "")
        {
            var today = DateTime.Now;
            if (dinnerDate == null) dinnerDate = today.AddDays(30);

            try
            {
                var pdfDoc = new Document(PageSize.A4, 40f, 40f, 60f, 60f);
                var path = System.AppDomain.CurrentDomain.BaseDirectory + "PDF\\";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                var name = "WalkingDinner-" + (dinnerDate != null ? dinnerDate.Value.ToString("dd-MM-yyyy") : "n/a") + ".pdf";
                PdfWriter.GetInstance(pdfDoc, new FileStream(path + name, FileMode.OpenOrCreate));
                pdfDoc.Open();
                var spacer = new Paragraph("")
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f
                };

                pdfDoc.Add(new Paragraph("Datum:    " + today.ToString("dd-MM-yyyy")));
                pdfDoc.Add(spacer);
                pdfDoc.Add(spacer);
                pdfDoc.Add(new Paragraph("Beste Meneer/Mevrouw,"));
                pdfDoc.Add(spacer);
                pdfDoc.Add(new Paragraph("Hierbij volgt de informatie betreffende de Walking Dinner op " + (dinnerDate != null ? dinnerDate.Value.ToString("dd-MM-yyyy") : "n/a")));
                pdfDoc.Add(new Paragraph("U mag het " + dinnerType + " verzorgen voor " + numberOfPeople + " personen, zonder u zelf en uw partner meegerekent."));
                pdfDoc.Add(new Paragraph("Deze ronde zal plaatsnemen om: " + time));
                if (dieetWensen.Length != 0) pdfDoc.Add(new Paragraph("Dieetwensen: " + dieetWensen));
                else pdfDoc.Add(new Paragraph("Er zijn geen dieetwensen"));

                pdfDoc.Add(spacer);
                pdfDoc.Add(new Paragraph("We hopen dat u veel plezier beleeft aan ons evenement zodat we jullie opnieuw mogen verwachten op de volgende editie."));

                pdfDoc.Add(spacer);
                pdfDoc.Add(spacer);
                pdfDoc.Add(new Paragraph("Met vriendelijke groeten,"));
                pdfDoc.Add(spacer);
                pdfDoc.Add(spacer);
                pdfDoc.Add(new Paragraph("Het Comité van Walking Dinner"));
                pdfDoc.Close();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static void Round(int roundNumber = 1, string naam = "Jaap en Annika", string dinnerAddress = "Hengelosestraat 5")
        {
            var spacer = new Paragraph("")
            {
                SpacingBefore = 10f,
                SpacingAfter = 10f
            };

            var today = DateTime.Now;
            var dinnerDate = today.AddDays(30);
            var pdfDoc = new Document(PageSize.A4, 40f, 40f, 60f, 60f);
            var path = System.AppDomain.CurrentDomain.BaseDirectory + "PDF\\";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var name = "WalkingDinner-" + (dinnerDate != null ? dinnerDate.ToString("dd-MM-yyyy") : "n/a") + "round" + roundNumber + ".pdf";
            pdfDoc.Add(new Paragraph("Beste duo " + naam));
            pdfDoc.Add(spacer);
            pdfDoc.Add(spacer);
            if (roundNumber == 1)
            {
                pdfDoc.Add(new Paragraph("Welkom op de Walking Dinner, wij hopen dat u een gezellige dag heeft."));
            }

            pdfDoc.Add(new Paragraph("Voor de " + roundNumber + "e ronde mag u naar " + dinnerAddress));
            pdfDoc.Add(new Paragraph("Success!"));
            pdfDoc.Add(spacer);
            pdfDoc.Add(spacer);
            pdfDoc.Add(new Paragraph("Het comité van de Walking Dinner"));
        }
    }
}
