using StuInfoManaSys.Models;
using StuInfoManaSys.Repositories;
using StuInfoManaSys.ViewModels;

namespace StuInfoManaSys.Services;

public class MajorService(MajorRepo majorRepo)
{
    public void Insert(MajorViewModel model)
    {
        Major major = new Major
        {
            Name = model.Name
        };
        majorRepo.Insert(major);
    }

    public List<MajorViewModel> GetList()
    {
        List<Major> majors = majorRepo.GetList();
        List<MajorViewModel> majorViewModels = new List<MajorViewModel>();
        foreach (Major major in majors)
        {
            majorViewModels.Add(new MajorViewModel()
            {
                Id = major.Id,
                Name = major.Name
            });
        }
        return majorViewModels;
    }

    public MajorViewModel Get(Guid id)
    {
        Major? major = majorRepo.Get(id);
        if (major == null) throw new NullReferenceException();

        return new MajorViewModel()
        {
            Id = major.Id,
            Name = major.Name
        };
    }

    public void Update(MajorViewModel model)
    {
        majorRepo.Update(new Major()
        {
            Name = model.Name,
            Id = model.Id
        });
    }

    /// <summary>
    /// 先从仓库得到专业对象，判断是否存在班级，存在则抛出异常
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NullReferenceException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void Delete(Guid id)
    {
        Major? major = majorRepo.GetWithClasses(id);
        if (major == null) throw new NullReferenceException("未找到该专业，无法删除");
        if (major.Classes.Any()) throw new InvalidOperationException("专业中存在班级，无法删除");
        majorRepo.Delete(major);
    }
}
