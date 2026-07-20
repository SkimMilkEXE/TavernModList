using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Buffs;
using TavernModList.Content.ItemBrowser;

namespace TavernModList.Content.Items.Potions
{
	public class HeavenlyBrew : ModItem, IObtainedFrom
	{
		public string ObtainedFromDescription => "~14% drop from Empress of Light";

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 26;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.useAnimation = 17;
			Item.useTime = 17;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.potion = true;
			Item.value = Item.sellPrice(gold: 15);
			Item.rare = ItemRarityID.Pink;
			Item.buffType = ModContent.BuffType<HeavenlyBrewBuff>();
			Item.buffTime = HeavenlyBrewBuff.TotalDuration;
		}

		public override bool? UseItem(Player player)
		{
			int healAmount = (int)(player.statLifeMax2 * 0.5f);
			player.statLife = Math.Min(player.statLifeMax2, player.statLife + healAmount);
			player.HealEffect(healAmount);
			return true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Daybloom, 1)
				.AddIngredient(ItemID.Bone, 5)
				.AddIngredient(ItemID.SoulofMight, 1)
				.AddIngredient(ItemID.BottledWater)
				.AddTile(TileID.AlchemyTable)
				.Register();
		}
	}
}
