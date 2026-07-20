using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TavernModList.Content.Items.Accessories;

namespace TavernModList.Content.Players
{
	public class LuckyCloverFishingPlayer : ModPlayer
	{
		public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
		{
			bool inWater = !attempt.inLava && !attempt.inHoney;

			// Uncommon catch, Hallow water only.
			if (inWater && Player.ZoneHallow && attempt.rare && Main.rand.NextBool(4))
			{
				itemDrop = ModContent.ItemType<LuckyClover>();
			}
		}
	}
}
