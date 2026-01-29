using ContaCorrenteAPI.Domain.Command.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContaCorrenteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Create(
            [FromServices] IMediator mediator,
            [FromBody] CreateContaCorrenteRequest command
        )
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }





        //// GET: ContaCorrenteController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: ContaCorrenteController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}


        //[HttpPost]
        //public ActionResult Create(
        //    [FromServices] ICreateContaCorrenteHandler handler,
        //    [FromBody] CreateContaCorrenteRequest command

        //    )
        //{
        //    var response = handler.Handle(command);
        //    return Ok(response);
        //}

        //// POST: ContaCorrenteController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ContaCorrenteController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ContaCorrenteController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ContaCorrenteController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ContaCorrenteController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
