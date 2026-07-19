using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Items.Weapons;

namespace TavernModList.Content.GlobalProjectiles
{
	public class IronSuitBlasterRicochet : GlobalProjectile
	{
		public override bool InstancePerEntity => true;

		private const int MaxRicochets = 3;
		private const float RicochetRange = 400f;

		private bool isBlasterBolt;
		private readonly List<int> alreadyHit = new();

		public override void OnSpawn(Projectile projectile, IEntitySource source)
		{
			if (projectile.type == ProjectileID.LaserMachinegunLaser
				&& source is EntitySource_ItemUse_WithAmmo itemSource
				&& itemSource.Item.type == ModContent.ItemType<IronSuitBlaster>())
			{
				isBlasterBolt = true;
				projectile.penetrate = MaxRicochets + 1;
			}
		}

		public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (!isBlasterBolt)
			{
				return;
			}

			alreadyHit.Add(target.whoAmI);

			NPC next = Main.npc
				.Where(npc => npc.active && !npc.friendly && !npc.dontTakeDamage && npc.chaseable
					&& !alreadyHit.Contains(npc.whoAmI)
					&& Vector2.Distance(npc.Center, projectile.Center) < RicochetRange)
				.OrderBy(npc => Vector2.Distance(npc.Center, projectile.Center))
				.FirstOrDefault();

			if (next != null)
			{
				projectile.velocity = (next.Center - projectile.Center).SafeNormalize(Vector2.UnitY) * projectile.velocity.Length();
			}
		}
	}
}
