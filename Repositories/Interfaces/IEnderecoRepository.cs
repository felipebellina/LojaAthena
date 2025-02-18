using LojaAthena.Models;

namespace LojaAthena.Repositories.Interfaces;

public interface IEnderecoRepository
{
    EnderecoModel GetEndereco(string cep);
}