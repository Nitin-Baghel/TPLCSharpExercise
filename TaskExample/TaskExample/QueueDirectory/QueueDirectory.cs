using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TaskExample.QueueDirectory
{
    class QueueDirectory
    {
        static Queue<Task> _queue;

        static QueueDirectory() { _queue = new Queue<Task>(); }
        public static void Enqueue(Action action, CancellationToken cancelToken = default(CancellationToken))
        {
            Task task = new Task(action, cancelToken);
            _queue.Enqueue(task);
        }

        public static void Dequeue()
        {
            while (true)
            {
                try
                {
                    Task task = _queue.Dequeue();
                    task.RunSynchronously();
                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}
