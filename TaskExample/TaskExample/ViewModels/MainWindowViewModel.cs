using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using TaskExample.Models;

namespace TaskExample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        static int itemCount = 1;
        static int queueAvailability = 100;

        public ICommand InsertTask { get; set; }

        private string initialText;
        public string InitialText
        {
            get { return initialText; }
            set { SetProperty(ref initialText, value); }
        }

        private string finalText;

        public string FinalText
        {
            get { return finalText; }
            set { SetProperty(ref finalText, value); }
        }

        public MainWindowViewModel()
        {
            var prodTask = Task.Factory.StartNew(() => ProduceTask());
            var consTask = Task.Factory.StartNew(() => ConsumeTasks());
            InsertTask = new DelegateCommand(InsertTaskHandler);
        }
        public void ProduceTask()
        {
            if (queueAvailability > 0)
            {

                queueAvailability = queueAvailability - 1;
                var queue = CreateQueueItem();
                ConcurrentQueue.ConcurrentQueueOps.Enqueue(() => { ProcessQueue(queue); });

                InitialText += "Enqueued: " + queue.QueueID + Environment.NewLine
                                    + "Producer ThreadID :" + queue.ProducerThreadID + Environment.NewLine
                                    + "RandomString   :" + queue.RandomString + Environment.NewLine;
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
            ConcurrentQueue.ConcurrentQueueOps.Dequeue();
        }
        private void InsertTaskHandler()
        {
            ProduceTask();
        }

        public void ProcessQueue(QueueItem queue)
        {

            var result = palindrome(queue.RandomString);
            string palindromeString = " ", notPalindrome = " ";

            if (result == true)
            {
                palindromeString = "String is Palindrome ";
            }
            else
            {
                notPalindrome = "String is not Palindrome ";
            }

            FinalText += "Dequeued: " + queue.QueueID +
                 Environment.NewLine + "Consumer ThreadID :" + Thread.CurrentThread.ManagedThreadId +
                 Environment.NewLine + palindromeString + notPalindrome + Environment.NewLine;

        }

        public static bool palindrome(string RandomString)
        {
            string revs = "";
            bool result;

            for (int i = RandomString.Length - 1; i >= 0; i--) //String Reverse  
            {
                revs += RandomString[i].ToString();
            }
            if (revs == RandomString) // Checking whether string is palindrome or not  
            {
                result = true;

            }
            else
            {
                result = false;

            }
            return result;
        }

    }
}
