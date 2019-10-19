public class Solution {
    class Node : IComparable<Node> {
        public int val;
        public int index;
        public readonly int prim;

        public Node (int _val, int _index, int _prim) {
            val = _val;
            index = _index;
            prim = _prim;
        }

        public int CompareTo (Node other) {
            return val.CompareTo (other.val);
        }
    }

    public int NthSuperUglyNumber (int n, int[] primes) {
        if (n < 1 || primes.Length < 1) return 0;

        int[] nums = new int[n];
        nums[0] = 1;
        var minHeap = new MinHeap<Node> (n);
        for (int i = 0; i < primes.Length; i++)
            minHeap.Add (new Node (primes[i], 1, primes[i]));

        int count = 0;

        for (int i = 1; i < n; i++) {
            nums[i] = minHeap.Top ().val;
            while (nums[i] == minHeap.Top ().val) {
                var min = minHeap.Remove ();
                minHeap.Add (new Node (
                    min.prim * nums[min.index],
                    min.index + 1, min.prim
                ));

                if (min.val > min.prim * nums[min.index]) count++;
            }

        }

        return nums[n - 1];
    }

    public class MinHeap<T> : FixedHeap<T>
        where T : IComparable<T> {
            public MinHeap (int size) : base (size) {

            }

            protected override int Compare (T item, T other) {
                return other.CompareTo (item);
            }
        }

    public abstract class FixedHeap<T>
        where T : IComparable<T> {
            protected IList<T> _list;
            protected int _lastPos = -1;

            //there is no auto-expanding methods here,
            //so there is no default constructor
            // -- -- -- -- -- -- -- -- -- -- --
            // public FixedHeap () => _array = new IList<T> ();

            protected FixedHeap (int size) => _list = new List<T> (size);

            public bool Empty () => _lastPos < 0 || _list.Count == 0;

            public T Top () => Empty () ?
            throw new IndexOutOfRangeException () : _list[0];
            bool Full () => _lastPos == _list.Count;

            protected abstract int Compare (T item, T other);

            public void Add (T item) {
                _lastPos++;
                if (Full ())
                    _list.Add (item);
                else
                    _list[_lastPos] = item;

                TrickleUp ();
            }

            public T Remove () {
                if (Empty ()) throw new IndexOutOfRangeException ("No items left");
                T top = _list[0];
                if (_lastPos == 0) return top;

                Swap (_lastPos--, 0);
                TrickleDown ();
                return top;
            }

            void TrickleUp () {
                int index = _lastPos;
                while (index > 0) {
                    int parentIndex = (index - 1) / 2;
                    //if (_list[index].Compare(_list[parentIndex]) < 0) break;
                    if (Compare (_list[index], _list[parentIndex]) <= 0) break;

                    Swap (index, parentIndex);
                    index = parentIndex;
                }
            }

            void TrickleUp (int index) {
                while (index > 0) {
                    int parentIndex = (index - 1) / 2;
                    if (Compare (_list[index], _list[parentIndex]) <= 0) break;
                    //if (_list[index].Compare(_list[parentIndex]) < 0) break;

                    Swap (index, parentIndex);
                    index = parentIndex;
                }
            }

            void TrickleDown (int index = 0) {
                while (index >= 0) {
                    int leftIndex = (index * 2) + 1;
                    int rightIndex = (index * 2) + 2;

                    if (leftIndex <= _lastPos) {
                        // T max = _list[left];
                        // if (right <= index) max == max.Compare (_list[right]) < 0 ? _list[right] : max;
                        int maxIndex = rightIndex <= _lastPos && Compare (_list[leftIndex], _list[rightIndex]) < 0 ?
                            rightIndex : leftIndex;
                        if (Compare (_list[maxIndex], _list[index]) <= 0) break;
                        Swap (maxIndex, index);
                        index = maxIndex;
                    } else {
                        break;
                    }
                }
            }

            void Swap (int a, int b) {
                T temp = _list[a];
                _list[a] = _list[b];
                _list[b] = temp;
            }
        }

}