﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Anamnese.Data;
using Anamnese.Models;
using System.Globalization;
using static Anamnese.Models.PacienteModel;
using Newtonsoft.Json;

namespace Anamnese.Controllers
{
    public class PacienteModelsController : Controller
    {
        private readonly Contexto _context;

        public PacienteModelsController(Contexto context)
        {
            _context = context;
 
        }

        // GET: PacienteModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PacienteModel.ToListAsync());
        }

        // GET: PacienteModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteModel = await _context.PacienteModel.FirstOrDefaultAsync(m => m.IdPaciente == id);

            if (pacienteModel == null)
            {
                return NotFound();
            }

            return View(pacienteModel);
        }

        // GET: PacienteModels/Create
        public IActionResult Create()
        { 
            return View();
        }

        // POST: PacienteModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPaciente,NomePaciente,SobrenomePaciente,DataNascimentoPaciente,GeneroPaciente,CpfPaciente,RgPaciente,CertidaoPaciente,TelefonePaciente,CelularPaciente,NaturalidadePaciente,EstadoCivilPaciente,CepPaciente,LogradouroPaciente,NumeroEnderecoPaciente,BairroPaciente,CidadePaciente,EstadoPaciente,DataCadastroPaciente")] PacienteModel pacienteModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pacienteModel.DataCadastroPaciente = DateTime.Now;

                    _context.Add(pacienteModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao salvar os dados: " + ex.Message);
                }
            }
            return View(pacienteModel);
        }

        // GET: PacienteModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteModel = await _context.PacienteModel.FindAsync(id);
            if (pacienteModel == null)
            {
                return NotFound();
            }
            return View(pacienteModel);
        }

        // POST: PacienteModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPaciente,NomePaciente,SobrenomePaciente,DataNascimentoPaciente,GeneroPaciente,CpfPaciente,RgPaciente,CertidaoPaciente,TelefonePaciente,CelularPaciente,NaturalidadePaciente,EstadoCivilPaciente,CepPaciente,LogradouroPaciente,NumeroEnderecoPaciente,BairroPaciente,CidadePaciente,EstadoPaciente,DataCadastroPaciente")] PacienteModel pacienteModel)
        {
            if (id != pacienteModel.IdPaciente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacienteModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteModelExists(pacienteModel.IdPaciente))
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
            return View(pacienteModel);
        }

        // GET: PacienteModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteModel = await _context.PacienteModel
                .FirstOrDefaultAsync(m => m.IdPaciente == id);
            if (pacienteModel == null)
            {
                return NotFound();
            }

            return View(pacienteModel);
        }

        // POST: PacienteModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pacienteModel = await _context.PacienteModel.FindAsync(id);
            if (pacienteModel != null)
            {
                _context.PacienteModel.Remove(pacienteModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteModelExists(int id)
        {
            return _context.PacienteModel.Any(e => e.IdPaciente == id);
        }


        public async Task<JsonResult> BuscarEnderecoPorCep(string cep)
        {
            if (string.IsNullOrEmpty(cep)) return Json(null);

            // Remove caracteres não numéricos do CEP
            cep = cep.Replace("-", "").Replace(".", "").Replace(" ", "");

            using (var client = new HttpClient())
            {
                var url = $"https://viacep.com.br/ws/{cep}/json/";
                var response = await client.GetStringAsync(url);
                var endereco = JsonConvert.DeserializeObject<Endereco>(response);

                if (endereco == null || endereco.Cep == null)
                {
                    return Json(new { error = "CEP não encontrado" });
                }

                return Json(endereco);
            }
        }

        public class Endereco
        {
            public string Cep { get; set; }
            public string Logradouro { get; set; }
            public string Bairro { get; set; }
            public string Localidade { get; set; }
            public string Uf { get; set; }
        }
    }
}
