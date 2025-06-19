using Cysharp.Threading.Tasks;

namespace Core.UseCases
{
    public interface IUseCase
    {
        UniTask Execute();
    }
}