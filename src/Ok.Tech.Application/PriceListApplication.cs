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
    public class PriceListApplication : ApplicationBase, IPriceListApplication
    {
        private readonly IPriceListRepository _priceListRepository;

        public PriceListApplication(IPriceListRepository priceListRepository, IUnitOfWork unitOfWork, INotifier notifier) : base(unitOfWork, notifier)
        {
            _priceListRepository = priceListRepository;
        }

        public async Task<IEnumerable<PriceList>> GetAllAsync()
        {
            return await _priceListRepository.GetAllAsync();
        }

        public async Task<PriceList> GetByIdAsync(Guid id)
        {
            return await _priceListRepository.GetByIdAsync(id);
        }

        public async Task Create(PriceList priceList)
        {
            if (!Validate(new PriceListValidation(), priceList))
            {
                return;
            }           

            _priceListRepository.Create(priceList);
            await UnitOfWork.SaveAsync();
        }

        public async Task Update(Guid id, PriceList priceList)
        {
            if (id != priceList.Id)
            {
                Notify($"The supplied ids {id} and {priceList.Id} are differents");
                return;
            }

            if (!Validate(new PriceListValidation(), priceList))
            {
                return;
            }

            var priceListToUpdate = await GetByIdAsync(id);

            if (priceListToUpdate == null)
            {
                Notify($"Price List {id} not found.");
                return;
            }

            priceListToUpdate.Name = priceList.Name;
            priceListToUpdate.Active = priceList.Active;

            _priceListRepository.Update(priceListToUpdate);
            await UnitOfWork.SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            var pricelisttodelete = await GetByIdAsync(id);

            if (pricelisttodelete == null)
            {
                Notify("$Price List {id} not found.");
                return;
            }

            _priceListRepository.Delete(pricelisttodelete);
            await UnitOfWork.SaveAsync();
        }
    }
}