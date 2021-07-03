﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace Stratosoft.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ButtonCommandInvoker"/> class.
    /// </summary>
    [TestClass]
    public class ButtonCommandInvokerTests
    {
        /// <summary>
        /// Check that the constructor works correctly. 
        /// </summary>
        [TestMethod]
        public void TestContructor()
        {
            var invoker = new ButtonCommandInvoker();

            Assert.IsNotNull(invoker);
        }

        /// <summary>
        /// Test that the AddInstance(Component, ICommand) method works correctly.
        /// </summary>
        [TestMethod]
        public void TestAddInstance()
        {
            var invoker = new ButtonCommandInvoker();

            var command = new MockCommand(new MockReceiver<string>());

            var button = new Button();

            invoker.AddInstance(button, command);

            button.PerformClick();

            Assert.IsTrue(command.ExecuteCalled);
        }

        /// <summary>
        /// Test that the Type property returns the correct type name.
        /// </summary>
        [TestMethod]
        public void TestGetType()
        {
            var invoker = new ButtonCommandInvoker();

            Assert.AreEqual("System.Windows.Forms.Button", invoker.Type);
        }

        /// <summary>
        /// Test that the RemoveInstance(Component) method works correctly.
        /// </summary>
        [TestMethod]
        public void TestRemoveInstance()
        {
            var invoker = new ButtonCommandInvoker();

            var command = new MockCommand(new MockReceiver<string>());

            var button = new Button();

            invoker.AddInstance(button, command);

            invoker.RemoveInstance(button);

            button.PerformClick();

            Assert.IsFalse(command.ExecuteCalled);
        }

        /// <summary>
        /// Test that the UpdateEnabledState(Component, bool) method works correctly.
        /// </summary>
        [TestMethod]
        public void TestUpdateEnabledState()
        {
            var invoker = new ButtonCommandInvoker();

            var button = new Button();

            Assert.IsTrue(button.Enabled);

            invoker.UpdateEnabledState(button, false);

            Assert.IsFalse(button.Enabled);
        }

        /// <summary>
        /// Test that the UpdateEnabledState(Component, bool) method does not throw an exception.
        /// </summary>
        [TestMethod]
        public void TestUpdateCheckedState()
        {
            var invoker = new ButtonCommandInvoker();

            var button = new Button();

            invoker.UpdateCheckedState(button, true);
        }
    }
}
