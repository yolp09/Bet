using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NTCBet.ViewModel
{
    public class ApplicationMain : IApplicationMain
    {
        private string _userName;
        private Page _currentPage;
        private Stack<Page> _backStack;

        public ApplicationMain()
        {
            _backStack = new Stack<Page>();
        }

        #region IApplicationMain

        public string UserName { get { return _userName; } private set { _userName = value; PageChanged(this, EventArgs.Empty); } }
        public Visibility VisibilityBackButton { get { return _backStack.Count > 0 ? Visibility.Visible : Visibility.Collapsed; } }
        public Page CurrentPage { get { return _currentPage; } private set { _currentPage = value; PageChanged(this, EventArgs.Empty); } }
        public event EventHandler PageChanged;
        #endregion

        public void ChangeUserName(string name)
        {
            UserName = name;
        }

        public void BackPage()
        {
            if (_backStack.Count > 0) ChangePage(_backStack.Pop());
        }

        public void NextPage(Page page)
        {
            if (page == null || CurrentPage == page) return;
            if (CurrentPage != null) _backStack.Push(CurrentPage);

            ChangePage(page);
        }

        private void ChangePage(Page page)
        {
            CurrentPage = page;
        }
    }
}
