using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Buffs;

namespace TavernModList.Content.Items.Potions
{
	public class PrimesPotion : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 26;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.value = Item.sellPrice(gold: 50);
			Item.rare = ItemRarityID.Rainbow;
			Item.buffType = ModContent.BuffType<PrimesPotionBuff>();
			Item.buffTime = 18000; // 5 minutes
		}
	}
}
