# Reverse Linked List

``` og

if(!head || !head->next) return head;

auto root = reverseList(head);
head->next.next =  head;
head->next = null;
return root;

```

#### "Iterative"

``` og
ListNode* prev = NULL;
ListNode* cur = head;
while(cur){
    auto tmp = cur->next;
    cur->next = prev;
    prev = cur;
    cur = tmp;
}
return prev;
```


## Reverse Pairs

``` og
if(!head || !head->next) return head;

auto n = head->next;
head->next = reversePairs(n.next);
n->next = head;
return n;
```