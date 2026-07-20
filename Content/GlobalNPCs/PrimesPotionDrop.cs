using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Items.Potions;

namespace TavernModList.Content.GlobalNPCs
{
	public class PrimesPotionDrop : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (npc.type == NPCID.MoonLordCore)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PrimesPotion>(), 100));
			}
		}
	}
}
