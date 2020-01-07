using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskExample.ConcurrentQueue
{
    class ConcurrentQueueOps
    {
        static ConcurrentQueue<Task> _queue;

        static ConcurrentQueueOps()
        {
            _queue = new ConcurrentQueue<Task>();
        }

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
                    Task task;
                    if (_queue.TryDequeue(out task)) { task.RunSynchronously(); }
                    Thread.Sleep(3000);
                }
                catch (NullReferenceException ex)
                {
                    string w = ex.Message;
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
