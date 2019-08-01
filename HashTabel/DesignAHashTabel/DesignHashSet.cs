public class MyHashSet {

    const int bucketLength = 1000;
    int[][] buckets;
    /** Initialize your data structure here. */
    public MyHashSet () {
        buckets = new int[bucketLength][];
    }

    int Hash (int key) {
        return key % bucketLength;
    }
    int Pos (int key) {
        return key / bucketLength;
    }

    public void Add (int key) {
        int index = Hash (key);
        if (buckets[index] == null) buckets[index] = new int[bucketLength];
        buckets[index][Pos[key]] = key;
    }

    public void Remove (int key) {
        int index = Hash (key);
        if (buckets[index] == null) return;
        buckets[index][Pos(key)] = -1;
    }

    /** Returns true if this set contains the specified element */
    public bool Contains (int key) {
        int index = Hash (key);
        return buckets[index] != null && buckets[index][Pos(key)] == key;
    }
}

/**
 * Your MyHashSet object will be instantiated and called as such:
 * MyHashSet obj = new MyHashSet();
 * obj.Add(key);
 * obj.Remove(key);
 * bool param_3 = obj.Contains(key);
 */