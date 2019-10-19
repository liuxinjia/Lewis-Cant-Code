using System;
using System.Collections;
using System.Collections.Generic;
using Cr7Sund_DS;

class Program {
    public static void Main () {
        var stack = new Stack<int> ();
        int[][] buildings = {
            new int[] { 2, 92, 10 },
            new int[] { 3, 7, 15 },
            new int[] { 5, 12, 12 },
            new int[] { 15, 20, 10 },
            new int[] { 19, 24, 8 }
        };

        var maxHeap = new MaxHeap<Node> ();
        for (int i = 1; i < 13; i++)
            maxHeap.Push (new Node (i));

        Console.WriteLine ("The 1st:");
        foreach (var item in maxHeap) {
            Console.Write ("{0} ", item.val);
        }
        Console.WriteLine ();

        Console.WriteLine ("The asscending:");
        var list = new List<Node> (maxHeap.Sort ());
        int prev = -1;

        foreach (var item in list) {
            prev = item.val;
            Console.Write ("{0} ", item.val);
        }
        Console.WriteLine ();

        Console.WriteLine ("The descending:");
        list = new List<Node> (maxHeap.Sort (true));
        prev = list[0].val;
        foreach (var item in list) {
            prev = item.val;
            Console.Write ("{0} ", item.val);
        }
        Console.WriteLine ();

    }

    class Node : IComparable<Node> {
        public int val;
        public int index;
        public readonly int prim;

        public Node (int _val, int _index = 0, int _prim = 0) {
            val = _val;
            index = _index;
            prim = _prim;
        }

        public int CompareTo (Node other) {
            return val.CompareTo (other.val);
        }
    }
}

namespace Cr7Sund_DS {
    using System.Collections.Generic;
    using System.Collections;
    using System;

    public class MinHeap<T> : Heap<T>
        where T : IComparable<T> {
            public MinHeap (int size) : base (size) {

            }

            public MinHeap () : base () {

            }

            public MinHeap (IEnumerable<T> collection) : base () {

            }

            protected override int Compare (T item, T other) {
                return other.CompareTo (item);
            }

            public MinHeap (int size, float growFactor) : base (size, growFactor) {

            }
        }

    public class MaxHeap<T> : Heap<T>
        where T : IComparable<T> {
            public MaxHeap (int size) : base (size) {

            }

            public MaxHeap () : base () {

            }

            public MaxHeap (IEnumerable<T> collection) : base () {

            }

            public MaxHeap (int size, float growFactor) : base (size, growFactor) {

            }

            protected override int Compare (T item, T other) {
                return item.CompareTo (other);
            }
        }

    public abstract class Heap<T> : ICollection<T>, ICollection
    where T : IComparable<T> {

        const int _defaluCapcity = 32;

        protected T[] _array;
        protected int _lastPos;
        int _version;
        int _growFactor; // 100 = 1.0, 130 = 1.3, 200 = 2.0
        Object _syncRoot;

        public int Count => _lastPos + 1;
        public bool IsReadOnly => false;
        public T Top => Empty () ?
        throw new IndexOutOfRangeException () : _array[0];

        //Be Cautions, it maybe lead to error in multiple threading
        //Did not be testify in real project
        public bool IsSynchronized => true;

        public object SyncRoot {
            get {
                if (_syncRoot == null) {
                    System.Threading.Interlocked.CompareExchange<Object> (
                        ref _syncRoot, new Object (), null);
                }
                return _syncRoot;
            }
        }

        protected abstract int Compare (T item, T other);

        #region
        protected Heap () : this (32, (float) 2.0) { }

        protected Heap (int size) : this (size, (float) 2.0) { }
        protected Heap (int capcity, float growFactor) {
            if (capcity < 0) {
                throw new ArgumentOutOfRangeException ("capacity", "ArgumentOutOfRange_NeedNonNegNum");
            }

            if (!(growFactor >= 1.0 && growFactor <= 10.0)) {
                throw new ArgumentOutOfRangeException ("growFactor", "ArgumentOutOfRange_QueueGrowFactor： 1, 10");
            }

            _growFactor = (int) (growFactor * 100);
            _array = new T[capcity];
            _lastPos = -1;
            _version = 0;
        }
        protected Heap (ICollection<T> col) : this ((col == null ? 32 : col.Count)) {
            if (col == null)
                throw new ArgumentNullException ("colllection");
            IEnumerator en = col.GetEnumerator ();
            while (en.MoveNext ())
                Add ((T) en.Current);
        }

        protected Heap (IEnumerable<T> collection) {
            if (collection == null)
                throw new ArgumentNullException ();

            var c = collection as ICollection<T>;
            if (c != null) {
                int count = c.Count;
                _array = new T[count];
                c.CopyTo (_array, 0);
                _lastPos = count - 1;
            } else {
                _array = new T[_defaluCapcity];

                using (var en = collection.GetEnumerator ()) {
                    while (en.MoveNext ()) {
                        Add (en.Current);
                    }
                }
            }

            _growFactor = 200;
            _lastPos = -1;
            _version = 0;
        }

        #endregion

        public bool Empty () => _lastPos < 0 || _array.Length == 0;

        bool Full () => _lastPos == _array.Length;

        #region Implement  interface 

        public void Clear () {
            Array.Clear (_array, 0, _array.Length);
            _lastPos = -1;
            _version++;
        }

        //Don't suggested , Cost O(n)time
        public bool Contains (T item) {
            if ((Object) item == null) return false;
            for (int i = 0; i < Count; i++) {
                if (_array[i].Equals (item)) {
                    return true;
                }
            }

            return false;
        }

        //implementing ICollection<T>
        public void CopyTo (T[] array, int startIndex = 0) {
            if (array == null) throw new NullReferenceException ();
            if (Empty ()) throw new IndexOutOfRangeException ("Empty heap");

            if (startIndex < 0 || startIndex > array.Length)
                throw new IndexOutOfRangeException ();

            //it should be out of range excetption
            if (array.Length - startIndex < Count)
                throw new IndexOutOfRangeException ("the left capcity should be larger than copying size ");

            Array.Copy (_array, 0, array, startIndex, Count);
        }

        // implement ICollection
        // Damn it no generics
        public void CopyTo (Array array, int index) {
            _array.CopyTo (array, index);
        }

        public void TrimExcess () {
            int threshold = (int) ((double) _array.Length * 0.9);
            if (Count < threshold) {
                var newarray = new T[Count];
                Array.Copy (_array, 0, newarray, 0, Count);
                _array = newarray;
            }

            _version++;
        }

        public void Add (T item) {
            ++_lastPos;
            if (Full ()) {
                int newcapacity =
                    _array.Length * (int) ((long) _array.Length * (long) _growFactor / 100);
                var newArray = new T[newcapacity];

                //list should use linq.Clone
                //be careful the size
                Array.Copy (_array, 0, newArray, 0, _lastPos);
                _array = newArray;
            }
            _array[_lastPos] = item;
            TrickleUp ();

            _version++;
        }

        public T Remove (int index) {
            if (index > _lastPos || Empty ()) throw new IndexOutOfRangeException ();
            var temp = _array[index];

            Swap (_lastPos--, index);
            int parent = (index - 1) / 2;
            if (parent >= 0 && Compare (_array[index], _array[parent]) > 0)
                TrickleUp (index);
            else
                TrickleDown (index);

            _version++;

            return temp;
        }

        // for implementing ICollection
        // But not recommend for it
        public bool Remove (T item) {
            if (Empty ()) throw new IndexOutOfRangeException ();
            if ((Object) item == null) return false;
            for (int i = 0; i <= _lastPos; i++) {
                if (_array[i].Equals (item)) {
                    Remove (i);
                    return true;
                }
            }

            return false;
        }

        // implement Enumerator at last
        //we speperate it from the above

        #endregion

        #region Custom methos

        public T[] ToArray () {
            var returnArray = new T[Count];
            CopyTo (returnArray);
            return returnArray;
        }

        //should use linq, so  decided  by yourself
        //-------------------------------
        // public List<T> ToList () {
        //     // return ToArray ().ToList ();
        // }

        #endregion

        #region Heap operation

        public void Push (T item) {
            Add (item);
        }

        public T Pop () {
            if (Empty ()) throw new IndexOutOfRangeException ("No items left");
            T top = _array[0];
            if (_lastPos == -1) return top;

            Swap (_lastPos--, 0);
            TrickleDown ();

            _version++;
            return top;
        }

        //it's more fast and cost less space
        //but only used in only one operation
        //because it does harm to next heap operation
        //recommanded Sort(bool)
        protected T[] SortOnlyOnce (bool isReversed) {
            T[] array = new T[Count];
            int size = this._lastPos;
            for (int i = 0; i <= size; i++) {
                T val = Pop ();
                if (isReversed) array[size - i] = val;
                else array[i] = val;
            }
            return array;
        }

        protected T[] ReverseSort () {
            var queue = new Queue<T> ();
            int goBack = this._lastPos;
            while (this._lastPos >= 0) {
                queue.Enqueue (Pop ());
            }
            this._lastPos = goBack;

            var newArray = new T[goBack + 1];
            int i = -1;
            while (queue.Count > 0) {
                newArray[++i] = queue.Dequeue ();
            }

            return newArray;
        }

        public T[] Sort (bool IsReverse = false) {
            var resultArray = ReverseSort ();
            // Why we need a new Array?
            var returnArray = new T[Count];
            if (IsReverse) {
                // 1. avoiding changing the newArray because of reference type
                Array.Copy (resultArray, 0, returnArray, 0, Count);
            } else {
                // 2. trimming the redundent null elements
                CopyTo (returnArray);
            }
            //go back to keep heap after sort
            //if not, next any heap operation will be cost a lot time 
            //Array.Copy(resultArray, 0, _array, 0, Count);
            _array = resultArray;

            return returnArray;
        }

        void TrickleUp () {
            int index = _lastPos;
            while (index > 0) {
                int parentIndex = (index - 1) / 2;
                if (Compare (_array[index], _array[parentIndex]) <= 0) break;

                Swap (index, parentIndex);
                index = parentIndex;
            }
        }

        void TrickleUp (int index) {
            while (index > 0) {
                int parentIndex = (index - 1) / 2;
                if (Compare (_array[index], _array[parentIndex]) <= 0) break;

                Swap (index, parentIndex);
                index = parentIndex;
            }
        }

        void TrickleDown (int index = 0) {
            while (index >= 0) {
                int leftIndex = (index * 2) + 1;
                int rightIndex = (index * 2) + 2;

                if (leftIndex <= _lastPos) {
                    int maxIndex = rightIndex <= _lastPos && Compare (_array[leftIndex], _array[rightIndex]) < 0 ?
                        rightIndex : leftIndex;
                    if (Compare (_array[maxIndex], _array[index]) <= 0) break;
                    Swap (maxIndex, index);
                    index = maxIndex;
                } else {
                    break;
                }
            }
        }

        //undo deletion if you not trim the arrya before
        void UnDoDelete (int steps) {
            steps = Math.Min (steps, _array.Length - Count);
            for (int i = steps; i > 0; i--) {
                Add (_array[_lastPos + i]);
            }
        }

        void Swap (int a, int b) {
            T temp = _array[a];
            _array[a] = _array[b];
            _array[b] = temp;
        }
        #endregion

        #region IEnumeator

        // Implement IEnumerator<T>
        public IEnumerator<T> GetEnumerator () {
            //return ((IEnumerable<T>)_array).GetEnumerator();
            return new Enumerator (this);
        }

        // Implement IEnumerator
        IEnumerator IEnumerable.GetEnumerator () {
            //return _array.GetEnumerator();
            return new Enumerator (this);
        }

        // Implements an enumerator for a Heap. The enumerator uses the 
        // internal version number of the list to ensure that no modificatins are
        // made to the list while en enumeration is in progress
        struct Enumerator : IEnumerator<T>,
        IEnumerator {
            Heap<T> _heap;
            T _current;
            int _index;
            int _version;

            internal Enumerator (Heap<T> heap) {
                _heap = heap;
                _current = default (T);
                _index = -2;
                _version = heap._version;
                if (heap.Count == 0)
                    _index = -1;
            }

            public T Current {
                get {
                    //enumNotStarted
                    if (_index == -2) throw new NullReferenceException ();
                    //enumEnded
                    if (_index == -1) throw new IndexOutOfRangeException ("Empty Heap");
                    return _current;
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose () => _index = -1;

            public bool MoveNext () {
                if (_version != _heap._version) throw new InvalidOperationException ("EnumFailedVersion: Can modified in iterating");

                bool retVal;
                //first call to enumerator         
                if (_index == -2) {
                    _index = _heap._lastPos;
                    retVal = (_index >= 0);
                    if (retVal) {
                        _current = _heap._array[_index];
                    }

                    return retVal;
                }
                if (_index == -1) return false;

                retVal = (--_index >= 0);
                if (retVal)
                    _current = _heap._array[_index];
                else
                    _current = default (T);

                return retVal;
            }

            public void Reset () {
                _index = -2;
                _current = default (T);
            }
        }

        #endregion

    }

}