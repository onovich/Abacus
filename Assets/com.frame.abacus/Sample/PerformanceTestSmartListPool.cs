using System;
using System.Diagnostics;
using System.Collections.Generic;
using MortiseFrame.Abacus;
using UnityEngine;

public class PerformanceTestSmartListPool : MonoBehaviour {
    private const int N = 1000000;

    private SmartListPool<int> smartListPool;
    private SmartList<int> smartList;
    private List<int> list;

    public void Setup() {
        smartListPool = new SmartListPool<int>(N);
        smartList = new SmartList<int>(N);
        list = new List<int>(N);

        for (int i = 0; i < N; i++) {
            smartListPool.Add(i);
            smartList.Add(i);
            list.Add(i);
        }
    }

    public void TestSmartListPoolAdd() {
        var stopwatch = Stopwatch.StartNew();
        smartListPool.Add(N);
        stopwatch.Stop();
        UnityEngine.Debug.Log($"SmartListPool add: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestSmartListAdd() {
        var stopwatch = Stopwatch.StartNew();
        smartList.Add(N);
        stopwatch.Stop();
        UnityEngine.Debug.Log($"SmartList add: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestListAdd() {
        var stopwatch = Stopwatch.StartNew();
        list.Add(N);
        stopwatch.Stop();
        UnityEngine.Debug.Log($"List add: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestSmartListPoolRemove() {
        var stopwatch = Stopwatch.StartNew();
        smartListPool.Remove(N / 2);
        stopwatch.Stop();
        UnityEngine.Debug.Log($"SmartListPool remove: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestSmartListRemove() {
        var stopwatch = Stopwatch.StartNew();
        smartList.Remove(N / 2);
        stopwatch.Stop();
        UnityEngine.Debug.Log($"SmartList remove: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestListRemove() {
        var stopwatch = Stopwatch.StartNew();
        list.Remove(N / 2);
        stopwatch.Stop();
        UnityEngine.Debug.Log($"List remove: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestSmartListPoolRemoveAt() {
        var stopwatch = Stopwatch.StartNew();
        smartListPool.RemoveAt(N / 2);
        stopwatch.Stop();
        UnityEngine.Debug.Log($"SmartListPool remove at: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestSmartListRemoveAt() {
        var stopwatch = Stopwatch.StartNew();
        smartList.RemoveAt(N / 2);
        stopwatch.Stop();
        UnityEngine.Debug.Log($"SmartList remove at: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestListRemoveAt() {
        var stopwatch = Stopwatch.StartNew();
        list.RemoveAt(N / 2);
        stopwatch.Stop();
        UnityEngine.Debug.Log($"List remove at: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestSmartListPoolContains() {
        var stopwatch = Stopwatch.StartNew();
        var result = smartListPool.Contains(N / 2);
        stopwatch.Stop();
        UnityEngine.Debug.Log($"SmartListPool contains: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestSmartListContains() {
        var stopwatch = Stopwatch.StartNew();
        var result = smartList.Contains(N / 2);
        stopwatch.Stop();
        UnityEngine.Debug.Log($"SmartList contains: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestListContains() {
        var stopwatch = Stopwatch.StartNew();
        var result = list.Contains(N / 2);
        stopwatch.Stop();
        UnityEngine.Debug.Log($"List contains: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestSmartListPoolSort() {
        var stopwatch = Stopwatch.StartNew();
        smartListPool.Sort();
        stopwatch.Stop();
        UnityEngine.Debug.Log($"SmartListPool sort: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestSmartListSort() {
        var stopwatch = Stopwatch.StartNew();
        smartList.Sort();
        stopwatch.Stop();
        UnityEngine.Debug.Log($"SmartList sort: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestListSort() {
        var stopwatch = Stopwatch.StartNew();
        list.Sort();
        stopwatch.Stop();
        UnityEngine.Debug.Log($"List sort: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestSmartListPoolClear() {
        var stopwatch = Stopwatch.StartNew();
        smartListPool.Clear();
        stopwatch.Stop();
        UnityEngine.Debug.Log($"SmartListPool clear: {stopwatch.ElapsedMilliseconds}ms");
    }
    public void TestSmartListClear() {
        var stopwatch = Stopwatch.StartNew();
        smartList.Clear();
        stopwatch.Stop();
        UnityEngine.Debug.Log($"SmartList clear: {stopwatch.ElapsedMilliseconds}ms");
    }

    public void TestListClear() {
        var stopwatch = Stopwatch.StartNew();
        list.Clear();
        stopwatch.Stop();
        UnityEngine.Debug.Log($"List clear: {stopwatch.ElapsedMilliseconds}ms");
    }

    private void Start() {
        Setup();

        TestSmartListPoolAdd();
        TestSmartListAdd();
        TestListAdd();

        TestSmartListPoolRemoveAt();
        TestSmartListRemoveAt();
        TestListRemoveAt();

        TestSmartListPoolRemove();
        TestSmartListRemove();
        TestListRemove();

        TestSmartListPoolContains();
        TestSmartListContains();
        TestListContains();

        TestSmartListPoolSort();
        TestSmartListSort();
        TestListSort();

        TestSmartListPoolClear();
        TestSmartListClear();
        TestListClear();
    }
}



