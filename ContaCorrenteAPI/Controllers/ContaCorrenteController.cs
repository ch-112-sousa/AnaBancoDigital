using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContaCorrenteAPI.Controllers
{
    public class ContaCorrenteController : Controller
    {
        // GET: ContaCorrenteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ContaCorrenteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContaCorrenteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContaCorrenteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContaCorrenteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContaCorrenteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContaCorrenteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContaCorrenteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
