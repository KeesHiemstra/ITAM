﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace License_Registration.Commands
{
  public static class MainWindowCommands
  {
    public static readonly RoutedUICommand Exit = new RoutedUICommand
      (
        "E_xit",
        "Exit",
        typeof(MainWindowCommands),
        new InputGestureCollection()
        {
          new KeyGesture(Key.F4, ModifierKeys.Alt)
        }
      );
  }
}
