namespace MyFirstApp.Models
{
    public class IndexViewModel
    {
        public IEnumerable<User> Users { get; }
        public PageViewModel PageViewModel { get; } 
        public IndexViewModel(IEnumerable<User> users, PageViewModel pageViewModel)
        {
            Users = users;
            PageViewModel = pageViewModel;
        }
    }
}
