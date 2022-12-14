using GameCatalog.Entity;
using GameCatalog.ViewModels;
namespace GameCatalog.ViewModels
{
    public class Display
    {
        public List<Game> Games { get; set; }
        public FilterVM Filter { get; set; }
        public PagerVM Pager { get; set; }
    }
}