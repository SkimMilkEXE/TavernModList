using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TavernModList.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class IronSuitBodyArmor : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.defense = 24;
			Item.value = Item.sellPrice(gold: 25);
			Item.rare = ItemRarityID.Red;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Ranged) += 0.10f;
			player.GetCritChance(DamageClass.Ranged) += 5;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LunarBar, 12);
			recipe.AddIngredient(ItemID.FragmentSolar, 5);
			recipe.AddIngredient(ItemID.FragmentVortex, 5);
			recipe.AddIngredient(ItemID.FragmentNebula, 5);
			recipe.AddIngredient(ItemID.FragmentStardust, 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}
