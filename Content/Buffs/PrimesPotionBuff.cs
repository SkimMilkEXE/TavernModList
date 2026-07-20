using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TavernModList.Content.Buffs
{
	public class PrimesPotionBuff : ModBuff
	{
		public override void Update(Player player, ref int buffIndex)
		{
			// Refresh every non-debuff buff each tick so all their effects apply while
			// only this buff's icon is visible (icons hidden in PrimesPotionIconHider).
			for (int type = 1; type < BuffID.Count; type++)
			{
				if (type != Type && !Main.debuff[type])
				{
					player.AddBuff(type, 2);
				}
			}
		}
	}
}
