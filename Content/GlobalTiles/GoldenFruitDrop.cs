using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TavernModList.Content.Items.Misc;

namespace TavernModList.Content.GlobalTiles
{
	public class GoldenFruitDrop : GlobalTile
	{
		public override void Drop(int i, int j, int type)
		{
			// Fires on every tile break; type check limits the drop to trees.
			// NextBool(100) is a 1-in-100 chance per tree tile broken.
			if (type == TileID.Trees && Main.rand.NextBool(100))
			{
				Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<GoldenFruit>());
			}
		}
	}
}
