using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.ItemBrowser;

namespace TavernModList.Content.Items.Weapons
{
	public class BloodyShell : ModItem, IObtainedFrom
	{
		public string ObtainedFromDescription => "~25% drop from the Dreadnautilus (Blood Moon fishing catch)";

		public override void SetDefaults()
		{
			// Stats aimed at the Dreadnautilus's own drops (Blood Butcherer, Flesh Knuckles) - early Hardmode tier.
			Item.damage = 10;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4f;
			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;

			Item.shoot = ProjectileID.Bullet;
			Item.shootSpeed = 11f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// Shotgun-style spread of 5 pellets, matching the "shell" name.
			for (int i = 0; i < 5; i++)
			{
				float spread = MathHelper.Lerp(-0.25f, 0.25f, i / 4f);
				Vector2 pelletVelocity = velocity.RotatedBy(spread) * Main.rand.NextFloat(0.9f, 1.1f);
				Projectile.NewProjectile(source, position, pelletVelocity, type, damage, knockback, player.whoAmI);
			}

			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(position, 4, 4, DustID.Blood, velocity.X * 0.4f, velocity.Y * 0.4f);
			}

			return false;
		}
	}
}
