using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Anamnese.Models
{
    [Table("Pacientes")]
    public class PacienteModel
    {
        [Display(Name = "Código Paciente")]
        [Column("IdPaciente")]
        public int IdPaciente { get; set; }

        [Display(Name = "Nome Paciente")]
        [Column("NomePaciente")]
        public string NomePaciente { get; set; }

        public string SobrenomePaciente { get; set; }

        public DateTime DataNascimentoPaciente { get; set; }

        public string GeneroPaciente { get; set; }

        public string CpfPaciente { get; set; }

        public string RgPaciente { get; set; }

        public string CertidaoPaciente { get; set; }

        public string TelefonePaciente { get; set; }

        public string CelularPaciente { get; set; }

        public string EstadoCivilPaciente { get; set; }

        public string CepPaciente { get; set; }

        public string LogradouroPaciente { get; set; }

        public string NumeroEnderecoPaciente { get; set; }

        public string BairroPaciente { get; set; }

        public string CidadePaciente { get; set; }

        public string EstadoPaciente { get; set; }

        public DateTime DataCadastroPaciente { get; set; }
    }
}
