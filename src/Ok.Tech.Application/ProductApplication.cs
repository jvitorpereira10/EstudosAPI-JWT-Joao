using Ok.Tech.Domain.Applications;
using Ok.Tech.Domain.Entities;
using Ok.Tech.Domain.Notifications;
using Ok.Tech.Domain.Repositories;
using Ok.Tech.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ok.Tech.Application
{
  public class ProductApplication : ApplicationBase, IProductApplication
  {

    private readonly IProductRepository _productRepository;

    public ProductApplication(IProductRepository productRepository, IUnitOfWork unitOfWork, INotifier notifier) : base(unitOfWork, notifier)
    {
      _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
      return await _productRepository.GetAllAsync();
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
      return await _productRepository.GetByIdAsync(id);
    }
    public async Task Create(Product product)
    {
      if (!Validate(new ProductValidation(), product))
      {
        return;
      }

      _productRepository.Create(product);

      await UnitOfWork.SaveAsync();
    }

    public async Task Update(Guid id, Product product)
    {
      if (id != product.Id)
      {
        Notify($"The supplied ids {id} and {product.Id} are differents");
        return;
      }

      if (!Validate(new ProductValidation(), product))
      {
        return;
      }

      var productToUpadate = await GetByIdAsync(id);
      if (productToUpadate == null)
      {
        Notify($"Product {id} not found.");
        return;
      }

      productToUpadate.Name = product.Name;
      productToUpadate.Description = product.Description;
      productToUpadate.Active = product.Active;

      _productRepository.Update(productToUpadate);

      await UnitOfWork.SaveAsync();

    }
    public async Task Delete(Guid id)
    {
      var productToDelete = await GetByIdAsync(id);
      if (productToDelete == null)
      {
        Notify($"Product {id} not found.");
        return;
      }

      _productRepository.Delete(productToDelete);

      await UnitOfWork.SaveAsync();
    }
  }
}
