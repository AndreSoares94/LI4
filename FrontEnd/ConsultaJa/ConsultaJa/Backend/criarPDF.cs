using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;


using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ConsultaJa.Backend
{
    public class CriarPDFVertical
    {
        public static void criaPDF(int idReceita, string nomePaciente, List<string> contactosPaciente, 
            string nifPaciente, string nomeMedico, List<string> contactosMedico, string nifMedico, 
            List<Prescricao> prescricoes)
        {
            StringBuilder sb = new StringBuilder();
            Document doc = new Document(PageSize.A4,7f,5f,5f,0f);

            //doc.SetPageSize(PageSize.A4.Rotate());
            //doc.SetMargins(7f, 5f, 5f, 0f);

            doc.AddCreationDate();

            string caminho = "receita.pdf";

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            Phrase mainPhrase = new Phrase();
            mainPhrase.Add("\n");

            doc.Open();

            string item = "./ClientApp/src/components/images/logo_consultaJa.png";

            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(item);

            image.ScaleAbsoluteHeight(60f);
            image.ScaleAbsoluteWidth(160f);


            doc.Add(mainPhrase);

            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false), 12);
            cb.SetTextMatrix(400, 790);
            cb.ShowText("Receita Médica nº " + idReceita);
            cb.EndText();

            //numReceita.Add("Receita Médica nº 2020");
            //doc.Add(numReceita);
            image.SetAbsolutePosition(50,760);
            doc.Add(image);

            PdfContentByte cb1 = writer.DirectContent;
            cb1.BeginText();
            cb1.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false), 12);
            cb1.SetTextMatrix(62, 726);
            cb1.NewlineShowText("Utente: " + nomePaciente);
            cb1.SetTextMatrix(62, 714);
            sb.Clear();
            sb.Append("Contacto(s): ");
            for (int i = 0; i < contactosPaciente.Count; i++)
            {
                if (i == contactosPaciente.Count - 1)
                    sb.Append(contactosPaciente[i]);
                else
                    sb.Append(contactosPaciente[i] + ", ");
            }
            cb1.NewlineShowText(sb.ToString());
            cb1.SetTextMatrix(62, 702);
            cb1.NewlineShowText("Entidade Responsável: SNS");
            cb1.SetTextMatrix(62, 690);
            cb1.NewlineShowText("NIF: " + nifPaciente);

            cb1.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false), 12);
            cb1.SetTextMatrix(380, 726);
            cb1.NewlineShowText("Médico: " + nomeMedico);
            cb1.SetTextMatrix(380, 714);
            cb1.NewlineShowText("Especialidade: Geral");
            cb1.SetTextMatrix(380, 702);
            sb.Clear();
            sb.Append("Contacto(s): ");
            for (int i = 0; i < contactosMedico.Count; i++)
            {
                if (i == contactosMedico.Count - 1)
                    sb.Append(contactosMedico[i]);
                else
                    sb.Append(contactosMedico[i] + ", ");
            }
            cb1.NewlineShowText(sb.ToString());
            cb1.SetTextMatrix(380, 690);
            cb1.NewlineShowText("NIF: " + nifMedico);
            cb1.EndText();


            //PdfContentByte cb2 = writer.DirectContent;
            //cb2.BeginText();
            //cb2.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false), 12);
            //cb2.SetTextMatrix(60, 320);
            //cb2.NewlineShowText("NIF: 123456789");
            //cb2.EndText();

            PdfPTable table = new PdfPTable(5);
            
            var bodyFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD);
            table.TotalWidth = 500f;
            table.LockedWidth = true;
            float[] widths = new float[] { 40f, 300f, 200f, 300f, 250f};
            table.SetWidths(widths);
            PdfPCell cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 1;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Nome"));
            cell.Colspan = 1;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Quantidade"));
            cell.Colspan = 1;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Posologia"));
            cell.Colspan = 1;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("Código"));
            cell.Colspan = 1;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);
            iTextSharp.text.Font arial = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL);
            PdfContentByte bd; Barcode39 bc39; iTextSharp.text.Image barcod;
            foreach (Prescricao p in prescricoes)
            {
                cell = new PdfPCell(new Phrase(p.getSeq() + ".", arial));
                cell.PaddingLeft = 5f;
                cell.Colspan = 1;
                cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("\n" + p.getNomeFarmaco() + "\n\n", arial));
                cell.PaddingLeft = 5f;
                cell.Colspan = 1;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("\n" + p.getQuantidade() + "\n\n", arial));
                cell.Colspan = 1;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("\n" + p.getPosologia() + "\n\n", arial));
                cell.Colspan = 1;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(cell);
                bd = writer.DirectContent;
                bc39 = new Barcode39();
                bc39.Code = "911001";
                // comment next line to show barcode text
                bc39.Font = null;
                barcod = bc39.CreateImageWithBarcode(bd, null, null);
                barcod.SetAbsolutePosition(450f, 615f);
                doc.Add(barcod);
                cell = new PdfPCell(new Phrase("\n\n\n911001\n", arial));
                cell.Colspan = 1;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(cell);
            }
            table.SpacingBefore = 160f;
            doc.Add(table);

            /*
            //criando uma string vazia
            string dados = "";

            //criando a variavel para paragrafo
            Paragraph paragrafo = new Paragraph(dados, new Font(Font.NORMAL, 14));
            //etipulando o alinhamneto
            paragrafo.Alignment = Element.ALIGN_JUSTIFIED;
            //Alinhamento Justificado
            //adicioando texto
            paragrafo.Add("TESTE TESTE TESTE");
            //acidionado paragrafo ao documento
            doc.Add(paragrafo);
            */
            //fechando documento para que seja salva as alteraçoes.
            doc.Close();
            
        }
    }
}
