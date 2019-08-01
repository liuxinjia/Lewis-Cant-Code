public class MyHashMap {

    int[][] maps;
    int mapLength = 1000;
    /** Initialize your data structure here. */
    public MyHashMap () {
        maps = new int[mapLength][];
    }

    int Hash (int key) {
        return key % mapLength;
    }

    int Pos (int key) {
        return key / mapLength;
    }

    /** value will always be non-negative. */
    public void Put (int key, int value) {
        int row = Hash (key);
        int col = Pos (key);
        if (maps[row] == null) maps[row] = new int[mapLength];;

        maps[row][col] = value == 0 ? -1 : value;
    }

    /** Returns the value to which the specified key is mapped, or -1 if this map contains no mapping for the key */
    public int Get (int key) {
        int row = Hash (key);
        int col = Pos (key);
        if (maps[row] == null || maps[row][col] == 0) return -1;
        return maps[row][col] == -1 ? -0 : maps[row][col];
    }

    /** Removes the mapping of the specified value key if this map contains a mapping for the key */
    public void Remove (int key) {
        int index = Hash (key);
        if (maps[index] == null) return;
        maps[index][Pos (key)] = 0;
    }
}

/**
 * Your MyHashMap object will be instantiated and called as such:
 * MyHashMap obj = new MyHashMap();
 * obj.Put(key,value);
 * int param_2 = obj.Get(key);
 * obj.Remove(key);
 */

//  less time and less space
// Yeah, it will cost less time when using single list

//linked list
public class MyHashMap {
    LinkNode[] set;
    int length = 1000;
    /** Initialize your data structure here. */
    public MyHashMap () {
        set = new LinkNode[length];
    }

    int Hash (int val) {
        return val % length;
    }

    /** value will always be non-negative. */
    public void Put (int key, int value) {
        int index = Hash (key);

        if (set[index] != null) {
            LinkNode cur = set[index];
            while (cur.next != null && cur.key != key) {
                cur = cur.next;
            }
            if (cur.key == key)
                cur.val = value;
            else
                cur.next = new LinkNode (key, value);
        } else
            set[index] = new LinkNode (key, value);
    }

    /** Returns the value to which the specified key is mapped, or -1 if this map contains no mapping for the key */
    public int Get (int key) {
        int index = Hash (key);
        LinkNode cur = set[index];
        if (cur == null) return -1;

        while (cur.next != null && cur.key != key) {
            cur = cur.next;
        }
        if (cur.key == key) return cur.val;
        return -1;
    }

    /** Removes the mapping of the specified value key if this map contains a mapping for the key */
    public void Remove (int key) {
        int index = Hash (key);

        if (set[index] == null) return;

        LinkNode cur = set[index];
        var prev = null;
        while (cur.key != key && cur.next != null) {
            prev = cur;
            cur = cur.next;
        }

        if (cur.key != key) return;
        if (prev == null) {
            set[index] = cur.next;
        } else {
            prev.next = cur.next;
        }
    }

    class LinkNode {
        public int val, key;
        public LinkNode next;
        public LinkNode (int _key, int _val) {
            key = _key;
            val = _val;
            next = null;
        }
    }
}

/**
 * Your MyHashMap object will be instantiated and called as such:
 * MyHashMap obj = new MyHashMap();
 * obj.Put(key,value);
 * int param_2 = obj.Get(key);
 * obj.Remove(key);
 */

//
public class MyHashMap {
    LinkNode[] set;
    int length = 1000;
    /** Initialize your data structure here. */
    public MyHashMap () {
        set = new LinkNode[length];
    }

    int Hash (int val) {
        return val % length;
    }

    /** value will always be non-negative. */
    public void Put (int key, int value) {
        int index = Hash (key);

        if (set[index] == null) set[index] = new LinkNode (-1, -1);
        LinkNode prev = FindNode (index, key);
        if (prev.next == null) prev.next = new LinkNode (key, value);
        else prev.next.val = value;
    }

    /** Returns the value to which the specified key is mapped, or -1 if this map contains no mapping for the key */
    public int Get (int key) {
        int index = Hash (key);
        if (set[index] == null) return -1;

        LinkNode prev = FindNode (index, key);
        return prev.next == null ? -1 : prev.next.val;
    }

    /** Removes the mapping of the specified value key if this map contains a mapping for the key */
    public void Remove (int key) {
        int index = Hash (key);
        if (set[index] == null) return;

        LinkNode prev = FindNode (index, key);
        if (prev.next == null) return;
        else prev.next = prev.next.next;
    }

    LinkNode FindNode (int index, int key) {
        LinkNode cur = set[index];
        LinkNode prev = null;
        while (cur != null && cur.key != key) {
            prev = cur;
            cur = cur.next;
        }

        return prev;
    }

    class LinkNode {
        public int val, key;
        public LinkNode next;
        public LinkNode (int _key, int _val) {
            key = _key;
            val = _val;
            next = null;
        }
    }
}

/**
 * Your MyHashMap object will be instantiated and called as such:
 * MyHashMap obj = new MyHashMap();
 * obj.Put(key,value);
 * int param_2 = obj.Get(key);
 * obj.Remove(key);
 */