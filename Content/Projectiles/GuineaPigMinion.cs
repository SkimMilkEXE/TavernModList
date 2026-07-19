using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Buffs;

namespace TavernModList.Content.Projectiles
{
	public class GuineaPigMinion : ModProjectile
	{
		// Add more file names here as you make more colors; each needs a matching .png next to this file.
		private static readonly string[] SkinNames = { "GuineaPigMinion", "GuineaPigMinion_Orange", "GuineaPigMinion_Tan",  "GuineaPigMinion_Tan", "GuineaPigMinion_Oreo", "GuineaPigMinion_Peanut"} ;
		private static Asset<Texture2D>[] skins;

		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 2; // 2 frames stacked vertically per skin png
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		}

		public override void Load()
		{
			if (Main.dedServ)
			{
				return;
			}

			skins = SkinNames.Select(name => ModContent.Request<Texture2D>($"{Mod.Name}/Content/Projectiles/{name}")).ToArray();
		}

		public override void OnSpawn(IEntitySource source)
		{
			// Picks the guinea pig's color once, at spawn, and keeps it for the minion's lifetime.
			Projectile.ai[0] = Main.rand.Next(SkinNames.Length);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture = skins[(int)Projectile.ai[0]].Value;
			int frameHeight = texture.Height / Main.projFrames[Projectile.type];
			Rectangle frame = new(0, Projectile.frame * frameHeight, texture.Width, frameHeight);
			SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

			Vector2 origin = new Vector2(frame.Width, frame.Height) / 2f;
			Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, frame, lightColor, Projectile.rotation, origin, Projectile.scale, effects, 0);

			return false;
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

			// Only animate while there's a target to attack; hold on frame 0 otherwise.
			if (target != null)
			{
				Projectile.frameCounter++;
				if (Projectile.frameCounter >= 5)
				{
					Projectile.frameCounter = 0;
					Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
				}
			}
			else
			{
				Projectile.frameCounter = 0;
				Projectile.frame = 0;
			}

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
