using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Players;

namespace TavernModList.Content.Items.Weapons
{
	public class IronSuitBlaster : ModItem
	{
		public override void SetDefaults()
		{
			// Fast, ammoless repeater - the suit's built-in laser, not a held gun.
			Item.damage = 60;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(gold: 15);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item12;
			Item.autoReuse = true;

			Item.shoot = ProjectileID.LaserMachinegunLaser;
			Item.shootSpeed = 16f;
		}

		public override bool CanUseItem(Player player)
		{
			return player.GetModPlayer<IronSuitPlayer>().fullSetActive;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LunarBar, 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}
