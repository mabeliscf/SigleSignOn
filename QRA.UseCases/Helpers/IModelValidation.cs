
using QRA.UseCases.DTOs;

namespace QRA.UseCases.Helpers
{
    public interface IModelValidation
    {
        bool isFieldsValid(RegisterDTO register);
    }
}