using Terraria;
using Terraria.ModLoader;
using TavernModList.Content.Projectiles;

namespace TavernModList.Content.Buffs
{
	public class GuineaPigBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			// Keep the buff alive as long as the minion is; drop it once the minion is gone.
			if (player.ownedProjectileCounts[ModContent.ProjectileType<GuineaPigMinion>()] > 0)
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}