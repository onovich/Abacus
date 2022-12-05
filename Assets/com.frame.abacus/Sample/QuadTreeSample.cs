using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace MortiseFrame.Abacus {

    public class QuadTreeSample : MonoBehaviour {

        public int capacity = 100;
        public System.Numerics.Vector2 worldSize = new System.Numerics.Vector2(100, 100);
        public byte depthLimit = 3;

        QuadTree quadTree;

        private void Start() {

            quadTree = new QuadTree(capacity, worldSize, depthLimit);

        }
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                float randomX = UnityEngine.Random.Range(-worldSize.X / 2, worldSize.X / 2);
                float randomY = UnityEngine.Random.Range(-worldSize.Y / 2, worldSize.Y / 2);
                var element = new System.Numerics.Vector2(randomX, randomY);
                uint elementID = 1;
                var offset = 0;
                byte depth = 0;
                var elements = new Dictionary<uint, int>();

                quadTree.Insert(ref quadTree.all, element, elementID, ref offset, ref depth, ref elements);

                Debug.Log("Insert" + element);
            }

        }

    }

}

