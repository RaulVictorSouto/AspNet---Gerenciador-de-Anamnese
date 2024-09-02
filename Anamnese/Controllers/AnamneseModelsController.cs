using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Anamnese.Data;
using Anamnese.Models;

namespace Anamnese.Controllers
{
    public class AnamneseModelsController : Controller
    {
        private readonly Contexto _context;

        public AnamneseModelsController(Contexto context)
        {
            _context = context;
        }

        // GET: AnamneseModels
        public async Task<IActionResult> Index(int idPaciente)
        {
            ViewBag.IdPaciente = idPaciente;
            var anamneses = await _context.AnamneseModel.Where(a => a.IdPaciente == idPaciente).ToListAsync();
            return View(anamneses);
        }

        // GET: AnamneseModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anamneseModel = await _context.AnamneseModel
                .FirstOrDefaultAsync(m => m.IdAnamnese == id);
            if (anamneseModel == null)
            {
                return NotFound();
            }

            return View(anamneseModel);
        }

        // GET: AnamneseModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnamneseModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAnamnese,IdPaciente,QueixaPrincipalAnamnese,HistoricoDoencaAtualAnamnese,AntecedentesPessoaisAnamnese,AntecedentesFamiliaresAnamnese,ExameFisicoAnamnese,HipotesesDiagnosticasAnamnese,PlanoTratamentoAnamnese,DataCadastroAnamnese")] AnamneseModel anamneseModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    anamneseModel.DataCadastroAnamnese = DateTime.Now;

                    _context.Add(anamneseModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao salvar os dados: " + ex.Message);
                }
                
            }
            return View(anamneseModel);
        }

        // GET: AnamneseModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anamneseModel = await _context.AnamneseModel.FindAsync(id);
            if (anamneseModel == null)
            {
                return NotFound();
            }
            return View(anamneseModel);
        }

        // POST: AnamneseModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAnamnese,IdPaciente,QueixaPrincipalAnamnese,HistoricoDoencaAtualAnamnese,AntecendentesPessoaisAnamnese,AntecendentesFamiliaresAnamnese,ExameFisicoAnamnese,HipoteseDiagnosticaAnamnese,PlanoTratamentoAnamnese,DataCadastroAnamnese")] AnamneseModel anamneseModel)
        {
            if (id != anamneseModel.IdAnamnese)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anamneseModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnamneseModelExists(anamneseModel.IdAnamnese))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(anamneseModel);
        }

        // GET: AnamneseModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anamneseModel = await _context.AnamneseModel
                .FirstOrDefaultAsync(m => m.IdAnamnese == id);
            if (anamneseModel == null)
            {
                return NotFound();
            }

            return View(anamneseModel);
        }

        // POST: AnamneseModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anamneseModel = await _context.AnamneseModel.FindAsync(id);
            if (anamneseModel != null)
            {
                _context.AnamneseModel.Remove(anamneseModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnamneseModelExists(int id)
        {
            return _context.AnamneseModel.Any(e => e.IdAnamnese == id);
        }
    }
}
