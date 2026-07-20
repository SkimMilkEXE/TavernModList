using Terraria.GameInput;
using Terraria.ModLoader;
using TavernModList.Content.ItemBrowser;

namespace TavernModList.Content.Players
{
	public class ItemBrowserPlayer : ModPlayer
	{
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (ItemBrowserSystem.ToggleKeybind.JustPressed)
			{
				ItemBrowserSystem.ToggleVisibility();
			}
		}
	}
}
