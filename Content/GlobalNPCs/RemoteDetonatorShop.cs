using Terraria.GameContent.Shops;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Items.Weapons;

namespace TavernModList.Content.GlobalNPCs
{
	public class RemoteDetonatorShop : GlobalNPC
	{
		public override void ModifyShop(NPCShop shop)
		{
			if (shop.NpcType == NPCID.Cyborg)
			{
				shop.Add(ModContent.ItemType<RemoteDetonator>());
			}
		}
	}
}
