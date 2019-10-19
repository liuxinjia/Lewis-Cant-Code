// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;

// //         //maxHeap.Sort();
// //         maxHeap.Pop ();
// //         maxHeap.Add (new HeapElement<int> (27));
// //         maxHeap.Add (new HeapElement<int> (17));
// //         maxHeap.Add (new HeapElement<int> (47));
// //         maxHeap.Add (new HeapElement<int> (37));
// //         maxHeap.Sort ();

// public class HeapElement<T> : IComparable<HeapElement<T>> where T : IComparable<T> {
//     public T x;
//     public T h;
//     public int id;

//     public HeapElement (T y, T x = default (T), int id = 0) {
//         this.x = x;
//         this.h = y;
//         this.id = id;
//     }

//     public int CompareTo (HeapElement<T> compareEvent) {
//         if (compareEvent == null) return 1;
//         //return Math.Abs(compareEvent.h).CompareTo(Math.Abs(h));
//         return h.CompareTo (compareEvent.h);
//     }
// }

// /* #region   */

// abstract class Heap<T> where T : IComparable<T> {
//     List<HeapElement<T>> itemList;
//     List<int> indexList;
//     int lastPos;

//     public Heap (int n) {
//         lastPos = 0;
//         //items = new T[n];
//         indexList = new List<int> (n);
//         itemList = new List<HeapElement<T>> (n);
//     }

//     public Heap () {
//         lastPos = 0;
//         indexList = new List<int> ();
//         itemList = new List<HeapElement<T>> ();
//     }

//     public bool IsEmpty => lastPos == 0;

//     protected HeapElement<T> Top () {
//         return (IsEmpty ? default (HeapElement<T>) : itemList[0]);
//     }

//     //the id can be different from the lize size
//     public void Add (HeapElement<T> obj, int id) {
//         itemList.Add (obj);
//         SwapItem (itemList.Count - 1, lastPos);
//         while (id >= indexList.Count) indexList.Add (indexList.Count);
//         indexList[id] = lastPos;
//         TrickleUp (lastPos++);
//     }

//     public void Add (HeapElement<T> obj) {
//         itemList.Add (obj);
//         SwapItem (itemList.Count - 1, lastPos);
//         obj.id = indexList.Count;
//         indexList.Add (indexList.Count);
//         TrickleUp (lastPos++);
//     }

//     public HeapElement<T> Remove (HeapElement<T> e) {
//         int pos = indexList[e.id];
//         var temp = itemList[pos];
//         SwapItem (pos, --lastPos);
//         SwapIndex (pos, lastPos);

//         int parent = (pos - 1) / 2;
//         if (parent >= 0 && Compare (itemList[pos], itemList[parent]))
//             TrickleUp (pos);
//         else
//             TrickleDown (pos);

//         return temp;
//     }

//     public HeapElement<T> Pop () {
//         var temp = itemList[0];
//         SwapItem (0, --lastPos);
//         SwapIndex (0, lastPos);
//         TrickleDown (0);
//         return temp;
//     }

//     //Return a sort list
//     //before sort you can't remove or pop
//     public List<HeapElement<T>> Sort () {
//         int goBack = this.lastPos;
//         while (this.lastPos > 0) {
//             Pop ();
//         }
//         this.lastPos = goBack;
//         if (itemList.Count > lastPos)
//             throw new InvalidProgramException ();
//         return itemList;
//     }

//     void TrickleUp (int pos) {
//         while (true) {
//             int parent = (pos - 1) / 2;
//             if (parent >= 0 && Compare (itemList[pos], itemList[parent])) {
//                 SwapIndex (parent, pos);
//                 SwapItem (parent, pos);
//                 pos = parent;
//             } else {
//                 return;
//             }
//         }
//     }

//     void TrickleDown (int parent) {
//         while (true) {
//             int left = parent * 2 + 1;
//             int right = parent * 2 + 2;

//             if (left < lastPos) {
//                 int swapIndex = left;

//                 if (right < lastPos && Compare (itemList[right], itemList[left])) {
//                     swapIndex = right;
//                 }

//                 if (Compare (itemList[swapIndex], itemList[parent])) {
//                     SwapIndex (swapIndex, parent);
//                     SwapItem (swapIndex, parent);
//                     parent = swapIndex;
//                 } else {
//                     return;
//                 }
//             } else {
//                 return;
//             }
//         }
//     }

//     void SwapItem (int index1, int index2) {
//         var temp = itemList[index1];
//         itemList[index1] = itemList[index2];
//         itemList[index2] = temp;

//     }

//     void SwapIndex (int index1, int index2) {
//         int tempIndex = indexList[itemList[index1].id];
//         indexList[itemList[index1].id] = indexList[itemList[index2].id];
//         indexList[itemList[index2].id] = tempIndex;

//     }

//     protected abstract bool Compare (HeapElement<T> item1, HeapElement<T> item2);

// }

// /* #endregion */

// public class MinHeap<T> : Heap<T> where T : IComparable<T> {
//     public MinHeap (int n) : base (n) { }

//     protected override bool Compare (HeapElement<T> item1, HeapElement<T> item2) {
//         return item1.CompareTo (item2) < 0;
//     }

//     public HeapElement<T> Min () {
//         return Top ();
//     }
// }

// public class MaxHeap<T> : Heap<T> where T : IComparable<T> {
//     public MaxHeap (int n) : base (n) { }

//     protected override bool Compare (HeapElement<T> item1, HeapElement<T> item2) {
//         return item2.CompareTo (item1) < 0;
//     }

//     public HeapElement<T> Max () {
//         return Top ();
//     }

// }