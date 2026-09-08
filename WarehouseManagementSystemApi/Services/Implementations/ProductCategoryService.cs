using AutoMapper;
using WarehouseManagementSystemApi.DTOs.ProductCategory;
using WarehouseManagementSystemApi.Models.ProductCategory;
using WarehouseManagementSystemApi.Repositories.Interfaces;
using WarehouseManagementSystemApi.Services.Interfaces;

namespace WarehouseManagementSystemApi.Services.Implementations
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateProductCategoryAsync(ProductCategoryCreateDto dto)
        {
            var ProductCategory = _mapper.Map<ProductCategories>(dto);

            await _unitOfWork.ProductCategories.AddAsync(ProductCategory);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProductCategoryAsync(int id)
        {
            var ProductCategory = await _unitOfWork.ProductCategories.GetByIdAsync(id);

            if (ProductCategory == null)
                throw new KeyNotFoundException("ProductCategory not found.");

            _unitOfWork.ProductCategories.Delete(ProductCategory);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ProductCategoryListDto?> GetProductCategoryByIdAsync(int id)
        {
            var ProductCategory = await _unitOfWork.ProductCategories.GetByIdAsync(id);

            if (ProductCategory == null)
                throw new KeyNotFoundException("ProductCategory not found.");

            return _mapper.Map<ProductCategoryListDto>(ProductCategory);
        }

        public async Task<IEnumerable<ProductCategoryListDto>> GetProductCategoryListAsync()
        {
            var ProductCategory = await _unitOfWork.ProductCategories.GetAllAsync();
            if (!ProductCategory.Any())
            {
                throw new KeyNotFoundException("ProductCategory not found");
            }

            return _mapper.Map<IEnumerable<ProductCategoryListDto>>(ProductCategory);
        }

        public async Task UpdateProductCategoryAsync(int id, ProductCategoryUpdateDto dto)
        {
            var ProductCategory = await _unitOfWork.ProductCategories.GetByIdAsync(id);

            if (ProductCategory == null)
                throw new KeyNotFoundException("ProductCategory not found.");

            _mapper.Map(dto, ProductCategory);

            _unitOfWork.ProductCategories.Update(ProductCategory);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
