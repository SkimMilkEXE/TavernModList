using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.ItemBrowser;

namespace TavernModList.Content.Items.Misc
{
    // The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.TavernModList.hjson' file.
    public class GoldenFruit : ModItem, IObtainedFrom
    {
        public string ObtainedFromDescription => "~0.4% chance when chopping down a tree";

        public override void SetDefaults()
        {
			Item.width = 20;
			Item.height = 20;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.sellPrice(platinum: 10);
			Item.rare = ItemRarityID.Red;
			Item.autoReuse = true;
			Item.maxStack = 999;
        }
    }
}