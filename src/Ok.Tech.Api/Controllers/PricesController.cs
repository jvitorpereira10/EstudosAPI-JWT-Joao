using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ok.Tech.Api.Models.Error;
using Ok.Tech.Api.Models.Price;
using Ok.Tech.Application;
using Ok.Tech.Domain.Applications;
using Ok.Tech.Domain.Entities;
using Ok.Tech.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ok.Tech.Api.Controllers
{
    [Route("api/prices")]
    public class PricesController : MainController
    {
        private readonly IPriceApplication _priceApplication;
        private readonly IMapper _mapper;

        public PricesController(IPriceApplication priceApplication, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _priceApplication = priceApplication;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PriceViewModel>))]
        public async Task<ActionResult<IEnumerable<PriceViewModel>>> GetAllAsync()
        {
            var priceViewModels = _mapper.Map<IEnumerable<PriceViewModel>>(await _priceApplication.GetAllWithPriceListAndProductAsync());
            return Ok(priceViewModels);
        }

        [HttpGet("product/{productId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PriceViewModel>))]
        public async Task<ActionResult<IEnumerable<PriceViewModel>>> GetByProductIdWithPriceListAndProductAsync(Guid productId)
        {
            var prices = await _priceApplication.GetByProductIdWithPriceListAndProductAsync(productId);
            var priceViewModels = _mapper.Map<IEnumerable<PriceViewModel>>(prices);
            return Ok(priceViewModels);
        }

        [HttpGet("priceList/{priceListId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PriceViewModel>))]
        public async Task<ActionResult<IEnumerable<PriceViewModel>>> GetByPriceListIdWithPriceListAndProductAsync(Guid priceListId)
        {
            var prices = await _priceApplication.GetByPriceListIdWithPriceListAndProductAsync(priceListId);
            var priceViewModels = _mapper.Map<IEnumerable<PriceViewModel>>(prices);
            return Ok(priceViewModels);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<PriceViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        public async Task<ActionResult> CreateAsync(PriceModel priceModel)
        {
            if (!IsModelValid())
            {
                return BadRequestResponse();
            }

            var price = _mapper.Map<Price>(priceModel);

            await _priceApplication.Create(price);

            return Ok(_mapper.Map<PriceModel>(price));
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PriceViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        public async Task<ActionResult> UpdateAsync(Guid id, PriceModel priceModel)
        {
            if (!IsModelValid())
            {
                return BadRequestResponse();
            }

            await _priceApplication.Update(id, _mapper.Map<Price>(priceModel));

            return OKResponse();
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _priceApplication.Delete(id);

            return OKResponse();
        }

    }
}
