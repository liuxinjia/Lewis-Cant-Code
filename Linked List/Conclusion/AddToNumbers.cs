/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
using System;

public class Solution
{
    public ListNode AddTwoNumbers_Reverse(ListNode l1, ListNode l2)
    {
        int rLength1, rLength2;
        ListNode rNode1 = ReverseList(l1);
        ListNode rNode2 = ReverseList(l2);


        ListNode sumNode = new ListNode(0);
        ListNode returnNode = sumNode;

        int decimalOne = 0;
        while (rNode1 != null && rNode2 != null)
        {
            int total = rNode1.val + rNode2.val + decimalOne;

            if (total > 9)
            {
                total -= 10;
                decimalOne = 1;
            }
            else
            {
                decimalOne = 0;
            }

            sumNode.next = new ListNode(total);
            //fixed01
            sumNode = sumNode.next;

            //fixed01
            rNode1 = rNode1.next;
            rNode2 = rNode2.next;
        }

        while (rNode1 != null)
        {
            int total = rNode1.val + decimalOne;
            sumNode.next = new ListNode(total);
            //fixed01
            sumNode = sumNode.next;

            decimalOne = 0;
            rNode1 = rNode1.next;
        }
        while (rNode2 != null)
        {
            int total = rNode2.val + decimalOne;
            sumNode.next = new ListNode(total);
            //fixed01
            sumNode = sumNode.next;

            decimalOne = 0;
            rNode2 = rNode2.next;
        }

        return ReverseList(returnNode.next);
    }
    public ListNode ReverseList(ListNode node, ref int length)
    {
        ListNode prev = null;

        while (node != null)
        {
            var next = node.next;

            node.next = prev;
            prev = node;
            node = next;
            length++;
        }

        return prev;
    }

    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {

        ListNode startNode = new ListNode(-1);
        ListNode returnNode = startNode;

        int decimalOne = 0;
        ListNode prevNode = new ListNode(-1);
        while (l1 != null && l2 != null)
        {
            int total = l1.val + l2.val + decimalOne;
            decimalOne = 0;
            if (total > 9)
            {
                total -= 10;
                decimalOne = 1;
            }

            var tempNode = new ListNode(total);
            startNode.next = tempNode;
            startNode = startNode.next;

            //fixed1
            if (l1.next != null)
                prevNode = l1;
            if (l2.next != null)
                prevNode = l2;

            l1 = l1.next;
            l2 = l2.next;

        }
        if (l1 != null || l2 != null) prevNode = prevNode.next;
        else prevNode = new ListNode(-1);
        while (prevNode != null && prevNode.val != -1)
        {
            if (decimalOne == 1)
            {
                int total = prevNode.val + decimalOne;
                decimalOne = 0;
                if (total > 9)
                {
                    total -= 10;
                    decimalOne = 1;
                }

                var tempNode = new ListNode(total);
                startNode.next = tempNode;
                startNode = startNode.next;

                prevNode = prevNode.next;
            }
            else
            {
                startNode.next = prevNode;
                startNode = startNode.next;
                break;
            }
        }

        if (decimalOne == 1)
        {
            var tempNode = new ListNode(decimalOne);
            startNode.next = tempNode;
        }

        return returnNode.next;

    }

    public ListNode AddTwoNumbers2(ListNode l1, ListNode l2)
    {
        ListNode startNode = new ListNode(-1);
        ListNode returnNode = startNode;

        int decimalOne = 0;
        while (l1 != null && l2 != null)
        {
            int total = l1.val + l2.val + decimalOne;
            decimalOne = 0;
            if (total > 9)
            {
                total -= 10;
                decimalOne = 1;
            }

            var tempNode = new ListNode(total);
            startNode.next = tempNode;
            startNode = startNode.next;

            l1 = l1.next;
            l2 = l2.next;

        }

        while (l1 != null)
        {
            if (decimalOne == 1)
            {
                int total = decimalOne + l1.val;
                decimalOne = 0;
                if (total > 9)
                {
                    total = total - 10;
                    decimalOne = 1;
                }
                startNode.next = new ListNode(total);
                startNode = startNode.next;

                l1 = l1.next;

            }
            else
            {
                startNode.next = l1;
                startNode = startNode.next;
                break;
            }
        }

        while (l2 != null)
        {
            if (decimalOne == 1)
            {
                int total = decimalOne + l2.val;
                decimalOne = 0;
                if (total > 9)
                {
                    total = total - 10;
                    decimalOne = 1;
                }
                startNode.next = new ListNode(total);
                startNode = startNode.next;

                l2 = l2.next;
            }
            else
            {
                startNode.next = l2;
                startNode = startNode.next;
                break;
            }
        }

        if (decimalOne == 1)
        {
            var tempNode = new ListNode(decimalOne);
            startNode.next = tempNode;
        }

        return returnNode.next;

    }

    public ListNode AddTwoNumbers3(ListNode l1, ListNode l2)
    {
        ListNode startNode = new ListNode(-1);
        ListNode returnNode = startNode;

        int sum = 0;
        while (l1 != null || l2 != null)
        {
            sum /= 10;
            if (l1 != null)
            {
                sum += l1.val;
                l1 = l1.next;
            }
            if (l2 != null)
            {
                sum += l2.val;
                l2 = l2.next;
            }
            startNode.next = new ListNode(sum%10);
            startNode = startNode.next;
        }

        if (sum / 10 == 1)
            startNode.next = new ListNode(1);

        return returnNode.next;
    }
}