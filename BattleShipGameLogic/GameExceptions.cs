using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipGameLogic
{
    public class AlreadyDamagedShipException : Exception
    {
        public AlreadyDamagedShipException()
        {
        }

        public AlreadyDamagedShipException(string message)
            : base(message)
        {
        }
    }

    public class ShipAlreadySunkException : Exception
    {
        public ShipAlreadySunkException()
        {
        }

        public ShipAlreadySunkException(string message)
            : base(message)
        {
        }
    }

    public class AttackInvalidCoordinatesException : Exception
    {
        public AttackInvalidCoordinatesException()
        {
        }

        public AttackInvalidCoordinatesException(string message)
            : base(message)
        {
        }
    }
}
