using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleManager(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(ArticleAddDto articleAddDto, string createdByName)
        {
            var article = _mapper.Map<Article>(articleAddDto);
            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            article.UserId = 1;

            await _unitOfWork.Articles.AddAsync(article).ContinueWith(t => _unitOfWork.SaveAsync());

            return new Result(ResultStatus.Success, $"{articleAddDto.Title} başlıklı makale başarıyla eklenmiştir");
        }

        public async Task<IResult> Delete(int articleId,string modifiedByName)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);

            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                if (article != null)
                {
                    article.IsDeleted = true;
                    article.ModifiedByName = modifiedByName;
                    article.ModifiedDate = DateTime.Now;
                    await _unitOfWork.Articles.UpdateAsync(article).ContinueWith(t => _unitOfWork.SaveAsync());

                    return new Result(ResultStatus.Success, "Kayıt başarılı bir şekilde silinmiştir");

                }

                return new Result(ResultStatus.Error, "Kayıt silinemedi"); 
            }

            return new Result(ResultStatus.Error, "Makale bulunamadı");
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleId)
        {
            var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId,a=> a.User,a => a.Category);
            if (article != null)
            {
                return new DataResult<ArticleDto>(new ArticleDto {
                    Articles = article,
                    ResultStatus = ResultStatus.Success

                },ResultStatus.Success);
            }
            return new DataResult<ArticleDto>(null,ResultStatus.Error,"Makale bulunamadı");
        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync();

            if (articles.Count > 0)
            {
                return new DataResult<ArticleListDto>(new ArticleListDto {
                Articles = articles,
                ResultStatus = ResultStatus.Success
                }, ResultStatus.Success);
            }

            return new DataResult<ArticleListDto>(null, ResultStatus.Error, "Hiç bir makale bulunamadı");
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(a => a.CategoryId == categoryId && !a.IsDeleted && a.IsActive, a => a.User, a => a.Category);
                if (articles.Count > 0)
                {
                    return new DataResult<ArticleListDto>(new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Success
                    }, ResultStatus.Success);
                }
                return new DataResult<ArticleListDto>(null, ResultStatus.Error, "Makaleler bulunamadı");
            }

            return new DataResult<ArticleListDto>(null, ResultStatus.Error, "Kategori bulunamadı");

          
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDelete()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted, a => a.User, a => a.Category);

            if (articles.Count > 0)
            {
                return new DataResult<ArticleListDto>(new ArticleListDto {
                Articles = articles,
                ResultStatus = ResultStatus.Success
                }, ResultStatus.Success);
            }

            return new DataResult<ArticleListDto>(null, ResultStatus.Error, "Makaleler bulunamadı");
        }

        public async Task<IResult> HardDelete(int articleId)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {

                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                if (article != null)
                {
                    await _unitOfWork.Articles.DeleteAsync(article).ContinueWith(t => _unitOfWork.SaveAsync());
                    return new Result(ResultStatus.Success, "Kayıt başarıluı bir şekilde silinmiştir");

                }

                return new Result(ResultStatus.Error, "Kayıt silinemedi"); 
            }

            return new Result(ResultStatus.Error, "Kayıt bulunamadı");
        }

        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var article = _mapper.Map<Article>(articleUpdateDto);
            article.ModifiedByName = modifiedByName;

            await _unitOfWork.Articles.UpdateAsync(article).ContinueWith(t => _unitOfWork.SaveAsync());

            return new Result(ResultStatus.Success, $"{articleUpdateDto.Title} başlıklı makale güncellenmiştir.");
        }
    }
}
