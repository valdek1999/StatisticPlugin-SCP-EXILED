using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Configuration;
using StatisticPlugin;
using Excel=Microsoft.Office.Interop.Excel;
namespace ExcelParserNet
{
	class Program
	{
		static void Main(string[] args)
		{
			Dictionary<string, string> list_play = new Dictionary<string, string>();
			list_play = new Dictionary<string, string>();
			string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			string pluginPath = Path.Combine(appData, "Plugins");
			string path = Path.Combine(pluginPath, "StatPlayers");
			string name = @"C:\Users\HardFoxy\AppData\Roaming\EXILED\StatPlayers\stat.txt";
			if (!File.Exists(name))
				File.Create(name).Close();
			using (StreamReader sr = new StreamReader(name, System.Text.Encoding.Default))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					string[] temp = line.Split(' ');
					string info = "";
					for (int i = 2; i < temp.Length; i++)
					{
						info += temp[i];
						if (temp.Length - 1 == i)
							break;
						info += " ";

					}
					list_play.Add(temp[1], info);
				}
			}

			using (var context = new MyDbContext())
			{
				string[] info;
				string key;
				foreach (var temp in list_play)
				{
					info = temp.Value.Split(' ');
					key = temp.Key.Replace("@steam", "");
					var Player = new Player()
					{
						NickName = info[4],
						Role = info[5],
						Ip = 0,
						SteamId = key
						, LastDataTime = DateTime.Now,
						Time = 0,
						
					};

					//все изменения перейдут в базу данных;
					var Character = new Character()
					{
						PlayerId = Player.Id,
						Expirience = 0,
						Lvl = 0,
						Player = Player
					};
					context.Players.Add(Player);
					context.Characters.Add(Character);
				}
				context.SaveChanges();
			}
				
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

