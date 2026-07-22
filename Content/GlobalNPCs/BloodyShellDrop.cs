using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Items.Weapons;

namespace TavernModList.Content.GlobalNPCs
{
	public class BloodyShellDrop : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (npc.type == NPCID.BloodNautilus)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BloodyShell>(), 4));
			}
		}
	}
}
