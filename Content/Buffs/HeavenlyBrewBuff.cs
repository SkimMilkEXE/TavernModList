using Terraria;
using Terraria.ModLoader;

namespace TavernModList.Content.Buffs
{
	public class HeavenlyBrewBuff : ModBuff
	{
		public const int TotalDuration = 10800; // 3 minutes
		public const int InvulnDuration = 180; // 3 seconds

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.buffTime[buffIndex] > TotalDuration - InvulnDuration)
			{
				player.immune = true;
				player.immuneTime = 10;
			}
			else
			{
				player.moveSpeed += 0.2f;
				player.statDefense += 10;
			}
		}
	}
}
