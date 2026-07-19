using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Items.Weapons;

namespace TavernModList.Content.GlobalProjectiles
{
	public class IronSuitUnibeamColor : GlobalProjectile
	{
		public override bool InstancePerEntity => true;

		private bool isUnibeamBeam;

		public override void OnSpawn(Projectile projectile, IEntitySource source)
		{
			if (projectile.type == ProjectileID.LastPrismLaser
				&& source is EntitySource_ItemUse_WithAmmo itemSource
				&& itemSource.Item.type == ModContent.ItemType<IronSuitUnibeam>())
			{
				isUnibeamBeam = true;
			}
		}

		public override bool PreDraw(Projectile projectile, ref Color lightColor)
		{
			if (isUnibeamBeam)
			{
				lightColor = Color.Blue;
			}

			return true;
		}
	}
}
