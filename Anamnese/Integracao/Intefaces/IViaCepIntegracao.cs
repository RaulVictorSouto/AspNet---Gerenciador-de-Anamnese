using Anamnese.Integracao.Response;

namespace Anamnese.Integracao.Intefaces
{
    public interface IViaCepIntegracao
    {
        Task<ViaCepResponse> ObterDadosViaCep(string cep); 
    }
}
