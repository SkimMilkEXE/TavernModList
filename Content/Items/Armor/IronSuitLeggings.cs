using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.ItemBrowser;

namespace TavernModList.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class IronSuitLeggings : ModItem, IObtainedFrom
	{
		public string ObtainedFromDescription => "Crafted at the Lunar Crafting Station after defeating Moon Lord";

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.defense = 18;
			Item.value = Item.sellPrice(gold: 20);
			Item.rare = ItemRarityID.Red;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetAttackSpeed(DamageClass.Ranged) += 0.10f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddIngredient(ItemID.FragmentSolar, 5);
			recipe.AddIngredient(ItemID.FragmentVortex, 5);
			recipe.AddIngredient(ItemID.FragmentNebula, 5);
			recipe.AddIngredient(ItemID.FragmentStardust, 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}
