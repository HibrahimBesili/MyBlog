using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Shared.Utilities;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using AutoMapper;

namespace ProgrammersBlog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryAddDto);
                category.CreatedByName = createdByName;
                category.ModifiedByName = createdByName;
                await _unitOfWork.Categories.AddAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori başarılı bir şekilde eklenmiştir.");

            }
            catch (Exception ex)
            {

                return new Result(ResultStatus.Error, ex.Message);

            }

        }

        public async Task<IResult> Delete(int categoryıd)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryıd);
            if (category != null)
            {
                category.IsDeleted = true;
                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Success, "Kayıt başarılı bir şekilde silinmiştir.");
            }

            return new Result(ResultStatus.Error, "Kayıt bulunamamıştır.");
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (category != null)
            {
                return new DataResult<CategoryDto>(new CategoryDto
                {
                    Category = category

                }, ResultStatus.Success);
            }
            return new DataResult<CategoryDto>(null, ResultStatus.Error, "Böyle bir kategori bulunamdı");
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(new CategoryListDto
                {

                    Categories = categories

                }, ResultStatus.Success);
            }

            return new DataResult<CategoryListDto>(null, ResultStatus.Error, " ");
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDelete()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(new CategoryListDto
                {
                    Categories = categories

                }, ResultStatus.Success);
            }

            return new DataResult<CategoryListDto>(null, ResultStatus.Error, "");
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Success, $"{category.Name} adlı kategori başarılı bir şekilde silinmiştir");
            }

            return new Result(ResultStatus.Error, "Böyle bir kayıt bulunamadı");
        }

        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.CategoryId);
            if (category != null)
            {
                category = _mapper.Map<Category>(categoryUpdateDto);
                category.ModifiedByName = modifiedByName;

                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Success, $"{categoryUpdateDto.Name} başarıyla güncellendi");
            }

            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı");
        }


        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleteAndActive()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(x => x.IsActive && !x.IsDeleted ,c => c.Articles);
            if (categories.Count > 1)
            {
                return new DataResult<CategoryListDto>(new CategoryListDto
                {
                    Categories = categories
                }, ResultStatus.Success);

            }
            return new DataResult<CategoryListDto>(null, ResultStatus.Error, "Kayıt bulunamadı");
        }

    }
}

