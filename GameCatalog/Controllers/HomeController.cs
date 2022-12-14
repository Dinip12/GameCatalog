using GameCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using GameCatalog.Data;
using Microsoft.AspNetCore.Hosting;
using GameCatalog.Entity;
using System.Data.Entity;
using GameCatalog.ViewModels;
using System.Linq.Expressions;

namespace GameCatalog.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IWebHostEnvironment _webHostEnvironment;
        private AppDbContext context;
        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            context = new AppDbContext();
        }
        public static bool isEditing = false;
        public FileResult GetImage(int id)
        {
           
            Game games = context.Games.Find(id);

            string rootPath = _webHostEnvironment.WebRootPath;
            var path = Path.Combine(rootPath + "\\img\\games\\", games.fileName + ".png");

            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            return File(imageByteData, "image/png");
        }
        public IActionResult Index(Display model)
        {
            model.Pager ??= new PagerVM();
            model.Filter ??= new FilterVM();
                        
            if (model.Filter.Title == "0000")
            {
                return RedirectToAction("AdminView");
            }
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;


            var filter = model.Filter.GetFilter();
            model.Games =GetAll(filter, model.Pager.Page, model.Pager.ItemsPerPage);
            model.Pager.PagesCount = (int)Math.Ceiling(GamesCount(filter) / (double)model.Pager.ItemsPerPage);

           
            
            return View(model);
        }
        public IActionResult AdminView(Display model)
        {
            model.Pager ??= new PagerVM();
            model.Filter ??= new FilterVM();

           
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;


            var filter = model.Filter.GetFilter();
            model.Games = GetAll(filter, model.Pager.Page, model.Pager.ItemsPerPage);
            model.Pager.PagesCount = (int)Math.Ceiling(GamesCount(filter) / (double)model.Pager.ItemsPerPage);



            return View(model);
        }
        public int GamesCount(Expression<Func<Game, bool>> filter = null)
        {
            
            IQueryable<Game> query = context.Games;

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        }
        public List<Game> GetAll(Expression<Func<Game, bool>> filter = null, int page = 1, int pageSize = int.MaxValue)
        {
            
            IQueryable<Game> query = context.Games;
            if (filter != null)
                query = query.Where(filter);

            return query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public IActionResult Details(int GameId)
        {
            
            return View(context.Games.Find(GameId)/*return the viewmodel using this info or just use class for a vm*/);

        }
        public IActionResult Browse()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            AppDbContext context = new AppDbContext();
            Game game = new Game();

            if (model.file != null)
            {
                game.fileName = model.file.FileName.Substring(0, model.file.FileName.Length - 4);

                var path = Path.Combine(_webHostEnvironment.WebRootPath + "\\img\\games", model.file.FileName);

                using var fileStream = new FileStream(path, FileMode.Create);
                model.file.CopyTo(fileStream);
            }


            game.Title = model.Title;
            game.Description = model.Description;
            game.ReleaseDate = model.ReleaseDate;
            game.Stars = model.Stars;
            game.Downloads = model.Downloads;
            context.Games.Add(game);
            context.SaveChanges();
            return RedirectToAction("Create");//or somewhere else??
        }
        public IActionResult Delete(int id)
        {
            AppDbContext context = new AppDbContext();
            Game item = context.Games.Find(id);

            if (item == null)
            {
                return RedirectToAction("AdminView");//or somewhere else??
            }

            context.Games.Remove(item);
            context.SaveChanges();

            return RedirectToAction("AdminView");//or somewhere else??
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            
            AppDbContext context = new AppDbContext();
            Game item = context.Games.Find(id);
            EditVM model = new EditVM();

            model.ID = item.Id;
            model.Title = item.Title;
            model.Description = item.Description;
            model.ReleaseDate = item.ReleaseDate;
            model.Stars = item.Stars;
            model.Downloads = item.Downloads;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            AppDbContext context = new AppDbContext();
            Game game = context.Games.Find(model.ID);

            game.Title = model.Title;
            game.Description = model.Description;
            game.ReleaseDate = model.ReleaseDate;
            game.Stars = model.Stars;
            game.Downloads = model.Downloads;

            context.Entry(game).State = EntityState.Modified;
            context.SaveChanges();
            isEditing = false;
            return RedirectToAction("AdminView");
        }
    }
}