using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TavernModList.Content.Items.Weapons
{
	public class RemoteDetonator : ModItem
	{
		public override void SetDefaults()
		{
			// Stats in line with the Proximity Mine Launcher, the Cyborg's own post-Plantera explosive launcher.
			Item.damage = 50;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 20;
			Item.useTime = 34;
			Item.useAnimation = 34;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 6.5f;
			Item.value = Item.sellPrice(gold: 15);
			Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;

			Item.shoot = ProjectileID.RocketI;
			Item.shootSpeed = 6f;
			Item.useAmmo = AmmoID.Rocket;
		}
	}
}
