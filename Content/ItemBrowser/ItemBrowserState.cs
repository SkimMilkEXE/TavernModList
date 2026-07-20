using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace TavernModList.Content.ItemBrowser
{
	public class ItemBrowserState : UIState
	{
		private readonly Mod _mod;
		private readonly UIList _list = new();
		private readonly SearchBox _searchBox = new("Search items...");
		private List<ModItem> _allItems = new();

		public ItemBrowserState(Mod mod)
		{
			_mod = mod;
		}

		public override void OnInitialize()
		{
			UIPanel panel = new()
			{
				Width = { Pixels = 320 },
				Height = { Pixels = 420 },
				HAlign = 0.5f,
				VAlign = 0.5f,
			};
			Append(panel);

			UIText title = new("Item Browser")
			{
				HAlign = 0.5f,
			};
			title.Top.Set(-30f, 0f);
			panel.Append(title);

			_searchBox.Width.Set(-24f, 1f);
			_searchBox.Height.Set(30f, 0f);
			_searchBox.Top.Set(0f, 0f);
			_searchBox.OnTextChange += (_, _) => Filter();
			panel.Append(_searchBox);

			_list.Width.Set(-25f, 1f);
			_list.Height.Set(-40f, 1f);
			_list.Top.Set(40f, 0f);
			_list.ListPadding = 4f;
			panel.Append(_list);

			UIScrollbar scrollbar = new();
			scrollbar.Height.Set(-40f, 1f);
			scrollbar.Top.Set(40f, 0f);
			scrollbar.HAlign = 1f;
			panel.Append(scrollbar);
			_list.SetScrollbar(scrollbar);

			_allItems = _mod.GetContent<ModItem>().OrderBy(item => item.Item.Name).ToList();
			Filter();
		}

		private void Filter()
		{
			string query = _searchBox.Text;
			_list.Clear();
			foreach (ModItem item in _allItems)
			{
				if (query.Length == 0 || item.DisplayName.Value.Contains(query, StringComparison.OrdinalIgnoreCase))
				{
					_list.Add(new ItemEntryElement(item.Item.type, item.DisplayName.Value));
				}
			}
		}

		// Custom text box: the vanilla UIFocusInputTextField class isn't accessible outside tModLoader's own code.
		private class SearchBox : UIElement
		{
			public string Text { get; private set; } = string.Empty;

			private readonly string _hint;
			private bool _focused;

			public event EventHandler OnTextChange;

			public SearchBox(string hint)
			{
				_hint = hint;
			}

			public override void LeftClick(UIMouseEvent evt)
			{
				_focused = true;
				base.LeftClick(evt);
			}

			public override void Update(GameTime gameTime)
			{
				base.Update(gameTime);

				if (_focused && Main.mouseLeft && !ContainsPoint(Main.MouseScreen))
				{
					_focused = false;
				}

				if (_focused)
				{
					string updated = Main.GetInputText(Text, false);
					if (updated != Text)
					{
						Text = updated;
						OnTextChange?.Invoke(this, EventArgs.Empty);
					}

					PlayerInput.WritingText = true;
					Main.instance.HandleIME();
				}
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dims = GetDimensions();
				Utils.DrawInvBG(spriteBatch, dims.ToRectangle(), Color.Black * 0.6f);

				bool empty = Text.Length == 0;
				string display = empty ? _hint : Text;
				Color color = empty ? Color.Gray : Color.White;
				Vector2 pos = new(dims.X + 8f, dims.Y + dims.Height / 2f - 10f);
				Utils.DrawBorderString(spriteBatch, display, pos, color, 1f, 0f, 0f, -1);
			}
		}

		private class ItemEntryElement : UIElement
		{
			private readonly int _itemType;
			private readonly string _name;

			public ItemEntryElement(int itemType, string name)
			{
				_itemType = itemType;
				_name = name;
				Width.Set(0f, 1f);
				Height.Set(36f, 0f);
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dims = GetDimensions();

				if (IsMouseHovering)
				{
					Utils.DrawInvBG(spriteBatch, dims.ToRectangle(), Color.White * 0.2f);
				}

				Texture2D texture = TextureAssets.Item[_itemType].Value;
				float scale = Math.Min(1f, 32f / Math.Max(texture.Width, texture.Height));
				Vector2 iconPos = new(dims.X + 4f + texture.Width * scale / 2f, dims.Y + dims.Height / 2f);
				spriteBatch.Draw(texture, iconPos, null, Color.White, 0f, new Vector2(texture.Width, texture.Height) / 2f, scale, SpriteEffects.None, 0f);

				Vector2 textPos = new(dims.X + 44f, dims.Y + dims.Height / 2f - 10f);
				Utils.DrawBorderString(spriteBatch, _name, textPos, Color.White, 1f, 0f, 0f, -1);
			}
		}
	}
}
