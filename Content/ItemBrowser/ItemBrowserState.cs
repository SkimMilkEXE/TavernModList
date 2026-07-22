using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace TavernModList.Content.ItemBrowser
{
	public class ItemBrowserState : UIState
	{
		private readonly Mod _mod;
		private readonly UIList _list = new();
		private readonly SearchBox _searchBox = new("Search items...");
		private readonly UIList _detailList = new();
		private List<ModItem> _allItems = new();

		public ItemBrowserState(Mod mod)
		{
			_mod = mod;
		}

		public override void OnInitialize()
		{
			UIPanel panel = new()
			{
				Width = { Pixels = 560 },
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

			_searchBox.Width.Set(180f, 0f);
			_searchBox.Height.Set(30f, 0f);
			_searchBox.Top.Set(0f, 0f);
			_searchBox.OnTextChange += (_, _) => Filter();
			panel.Append(_searchBox);

			_list.Width.Set(180f, 0f);
			_list.Height.Set(-40f, 1f);
			_list.Top.Set(40f, 0f);
			_list.ListPadding = 4f;
			_list.ManualSortMethod = _ => { };
			panel.Append(_list);

			UIScrollbar scrollbar = new();
			scrollbar.Height.Set(-40f, 1f);
			scrollbar.Top.Set(40f, 0f);
			scrollbar.Left.Set(184f, 0f);
			panel.Append(scrollbar);
			_list.SetScrollbar(scrollbar);

			_detailList.Width.Set(320f, 0f);
			_detailList.Height.Set(-10f, 1f);
			_detailList.Top.Set(0f, 0f);
			_detailList.Left.Set(210f, 0f);
			_detailList.ListPadding = 6f;
			_detailList.ManualSortMethod = _ => { };
			panel.Append(_detailList);

			UIScrollbar detailScrollbar = new();
			detailScrollbar.Height.Set(-10f, 1f);
			detailScrollbar.Top.Set(0f, 0f);
			detailScrollbar.Left.Set(534f, 0f);
			panel.Append(detailScrollbar);
			_detailList.SetScrollbar(detailScrollbar);

			_allItems = _mod.GetContent<ModItem>().OrderBy(item => item.Item.Name).ToList();
			Filter();
			ShowDetailFor(null);
		}

		private void Filter()
		{
			string query = _searchBox.Text;
			_list.Clear();
			foreach (ModItem item in _allItems)
			{
				if (query.Length == 0 || item.DisplayName.Value.Contains(query, StringComparison.OrdinalIgnoreCase))
				{
					_list.Add(new ItemEntryElement(item, ShowDetailFor));
				}
			}
		}

		private void ShowDetailFor(ModItem item)
		{
			_detailList.Clear();

			if (item == null)
			{
				AddDetailLine("Click an item to see how to get it", 1f, false);
				return;
			}

			AddDetailLine(item.DisplayName.Value, 1.1f, true);

			bool foundRecipe = false;
			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				Recipe recipe = Main.recipe[i];
				if (recipe.createItem.type != item.Item.type)
				{
					continue;
				}

				if (foundRecipe)
				{
					AddDetailLine("- or -", 0.9f, false);
				}

				foundRecipe = true;
				AddDetailLine("Crafted from:", 1f, false);
				foreach (Item ingredient in recipe.requiredItem)
				{
					AddDetailLine($"  {ingredient.stack}x {Lang.GetItemNameValue(ingredient.type)}", 0.9f, false);
				}

				if (recipe.requiredTile.Count > 0)
				{
					string stations = string.Join(", ", recipe.requiredTile.Select(TileID.Search.GetName));
					AddDetailLine($"  at: {stations}", 0.9f, false);
				}
			}

			if (!foundRecipe)
			{
				AddDetailLine("Not craftable", 1f, false);
			}

			if (item is IObtainedFrom obtainedFrom)
			{
				AddDetailLine("Obtained from:", 1f, false);
				AddDetailLine($"  {obtainedFrom.ObtainedFromDescription}", 0.9f, false);
			}
		}

		private void AddDetailLine(string text, float scale, bool large)
		{
			_detailList.Add(new UIText(text, scale, large));
		}

		// Custom text box: the vanilla UIFocusInputTextField class isn't accessible outside tModLoader's own code.
		// No click-to-focus here - this is the only text field in the browser, so it just always
		// takes keyboard input while the browser is open (avoids a click/focus dispatch bug where
		// clicking the box never actually started capturing input).
		private class SearchBox : UIElement
		{
			public string Text { get; private set; } = string.Empty;

			private readonly string _hint;

			public event EventHandler OnTextChange;

			public SearchBox(string hint)
			{
				_hint = hint;
			}

			public override void Update(GameTime gameTime)
			{
				base.Update(gameTime);

				string updated = Main.GetInputText(Text, false);
				if (updated != Text)
				{
					Text = updated;
					OnTextChange?.Invoke(this, EventArgs.Empty);
				}

				PlayerInput.WritingText = true;
				Main.instance.HandleIME();
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
			private readonly ModItem _modItem;
			private readonly Action<ModItem> _onClick;

			public ItemEntryElement(ModItem modItem, Action<ModItem> onClick)
			{
				_modItem = modItem;
				_onClick = onClick;
				Width.Set(0f, 1f);
				Height.Set(36f, 0f);
			}

			public override void LeftClick(UIMouseEvent evt)
			{
				_onClick(_modItem);
				base.LeftClick(evt);
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dims = GetDimensions();

				if (IsMouseHovering)
				{
					Utils.DrawInvBG(spriteBatch, dims.ToRectangle(), Color.White * 0.2f);
				}

				int itemType = _modItem.Item.type;
				Texture2D texture = TextureAssets.Item[itemType].Value;

				// Animated items store every frame in one tall texture;
				// slice out just the current frame instead of drawing the whole sheet.
				DrawAnimation animation = Main.itemAnimations[itemType];
				Rectangle frame = animation != null ? animation.GetFrame(texture) : texture.Bounds;

				float scale = Math.Min(1f, 32f / Math.Max(frame.Width, frame.Height));
				Vector2 iconPos = new(dims.X + 4f + frame.Width * scale / 2f, dims.Y + dims.Height / 2f);
				spriteBatch.Draw(texture, iconPos, frame, Color.White, 0f, new Vector2(frame.Width, frame.Height) / 2f, scale, SpriteEffects.None, 0f);

				Vector2 textPos = new(dims.X + 44f, dims.Y + dims.Height / 2f - 10f);
				Utils.DrawBorderString(spriteBatch, _modItem.DisplayName.Value, textPos, Color.White, 1f, 0f, 0f, -1);
			}
		}
	}
}
