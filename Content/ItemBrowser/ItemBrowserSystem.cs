using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace TavernModList.Content.ItemBrowser
{
	public class ItemBrowserSystem : ModSystem
	{
		public static ModKeybind ToggleKeybind { get; private set; }

		private UserInterface _userInterface;
		private ItemBrowserState _state;
		private bool _visible;

		public override void Load()
		{
			if (Main.dedServ)
			{
				return;
			}

			ToggleKeybind = KeybindLoader.RegisterKeybind(Mod, "Toggle Item Browser", Keys.OemTilde);
			_userInterface = new UserInterface();
			_state = new ItemBrowserState(Mod);
		}

		public override void Unload()
		{
			ToggleKeybind = null;
			_userInterface = null;
			_state = null;
		}

		public static void ToggleVisibility()
		{
			ItemBrowserSystem system = ModContent.GetInstance<ItemBrowserSystem>();
			system._visible = !system._visible;
			system._userInterface.SetState(system._visible ? system._state : null);
		}

		public override void UpdateUI(GameTime gameTime)
		{
			_userInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseTextIndex != -1)
			{
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"TavernModList: Item Browser",
					delegate
					{
						_userInterface?.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI));
			}
		}
	}
}
