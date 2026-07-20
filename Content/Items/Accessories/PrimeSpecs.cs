using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Players;

namespace TavernModList.Content.Items.Accessories
{
	[AutoloadEquip(EquipType.Face)]
	public class PrimeSpecs : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 20);
			Item.rare = ItemRarityID.Yellow;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<PrimeSpecsPlayer>().scanActive = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HallowedBar, 8)
				.AddIngredient(ItemID.SoulofSight)
				.AddIngredient(ItemID.SoulofMight)
				.AddIngredient(ItemID.SoulofFright)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
