using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
namespace ExcelParserNet
{
    class Program
    {
        static void Main(string[] args)
        {
            string name_path;
            Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
            excelapp.Visible = true;
            Console.WriteLine("Введите путь к текстовому файлу");
            name_path = Console.ReadLine();
            excelapp.Workbooks.OpenText(
  @name_path,
  Excel.XlPlatform.xlWindows,
  1,            //С первой строки
  Excel.XlTextParsingType.xlDelimited, //Текст с разделителями
  Excel.XlTextQualifier.xlTextQualifierDoubleQuote, //Признак окончания разбора строки
  true,          //Разделители одинарные
  false,          //Разделители :Tab
  false,         //Semicolon
  false,         //Comma
  true,         //Space
  false,         //Other
  Type.Missing,  //OtherChar
  new object[] {new object[]{1,Excel.XlColumnDataType.xlSkipColumn},
                new object[]{2,Excel.XlColumnDataType.xlGeneralFormat},
                new object[]{2,Excel.XlColumnDataType.xlMDYFormat},
                new object[]{3,Excel.XlColumnDataType.xlMYDFormat},
                new object[]{4,Excel.XlColumnDataType.xlTextFormat},
                new object[]{5,Excel.XlColumnDataType.xlTextFormat}},
  Type.Missing,  //Размещение текста
  ".",           //Разделитель десятичных разрядов
 ",");           //Разделитель тысяч
            
        }
    }
}
