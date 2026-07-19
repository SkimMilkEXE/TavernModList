using Terraria;
using Terraria.ModLoader;

namespace TavernModList.Content.Players
{
	public class IronSuitPlayer : ModPlayer
	{
		public bool fullSetActive;

		// Custom flight timer, refills on the ground. Self-contained rather than
		// piggybacking on a vanilla wing's stats, since this isn't a wing item.
		private int flightTime;
		private const int MaxFlightTime = 300;

		public override void ResetEffects()
		{
			fullSetActive = false;
		}

		public override void PostUpdateEquips()
		{
			if (!fullSetActive)
			{
				return;
			}

			Player.noFallDmg = true;

			if (Player.velocity.Y == 0f)
			{
				flightTime = MaxFlightTime;
			}
			else if (Player.controlJump && flightTime > 0)
			{
				Player.velocity.Y -= 0.5f;
				if (Player.velocity.Y < -8f)
				{
					Player.velocity.Y = -8f;
				}

				flightTime--;
			}
			else if (Player.velocity.Y > 3f)
			{
				Player.velocity.Y = 3f;
			}
		}
	}
}
