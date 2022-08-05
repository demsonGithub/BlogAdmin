using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.IO;
using System.Text;

namespace Demkin.Blog.Utils.Help
{
    public class PdfHelper
    {
        public static byte[] DataTableToPDF(DataTable dt, float fontSize)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                // 默认页面大小
                Document document = new Document();
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, ms);
                document.Open();
                // 设置字体
                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\simfang.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font = new Font(baseFont, fontSize);
                PdfPTable table = new PdfPTable(dt.Columns.Count);
                table.WidthPercentage = 100;
                table.DefaultCell.Padding = 1;
                table.DefaultCell.BorderWidth = 1;
                table.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                // 将datatable的表头转换成pdfTable的表头
                foreach (DataColumn item in dt.Columns)
                {
                    table.AddCell(new Phrase(item.ColumnName.ToString(), font));
                }
                // 插入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        table.AddCell(new Phrase(dt.Rows[i][j].ToString(), font));
                    }
                }
                document.Add(table);
                document.Close();
                byte[] result = ms.ToArray();
                return result;
            }
        }
    }
}