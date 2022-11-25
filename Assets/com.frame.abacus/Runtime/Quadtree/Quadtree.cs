using System;
using System.Collections.Generic;
using System.Numerics;

namespace MortiseFrame.Abacus {

    public class QuadTree {

        Memory<QuadNode> all; // saves all nodes state in the QuadTree
        Dictionary<uint/*id*/, int/*index*/> elements;  // saves all elements's index of the Memory<QuadNode> all
        Dictionary<ushort /*boundID*/, AABB> bounds;
        byte depthLimit; // must be less than 8
        Vector2 worldSize;
        Vector2 cellSize;

        public QuadTree(int capacity, Vector2 worldSize, byte depthLimit) {
            this.depthLimit = Math.Clamp(depthLimit, (byte)1, (byte)7);
            this.all = new Memory<QuadNode>(new QuadNode[capacity]);
            CaculateCellSize();
            CaculateAllBounds(depthLimit, worldSize);
        }
        void CaculateCellSize() {
            var quadrantSize = worldSize / (1 << 1);
            cellSize = quadrantSize / (1 << depthLimit);
        }

        public void Insert(ref Memory<QuadNode> all, Vector2 element, uint elementID, ref int offset, ref byte depth, ref Dictionary<uint, int> elements) {
            var span = all.Span.Slice(offset, 4);
            for (byte i = 0; i < 4; i++) {
                if (!InsertWithCell(element, depth, i)) {
                    continue;
                }
                switch (span[i].state) {
                    // empty: create new node, tag as leaf, insert into elements
                    case 0:
                        span[i].state = 1;
                        ushort key = i;
                        key |= (ushort)(depth << 8);
                        span[i].key = key;
                        span[i].count += 1;
                        elements.Add(elementID, offset + i);
                        break;
                    // brach: insert into brach's child
                    case 1:
                        offset *= 4;
                        depth += 1;
                        Insert(ref all, element, elementID, ref offset, ref depth, ref elements);
                        break;
                    // leaf: insert into elements
                    case 2:
                        key = i;
                        key |= (ushort)(depth << 8);
                        span[i].key = key;
                        span[i].count += 1;
                        elements.Add(elementID, offset + i);
                        break;
                }
            }
        }

        public void Remove(Memory<QuadNode> all, uint elementID, Dictionary<uint, int> elements) {
            if (!elements.TryGetValue(elementID, out int index)) {
                return;
            }
            var span = all.Span;
            var node = span[index];
            node.count -= 1;
            if (node.count == 0) {
                node.key = 0;
                node.state = 0;
            }
            elements.Remove(elementID);
        }

        public void Refresh(Memory<QuadNode> all, Vector2[] elementsPos, uint[] elementsID, Dictionary<uint, int> elements) {

            var span = all.Span;
            for (int i = 0; i < elementsPos.Length; i++) {
                if (!elements.TryGetValue(elementsID[i], out int index)) {
                    continue;
                }
                var node = span[index];
                var key = node.key;
                if (bounds[key].Contains(elementsPos[i])) {
                    continue;
                }

                Remove(all, elementsID[i], elements);
                int offset = 0; byte depth = 0;
                Insert(ref all, elementsPos[i], elementsID[i], ref offset, ref depth, ref elements);

            }
        }

        bool InsertWithCell(Vector2 point, byte depth, byte index) {
            ushort key = index;
            key |= (ushort)(depth << 8);
            var bound = bounds[key];
            var min = bound.Min;
            var max = bound.Max;

            if (point.X > max.X) {
                return false;
            }
            if (point.X < min.X) {
                return false;
            }
            if (point.Y > max.Y) {
                return false;
            }
            if (point.Y < min.Y) {
                return false;
            }
            return true;
        }

        void CaculateAllBounds(byte depthLimit, Vector2 worldSize) {
            var center = Vector2.Zero;
            for (byte depth = 0; depth < depthLimit; depth++) {
                for (byte index = 0; index < 4; index++) {
                    center = GetCenter(depth, index, worldSize, center);
                    var boundsMin = GetBoundsMin(depth, index, worldSize, center);
                    var boundsMax = GetBoundsMax(depth, index, worldSize, center);
                    var bound = new AABB(boundsMin, boundsMax);
                    ushort key = index;
                    key |= (ushort)(depth << 8);
                    bounds.Add(key, bound);
                }
            }
        }

        Vector2 GetCenter(byte depth, byte index, Vector2 mapSize, Vector2 center) {
            while (depth > 0) {
                mapSize = mapSize / (1 << 1);
                center = center + mapSize / (1 << 2);
                depth -= 1;
            }
            switch (index) {
                case 0:
                    break;
                case 1:
                    center = new Vector2(-center.X, center.Y);
                    break;
                case 2:
                    center = new Vector2(-center.X, -center.Y);
                    break;
                case 3:
                    center = new Vector2(center.X, -center.Y);
                    break;
            }
            return center;
        }

        Vector2 GetBoundsMin(byte depth, byte index, Vector2 mapSize, Vector2 center) {

            switch (index) {
                case 0:
                    return new Vector2(0, 0) + center;
                case 1:
                    return new Vector2(-mapSize.X / 2, 0) + center;
                case 2:
                    return new Vector2(-mapSize.X / 2, -mapSize.Y / 2) + center;
                case 3:
                    return new Vector2(0, -mapSize.Y / 2) + center;
                default:
                    return new Vector2(0, 0);
            }
        }

        Vector2 GetBoundsMax(byte depth, byte index, Vector2 mapSize, Vector2 center) {

            switch (index) {
                case 0:
                    return new Vector2(mapSize.X / 2, mapSize.Y / 2) + center;
                case 1:
                    return new Vector2(0, mapSize.Y / 2) + center;
                case 2:
                    return new Vector2(0, 0) + center;
                case 3:
                    return new Vector2(mapSize.X / 2, 0) + center;
                default:
                    return new Vector2(0, 0);
            }
        }

    }

}






