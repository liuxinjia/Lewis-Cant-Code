using System.Collections.Generic;
public class Solution {
    public IList<int> TopKFrequent (int[] nums, int k) {
        var map1 = new Dictionary<int, int> ();
        for (int i = 0; i < nums.Length; i++) {
            if (!map1.TryAdd (nums[i], 1)) {
                map1[nums[i]]++;
            }
        }

        var map2 = new SortedDictionary<int, List<int>> (new DescendingComparer<int> ());
        foreach (var pair in map1) {
            if (!map2.ContainsKey (pair.Value)) {
                var tempList = new List<int> ();
                tempList.Add (pair.Key);
                map2.Add (pair.Value, tempList);
            } else {
                map2[pair.Value].Add (pair.Key);
            }
        }

        var list = new List<int> ();
        foreach (var pair in map2) {
            if (pair.Value.Count < k - list.Count) {
                list.AddRange (pair.Value);
                if (list.Count == k) return list;
            } else {
                foreach (var item in pair.Value) {
                    list.Add (item);
                    if (list.Count == k) return list;
                }
            }
        }

        return list;
    }

    class DescendingComparer<T> : IComparer<T> where T : IComparable<T> {
        public int Compare (T x, T y) {
            return y.CompareTo (x);
        }
    }

    public IList<int> TopKFrequent (int[] nums, int k) {
        var map = new Dictionary<int, int> ();
        int max = 0;
        for (int i = 0; i < nums.Length; i++) {
            if (!map.TryAdd (nums[i], 0)) {
                map[nums[i]]++;
                max = Math.Max (max, map[nums[i]]);
            }
        }

        var buckets = new List<int>[max + 1];
        foreach (var pair in map) {
            if (buckets[pair.Value] == null) buckets[pair.Value] = new List<int> ();
            buckets[pair.Value].Add (pair.Key);
        }

        var list = new List<int> ();
        for (int i = max; i >= 0; i--) {
            if (buckets[i] == null) continue;

            if (buckets[i].Count <= k - list.Count) {
                list.AddRange (buckets[i]);
                if (list.Count == k) return list;
            } else {
                foreach (var item in buckets[i]) {
                    list.Add (item);
                    if (list.Count == k) return list;
                }
            }
        }

        return list;
    }

}