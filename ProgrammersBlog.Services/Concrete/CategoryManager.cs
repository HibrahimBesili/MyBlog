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

namespace ProgrammersBlog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public  async Task<IResult> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            await _unitOfWork.Categories.AddAsync(new Category {
                Name = categoryAddDto.Name,
                Description = categoryAddDto.Description,
                Note = categoryAddDto.Note,
                IsActive = categoryAddDto.ısActive,
                CreatedByName = createdByName,
                CreatedDate = DateTime.Now,
                ModifiedByName = createdByName,
                ModifiedDate = DateTime.Now,
                IsDeleted = false
            }).ContinueWith(t =>_unitOfWork.SaveAsync());

            //await _unitOfWork.SaveAsync();

            return new Result(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori başarılı bir şekilde eklenmiştir.");


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

        public async Task<IDataResult<Category>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (category != null)
            {
                return new DataResult<Category>(category,ResultStatus.Success);
            }
            return new DataResult<Category>(null,ResultStatus.Error, "Böyle bir kategori bulunamdı");
        }

        public async Task<IDataResult<IList<Category>>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null,c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<IList<Category>>(categories,ResultStatus.Success);
            }

            return new DataResult<IList<Category>>(null,ResultStatus.Error," ");
        }

        public async Task<IDataResult<IList<Category>>> GetAllByNonDelete()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted,c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<IList<Category>>(categories, ResultStatus.Success);
            }

            return new DataResult<IList<Category>>(null, ResultStatus.Error,"");
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

        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto,string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.CategoryId);

            if (category != null)
            {
                category.Name = categoryUpdateDto.Name;
                category.Description = categoryUpdateDto.Description;
                category.Note = categoryUpdateDto.Note;
                category.IsActive = categoryUpdateDto.ısActive;
                category.ModifiedByName = modifiedByName;
                category.IsDeleted = categoryUpdateDto.ısDeleted;
                category.ModifiedDate = DateTime.Now;

                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Success, $"{categoryUpdateDto.Name} başarıyla güncellendi");
            }

            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı");
        }
    }
}
