namespace TavernModList.Content.ItemBrowser
{
	// Implement on a ModItem to add an "Obtained from" line in the item browser detail panel,
	// for sources a recipe can't express (drops, tile breaks, fishing, etc).
	public interface IObtainedFrom
	{
		string ObtainedFromDescription { get; }
	}
}
