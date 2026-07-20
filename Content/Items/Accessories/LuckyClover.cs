using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TavernModList.Content.Items.Accessories
{
	public class LuckyClover : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.LightRed;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.fishingSkill += 15;
		}
	}
}
