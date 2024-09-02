using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anamnese.Models
{
    [Table("tblanamneses")]
    public class AnamneseModel
    {
        [Key]
        [Display(Name = "Código Anamnese")]
        [Column("IdAnamnese")]
        public int IdAnamnese { get; set; }

        [ForeignKey("IdPaciente")]
        [Column("IdPaciente")]
        public int IdPaciente { get; set; }

        [Display(Name = "Queixa Principal")]
        [StringLength(500, ErrorMessage = "A queixa principal deve ter no máximo 500 caracteres.")]
        public string? QueixaPrincipalAnamnese { get; set; }

        [Display(Name = "Histórico da Doença Atual")]
        [StringLength(1000, ErrorMessage = "O histórico da doença atual deve ter no máximo 1000 caracteres.")]
        public string? HistoricoDoencaAtualAnamnese { get; set; }

        [Display(Name = "Antecedentes Pessoais")]
        [StringLength(1000, ErrorMessage = "Os antecedentes pessoais devem ter no máximo 1000 caracteres.")]
        public string? AntecedentesPessoaisAnamnese { get; set; }

        [Display(Name = "Antecedentes Familiares")]
        [StringLength(1000, ErrorMessage = "Os antecedentes familiares devem ter no máximo 1000 caracteres.")]
        public string? AntecedentesFamiliaresAnamnese { get; set; }

        [Display(Name = "Exame Físico")]
        [StringLength(1000, ErrorMessage = "O exame físico deve ter no máximo 1000 caracteres.")]
        public string? ExameFisicoAnamnese { get; set; }

        [Display(Name = "Hipótese Diagnóstica")]
        [StringLength(1000, ErrorMessage = "A hipótese diagnóstica deve ter no máximo 1000 caracteres.")]
        public string? HipotesesDiagnosticasAnamnese { get; set; }

        [Display(Name = "Plano de Tratamento")]
        [StringLength(1000, ErrorMessage = "O plano de tratamento deve ter no máximo 1000 caracteres.")]
        public string? PlanoTratamentoAnamnese { get; set; }

        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataCadastroAnamnese { get; set; }

        // Propriedade de Navegação para o Paciente
        public virtual PacienteModel? Paciente { get; set; }

    }
}
