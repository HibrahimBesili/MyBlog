using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace ProgrammersBlog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<Category>> Get(int categoryId);
        Task<IDataResult<IList<Category>>> GetAll();
        Task<IDataResult<IList<Category>>> GetAllByNonDelete();
        Task<IResult> Add(CategoryAddDto categoryAddDto);
        Task<IResult> Update(CategoryUpdateDto categoryUpdateDto);
        Task<IResult> Delete(int categoryıd);
        Task<IResult> HardDelete(int categoryId);

    }
}
