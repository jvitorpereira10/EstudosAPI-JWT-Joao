using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ok.Tech.Api.Models.Error;
using Ok.Tech.Api.Models.Price;
using Ok.Tech.Api.Models.PriceList;
using Ok.Tech.Domain.Applications;
using Ok.Tech.Domain.Entities;
using Ok.Tech.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ok.Tech.Api.Controllers
{

    [Route("api/pricelist")]
    public class PriceListController : MainController
    {
        private readonly IPriceListApplication _priceListApplication;
        private readonly IMapper _mapper;

        public PriceListController(IPriceListApplication priceListApplication, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _priceListApplication = priceListApplication;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceListModel>>> GetAllAsync()
        {
            return Ok(_mapper.Map<IEnumerable<PriceListModel>>(await _priceListApplication.GetAllAsync()));
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<PriceListModel>> GetByAsync(Guid id)
        {
            var priceListModel = _mapper.Map<PriceListModel>(await _priceListApplication.GetByIdAsync(id));

            if (priceListModel == null)
            {
                NoContent();
            }

            return Ok(priceListModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<PriceViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        public async Task<ActionResult> CreateAsync(PriceListModel priceListModel)
        {
            if (!IsModelValid())
            {
                return BadRequestResponse();
            }

            var pricelist = _mapper.Map<PriceList>(priceListModel);

            await _priceListApplication.Create(pricelist);

            return CreatedResponse(nameof(GetByAsync), new { id = pricelist.Id }, _mapper.Map<PriceListModel>(pricelist));
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, PriceListModel priceListModel)
        {
            if (!IsModelValid())
            {
                return BadRequestResponse();
            }

            var pricelist = _mapper.Map<PriceList>(priceListModel);

            await _priceListApplication.Update(id, pricelist);

            return OKResponse(priceListModel);
        }
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _priceListApplication.Delete(id);
            return OKResponse();
        }
    }
}