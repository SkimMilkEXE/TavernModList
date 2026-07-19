using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Items.Weapons;

namespace TavernModList.Content.GlobalNPCs
{
	public class GuineaPigStaffDrop : GlobalNPC
	{
		// Dungeon enemies. Main.hardMode gates the drop to after Wall of Flesh is defeated.
		private static readonly HashSet<int> DungeonPool = new()
		{
			NPCID.CursedSkull,
			NPCID.DarkCaster,
			NPCID.RaggedCaster,
			NPCID.Necromancer,
			NPCID.GiantCursedSkull,
		};

		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (Main.hardMode && DungeonPool.Contains(npc.type))
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GuineaPigStaff>(), 35));
			}
		}
	}
}