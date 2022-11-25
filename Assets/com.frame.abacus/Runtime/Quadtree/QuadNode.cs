using System;
using System.Collections.Generic;
using System.Numerics;

namespace MortiseFrame.Abacus {

    public class QuadNode {

        public byte state;       // 0 = empty, 1 = brach, 2 = leaf
        public byte count;       // number of elements in this node
        public ushort key;       // tag the node's bound

        public QuadNode() {
            state = 0;
        }

    }

}

