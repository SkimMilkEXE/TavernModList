using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Items.Potions;

namespace TavernModList.Content.GlobalNPCs
{
	public class HeavenlyBrewDrop : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (npc.type == NPCID.HallowBoss)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HeavenlyBrew>(), 7));
			}
		}
	}
}
