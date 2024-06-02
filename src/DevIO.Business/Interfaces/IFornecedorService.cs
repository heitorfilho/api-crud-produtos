using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IFornecedorService : IDisposable
    {
        Task<bool> Adicionar(Fornecedor Fornecedor);
        Task<bool> Atualizar(Fornecedor Fornecedor);
        Task<bool> Remover(Guid id);

        Task AtualizarEndereco(Endereco endereco);
    }
}
