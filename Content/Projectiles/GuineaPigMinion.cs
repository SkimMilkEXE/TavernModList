using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Buffs;

namespace TavernModList.Content.Projectiles
{
	public class GuineaPigMinion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1f;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.netImportant = true;
		}

		// Ticks down between rainbow shots; only the owner's client fires.
		private int attackCooldown;

		public override void AI()
		{
			Player owner = Main.player[Projectile.owner];

			if (owner.dead || !owner.HasBuff(ModContent.BuffType<GuineaPigBuff>()))
			{
				owner.ClearBuff(ModContent.BuffType<GuineaPigBuff>());
			}
			else
			{
				Projectile.timeLeft = 2;
			}

			const float attackRange = 500f;
			NPC target = Main.npc
				.Where(npc => npc.active && !npc.friendly && !npc.dontTakeDamage && npc.chaseable
					&& Vector2.Distance(npc.Center, Projectile.Center) < attackRange)
				.OrderBy(npc => Vector2.Distance(npc.Center, Projectile.Center))
				.FirstOrDefault();

			Vector2 destination = target != null ? target.Center : owner.Center + new Vector2(-30 * owner.direction, -10);
			Vector2 toDestination = destination - Projectile.Center;
			float speed = target != null ? 12f : 8f;

			if (toDestination.Length() > 40f)
			{
				toDestination.Normalize();
				Projectile.velocity = Vector2.Lerp(Projectile.velocity, toDestination * speed, 0.1f);
			}
			else
			{
				Projectile.velocity *= 0.9f;
			}

			Projectile.rotation = Projectile.velocity.X * 0.05f;
			Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;

			if (attackCooldown > 0)
			{
				attackCooldown--;
			}
			else if (target != null)
			{
				if (Projectile.owner == Main.myPlayer)
				{
					Vector2 shootVelocity = (target.Center - Projectile.Center).SafeNormalize(Vector2.UnitY) * 10f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, shootVelocity, ProjectileID.RainbowRodBullet, Projectile.damage, Projectile.knockBack, Projectile.owner);
				}

				attackCooldown = 40;
			}
		}
	}
}
