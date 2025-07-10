using System;

namespace Core.Shared
{
    public struct Player
    {
        public PlayerType OwnerType;
        public Guid PlayerId;

        public Player(PlayerType ownerType, Guid playerId)
        {
            OwnerType = ownerType;
            PlayerId = playerId;
        }
    }
}