using LojaAthena.Data;
using Microsoft.EntityFrameworkCore;

namespace LojaAthena.Models;

public class CarrinhoCompraModel
{
    private readonly BancoContext _bancoContext;

    public CarrinhoCompraModel(BancoContext bancoContext)
    {
        _bancoContext = bancoContext;
    }

    public string? Id { get; set; }
    public List<CarrinhoCompraItemModel>? CarrinhoCompraItens { get; set; }
    
    public static CarrinhoCompraModel GetCarrinho(IServiceProvider services)
    {
        //define uma sessão
        ISession? session = services.GetRequiredService<IHttpContextAccessor>()? .HttpContext.Session;

        //obtem um serviço do tipo do nosso contexto
        var context = services.GetService<BancoContext>();
        
        //obtem ou gera o Id do carrinho
        string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        //atribui o id do carrinho na Sessão
        session.SetString("CarrinhoId", carrinhoId);

        //retorna o carrinho com o contexto e o Id atribuido ou obtido
        return new CarrinhoCompraModel(context)
        {
            Id = carrinhoId
        };
    }

    public void AdicionarAoCarrinho(RoupaModel roupa)
    {
        var carrinhoCompraItem = _bancoContext.CarrinhoCompraItens.SingleOrDefault(x => x.Roupa.Id == roupa.Id && x.CarrinhoCompraId == Id);

        if (carrinhoCompraItem is null)
        {
            carrinhoCompraItem = new CarrinhoCompraItemModel
            {
                CarrinhoCompraId = Id,
                Roupa = roupa,
                Quantidade = 1
            };
            _bancoContext.CarrinhoCompraItens.Add(carrinhoCompraItem);
        }
        else
        {
            carrinhoCompraItem.Quantidade++;
        }
        _bancoContext.SaveChanges();
    }

    public void RemoverDoCarrinho(RoupaModel roupa)
    {
        var carrinhoCompraItem = _bancoContext.CarrinhoCompraItens.SingleOrDefault(x => x.Roupa.Id == roupa.Id && x.CarrinhoCompraId == Id);

        
        if (carrinhoCompraItem != null)
        {
            if (carrinhoCompraItem.Quantidade > 1)
            {
                carrinhoCompraItem.Quantidade--;
            }
            else
            {
                _bancoContext.CarrinhoCompraItens.Remove(carrinhoCompraItem);
            }
        }
        _bancoContext.SaveChanges();
    }

    public List<CarrinhoCompraItemModel> GetCarrinhoCompraItens()
    {
        return CarrinhoCompraItens ?? (CarrinhoCompraItens = _bancoContext.CarrinhoCompraItens.Where(x => x.CarrinhoCompraId == Id).Include(x => x.Roupa).ToList());
    }

    public void LimparCarrinho()
    {
        var carrinhoItens = _bancoContext.CarrinhoCompraItens.Where(carrinho => carrinho.CarrinhoCompraId == Id);
        _bancoContext.CarrinhoCompraItens.RemoveRange(carrinhoItens);
        _bancoContext.SaveChanges();
    }

    public decimal GetCarrinhoCompraTotal()
    {
        var total = _bancoContext.CarrinhoCompraItens.Where(x => x.CarrinhoCompraId == Id).Select(x => x.Roupa.Preco * x.Quantidade).Sum();
        return total;
    }
}
