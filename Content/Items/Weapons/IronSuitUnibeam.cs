using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Players;

namespace TavernModList.Content.Items.Weapons
{
	public class IronSuitUnibeam : ModItem
	{
		public override void SetDefaults()
		{
			// Continuous channeled beam, 5 beams per shot fanned out like the Last Prism. Per-beam
			// damage is lower than the single-beam version since 5 now fire each trigger.
			Item.damage = 8;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 2;
			Item.useAnimation = 2;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.knockBack = 1f;
			Item.value = Item.sellPrice(gold: 30);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item12;
			Item.channel = true;
			Item.autoReuse = true;

			Item.shoot = ProjectileID.LastPrismLaser;
			Item.shootSpeed = 20f;
		}

		public override bool CanUseItem(Player player)
		{
			return player.GetModPlayer<IronSuitPlayer>().fullSetActive;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// 5 beams fanned out, sweeping back and forth over time - same feel as the vanilla Last Prism.
			float sweep = (float)System.Math.Sin(Main.GameUpdateCount * 0.05f) * 0.15f;

			for (int i = 0; i < 5; i++)
			{
				float spread = MathHelper.Lerp(-0.3f, 0.3f, i / 4f) + sweep;
				Vector2 beamVelocity = velocity.RotatedBy(spread);
				Projectile.NewProjectile(source, position, beamVelocity, type, damage, knockback, player.whoAmI);
			}

			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddIngredient(ItemID.FragmentSolar, 1);
			recipe.AddIngredient(ItemID.FragmentVortex, 1);
			recipe.AddIngredient(ItemID.FragmentNebula, 1);
			recipe.AddIngredient(ItemID.FragmentStardust, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}
