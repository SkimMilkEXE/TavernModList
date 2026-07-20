using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.ItemBrowser;

namespace TavernModList.Content.Items.Accessories
{
	public class LuckyClover : ModItem, IObtainedFrom
	{
		public string ObtainedFromDescription => "Rare fishing catch in Hallow water";

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
