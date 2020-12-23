using System;
using System.Windows;
using System.Windows.Controls;

namespace NTCBet.ViewModel
{
    public interface IApplicationMain
    {
        string UserName { get; }
        Visibility VisibilityBackButton { get; }
        Page CurrentPage { get; }
        event EventHandler PageChanged;
    }
}
