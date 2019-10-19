using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cr7Sund_DS;

class Program {
    public static void Main () {

        Test1 ();

        //Test2();
    }

    private static void Test2 () {
        Console.WriteLine ("\nHello world");

        var heap = new MaxHeap<int, int> ();

        for (int i = 0; i < 198; i++) {
            heap.Push (new KeyValuePair<int, int> (i + 13 * -1, i * 2));
        }

        for (int i = 0; i < 98; i++) {
            var temp = heap.Pop ();

            Console.Write ("{0},  ", temp);
        }

        Console.WriteLine ("\nHello world");
        for (int i = 200; i < 2108; i++) {
            Console.Write ("{0}, ", i);
            heap.Push (new KeyValuePair<int, int> (i, i * 2));
        }

        Console.WriteLine ("Fuck World");
    }

    public static void Test1 () {
        MaxHeap<int, int> heap = new MaxHeap<int, int> ();

        for (int i = 0; i < 15; i++) {
            heap.Push (new KeyValuePair<int, int> (i + 1, i * 2));
        }

        //for (int i = 0; i < 11; i++)
        //{
        //    if (heap.TryAdd(new KeyValuePair<int, int>(i + 1, i * 2)))
        //        heap.Remove(i+1);
        //    else
        //    {
        //        heap[i + 1] = -1 * i;
        //    }
        //}

        //for (int i = 0; i < 108; i++)
        //{

        //    heap.Push(new KeyValuePair<int, int>(i, i * 2));
        //}

        Console.WriteLine ("FUck Y");

        int count = 1;
        foreach (var item in heap) {
            Console.Write ("{0}, ", heap.Pop ());
            ++count;

        }

        Console.WriteLine ("\n" + count);

    }

}

namespace Cr7Sund_DS {
    using System.Collections.Generic;
    using System.Collections;
    using System.Diagnostics;
    using System;

    #region  Heap 

    [DebuggerDisplay ("MyCount = {Count}")]
    public abstract class HeapMap<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : IComparable, IComparable<TKey>
        where TValue : IComparable<TValue> {

            protected struct Entry {
                public int hashCode;
                public int next;
                public TKey key;
                public TValue value;

                // int version;
            }
            protected Entry[] entries;
            protected int[] buckets;
            int _lastPos = -1;
            int count;
            int freeCount;
            int freeList;
            IEqualityComparer<TKey> comparer;

            Object _syncRoot;

            KeyCollection keys;
            ValueCollection values;

            static Entry[] EMPTYENTRYARRAY = new Entry[0];
            static int[] EMPTYINDEXARRAY = new int[0];

            public TValue this [TKey key] {
                get {

                    int i = FindEntry (key);
                    if (i >= 0) return entries[i].value;
                    throw new KeyNotFoundException ();
                    //return default(TValue);
                }
                set {
                    Insert (key, value, false);
                }
            }

            public ICollection<TKey> Keys {
                get {
                    if (keys == null) keys = new KeyCollection (this);
                    return keys;
                }
            }

            public ICollection<TValue> Values {
                get {
                    if (values == null) values = new ValueCollection (this);
                    return values;
                }
            }

            public int Count {
                get { return count - freeCount; }
            }

            public bool IsReadOnly => false;

            public object SyncRoot {
                get {
                    if (_syncRoot == null) {
                        System.Threading.Interlocked.CompareExchange<Object> (
                            ref _syncRoot, new Object (), null);
                    }
                    return _syncRoot;
                }
            }

            public bool Empty () => _lastPos < 0 || entries.Length == 0;

            public TValue Top () => Empty () ?
            throw new IndexOutOfRangeException () : entries[0].value;

            protected abstract int Compare (TValue item1, TValue item2);

            #region

            public HeapMap () : this (0, null) { }

            public HeapMap (int capacity) : this (capacity, null) { }

            public HeapMap (int capcity, IEqualityComparer<TKey> comparer) {
                if (capcity < 0) throw new ArgumentOutOfRangeException ();
                if (capcity > 0) Initialize (capcity);
                this.comparer = comparer ?? EqualityComparer<TKey>.Default;
            }
            #endregion

            #region

            public void Clear () {
                if (count > 0) {
                    for (int i = 0; i < buckets.Length; i++) buckets[i] = -1;
                    Array.Clear (entries, 0, count);
                    freeList = -1;
                    count = 0;
                    freeCount = 0;
                }

            }

            public bool Contains (KeyValuePair<TKey, TValue> item) {
                int i = FindEntry (item.Key);
                if (i >= 0 && EqualityComparer<TValue>.Default.Equals (entries[i].value, item.Value)) {
                    return true;
                }
                return false;
                //return item.Key < buckets.Length ? entries[buckets[item.Key]] == item.Value : false;
            }

            public bool ContainsKey (TKey key) {
                return FindEntry (key) >= 0;
                //return key < buckets.Length ? entries[buckets[key]] == null : false;
            }

            public bool ContainsValue (TValue value) {
                if (value != null) {
                    EqualityComparer<TValue> c = EqualityComparer<TValue>.Default;
                    for (int i = 0; i < count; i++) {
                        if (entries[i].hashCode >= 0 && c.Equals (entries[i].value, value)) return true;
                    }
                }
                return false;
            }

            public void CopyTo (KeyValuePair<TKey, TValue>[] _values, int startIndex = 0) {
                if (_values == null) throw new NullReferenceException ();
                if (Empty ()) throw new IndexOutOfRangeException ("Empty heap");

                if (startIndex < 0 || startIndex > _values.Length)
                    throw new IndexOutOfRangeException ();

                //it should be out of range excetption
                if (_values.Length - startIndex < Count)
                    throw new IndexOutOfRangeException ("the left capcity should be larger than copying size ");

                int length = this.count;
                Entry[] temp = this.entries;
                for (int i = 0; i < length; i++) {
                    _values[startIndex++] = new KeyValuePair<TKey, TValue> (temp[i].key, temp[i].value);
                }
            }

            public void Add (TKey key, TValue value) {
                Insert (key, value, true);
            }

            public bool Remove (TKey key) {
                return Delete (key);
            }

            public void Add (KeyValuePair<TKey, TValue> item) {
                Insert (item.Key, item.Value, true);
            }

            public bool Remove (KeyValuePair<TKey, TValue> item) {
                return Delete (item.Key, item.Value, true);
            }

            public bool TryAdd (KeyValuePair<TKey, TValue> item) {
                return Insert (item.Key, item.Value, true);
            }

            public bool TryAdd (TKey key, TValue value) {
                return Insert (key, value, true);
            }

            public bool TryGetValue (TKey key, out TValue value) {
                int i = FindEntry (key);
                if (i >= 0) {
                    value = entries[i].value;
                    return true;
                }
                value = default (TValue);
                return false;
            }

            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator () {
                return new Enumerator (this, Enumerator.keyValuePair);
            }

            IEnumerator IEnumerable.GetEnumerator () {
                return new Enumerator (this, Enumerator.keyValuePair);
            }
            #endregion

            #region
            internal TValue TryGetValueOrDefault (TKey key) {
                int i = FindEntry (key);
                return i >= 0 ? entries[i].value : default (TValue);
            }

            void Resize () {
                Resize (HashHelpers.ExpandPrime (count), false);
            }

            void Resize (int newSize, bool forceNewHashCoses) {
                int[] newBuckets = new int[newSize];
                for (int i = 0; i < newBuckets.Length; i++) newBuckets[i] = -1;

                Entry[] newEntries = new Entry[newSize];
                Array.Copy (entries, 0, newEntries, 0, count);
                if (forceNewHashCoses) {
                    for (int i = 0; i < count; i++) {
                        if (newEntries[i].hashCode != -1) {
                            newEntries[i].hashCode = (comparer.GetHashCode (newEntries[i].key) & 0x7FFFFFFF);
                        }
                    }
                }

                for (int i = 0; i < count; i++) {
                    if (newEntries[i].hashCode >= 0) {
                        int bucket = newEntries[i].hashCode % newSize;
                        newEntries[i].next = newBuckets[bucket];
                        newBuckets[bucket] = i;
                    }
                }

                buckets = newBuckets;
                entries = newEntries;
            }

            int FindEntry (TKey key) {
                if (key == null) {
                    throw new ArgumentNullException ();
                }

                if (buckets != null) {
                    //Math.Abs
                    int hashCode = comparer.GetHashCode (key) & 0x7FFFFFFF;
                    for (int i = buckets[hashCode % buckets.Length]; i >= 0; i = entries[i].next) {
                        if (entries[i].hashCode == hashCode && comparer.Equals (entries[i].key, key)) return i;
                    }
                }
                return -1;
            }

            void Initialize (int capacity) {
                int size = HashHelpers.GetPrime (capacity);
                buckets = new int[size];
                for (int i = 0; i < buckets.Length; i++) buckets[i] = -1;
                entries = new Entry[size];
                freeList = -1;
                _lastPos = -1;
            }

            bool Insert (TKey key, TValue value, bool add) {
                if (key == null) {
                    throw new NullReferenceException ();
                }

                if (buckets == null) Initialize (0);
                int hashCode = comparer.GetHashCode (key) & 0x7FFFFFFF;
                int targetBucket = hashCode % buckets.Length;

#if FEATURE_RANDOMIZED_STRING_HASHING
                int collisionCount = 0;
#endif
                for (int i = buckets[targetBucket]; i >= 0; i = entries[i].next) {
                    if (entries[i].hashCode == hashCode && Comparer.Equals (entries[i].key, key)) {
                        if (add) {
                            return false;
                            //throw new Exception("Arguement_ AddingDuplicate");
                        }

                        // _entries[i].version++;
                        //int cmp = Compare(entries[i].value, value);
                        //if (cmp > 0)
                        //{
                        //    TrickleUp(i);
                        //}
                        //else if (cmp < 0)
                        //{
                        //    TrickleDown(i);
                        //}
                        entries[i].value = value;
                        TrickleDown (i);
                        TrickleUp (i);
                        return true;
                    }
#if FEATURE_RANDOMIZED_STRING_HASHING
                    collisionCount++;
#endif
                }
                int index;
                if (freeCount > 0) {
                    index = freeList;
                    freeList = entries[index].next;
                    freeCount--;
                } else {
                    if (count == entries.Length) {
                        Resize ();
                        targetBucket = hashCode % buckets.Length;
                    }
                    index = count++;
                }

                ++_lastPos;
                entries[index].hashCode = hashCode;
                entries[index].next = buckets[targetBucket];
                entries[index].key = key;
                entries[index].value = value;
                buckets[targetBucket] = index;
                TrickleUp (_lastPos);

#if FEATURE_RANDOMIZED_STRING_HASHING

#if FEATURE_CORECLR
                // In case we hit the collision threshold we'll need to switch to the comparer which is using randomized string hashing
                // in this case will be EqualityComparer<string>.Default.
                // Note, randomized string hashing is turned on by default on coreclr so EqualityComparer<string>.Default will 
                // be using randomized string hashing

                //             if (collisionCount > HashHelpers.HashCollisionThreshold && comparer == NonRandomizedStringEqualityComparer.Default) {
                //                 comparer = (IEqualityComparer<TKey>) EqualityComparer<string>.Default;
                //                 Resize (entries.Length, true);
                //             }
                // #else
                //             if (collisionCount > HashHelpers.HashCollisionThreshold && HashHelpers.IsWellKnownEqualityComparer (comparer)) {
                //                 comparer = (IEqualityComparer<TKey>) HashHelpers.GetRandomizedEqualityComparer (comparer);
                //                 Resize (entries.Length, true);
                //             }
#endif // FEATURE_CORECLR

#endif

                return true;

            }

            bool Delete (TKey key, TValue value = default (TValue), bool checkValue = false) {
                if (key == null) {
                    throw new ArgumentNullException ();
                }
                if (Empty ()) throw new IndexOutOfRangeException ("No items left");

                if (buckets == null) return false;

                int hashCode = comparer.GetHashCode (key) & 0x7FFFFFFF;
                int bucket = hashCode % buckets.Length;
                int last = -1;
                for (int i = buckets[bucket]; i >= 0; last = i, i = entries[i].next) {
                    if (entries[i].hashCode == hashCode &&
                        entries[i].key.CompareTo (key) == 0 &&
                        (!checkValue || Compare (entries[i].value, value) == 0)
                    ) {
                        if (last < 0) {
                            buckets[bucket] = entries[i].next;
                        } else {
                            entries[last].next = entries[i].next;
                            //can be neglect
                            buckets[bucket] = -1;
                        }

                        int j = entries[_lastPos].hashCode % buckets.Length;
                        buckets[j] = entries[_lastPos].next;

                        SwapEntries (i, _lastPos);
                        entries[_lastPos].hashCode = -1;
                        entries[_lastPos].next = freeList;
                        freeList = _lastPos--;
                        freeCount++;

                        //if-else , don't worry about that
                        // it will not both go deeper
                        TrickleDown (i);
                        TrickleUp (i);
                        return true;
                    }
                }

                return false;
            }

            #endregion

            #region Heap operation

            public TValue Pop () {
                return PopEntry ().value;
            }

            public void Push (KeyValuePair<TKey, TValue> item) {
                Add (item.Key, item.Value);
            }

            Entry PopEntry () {
                if (Empty ()) throw new IndexOutOfRangeException ("No items left");

                Entry top = entries[0];

                int last = -1;
                int hashCode = entries[0].hashCode % entries.Length;

                for (int i = buckets[hashCode]; i >= 0; last = i, i = entries[i].next) {
                    if (entries[i].hashCode == entries[0].hashCode) {
                        if (Compare (entries[0].value, entries[i].value) != 0) throw new Exception ("Test");
                        if (last < 0) {
                            buckets[hashCode] = entries[i].next;
                        } else {
                            buckets[hashCode] = -1;
                            entries[last].next = entries[i].next;
                        }
                    }
                }
                buckets[entries[_lastPos].hashCode % entries.Length] = entries[_lastPos].next;

                if (_lastPos != 0) {
                    SwapEntries (0, _lastPos);
                }

                entries[_lastPos].hashCode = -1;
                entries[_lastPos].next = freeList;
                freeList = _lastPos--;
                freeCount++;

                TrickleDown ();
                return top;
            }

            int Compare (Entry e1, Entry e2) => Compare (e1.value, e2.value);

            void TrickleUp (int pos) {
                while (pos > 0) {
                    int parent = (pos - 1) / 2;
                    if (Compare (entries[pos], entries[parent]) > 0) {
                        Swap (parent, pos);
                        pos = parent;
                    } else {
                        return;
                    }
                }
            }

            void TrickleDown (int parent = 0) {
                while (parent >= 0) {
                    int left = parent * 2 + 1;
                    int right = parent * 2 + 2;

                    if (left <= _lastPos) {
                        int swapIndex = left;

                        if (right <= _lastPos && Compare (entries[right], entries[left]) > 0) {
                            swapIndex = right;
                        }

                        if (Compare (entries[swapIndex], entries[parent]) > 0) {
                            Swap (swapIndex, parent);
                            parent = swapIndex;
                        } else {
                            return;
                        }
                    } else {
                        return;
                    }
                }
            }

            //params: ea , eb : Entry Index
            void Swap (int ea, int eb) {
                //each entry node is a linked list
                //the previous item should be consider into swap
                int last = -1;
                int hashCode = entries[ea].hashCode % entries.Length;
                for (int i = buckets[hashCode]; i >= 0; last = i, i = entries[i].next) {
                    if (i == ea) {
                        //if(Comparer.Equals(entries[i].key, entries[ea].key)) throw new Exception("Error");
                        if (last < 0) {
                            buckets[hashCode] = eb;
                        } else {
                            entries[last].next = eb;
                        }
                        break;
                    }
                }
                last = -1;
                hashCode = entries[eb].hashCode % entries.Length;
                for (int i = buckets[hashCode]; i >= 0; last = i, i = entries[i].next) {
                    if (i == eb) {
                        //if(Comparer.Equals(entries[i].key, entries[eb].key) ) throw new Exception("Error"); 
                        if (last < 0) {
                            buckets[hashCode] = ea;
                        } else {
                            entries[last].next = ea;
                        }
                        break;
                    }
                }

                SwapEntries (ea, eb);
            }

            void SwapEntries (int a, int b) {
                Entry temp = entries[a];
                entries[a] = entries[b];
                entries[b] = temp;
            }

            #endregion

            public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>,
            IDictionaryEnumerator {
                HeapMap<TKey, TValue> map;
                int index;
                KeyValuePair<TKey, TValue> current;
                int getEnumeratorRetType; // what should Enumetaor.Current return ？

                internal const int DictEntry = 1;
                internal const int keyValuePair = 2;

                internal Enumerator (HeapMap<TKey, TValue> map, int getEnumeratorRetType) {
                    this.map = map;
                    index = 0;
                    this.getEnumeratorRetType = getEnumeratorRetType;
                    current = new KeyValuePair<TKey, TValue> ();
                }

                public bool MoveNext () {
                    // Use ussigned comparsion since we set index to map.count + 1 when the enumeration ends
                    ///map.count + 1 could be negative if map.count is Int32.MaxValue
                    while ((uint) index < (uint) map.Count) {
                        if (map.entries[index].hashCode >= 0) {
                            current = new KeyValuePair<TKey, TValue> (
                                map.entries[index].key, map.entries[index].value
                            );
                            index++;
                            return true;
                        }
                        index++;
                    }

                    index = map.Count + 1;
                    current = new KeyValuePair<TKey, TValue> ();
                    return false;
                }

                public KeyValuePair<TKey, TValue> Current => current;

                //not orginal collection , will not GC 
                //need to overwrite the finalize method or inherit IDisposible
                public void Dispose () { }

                object IEnumerator.Current {
                    get {
                        if (index == 0 || index == map.count + 1) {
                            throw new InvalidOperationException ("Empty, Can't happen");
                        }

                        if (getEnumeratorRetType == DictEntry) {
                            return new System.Collections.DictionaryEntry (current.Key, current.Value);
                        } else {
                            return new KeyValuePair<TKey, TValue> (current.Key, current.Value);
                        }
                    }
                }

                void IEnumerator.Reset () {
                    index = 0;
                    current = new KeyValuePair<TKey, TValue> ();
                }

                DictionaryEntry IDictionaryEnumerator.Entry {
                    get {
                        if (index == 0 || index == map.count + 1) {
                            throw new InvalidOperationException ("Empty, Can't happen");
                        }

                        return new DictionaryEntry (current.Key, current.Value);
                    }
                }

                object IDictionaryEnumerator.Key {
                    get {
                        if (index == 0 || index == map.count + 1) {
                            throw new InvalidOperationException ("Empty, Can't happen");
                        }
                        return current.Key;
                    }
                }

                object IDictionaryEnumerator.Value {
                    get {
                        if (index == 0 || index == map.count + 1) {
                            throw new InvalidOperationException ("Empty, Can't happen");
                        }
                        return current.Value;
                    }
                }

            }

            [System.Diagnostics.DebuggerDisplay ("Count = {Count}")]
            [Serializable]
            public sealed class KeyCollection : ICollection<TKey>,
            ICollection,
            IReadOnlyCollection<TKey> {
                private HeapMap<TKey, TValue> map;

                public KeyCollection (HeapMap<TKey, TValue> map) {
                    if (map == null) {
                        throw new ArgumentNullException ();
                    }
                    this.map = map;
                }

                public Enumerator GetEnumerator () {
                    return new Enumerator (map);
                }

                public void CopyTo (TKey[] array, int index) {
                    if (array == null) {
                        throw new ArgumentNullException ();
                    }

                    if (index < 0 || index > array.Length) {
                        throw new ArgumentOutOfRangeException ();
                    }

                    if (array.Length - index < map.Count) {
                        throw new ArgumentOutOfRangeException ("Please choose a bigger one. Bigger one, one ");
                    }

                    int count = map.count;
                    Entry[] entries = map.entries;
                    for (int i = 0; i < count; i++) {
                        if (entries[i].hashCode >= 0) array[index++] = entries[i].key;
                    }
                }

                public int Count {
                    get { return map.Count; }
                }

                bool ICollection<TKey>.IsReadOnly {
                    get { return true; }
                }

                void ICollection<TKey>.Add (TKey item) {
                    throw new NotSupportedException ("no such function");
                }

                void ICollection<TKey>.Clear () {
                    throw new NotSupportedException ("no such function");
                }

                bool ICollection<TKey>.Contains (TKey item) {
                    return map.ContainsKey (item);
                }

                bool ICollection<TKey>.Remove (TKey item) {
                    throw new NotSupportedException ("no such function");
                }

                IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator () {
                    return new Enumerator (map);
                }

                IEnumerator IEnumerable.GetEnumerator () {
                    return new Enumerator (map);
                }

                void ICollection.CopyTo (Array array, int index) {

                    if (array == null) {
                        throw new ArgumentNullException ();
                    }

                    if (index < 0 || index > array.Length) {
                        throw new ArgumentOutOfRangeException ();
                    }

                    if (array.Length - index < map.Count) {
                        throw new ArgumentOutOfRangeException ("Please choose a bigger one. Bigger one, one ");
                    }

                    if (array.Rank != 1) {
                        throw new ArgumentException ("Arg_RankMultiDimNotSupported");
                    }

                    if (array.GetLowerBound (0) != 0) {
                        throw new ArgumentException ("Arg_NonZeroLowerBound");
                    }

                    TKey[] keys = array as TKey[];
                    if (keys != null) {
                        CopyTo (keys, index);
                    } else {
                        object[] objects = array as object[];
                        if (objects == null) {
                            throw new InvalidCastException ();
                        }

                        int count = map.count;
                        Entry[] entries = map.entries;
                        try {
                            for (int i = 0; i < count; i++) {
                                if (entries[i].hashCode >= 0) objects[index++] = entries[i].key;
                            }
                        } catch (ArrayTypeMismatchException) {
                            throw new ArgumentException ("Argument_InvalidArrayType");
                        }
                    }
                }

                bool ICollection.IsSynchronized {
                    get { return false; }
                }

                Object ICollection.SyncRoot {
                    get { return ((ICollection) map).SyncRoot; }
                }

                [Serializable]
                public struct Enumerator : IEnumerator<TKey>,
                System.Collections.IEnumerator {
                    private HeapMap<TKey, TValue> map;
                    private int index;
                    private TKey currentKey;

                    internal Enumerator (HeapMap<TKey, TValue> map) {
                        this.map = map;
                        index = 0;
                        currentKey = default (TKey);
                    }

                    public void Dispose () { }

                    public bool MoveNext () {
                        while ((uint) index < (uint) map.count) {
                            if (map.entries[index].hashCode >= 0) {
                                currentKey = map.entries[index].key;
                                index++;
                                return true;
                            }
                            index++;
                        }

                        index = map.count + 1;
                        currentKey = default (TKey);
                        return false;
                    }

                    public TKey Current {
                        get {
                            return currentKey;
                        }
                    }

                    Object System.Collections.IEnumerator.Current {
                        get {
                            if (index == 0 || (index == map.count + 1)) {
                                throw new InvalidOperationException ();
                            }

                            return currentKey;
                        }
                    }

                    void System.Collections.IEnumerator.Reset () {
                        index = 0;
                        currentKey = default (TKey);
                    }
                }

            }

            [System.Diagnostics.DebuggerDisplay ("Count = {Count}")]
            [Serializable]
            public sealed class ValueCollection : ICollection<TValue>,
            ICollection,
            IReadOnlyCollection<TValue> {
                private HeapMap<TKey, TValue> map;

                public ValueCollection (HeapMap<TKey, TValue> map) {
                    if (map == null) {
                        throw new ArgumentNullException ();
                    }
                    this.map = map;
                }

                public Enumerator GetEnumerator () {
                    return new Enumerator (map);
                }

                public void CopyTo (TValue[] array, int index) {
                    if (array == null) {
                        throw new ArgumentNullException ();
                    }

                    if (index < 0 || index > array.Length) {
                        throw new ArgumentOutOfRangeException ();
                    }

                    if (array.Length - index < map.Count) {
                        throw new ArgumentOutOfRangeException ("Please choose a bigger one. Bigger one, one ");
                    }

                    int count = map.count;
                    Entry[] entries = map.entries;
                    for (int i = 0; i < count; i++) {
                        if (entries[i].hashCode >= 0) array[index++] = entries[i].value;
                    }
                }

                public int Count {
                    get { return map.Count; }
                }

                bool ICollection<TValue>.IsReadOnly {
                    get { return true; }
                }

                void ICollection<TValue>.Add (TValue item) {
                    throw new NotSupportedException ("no such function");
                }

                void ICollection<TValue>.Clear () {
                    throw new NotSupportedException ("no such function");
                }

                bool ICollection<TValue>.Contains (TValue item) {
                    return map.ContainsValue (item);
                }

                bool ICollection<TValue>.Remove (TValue item) {
                    throw new NotSupportedException ("no such function");
                }

                IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator () {
                    return new Enumerator (map);
                }

                IEnumerator IEnumerable.GetEnumerator () {
                    return new Enumerator (map);
                }

                void ICollection.CopyTo (Array array, int index) {
                    if (array == null) {
                        throw new ArgumentNullException ();
                    }

                    if (index < 0 || index > array.Length) {
                        throw new ArgumentOutOfRangeException ();
                    }

                    if (array.Length - index < map.Count) {
                        throw new ArgumentOutOfRangeException ("Please choose a bigger one. Bigger one, one ");
                    }

                    if (array.Rank != 1) {
                        throw new ArgumentException ("Arg_RankMultiDimNotSupported");
                    }

                    if (array.GetLowerBound (0) != 0) {
                        throw new ArgumentException ("Arg_NonZeroLowerBound");
                    }

                    TValue[] values = array as TValue[];
                    if (values != null) {
                        CopyTo (values, index);
                    } else {
                        object[] objects = array as object[];
                        if (objects == null) {
                            throw new InvalidCastException ();
                        }

                        int count = map.count;
                        Entry[] entries = map.entries;
                        try {
                            for (int i = 0; i < count; i++) {
                                if (entries[i].hashCode >= 0) objects[index++] = entries[i].value;
                            }
                        } catch (ArrayTypeMismatchException) {
                            throw new ArgumentException ("Argument_InvalidArrayType");
                        }
                    }
                }

                bool ICollection.IsSynchronized {
                    get { return false; }
                }

                Object ICollection.SyncRoot {
                    get { return ((ICollection) map).SyncRoot; }
                }

                [Serializable]
                public struct Enumerator : IEnumerator<TValue>,
                System.Collections.IEnumerator {
                    private HeapMap<TKey, TValue> map;
                    private int index;
                    private TValue currentKey;

                    internal Enumerator (HeapMap<TKey, TValue> map) {
                        this.map = map;
                        index = 0;
                        currentKey = default (TValue);
                    }

                    public void Dispose () { }

                    public bool MoveNext () {
                        while ((uint) index < (uint) map.count) {
                            if (map.entries[index].hashCode >= 0) {
                                currentKey = map.entries[index].value;
                                index++;
                                return true;
                            }
                            index++;
                        }

                        index = map.count + 1;
                        currentKey = default (TValue);
                        return false;
                    }

                    public TValue Current {
                        get {
                            return currentKey;
                        }
                    }

                    Object System.Collections.IEnumerator.Current {
                        get {
                            if (index == 0 || (index == map.count + 1)) {
                                throw new InvalidOperationException ();
                            }

                            return currentKey;
                        }
                    }

                    void System.Collections.IEnumerator.Reset () {
                        index = 0;
                        currentKey = default (TValue);
                    }
                }

            }

        }

    #endregion

    public class MaxHeap<TKey, TValue> : HeapMap<TKey, TValue>
        where TKey : IComparable, IComparable<TKey>
        where TValue : IComparable<TValue> {
            public MaxHeap (int n) : base (n) { }

            public MaxHeap () : base () { }

            protected override int Compare (TValue item1, TValue item2) {
                return item1.CompareTo (item2);
            }
        }

    internal static class HashHelpers {

#if FEATURE_RANDOMIZED_STRING_HASHING
        public const int HashCollisionThreshold = 100;
        public static bool s_UseRandomizedStringHashing = String.UseRandomizedHashing ();
#endif

        // Table of prime numbers to use as hash table sizes. 
        // A typical resize algorithm would pick the smallest prime number in this array
        // that is larger than twice the previous capacity. 
        // Suppose our Hashtable currently has capacity x and enough elements are added 
        // such that a resize needs to occur. Resizing first computes 2x then finds the 
        // first prime in the table greater than 2x, i.e. if primes are ordered 
        // p_1, p_2, ..., p_i, ..., it finds p_n such that p_n-1 < 2x < p_n. 
        // Doubling is important for preserving the asymptotic complexity of the 
        // hashtable operations such as add.  Having a prime guarantees that double 
        // hashing does not lead to infinite loops.  IE, your hash function will be 
        // h1(key) + i*h2(key), 0 <= i < size.  h2 and the size must be relatively prime.
        public static readonly int[] primes = {
            3,
            7,
            11,
            17,
            23,
            29,
            37,
            47,
            59,
            71,
            89,
            107,
            131,
            163,
            197,
            239,
            293,
            353,
            431,
            521,
            631,
            761,
            919,
            1103,
            1327,
            1597,
            1931,
            2333,
            2801,
            3371,
            4049,
            4861,
            5839,
            7013,
            8419,
            10103,
            12143,
            14591,
            17519,
            21023,
            25229,
            30293,
            36353,
            43627,
            52361,
            62851,
            75431,
            90523,
            108631,
            130363,
            156437,
            187751,
            225307,
            270371,
            324449,
            389357,
            467237,
            560689,
            672827,
            807403,
            968897,
            1162687,
            1395263,
            1674319,
            2009191,
            2411033,
            2893249,
            3471899,
            4166287,
            4999559,
            5999471,
            7199369
        };

        public static bool IsPrime (int candidate) {
            if ((candidate & 1) != 0) {
                int limit = (int) Math.Sqrt (candidate);
                for (int divisor = 3; divisor <= limit; divisor += 2) {
                    if ((candidate % divisor) == 0)
                        return false;
                }
                return true;
            }
            return (candidate == 2);
        }

        public static int GetPrime (int min) {
            if (min < 0)
                throw new ArgumentException ("Arg_HTCapacityOverflow");

            for (int i = 0; i < primes.Length; i++) {
                int prime = primes[i];
                if (prime >= min) return prime;
            }

            //outside of our predefined table. 
            //compute the hard way. 
            for (int i = (min | 1); i < Int32.MaxValue; i += 2) {
                //if (IsPrime(i) && ((i - 1) % Hashtable.HashPrime != 0))
                if (IsPrime (i))
                    return i;
            }
            return min;
        }

        public static int GetMinPrime () {
            return primes[0];
        }

        // Returns size of hashtable to grow to.
        public static int ExpandPrime (int oldSize) {
            int newSize = 2 * oldSize;

            // Allow the hashtables to grow to maximum possible size (~2G elements) before encoutering capacity overflow.
            // Note that this check works even when _items.Length overflowed thanks to the (uint) cast
            if ((uint) newSize > MaxPrimeArrayLength && MaxPrimeArrayLength > oldSize) {
                //Contract.Assert(MaxPrimeArrayLength == GetPrime(MaxPrimeArrayLength), "Invalid MaxPrimeArrayLength");
                GetPrime (MaxPrimeArrayLength);
                return MaxPrimeArrayLength;
            }

            return GetPrime (newSize);
        }

        // This is the maximum prime smaller than Array.MaxArrayLength
        public const int MaxPrimeArrayLength = 0x7FEFFFFD;

#if FEATURE_RANDOMIZED_STRING_HASHING
        public static bool IsWellKnownEqualityComparer (object comparer) {
            return (comparer == null || comparer == System.Collections.Generic.EqualityComparer<string>.Default || comparer is IWellKnownStringEqualityComparer);
        }

        public static IEqualityComparer GetRandomizedEqualityComparer (object comparer) {
            Contract.Assert (comparer == null || comparer == System.Collections.Generic.EqualityComparer<string>.Default || comparer is IWellKnownStringEqualityComparer);

            if (comparer == null) {
                return new System.Collections.Generic.RandomizedObjectEqualityComparer ();
            }

            if (comparer == System.Collections.Generic.EqualityComparer<string>.Default) {
                return new System.Collections.Generic.RandomizedStringEqualityComparer ();
            }

            IWellKnownStringEqualityComparer cmp = comparer as IWellKnownStringEqualityComparer;

            if (cmp != null) {
                return cmp.GetRandomizedEqualityComparer ();
            }

            Contract.Assert (false, "Missing case in GetRandomizedEqualityComparer!");

            return null;
        }

        public static object GetEqualityComparerForSerialization (object comparer) {
            if (comparer == null) {
                return null;
            }

            IWellKnownStringEqualityComparer cmp = comparer as IWellKnownStringEqualityComparer;

            if (cmp != null) {
                return cmp.GetEqualityComparerForSerialization ();
            }

            return comparer;
        }

        private const int bufferSize = 1024;
        private static RandomNumberGenerator rng;
        private static byte[] data;
        private static int currentIndex = bufferSize;
        private static readonly object lockObj = new Object ();

        internal static long GetEntropy () {
            lock (lockObj) {
                long ret;

                if (currentIndex == bufferSize) {
                    if (null == rng) {
                        rng = RandomNumberGenerator.Create ();
                        data = new byte[bufferSize];
                        Contract.Assert (bufferSize % 8 == 0, "We increment our current index by 8, so our buffer size must be a multiple of 8");
                    }

                    rng.GetBytes (data);
                    currentIndex = 0;
                }

                ret = BitConverter.ToInt64 (data, currentIndex);
                currentIndex += 8;

                return ret;
            }
        }
#endif // FEATURE_RANDOMIZED_STRING_HASHING

    }
}