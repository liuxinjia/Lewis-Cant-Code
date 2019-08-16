using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class Program {
    public static void Main () {
        var stack = new Stack<int> ();
        int[][] buildings = {
            new int[] { 2, 9, 10 },
            new int[] { 3, 7, 15 },
            new int[] { 5, 12, 12 },
            new int[] { 15, 20, 10 },
            new int[] { 19, 24, 8 }
        };

        MaxHeap<int> maxHeap = new MaxHeap<int> ();

        maxHeap.Push (0);
        maxHeap.Push (20);
        maxHeap.Push (10);
        maxHeap.Push (40);
        maxHeap.Push (60);
        var list = new List<int> ();
        Console.WriteLine ("The 1st:");
        foreach (var item in maxHeap) {
            Console.Write ("{0} ", item);
        }
        Console.WriteLine ();
        maxHeap.Pop ();
        maxHeap.Pop ();
        maxHeap.Pop ();
        Console.WriteLine ("The 2nd:");
        list = maxHeap.ToAscendingList ();
        foreach (var item in list) {
            Console.Write ("{0} ", item);
        }
        Console.WriteLine ();

    }
}

/* #region  Heap */

public abstract class Heap<T> : IEnumerable
where T : IComparable<T> {
    protected int _defaulCapcity = 4;
    protected T[] _array;
    protected int _lastPos;
    Object _syncRoot;

    static T[] EMPTYARRAY = new T[0];

    public Heap () {
        _lastPos = 0;
        _array = EMPTYARRAY;
    }

    public Heap (int n) {
        //Maybe other exception
        if (n < 0) throw new InvalidCastException ();
        _array = new T[n];
    }

    public Heap (IEnumerable<T> collection) {
        if (collection == null)
            throw new NullReferenceException ();

        var c = collection as ICollection<T>;
        if (c != null) {
            int count = c.Count;
            _array = new T[count];
            c.CopyTo (_array, 0);
            _lastPos = count;
        } else {
            _lastPos = 0;
            _array = new T[_defaulCapcity];

            using (var en = collection.GetEnumerator ()) {
                while (en.MoveNext ()) {
                    Push (en.Current);
                }
            }
        }
    }

    public int Count => _lastPos;

    //Something wrong here
    //bool ICollection.IsSynchronized => false;

    //returns an object can be used to synchronize access to the ICollection
    public object SyncRoot {
        get {
            if (_syncRoot == null) {
                System.Threading.Interlocked.CompareExchange<Object> (ref _syncRoot, new Object (), null);
            }
            return _syncRoot;
        }
    }

    public bool IsEmpty => _lastPos == 0;

    public void Clear () {
        Array.Clear (_array, 0, _array.Length);
        _lastPos = 0;
    }

    public bool Contains (T item) {
        for (int i = 0; i < _lastPos; i++) {
            if (((Object) item) == null) {
                if (((Object) _array[i]) == null)
                    return true;
            } else if (_array[i].Equals (item)) {
                return true;
            }
        }

        return false;
    }

    public IEnumerator GetEnumerator () {
        return new Enumerator (this);
    }

    //IEnumerator<T> IEnumerable<T>.GetEnumerator()
    //{
    //    return new Enumerator(this);
    //}

    //remove extra capcity if less than a threshold
    public void TrimExcess () {
        int threshold = (int) ((double) _array.Length * 0.9);
        if (_lastPos < threshold) {
            var newarray = new T[_lastPos];
            Array.Copy (_array, 0, newarray, 0, _lastPos);
            _array = newarray;
        }
    }

    //arrayIndex = array's  copy startign index
    protected void CopyTo (T[] array, int arrayIndex = 0) {
        if (array == null) throw new NullReferenceException ();

        if (arrayIndex < 0 || arrayIndex > array.Length)
            throw new IndexOutOfRangeException ();

        //it should be out of range excetption
        if (array.Length - arrayIndex < _lastPos)
            throw new IndexOutOfRangeException ();

        Array.Copy (_array, 0, array, arrayIndex, _lastPos);
    }

    public T[] ToArray () {
        var returnArray = new T[_lastPos];
        CopyTo (returnArray);
        return returnArray;
    }

    public List<T> ToList () {
        return ToArray ().ToList ();
    }

    protected T Peek () {
        if (IsEmpty) throw new IndexOutOfRangeException ();
        return _array[0];
    }

    public void Push (T obj) {
        if (_lastPos == _array.Length) {
            var newArray = new T[(_array.Length == 0) ? _defaulCapcity : 2 * _array.Length];
            Array.Copy (_array, 0, newArray, 0, _lastPos);
            _array = newArray;
        }
        _array[_lastPos] = obj;
        TrickleUp (_lastPos++);
    }

    public T Remove (int pos) {
        if (pos >= _lastPos || IsEmpty) throw new IndexOutOfRangeException ();
        var temp = _array[pos];

        Swap (pos, --_lastPos);
        int parent = (pos - 1) / 2;
        if (parent >= 0 && _array[pos].CompareTo (_array[parent]) > 0)
            TrickleUp (pos);
        else
            TrickleDown (pos);

        return temp;
    }

    public T Pop () {
        if (IsEmpty) throw new IndexOutOfRangeException ();
        return Remove (0);
    }

    protected void Sort () {
        int goBack = this._lastPos;
        while (this._lastPos > 0) {
            Remove (0);
        }
        this._lastPos = goBack;
    }

    void TrickleUp (int pos) {
        while (true) {
            int parent = (pos - 1) / 2;
            if (parent >= 0 && _array[pos].CompareTo (_array[parent]) > 0) {
                Swap (parent, pos);
                pos = parent;
            } else {
                return;
            }
        }
    }

    void TrickleDown (int parent) {
        while (true) {
            int left = parent * 2 + 1;
            int right = parent * 2 + 2;

            if (left < _lastPos) {
                int swapIndex = left;

                if (right < _lastPos && _array[left].CompareTo (_array[right]) < 0) {
                    swapIndex = right;
                }

                if (_array[parent].CompareTo (_array[swapIndex]) < 0) {
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

    void Swap (int i, int j) {
        var temp = _array[i];
        _array[i] = _array[j];
        _array[j] = temp;
    }

    protected abstract bool Compare (T item1, T item2);

    public struct Enumerator : IEnumerator<T>,
    System.Collections.IEnumerator {
        Heap<T> _heap;
        T currentElement;
        int _index;

        internal Enumerator (Heap<T> heap) {
            _heap = heap;
            currentElement = default (T);
            _index = -2;
        }

        public T Current =>
            throw new NotImplementedException ();

        object IEnumerator.Current {
            get {
                //enumNotStarted
                if (_index == -2) throw new NullReferenceException ();
                //enumEnded
                if (_index == -1) throw new IndexOutOfRangeException ();
                return currentElement;
            }
        }
        public void Dispose () {
            _index = -1;
        }

        bool IEnumerator.MoveNext () {
            bool retVal;
            //first call to enumerator         
            if (_index == -2) {
                _index = _heap._lastPos - 1;
                retVal = (_index >= 0);
                if (retVal)
                    currentElement = _heap._array[_index];
                return retVal;
            }
            if (_index == -1) return false;

            retVal = (--_index >= 0);
            if (retVal)
                currentElement = _heap._array[_index];
            else
                currentElement = default (T);

            return retVal;
        }

        void IEnumerator.Reset () {
            _index = -2;
            currentElement = default (T);
        }
    }

}

public class MinHeap<T> : Heap<T> where T : IComparable<T> {
    public T Min {
        get => Peek ();
    }

    protected override bool Compare (T item1, T item2) {
        return item1.CompareTo (item2) < 0;
    }

    public List<T> ToDescendingList () {
        Sort ();

        var returnArray = new T[_lastPos];
        CopyTo (returnArray);
        return returnArray.ToList ();

    }

    public List<T> ToAscedngingList () {
        Sort ();

        var returnArray = new T[_lastPos];
        CopyTo (returnArray);
        returnArray.Reverse ();
        return returnArray.ToList ();
    }

}

public class MaxHeap<T> : Heap<T> where T : IComparable<T> {
    public T Max => Peek ();

    protected override bool Compare (T item1, T item2) {
        return item2.CompareTo (item1) < 0;
    }

    public List<T> ToAscendingList () {
        return ToAscedningList ().ToList ();
    }

    public T[] ToAscendingArray () {
        Sort ();

        var returnArray = new T[_lastPos];
        CopyTo (returnArray);
        return returnArray;
    }

    public T[] ToDescendingArray () {
        var returnArray = ToAscendingArray ();
        returnArray.Reverse ();
        return returnArray;
    }

    public List<T> ToAscedningList () {
        return ToDescendingArray ().ToList ();
    }

}