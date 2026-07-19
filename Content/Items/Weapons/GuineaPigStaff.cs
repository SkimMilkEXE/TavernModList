using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Buffs;
using TavernModList.Content.Projectiles;

namespace TavernModList.Content.Items.Weapons
{
	public class GuineaPigStaff : ModItem
	{
		public override void SetDefaults()
		{
			// Stats match the Spider Staff, vanilla's first post-Wall of Flesh summon weapon.
			Item.damage = 17;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 10;
			Item.width = 20;
			Item.height = 20;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 1.5f;
			Item.value = Item.sellPrice(gold: 3);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item44;
			Item.noUseGraphic = true;

			// buffType/shoot link the item to its minion buff and minion projectile.
			Item.buffType = ModContent.BuffType<GuineaPigBuff>();
			Item.shoot = ModContent.ProjectileType<GuineaPigMinion>();
			Item.shootSpeed = 10f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			return false;
		}
	}
}
