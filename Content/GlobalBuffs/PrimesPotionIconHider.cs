using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TavernModList.Content.Buffs;

namespace TavernModList.Content.GlobalBuffs
{
	public class PrimesPotionIconHider : GlobalBuff
	{
		public override bool PreDraw(SpriteBatch spriteBatch, int type, int buffIndex, ref BuffDrawParams drawParams)
		{
			int potionType = ModContent.BuffType<PrimesPotionBuff>();

			// Debuffs still show; Prime's Potion only grants non-debuffs, so those are the ones to hide.
			if (type != potionType && !Main.debuff[type] && Main.LocalPlayer.HasBuff(potionType))
			{
				return false;
			}

			return true;
		}
	}
}
