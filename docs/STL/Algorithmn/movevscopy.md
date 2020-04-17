Copy VS Move: A few numbers

Why you should use a move operation?

* copy is expensive since it will copy the elements of the resources, the the move semantic will move the elements of the resources.
* copy will be throw a std::bad_alloc exception when program is out of memory

Is there something  need to be careful using move operation?
- The resources of the move operation is afterwards in a "invalid but upsepcified sate".

![move]("https://www.modernescpp.com/images/blog/EmbeddedProgrammierung/CopyVersusMove/move.jpg")
 


## Reference

 * [MC++'s Copy versus Move Semantic: A few Numbers](https://www.modernescpp.com/index.php/copy-versus-move-semantic-a-few-numbers)

