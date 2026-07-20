using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.ItemBrowser;
using TavernModList.Content.Players;

namespace TavernModList.Content.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class IronSuitHelmet : ModItem, IObtainedFrom
	{
		public string ObtainedFromDescription => "Crafted at the Lunar Crafting Station after defeating Moon Lord";

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.defense = 15;
			Item.value = Item.sellPrice(gold: 20);
			Item.rare = ItemRarityID.Red;
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

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<IronSuitBodyArmor>() && legs.type == ModContent.ItemType<IronSuitLeggings>();
		}

		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Ranged) += 0.10f;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Grants flight, +15% ranged damage, and unlocks the suit's blaster and unibeam";
			player.GetDamage(DamageClass.Ranged) += 0.15f;
			player.GetModPlayer<IronSuitPlayer>().fullSetActive = true;
		}
	}
}
