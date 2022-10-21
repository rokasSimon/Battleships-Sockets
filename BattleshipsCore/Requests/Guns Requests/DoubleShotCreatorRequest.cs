﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Requests.Guns_Requests
{
    public class DoubleShotCreatorRequest : GunCreatorRequest
    {
        public override MessageType Type => MessageType.DoubleShoot;

        public string Initiator { get; set; }
        public Vec2 Pos { get; set; }
        public DoubleShotCreatorRequest(string initiator, Vec2 pos) : base(initiator, pos)
        {
            Initiator = initiator;
            Pos = pos;
        }


        public override GunRequest createGun()
        {
            return new DoubleShotRequest(Initiator, Pos);
        }

        public override Message Execute()
        {
            throw new NotImplementedException();
        }
    }
}