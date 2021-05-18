﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace HospitalService.View.DoctorUI.Commands
{
    public class KeyUpWithArgsBehavior : Behavior<UIElement>
    {
        public ICommand KeyUpCommand
        {
            get { return (ICommand)GetValue(KeyUpCommandProperty); }
            set { SetValue(KeyUpCommandProperty, value); }
        }

        public static readonly DependencyProperty KeyUpCommandProperty =
            DependencyProperty.Register("KeyUpCommand", typeof(ICommand), typeof(KeyUpWithArgsBehavior), new UIPropertyMetadata(null));


        protected override void OnAttached()
        {
            AssociatedObject.KeyUp += new KeyEventHandler(AssociatedObjectKeyUp);
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.KeyUp -= new KeyEventHandler(AssociatedObjectKeyUp);
            base.OnDetaching();
        }

        private void AssociatedObjectKeyUp(object sender, KeyEventArgs e)
        {
            if (KeyUpCommand != null)
            {
                if(e.Key.Equals(Key.A))
                    KeyUpCommand.Execute(e.Key);
            }
        }
    }
}
