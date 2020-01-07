# TPLCSharpExercise

This WPF application show the implementation of following task:
A C# (WPF) application to execute a number of pieces of work (actions) in the background (i.e. without blocking the program’s execution).

Here our actions are tasks. while we press insert button task get inserted into Queue and then Removed in a FIFO(First in First Out) manner. Using TPL librery we are able to perform the actions in Multithreads and while some tasks are getting executed in some Thread we can insert new tasks inside Queue using different Thread.
