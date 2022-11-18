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
    public class PriceApplication : ApplicationBase, IPriceApplication
    {
        private readonly IPriceRepository _priceRepository;

        public PriceApplication(IPriceRepository priceRepository, IUnitOfWork unitOfWork, INotifier notifier) : base(unitOfWork, notifier)
        {
            _priceRepository = priceRepository;
        }
        public async Task<IEnumerable<Price>> GetAllAsync()
        {
            return await _priceRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Price>> GetAllWithPriceListAndProductAsync()
        {
            return await _priceRepository.GetAllWithPriceListAndProductAsync();
        }

        public async Task<Price> GetByIdAsync(Guid id)
        {
            return await _priceRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Price>> GetByProductIdWithPriceListAndProductAsync(Guid productId)
        {
            return await _priceRepository.GetByProductIdWithPriceListAndProductAsync(productId);
        }

        public async Task<IEnumerable<Price>> GetByPriceListIdWithPriceListAndProductAsync(Guid priceListId)
        {
            return await _priceRepository.GetByPriceListIdWithPriceListAndProductAsync(priceListId);
        }

        public async Task Create(Price price)
        {
            if (!Validate(new PriceValidation(), price))
            {
                return;
            }

            _priceRepository.Create(price);
            await UnitOfWork.SaveAsync();

        }
        public async Task Update(Guid id, Price price)
        {
            if (id != price.Id)
            {
                Notify($"The supplied ids {id} and {price.Id} are differents.");
            }

            if (!Validate(new PriceValidation(), price))
            {
                return;
            }

            var priceToUpdate = await GetByIdAsync(id);

            if (priceToUpdate == null)
            {
                Notify($"Price {id} not found.");
                return;
            }

            priceToUpdate.Value = price.Value;

            _priceRepository.Update(priceToUpdate);
            await UnitOfWork.SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            var priceToDelete = await GetByIdAsync(id);

            if (priceToDelete == null)
            {
                Notify($"Price {id} not found.");
                return;
            }

            _priceRepository.Delete(priceToDelete);
            await UnitOfWork.SaveAsync();
        }
    }
}