using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Buffs;
using TavernModList.Content.ItemBrowser;

namespace TavernModList.Content.Items.Potions
{
	public class PrimesPotion : ModItem, IObtainedFrom
	{
		public string ObtainedFromDescription => "1% drop from Moon Lord";

		public override void SetStaticDefaults()
		{
			// PrimesPotion.png is a vertical spritesheet: 8 frames, 12x26 each.
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 8, false));
		}

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 26;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.value = Item.sellPrice(gold: 50);
			Item.rare = ItemRarityID.Purple;
			Item.buffType = ModContent.BuffType<PrimesPotionBuff>();
			Item.buffTime = 18000; // 5 minutes
		}
	}
}
