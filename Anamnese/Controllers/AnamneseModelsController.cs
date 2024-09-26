using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Anamnese.Data;
using Anamnese.Models;
using NuGet.Packaging;
using System.ComponentModel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

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

            var paciente = await _context.PacienteModel.Where(p => p.IdPaciente == idPaciente).FirstOrDefaultAsync();
            ViewBag.Nome = paciente.NomeCompletoPaciente;

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
                    return RedirectToAction("Index", new {idPaciente = anamneseModel.IdPaciente});
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
        public async Task<IActionResult> Edit(int id, [Bind("IdAnamnese,IdPaciente,QueixaPrincipalAnamnese,HistoricoDoencaAtualAnamnese,AntecedentesPessoaisAnamnese,AntecedentesFamiliaresAnamnese,ExameFisicoAnamnese,HipotesesDiagnosticasAnamnese,PlanoTratamentoAnamnese,DataCadastroAnamnese")] AnamneseModel anamneseModel)
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
                return RedirectToAction("Index", new { idPaciente = anamneseModel.IdPaciente });
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
            return RedirectToAction("Index", new {idPaciente = anamneseModel.IdPaciente});
        }

        private bool AnamneseModelExists(int id)
        {
            return _context.AnamneseModel.Any(e => e.IdAnamnese == id);
        }


        #region Gerar PDF

        public async Task<IActionResult> GerarPdf(int id, int idPaciente)
        {
            var pdfResult = await AnamneseCreatePdf(id, idPaciente);
            return pdfResult ?? NotFound();
        }

        private async Task<IActionResult> AnamneseCreatePdf(int id, int idPaciente)
        {
            var anamnese = await _context.AnamneseModel.FindAsync(id);
            var paciente = await _context.PacienteModel.FindAsync(idPaciente);

            if (anamnese == null || paciente == null)
            {
                return NotFound(); // Se qualquer um dos dados estiver ausente, retorna 404
            }

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            try
            {
                using (var stream = new MemoryStream())
                {
                    // Criar o PDF
                    Document.Create(container =>
                    {
                        container.Page(page =>
                        {
                            page.Margin(50);
                            page.Content().Column(column =>
                            {
                                // Cabeçalho
                                column.Item().AlignCenter().Text("Sistema de Gerenciamento de Anamneses", TextStyle.Default.Size(25).Bold());
                                column.Item().PaddingBottom(30);
                                column.Item().Text("Relatório de Anamnese", TextStyle.Default.Size(20).Bold());
                                column.Item().PaddingBottom(20);

                                // Dados do Paciente
                                column.Item().Text($"Nome do Paciente: {paciente.NomeCompletoPaciente}");
                                column.Item().Text($"Data de Nascimento: {((DateTime)paciente.DataNascimentoPaciente).ToString("dd/MM/yyyy")}");
                                column.Item().PaddingBottom(20);

                                // Dados da Anamnese
                                column.Item().Text("Detalhes da Anamnese:", TextStyle.Default.Size(18).Bold());
                                column.Item().PaddingBottom(5);
                                column.Item().Text($"Queixa Principal: {anamnese.QueixaPrincipalAnamnese}");
                                column.Item().Text($"Histórico da Doença Atual: {anamnese.HistoricoDoencaAtualAnamnese}");
                                column.Item().Text($"Antecedentes Pessoais: {anamnese.AntecedentesPessoaisAnamnese}");
                                column.Item().Text($"Antecedentes Familiares: {anamnese.AntecedentesFamiliaresAnamnese}");
                                column.Item().Text($"Exame Físico: {anamnese.ExameFisicoAnamnese}");
                                column.Item().Text($"Hipóteses Diagnósticas: {anamnese.HipotesesDiagnosticasAnamnese}");
                                column.Item().Text($"Plano de Tratamento: {anamnese.PlanoTratamentoAnamnese}");
                                column.Item().Text($"Data de Cadastro: {anamnese.DataCadastroAnamnese.ToString("dd/MM/yyyy")}");
                            });
                            page.Footer().AlignCenter().Text("Sistema desenvolvido por Raul Souto", TextStyle.Default.Size(11).Bold());
                        });
                    }).GeneratePdf(stream);

                    return File(stream.ToArray(), "application/pdf", $"Anamnese_{id}.pdf");
                }
            }
            catch (Exception ex)
            {
                // Logar o erro (se necessário)
                Console.WriteLine($"Erro ao gerar PDF: {ex.Message}");
                return StatusCode(500, "Erro interno ao gerar o PDF");
            }
        }

        #endregion



    }
}
