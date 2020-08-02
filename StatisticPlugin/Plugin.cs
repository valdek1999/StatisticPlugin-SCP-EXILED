using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Exiled.API.Extensions;
using Exiled.API.Interfaces;
using Exiled.Events;
using Handlers = Exiled.Events.Handlers;
using System.IO;

namespace StatisticPlugin
{
    public class Plugin:Plugin<Config>
    {
		public override string Author { get; } = "HardFoxy";
		public override string Name { get; } = "Statistic about players";
		public override string Prefix { get; } = "stat_about_players";
		public override Version Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;
		public override Version RequiredExiledVersion { get; } = new Version(2, 0, 7);

		public string name;
		public EventHandlers EventHandlers;
		public string text = "kek\nprivet\nkak dela 1";

		public Dictionary<string, string> list { get; set; } = new Dictionary<string, string>();
		public override void OnEnabled()
		{
			
			try
			{
				string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				string pluginPath = Path.Combine(appData, "Plugins");
				string path = Path.Combine(pluginPath, "StatPlayers");
				string name = this.Config.FullPath;
				if (!File.Exists(name))
					File.Create(name).Close();
				using (StreamReader sr = new StreamReader(name, System.Text.Encoding.Default))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						string[] temp = line.Split(' ');
						string info = "";
						for (int i = 1; i< temp.Length; i ++)
						{
							info += temp[i];
							if (temp.Length - 1 == i)
								break;
							info += " ";
							
						}	
						list.Add(temp[0],info);
					}
				}
				//InfoPlayers();
				EventHandlers = new EventHandlers(this);
				base.OnEnabled();
				RegisterEvents();
			}
			catch (Exception e)
			{
				Log.Error($"Loading error: {e}");
			}
		}
		internal void RegisterEvents()
		{
			Handlers.Player.Spawning += EventHandlers.FullPlayerInfo1;
			Handlers.Player.Joined += EventHandlers.WriteInfo;
			Handlers.Player.Left += EventHandlers.WriteInfoLeft;
			Handlers.Server.RoundEnded += EventHandlers.RoundStart;
			Handlers.Server.RoundStarted += EventHandlers.RoundStart1;
		}
		public override void OnDisabled()
		{
			
		}
		public void InfoPlayers()
		{
			foreach(var t in list)
			{
				Log.Info(t.Key + " " + t.Value);
			}
		}

		public override void OnReloaded()
		{

		}
	}
}
