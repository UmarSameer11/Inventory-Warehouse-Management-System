using AutoMapper;
using WarehouseManagementSystemApi.DTOs.Product;
using WarehouseManagementSystemApi.DTOs.UnitOfMeasure;
using WarehouseManagementSystemApi.Models.Products;
using WarehouseManagementSystemApi.Models.UnitOfMeasure;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateProductAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            await _unitOfWork.Product.AddAsync(product);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            _unitOfWork.Product.Delete(product);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ProductListDto?> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            return _mapper.Map<ProductListDto>(product);
        }

        public async Task<IEnumerable<ProductListDto>> GetProductListAsync()
        {
            var product = await _unitOfWork.Product.GetAllAsync();
            if (!product.Any())
            {
                throw new KeyNotFoundException("Product not found");
            }

            return _mapper.Map<IEnumerable<ProductListDto>>(product);
        }

        public async Task UpdateProductAsync(int id, ProductUpdateDto dto)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            _mapper.Map(dto, product);

            _unitOfWork.Product.Update(product);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
