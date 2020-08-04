using System;
using System.Dynamic;
using Excel = Microsoft.Office.Interop.Excel;
namespace ExcelParser
{
    class Program
    {
        private static Excel.Application excelapp;
       
        static void Main(string[] args)
        {
            string name;
            Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
            Console.WriteLine("Введите путь файла .txt : ");
            name = Console.ReadLine();
            //excelapp.Workbooks.OpenText(
            //name,
            //Excel.XlPlatform.xlWindows,
            //1,            //С первой строки
            //Excel.XlTextParsingType.xlDelimited, //Текст с разделителями
            //Excel.XlTextQualifier.xlTextQualifierDoubleQuote, //Признак окончания разбора строки
            //true,          //Разделители одинарные
            //true,          //Разделители :Tab
            //false,         //Semicolon
            //false,         //Comma
            //false,         //Space
            //false,         //Other
            //Type.Missing,  //OtherChar
            //new object[] {new object[]{1,Excel.XlColumnDataType.xlSkipColumn},
            //    new object[]{2,Excel.XlColumnDataType.xlGeneralFormat},
            //    new object[]{2,Excel.XlColumnDataType.xlMDYFormat},
            //    new object[]{3,Excel.XlColumnDataType.xlMYDFormat},
            //    new object[]{4,Excel.XlColumnDataType.xlTextFormat},
            //    new object[]{5,Excel.XlColumnDataType.xlTextFormat}},
            //Type.Missing,  //Размещение текста
            //".",           //Разделитель десятичных разрядов
            //",");       //Разделитель тысяч
        }
    }
}
