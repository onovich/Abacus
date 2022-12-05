using System;
using System.Collections.Generic;
using System.Numerics;

namespace MortiseFrame.Abacus {
    public interface IBoundable {
        AABB Bounds { get; }
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

    }
}