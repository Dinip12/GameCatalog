using GameCatalog.Entity;
using System.ComponentModel;
using System.Linq.Expressions;

namespace GameCatalog.ViewModels
{
    public class FilterVM
    {
        public string Title { get; set; }
        public Expression<Func<Game, bool>> GetFilter()
        {
            return i => (string.IsNullOrEmpty(Title) || i.Title.Contains(Title));
        }
    }
}
