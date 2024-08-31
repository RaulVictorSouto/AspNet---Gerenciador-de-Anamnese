using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anamnese.Models
{
    [Table("tblpacientes")]
    public class PacienteModel
    {
        [Key]
        [Display(Name = "Código Paciente")]
        [Column("IdPaciente")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "O nome do paciente é obrigatório.")]
        [Display(Name = "Nome Paciente")]
        [Column("NomePaciente")]
        [StringLength(100, ErrorMessage = "O nome do paciente deve ter no máximo 100 caracteres.")]
        public string NomePaciente { get; set; }

        [Display(Name = "Sobrenome Paciente")]
        [StringLength(200, ErrorMessage = "O sobrenome do paciente deve ter no máximo 200 caracteres.")]
        public string? SobrenomePaciente { get; set; } 

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataNascimentoPaciente { get; set; } 

        [Display(Name = "Gênero")]
        [StringLength(50, ErrorMessage = "O gênero deve ter no máximo 50 caracteres.")]
        public string? GeneroPaciente { get; set; } 

        [Display(Name = "CPF")]
        [StringLength(11, ErrorMessage = "O CPF deve ter 11 caracteres.")]
        public string? CpfPaciente { get; set; } 

        [Display(Name = "RG")]
        [StringLength(20, ErrorMessage = "O RG deve ter no máximo 20 caracteres.")]
        public string? RgPaciente { get; set; }

        [Display(Name = "Certidão de Nascimento")]
        [StringLength(50, ErrorMessage = "A certidão deve ter no máximo 50 caracteres.")]
        public string? CertidaoPaciente { get; set; } 

        [Display(Name = "Telefone")]
        [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres.")]
        [DataType(DataType.PhoneNumber)]
        public string? TelefonePaciente { get; set; } 

        [Display(Name = "Celular")]
        [StringLength(15, ErrorMessage = "O celular deve ter no máximo 15 caracteres.")]
        [DataType(DataType.PhoneNumber)]
        public string? CelularPaciente { get; set; }

        [Display(Name = "Naturalidade")]
        [Required(ErrorMessage = "A naturalidade do paciente é obrigatória.")]
        public string NaturalidadePaciente { get; set; } = "Desconhecido";

        [Display(Name = "Estado Civil")]
        [StringLength(20, ErrorMessage = "O estado civil deve ter no máximo 20 caracteres.")]
        public string? EstadoCivilPaciente { get; set; } 

        [Display(Name = "CEP")]
        [StringLength(8, ErrorMessage = "O CEP deve ter no máximo 8 caracteres.")]
        public string? CepPaciente { get; set; } 

        [Display(Name = "Logradouro")]
        [StringLength(100, ErrorMessage = "O logradouro deve ter no máximo 100 caracteres.")]
        public string? LogradouroPaciente { get; set; } 

        [Display(Name = "Número")]
        [StringLength(10, ErrorMessage = "O número deve ter no máximo 10 caracteres.")]
        public string? NumeroEnderecoPaciente { get; set; } 

        [Display(Name = "Bairro")]
        [StringLength(50, ErrorMessage = "O bairro deve ter no máximo 50 caracteres.")]
        public string? BairroPaciente { get; set; } 

        [Display(Name = "Cidade")]
        [StringLength(50, ErrorMessage = "A cidade deve ter no máximo 50 caracteres.")]
        public string? CidadePaciente { get; set; } 

        [Display(Name = "Estado")]
        [StringLength(2, ErrorMessage = "O estado deve ter no máximo 2 caracteres.")]
        public string? EstadoPaciente { get; set; } 

        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataCadastroPaciente { get; set; } 
    }
}
