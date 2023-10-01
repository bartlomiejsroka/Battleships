using Battleship.Enums;
using Battleship.ValueObjects;

namespace Battleship
{
    internal class Ship
    {
        public short Size { get; }

        public Coordinates Coordinates { get; private set; }

        public Direction Direction { get; private set; }

        public short HitCounter { get; private set; }

        public void HitShip()
        {
            HitCounter++;
        }

        public bool IsDead()
           => HitCounter >= Size;

        public Ship(ShipSize size, Coordinates coordinates, Direction direction)
        {
            Size = (short)size;
            Coordinates = coordinates;
            Direction = direction;
        }
    }
}
