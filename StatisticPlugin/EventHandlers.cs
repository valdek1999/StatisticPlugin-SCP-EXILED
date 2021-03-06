﻿using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace StatisticPlugin
{
    public class EventHandlers
    {
        public readonly Plugin plugin;

        public bool server_restart = false;

        Dictionary<string, string> list_play;
        public EventHandlers(Plugin plugin, Dictionary<string, string> list_play)
        {
            this.plugin = plugin;
            this.list_play = list_play;
            
        }

        internal void FullPlayerInfo1(SpawningEventArgs ev)
        {
            Exiled.API.Features.Player player = ev.Player;

            //Log.Info("Abilities: " + player.Abilities.ToString());
            //Log.Info("AdrenalineHealth" + player.AdrenalineHealth.ToString());
            //Log.Info("AuthenticationToken: " + player.AuthenticationToken.ToString());
            //Log.Info("AuthenticationType" + player.AuthenticationType.ToString());
            //Log.Info("BadgeHidden: " + player.BadgeHidden.ToString());
            //Log.Info("CufferId" + player.CufferId.ToString());
            //Log.Info("CurrentItem: " + player.CurrentItem.ToString());
            //Log.Info("CurrentRoom.Name" + player.CurrentRoom.Name.ToString());  
            //Log.Info("Energy" + player.Energy.ToString());
            //Log.Info("Experience: " + player.Experience.ToString());
            //Log.Info("GameObject.name" + player.GameObject.name.ToString());  
            //if (player.GroupName != null)
            //    Log.Info("GroupName" + player.GroupName);
            //Log.Info("Health" + player.Health.ToString());
            //Log.Info("Id" + player.Id.ToString());
            //Log.Info("Inventory: " + player.Inventory.ToString());
            //Log.Info("IPAddress" + player.IPAddress.ToString());
            //if (player.IsBypassModeEnabled != null)
            //    Log.Info("IsBypassModeEnabled" + player.IsBypassModeEnabled.ToString());
            //Log.Info("IsCuffed: " + player.IsCuffed.ToString());
            //Log.Info("IsDead" + player.IsDead.ToString());
            //Log.Info("IsFriendlyFireEnabled." + player.IsFriendlyFireEnabled.ToString());
            //Log.Info("IsGodModeEnabled" + player.IsGodModeEnabled.ToString());
            //Log.Info("IsHost:  " + player.IsHost.ToString());
            //Log.Info("IsIntercomMuted" + player.IsIntercomMuted.ToString());
            //Log.Info("IsInvisible" + player.IsInvisible.ToString());
            //Log.Info("IsMuted: " + player.IsMuted.ToString());
            //Log.Info("IsNTF" + player.IsNTF.ToString());
            //Log.Info("IsOverwatchEnabled" + player.IsOverwatchEnabled.ToString());
            ////
            //Log.Info("IsReloading: " + player.IsReloading.ToString());
            //Log.Info("IsStaffBypassEnabled" + player.IsStaffBypassEnabled.ToString());
            //Log.Info("IsZooming" + player.IsZooming.ToString());
            //if (player.Level != null)
            //    Log.Info("Level: " + player.Level.ToString());
            //if (player.LockedDoors.ToString() != null)
            //    Log.Info("LockedDoors" + player.LockedDoors.ToString());
            //Log.Info("MaxAdrenalineHealth" + player.MaxAdrenalineHealth.ToString());
            //Log.Info("MaxEnergy: " + player.MaxEnergy.ToString());
            //Log.Info("MaxHealth" + player.MaxHealth.ToString());
            //Log.Info("Nickname" + player.Nickname.ToString());
            //Log.Info("NoClipEnabled: " + player.NoClipEnabled.ToString());
            //if (player.PlayerCamera.ToString() != null)
            //    Log.Info("PlayerCamera" + player.PlayerCamera.ToString());
            //if (player.Position.ToString() != null)
            //    Log.Info("Position" + player.Position.ToString());
            //Log.Info("RankColor" + player.RankColor.ToString());
            //Log.Info("RankName: " + player.RankName.ToString());
            //if (player.ReferenceHub.serverRoles.name.ToString() != null)
            //    Log.Info("ReferenceHub serverRoles name" + player.ReferenceHub.serverRoles.name.ToString());
            //Log.Info("Role" + player.Role.ToString());
            //Log.Info("RoleColor: " + player.RoleColor.ToString());
           
            //Log.Info("Side" + player.Side.ToString());
            ////
            //if(player.Team.ToString()!=null)
            //Log.Info("Team" + player.Team.ToString());
            //Log.Info("UserId: " + player.UserId.ToString());  
        }

        internal void RoundStart1()
        {
            //plugin.InfoPlayers(list_play);
            string text = "";
            bool first = true;
            foreach(var t in list_play)
            {
                if(first)
                {
                    text += " "+t.Key + " " + t.Value;
                    first = false;
                    continue;
                }
                text += "\n" + " " + t.Key + " " + t.Value;
            }
            using (StreamWriter sw = new StreamWriter(plugin.Config.FullPath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(text);
            }
        }

        internal void RoundStart(RoundEndedEventArgs ev)
        {

            IEnumerable<Exiled.API.Features.Player> p = Exiled.API.Features.Player.List;
            foreach(var t in p)
            {
                Exiled.API.Features.Player player = t;
                string key = player.UserId;
                key = key.Replace("@steam", "");
                key = "https://steamcommunity.com/profiles/" + key;
                string data = DataString(key, player, true);
                string Nickname = player.Nickname.Replace(' ', '_');
                string GroupName = player.GroupName;
                if (GroupName == "" || GroupName == " " || GroupName == null)
                    player.GroupName = "none";
                string info = data + " " + player.IPAddress + " " + Nickname + " " + GroupName;
                if (list_play.ContainsKey(key))
                {
                    list_play.Remove(key);
                    list_play.Add(key, info);
                }
                //plugin.InfoPlayers();
            }    
        }

        internal void WriteInfoLeft(LeftEventArgs ev)
        {
            Exiled.API.Features.Player player = ev.Player;
            string key = player.UserId;
            key = key.Replace("@steam", "");
            key = "https://steamcommunity.com/profiles/" + key;
            string data = DataString(key, player, true);
            string Nickname = player.Nickname.Replace(' ', '_');
            string GroupName = player.GroupName;
            if (GroupName == "" || GroupName == " " || GroupName==null)
                player.GroupName = "none";
            string info = data + " " + player.IPAddress + " " + Nickname + " " + GroupName;
            if (list_play.ContainsKey(key))
            {
                list_play.Remove(key);
                list_play.Add(key, info);
            }
            //plugin.InfoPlayers();
        }

        internal String DataString(string key , Exiled.API.Features.Player player, bool disconnect)
        {
            string temp="";
            if(list_play.TryGetValue(key,out temp))
            {
                string[] info = temp.Split(' ');
                DateTime time = DateTime.Parse(info[1]+" "+info[2]);
                int minute_sesion = Convert.ToInt32(info[0]);
                if (disconnect)
                    minute_sesion += Convert.ToInt32((new TimeSpan(DateTime.Now.Ticks - time.Ticks)).TotalMinutes);
                return minute_sesion + " " + DateTime.Now.ToString();
            }
            
            return 0 + " " + DateTime.Now.ToString();
        }
        internal void WriteInfo(JoinedEventArgs ev)
        {
            Exiled.API.Features.Player player = ev.Player;
            string key = player.UserId;
            key = key.Replace("@steam", "");
            key = "https://steamcommunity.com/profiles/" + key;
            string data = DataString(key, player,false);
            string Nickname = player.Nickname.Replace(' ', '_');
            string GroupName = player.GroupName;
            if (GroupName == "" || GroupName == " " || GroupName == null)
                player.GroupName = "none";
            string info = data + " " + player.IPAddress + " " + Nickname + " " + GroupName;

            if (!list_play.ContainsKey(key))
            {
                list_play.Add(key, info);
            }
            else
            {
                list_play.Remove(key);
                list_play.Add(key, info);
            }
            //plugin.InfoPlayers();
        }

        
    }
}
