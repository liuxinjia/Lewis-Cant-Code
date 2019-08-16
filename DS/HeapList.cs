public abstract class Heap<T> : IEnumerable
where T : IComparable<T> {
    protected List<T> items;
    protected int lastPos;
    protected int _defaulCapcity = 4;

    public Heap () {
        lastPos = 0;
        items = new List<T> (_defaulCapcity);
    }

    public bool IsEmpty => lastPos == 0;

    protected T Top {
        get {
            return IsEmpty ? default (T) : items[0];
        }
    }

    public void Push (T obj) {
        items.Add (obj);
        TrickleUp (lastPos++);
    }

    public T Remove (int pos) {
        if (pos >= lastPos || IsEmpty) throw new IndexOutOfRangeException ();
        var temp = items[pos];
        Swap (pos, --lastPos);
        int parent = (pos - 1) / 2;
        if (parent >= 0 && items[pos].CompareTo (items[parent]) > 0)
            TrickleUp (pos);
        else
            TrickleDown (pos);

        return temp;
    }

    public T Pop () {
        return Remove (0);
    }

    public T Peek () {
        return items[0];
    }

    protected void Sort () {
        int goBack = this.lastPos;
        while (this.lastPos > 0) {
            Remove (0);
        }
        this.lastPos = goBack;
    }

    void TrickleUp (int pos) {
        while (true) {
            int parent = (pos - 1) / 2;
            if (parent >= 0 && items[pos].CompareTo (items[parent]) > 0) {
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

            if (left < lastPos) {
                int swapIndex = left;

                if (right < lastPos && items[left].CompareTo (items[right]) < 0) {
                    swapIndex = right;
                }

                if (items[parent].CompareTo (items[swapIndex]) < 0) {
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
        var temp = items[i];
        items[i] = items[j];
        items[j] = temp;
    }

    protected abstract bool Compare (T item1, T item2);

    public IEnumerator GetEnumerator () {
        return new HeapEnum<T> (items.ToArray ());
    }
}

public class MinHeap<T> : Heap<T> where T : IComparable<T> {
    public T Min {
        get => Top;
    }

    protected override bool Compare (T item1, T item2) {
        return item1.CompareTo (item2) < 0;
    }

    public List<T> SortDescending () {
        Sort ();

        if (lastPos == items.Count)
            return items;
        else {
            var list = new List<T> ();
            for (int i = 0; i < lastPos; i++) {
                list.Add (items[i]);
            }
            return list;
        }
    }

    public List<T> SortAscednging () {
        Sort ();

        if (lastPos == items.Count) {
            items.Reverse ();
            return items;
        } else {
            var list = new List<T> ();
            for (int i = lastPos - 1; i >= 0; i--) {
                list.Add (items[i]);
            }
            return list;
        }
    }

}

public class MaxHeap<T> : Heap<T> where T : IComparable<T> {

    public T Max => Top;

    protected override bool Compare (T item1, T item2) {
        return item2.CompareTo (item1) < 0;
    }

    public List<T> SortAscending () {
        Sort ();

        if (lastPos == items.Count) {
            return items;
        } else {
            var list = new List<T> ();
            for (int i = 0; i < lastPos; i++) {
                list.Add (items[i]);
            }
            return list;
        }
    }

    public List<T> SortDescending () {
        Sort ();

        if (lastPos == items.Count) {
            items.Reverse ();
            return items;
        } else {
            var list = new List<T> ();
            for (int i = lastPos - 1; i >= 0; i--) {
                list.Add (items[i]);
            }
            return list;
        }
    }

}

public class HeapEnum<T> : IEnumerator<T>, IDisposable<T> {