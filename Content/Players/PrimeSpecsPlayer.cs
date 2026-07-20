using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TavernModList.Content.Players
{
	public class PrimeSpecsPlayer : ModPlayer
	{
		public bool scanActive;

		public override void ResetEffects()
		{
			scanActive = false;
		}

		public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
		{
			ScanText(target);
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
		{
			ScanText(target);
		}

		private void ScanText(NPC target)
		{
			if (!scanActive)
			{
				return;
			}

			string text = $"{target.FullName}\n{target.life}/{target.lifeMax} HP  {target.defense} DEF";
			CombatText.NewText(target.getRect(), Color.Cyan, text);
		}
	}
}
