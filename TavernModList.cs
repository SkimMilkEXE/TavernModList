using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace TavernModList
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class TavernModList : Mod
	{
		// Prime's Potion refreshes every non-debuff buff at once; vanilla's 44 slots aren't enough to hold them all.
		public override uint ExtraPlayerBuffSlots => 300;
	}
}
