using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;
using TaskExample.ViewModels;

namespace TaskExampleTest.ViewModelFacts
{
    [TestClass]
    public class MainWindowViewModelFacts
    {
        [TestMethod]
        public void Raising_Insert_Task_Will_Produce_Task()
        {
            // arrange
            var vm = CreateViewModel();

            // act
            vm.InsertTask.Execute(null);

            // assert
            vm.InitialText.ShouldNotBeNull();

            Task.Delay(6000);
            vm.FinalText.ShouldNotBeNull();
        }

        [TestMethod]
        public void FinalText_Should_Be_Updated_When_Passing_Palindrome_ToProcessQueue()
        {
            // arrange
            var vm = CreateViewModel();

            // act
            vm.ProcessQueue(new TaskExample.Models.QueueItem()
            {
                RandomString="ABA"
            });

            // assert
            vm.FinalText.ShouldContain("String is Palindrome");
        }

        private MainWindowViewModel CreateViewModel()
        {
            return new MainWindowViewModel();
        }
    }
}
