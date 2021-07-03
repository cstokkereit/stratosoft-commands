﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Windows.Forms;

namespace Stratosoft.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="CommandInvoker&lt;TComponent&gt;"/> class.
    /// </summary>
    [TestClass]
    public class CommandInvokerTests
    {
        /// <summary>
        /// Check that the constructor works correctly. 
        /// </summary>
        [TestMethod]
        public void TestContructor()
        {
            var invoker = new ButtonInvoker();

            Assert.IsNotNull(invoker);
        }

        /// <summary>
        /// Test that the AddInstance(Component, ICommand) method works correctly.
        /// </summary>
        [TestMethod]
        public void TestAddInstance()
        {
            var invoker = new ButtonInvoker();

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
            var invoker = new ButtonInvoker();

            Assert.AreEqual("System.Windows.Forms.Button", invoker.Type);
        }

        /// <summary>
        /// Test that the RemoveInstance(Component) method works correctly.
        /// </summary>
        [TestMethod]
        public void TestRemoveInstance()
        {
            var invoker = new ButtonInvoker();

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
            var invoker = new ButtonInvoker();

            var button = new Button();

            Assert.IsTrue(button.Enabled);

            invoker.UpdateEnabledState(button, false);

            Assert.IsFalse(button.Enabled);
        }

        /// <summary>
        /// Test that the UpdateEnabledState(Component, bool) method works correctly.
        /// </summary>
        [TestMethod]
        public void TestUpdateCheckedState()
        {
            var invoker = new MenuItemInvoker();

            var menu = new MenuItem();

            Assert.IsFalse(menu.Checked);

            invoker.UpdateCheckedState(menu, true);

            Assert.IsTrue(menu.Checked);
        }

        /// <summary>
        /// A derived class used to test the abstract <see cref="CommandInvoker&lt;TComponent&gt;"/> class.
        /// </summary>
        private class ButtonInvoker : CommandInvoker<Button>
        {
            public override void AddInstance(Component component, ICommand command)
            {
                base.AddInstance(component, command);
                var button = Cast(component);
                button.Click += OnClick;
            }

            public override void RemoveInstance(Component component)
            {
                base.RemoveInstance(component);
                var button = Cast(component);
                button.Click -= OnClick;
            }

            public override void UpdateEnabledState(Component component, bool value)
            {
                var button = Cast(component);
                button.Enabled = value;
            }

            private void OnClick(object sender, System.EventArgs e)
            {
                var command = GetCommandForInstance(sender as Button);
                command.Execute();
            }
        }

        /// <summary>
        /// A derived class used to test the abstract <see cref="CommandInvoker&lt;TComponent&gt;"/> class.
        /// </summary>
        private class MenuItemInvoker : CommandInvoker<MenuItem>
        {
            public override void AddInstance(Component component, ICommand command)
            {
                base.AddInstance(component, command);
            }

            public override void UpdateCheckedState(Component component, bool value)
            {
                var menuItem = Cast(component);
                menuItem.Checked = value;
            }
        }
    }
}
