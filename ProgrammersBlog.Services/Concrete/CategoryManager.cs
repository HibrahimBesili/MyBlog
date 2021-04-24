﻿using ProgrammersBlog.Entities.Concrete;
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

        public  Task<IResult> Add(CategoryAddDto categoryAddDto)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Delete(int categoryıd)
        {
            throw new NotImplementedException();
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

            return new DataResult<IList<Category>>(null, ResultStatus.Error, "");
        }

        public Task<IResult> HardDelete(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
