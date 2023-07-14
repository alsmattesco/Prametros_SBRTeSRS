using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.Generic;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Prametros_SBRTeSRS.View;
using Prametros_SBRTeSRS.ViewModel;
using System.Runtime.CompilerServices;
using System.Reflection;

[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]
[assembly: AssemblyInformationalVersion("1.0")]

namespace VMS.TPS
{
      public class Script
      {
            public Script()
            {
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
             public void Execute(ScriptContext context, System.Windows.Window window)
            {
                // TODO : Add here your code that is called when the script is launched from Eclipse
                ShellView myWindow = new ShellView { DataContext = new ShellViewModel(context.Patient) };
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                window.Width = 600;
                window.Height = 650;
                window.Content = myWindow;

            }
      }
}
