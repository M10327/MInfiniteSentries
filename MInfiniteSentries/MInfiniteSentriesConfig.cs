using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MInfiniteSentries
{
    public class MInfiniteSentriesConfig : IRocketPluginConfiguration
    {
        public bool UseForAllSentries;
        public bool AllowAllMags;
        public bool AllowAllGuns;
        public List<ulong> UserList;
        public List<ushort> GunWhitelist;
        public List<ushort> MagWhitelist;
        
        public void LoadDefaults()
        {
            UseForAllSentries = false;
            AllowAllMags = true;
            AllowAllGuns = true;
            UserList = new List<ulong>() { 76561198167303824 };
            GunWhitelist = new List<ushort>() { 363 };
            MagWhitelist = new List<ushort>() { 6 };
        }
    }
}
