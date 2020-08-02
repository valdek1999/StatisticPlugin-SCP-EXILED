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

		public EventHandlers EventHandlers;
		public override void OnEnabled()
		{
			
			try
			{
				Dictionary<string, string> list_play = new Dictionary<string, string>();
				list_play = new Dictionary<string, string>();
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
						list_play.Add(temp[0],info);
					}
				}
				//InfoPlayers(list_play);
				EventHandlers = new EventHandlers(this, list_play);
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
		public void InfoPlayers(Dictionary<string, string> list_play)
		{
			foreach(var t in list_play)
			{
				Log.Info(t.Key + " " + t.Value);
			}
		}

		public override void OnReloaded()
		{

		}
	}
}
