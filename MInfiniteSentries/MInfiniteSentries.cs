using Rocket.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MInfiniteSentries
{
    public class MInfiniteSentries : RocketPlugin<MInfiniteSentriesConfig>
    {
        public static MInfiniteSentries Instance { get; private set; }
        protected override void Load()
        {
            Instance = this;
            Patches.PatchAll();
        }

        protected override void Unload()
        {
            Patches.UnpatchAll();
        }
    }
}
