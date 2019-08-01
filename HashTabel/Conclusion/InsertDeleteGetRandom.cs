using System.Collections.Generic;
public class RandomizedSet {
    Dictionary<int, int> map;
    List<int> list;
    Random random;
    /** Initialize your data structure here. */
    public RandomizedSet () {
        map = new Dictionary<int, int> ();
        list = new List<int> ();
        random = new Random ();
    }

    /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
    public bool Insert (int val) {
        if (map.TryAdd (val, map.Count)) {
            list.Add (val);
            return true;
        }
        return false;
    }

    /** Removes a value from the set. Returns true if the set contained the specified element. */
    public bool Remove (int val) {
        if (map.ContainsKey (val)) {
            if (map[val] != map.Count - 1) {
                var last = list[map.Count - 1];
                map[last] = map[val];
                list[map[val]] = last;
            }
            list.RemoveAt (map.Count - 1);
            map.Remove (val);
            return true;
        }
        return false;
    }

    /** Get a random element from the set. */
    public int GetRandom () {
        return list[random.Next (list.Count)];
    }
}

/**
 * Your RandomizedSet object will be instantiated and called as such:
 * RandomizedSet obj = new RandomizedSet();
 * bool param_1 = obj.Insert(val);
 * bool param_2 = obj.Remove(val);
 * int param_3 = obj.GetRandom();
 */