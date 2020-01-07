using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using TaskExample.Models;

namespace TaskExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int itemCount = 1;
        static int queueAvailability = 100; // Queue queue.
        public MainWindow()
        {
            InitializeComponent();
            var prodTask = Task.Factory.StartNew(() => ProduceTask());
            var consTask = Task.Factory.StartNew(() => ConsumeTasks());
            //Task.WaitAll(prodTask, consTask);
        }
        public void ProduceTask()
        {           
            if (queueAvailability > 0)
            {

                queueAvailability = queueAvailability - 1;
                var queue = CreateQueueItem();
                ConcurrentQueue.ConcurrentQueueOps.Enqueue(() => { ProcessQueue(queue); });
                Dispatcher.Invoke(() =>
                txtProducer.Text += "Enqueued: " + queue.QueueID + Environment.NewLine
                                    + "Producer ThreadID :" + queue.ProducerThreadID + Environment.NewLine
                                    + "RandomString   :" + queue.RandomString + Environment.NewLine);
                itemCount++;
            }

           

        }

        private QueueItem CreateQueueItem()
        {
            var random = new Random();
            return new QueueItem
            {
                QueueID = itemCount,
                ProducerThreadID = Thread.CurrentThread.ManagedThreadId,
                EnqueueDateTime = DateTime.Now,
                RandomString = new string(Enumerable.Repeat("ABC", 3).Select(s => s[random.Next(s.Length)]).ToArray())
            };
        }

        public static void ConsumeTasks()
        {
            //QueueService.Dequeue();
            ConcurrentQueue.ConcurrentQueueOps.Dequeue();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProduceTask();
        }

        public void ProcessQueue(QueueItem queue)
        {

            var result = palindrome(queue.RandomString);
            string PalindromeString = " ", NotPalindrome = " ";

            if (result == true)
            {
                PalindromeString = "String is Palindrome ";
            }
            else
            {
                NotPalindrome = "String is not Palindrome ";
            }
            Dispatcher.Invoke(() =>
           txtConsumer.Text += "Dequeued: " + queue.QueueID +
                 Environment.NewLine + "Consumer ThreadID :" + Thread.CurrentThread.ManagedThreadId +
                 Environment.NewLine + PalindromeString + NotPalindrome + Environment.NewLine);
               
        }

        public static bool palindrome(string s)
        {
            string revs = "";
            bool t;

            for (int i = s.Length - 1; i >= 0; i--) //String Reverse  
            {
                revs += s[i].ToString();
            }
            if (revs == s) // Checking whether string is palindrome or not  
            {
                t = true;

            }
            else
            {
                t = false;

            }
            return t;
        }

    }
}

