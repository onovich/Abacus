using System;
using System.Collections.Generic;
using System.Numerics;

namespace MortiseFrame.Abacus {
    public interface IBoundable {
        AABB Bounds { get; }
        Vector2 GetPosition();
        void SetPosition(Vector2 pos);
    }
    class LooseQuadTree<T> where T : IBoundable {

        // 空间索引的边界
        private AABB bounds;

        // 当前节点包含的对象
        private List<T> objects;

        // 当前节点的子节点
        private LooseQuadTree<T>[] children;

        // 当前节点的松散系数
        private int looseness;

        // 构造函数
        public LooseQuadTree(AABB bounds, int looseness) {
            this.bounds = bounds;
            this.objects = new List<T>();
            this.children = null;
            this.looseness = looseness;
        }

        // 插入对象
        public void Insert(T obj) {
            // 如果对象不在空间索引的边界内，则不进行插入
            if (!bounds.Intersects(obj.Bounds)) {
                return;
            }

            // 如果当前节点没有子节点，则把对象插入当前节点
            if (children == null) {
                objects.Add(obj);

                // 如果当前节点包含的对象数量超过了松散系数，则把当前节点划分为四个子节点
                if (objects.Count > looseness) {
                    Subdivide();
                }
            }
            // 否则，把对象插入到与它相交的子节点中
            else {
                foreach (var child in children) {
                    child.Insert(obj);
                }
            }
        }

        // 把当前节点划分为四个子节点
        private void Subdivide() {
            // 计算子节点的边界
            var x = bounds.Min.X;
            var y = bounds.Min.Y;
            var w = bounds.Size.X / 2;
            var h = bounds.Size.Y / 2;

            // 创建四个子节点
            children = new LooseQuadTree<T>[4];
            children[0] = new LooseQuadTree<T>(new AABB(new Vector2(x, y), new Vector2(w, h)), looseness);
            children[1] = new LooseQuadTree<T>(new AABB(new Vector2(x + w, y), new Vector2(w, h)), looseness);
            children[2] = new LooseQuadTree<T>(new AABB(new Vector2(x, y + h), new Vector2(w, h)), looseness);
            children[3] = new LooseQuadTree<T>(new AABB(new Vector2(x + w, y + h), new Vector2(w, h)), looseness);

            // 把当前节点包含的对象插入到对应的子节点中
            foreach (var obj in objects) {
                foreach (var child in children) {
                    child.Insert(obj);
                }
            }

            // 清空当前节点的对象
            objects.Clear();
        }

        // 查询给定矩形范围内的对象
        public List<T> Query(AABB range) {
            var result = new List<T>();

            // 如果给定的范围与当前节点的边界不相交，则返回空结果
            if (!bounds.Intersects(range)) {
                return result;
            }

            // 如果当前节点没有子节点，则返回当前节点包含的对象
            if (children == null) {
                result.AddRange(objects);
                return result;
            }

            // 如果当前节点有子节点，则递归查询子节点
            foreach (var child in children) {
                result.AddRange(child.Query(range));
            }

            return result;
        }

        // 查询所有对象
        public List<T> QueryAll() {
            var result = new List<T>();

            // 如果当前节点没有子节点，则返回当前节点包含的对象
            if (children == null) {
                result.AddRange(objects);
                return result;
            }

            // 如果当前节点有子节点，则递归查询子节点
            foreach (var child in children) {
                result.AddRange(child.QueryAll());
            }

            return result;
        }

        public void Clear() {
            // 清空当前节点包含的对象
            objects.Clear();
        }

        // 更新对象
        public void Update(T obj) {
            // 如果对象不在空间索引的边界内，则不进行更新
            if (!bounds.Intersects(obj.Bounds)) {
                return;
            }
            // 如果当前节点没有子节点，则直接更新对象
            if (children == null) {
                // 更新对象
                // ...

                // 如果当前节点包含的对象数量超过了松散系数，则把当前节点划分为四个子节点
                if (objects.Count > looseness) {
                    Subdivide();
                }
            }
            // 否则，把对象更新到与它相交的子节点中
            else {
                foreach (var child in children) {
                    child.Update(obj);
                }
            }
        }

        // 尝试移动对象
        public bool TryMove(T obj, Vector2 offset) {
            // 如果对象不在空间索引的边界内，则不进行移动
            if (!bounds.Intersects(obj.Bounds)) {
                return false;
            }
            // 计算移动后的对象边界
            var newBounds = obj.Bounds;
            newBounds.SetMin(newBounds.Min + offset);
            newBounds.SetMax(newBounds.Max + offset);

            // 如果当前节点没有子节点，则直接移动对象
            if (children == null) {
                // 如果移动后的对象边界还在当前节点的边界内，则移动对象
                if (bounds.Contains(newBounds.Center)) {
                    // 移动对象
                    // 移动对象边界
                    obj.Bounds.SetMin(obj.Bounds.Min + offset);
                    obj.Bounds.SetMax(obj.Bounds.Max + offset);
                    // 移动对象位置
                    var position = obj.GetPosition();
                    obj.SetPosition(position + offset);
                    return true;
                }
            }
            // 否则，把对象移动到与它相交的子节点中
            else {
                foreach (var child in children) {
                    if (child.TryMove(obj, offset)) {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Rebalance() {
            // 如果当前节点有子节点，则先重新平衡子节点
            if (children != null) {
                foreach (var child in children) {
                    child.Rebalance();
                }
            }
            // 如果当前节点包含的对象数量小于等于松散系数，则删除子节点
            if (objects.Count <= looseness) {
                // 把子节点中的对象合并到当前节点
                if (children != null) {
                    foreach (var child in children) {
                        objects.AddRange(child.objects);
                    }
                    children = null;
                }
            }
            // 否则，如果当前节点没有子节点，则把当前节点划分为四个子节点
            else if (children == null) {
                Subdivide();
            }
        }

    }

}