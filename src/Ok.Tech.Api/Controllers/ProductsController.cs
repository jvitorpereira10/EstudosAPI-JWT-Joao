using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ok.Tech.Api.Models.Error;
using Ok.Tech.Api.Models.Product;
using Ok.Tech.Domain.Applications;
using Ok.Tech.Domain.Entities;
using Ok.Tech.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ok.Tech.Api.Controllers
{
    [Authorize]
    [Route("api/products")]
    public class ProductsController : MainController
    {
        private readonly IProductApplication _productApplication;
        private readonly IMapper _mapper;

        public ProductsController(IProductApplication productApplication, IMapper mapper, INotifier notifier) : base(notifier)
        {
            _productApplication = productApplication;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductModel>))]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetAllAsync()
        {
            var products = await _productApplication.GetAllAsync();

            var productModels = _mapper.Map<IEnumerable<ProductModel>>(products);

            return Ok(productModels);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<ProductModel>> GetByIdAsync(Guid id)
        {
            var productModel = _mapper.Map<ProductModel>(await _productApplication.GetByIdAsync(id));
            if (productModel == null)
            {
                return NoContent();
            }

            return Ok(productModel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        public async Task<ActionResult> CreateAsync(ProductModel productModel)
        {
            if (!IsModelValid())
            {
                BadRequestResponse();
            }

            var product = _mapper.Map<Product>(productModel);

            await _productApplication.Create(product);

            return CreatedResponse(nameof(GetByIdAsync), new { id = product.Id }, _mapper.Map<ProductModel>(product));
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        public async Task<ActionResult> UpdateAsync(Guid id, ProductModel productModel)
        {
            if (!IsModelValid())
            {
                BadRequestResponse();
            }

            var product = _mapper.Map<Product>(productModel);

            await _productApplication.Update(id, product);

            return OKResponse(productModel);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _productApplication.Delete(id);

            return OKResponse();
        }
    }
}
