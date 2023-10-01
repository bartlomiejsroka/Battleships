using Battleship;
using Battleship.Enums;
using Battleship.ValueObjects;

namespace BattleShip.Tests.ObjectBuilders
{
    internal class ShipBuilder
    {
        public ShipSize ShipSize { get; private set; }

        public Coordinates Coordinates { get; private set; }

        public Direction Direction { get; private set; }

        public short HitCounter { get; private set; }

        public ShipBuilder()
        {
            Coordinates = new Coordinates(0, 0);
        }

        public Ship Build()
        {
            if (HitCounter == 0)
                return new Ship(ShipSize, Coordinates, Direction);

            else
            {
                var ship = new Ship(ShipSize, Coordinates, Direction);
                for (var i = 0; i < HitCounter; i++)
                    ship.HitShip();
                return ship;
            }
        }

        public ShipBuilder WithSize(ShipSize size)
        {
            ShipSize = size;
            return this;
        }

        public ShipBuilder WithDirection(Direction direction)
        {
            Direction = direction;
            return this;
        }

        public ShipBuilder WithCoordinates(short column, short row)
        {
            Coordinates = new Coordinates(column, row);
            return this;
        }

        public ShipBuilder WithHitCounter(short hitCounter)
        {
            HitCounter = hitCounter;
            return this;
        }
    }
}
